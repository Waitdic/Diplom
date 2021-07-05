using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using COPOL.BLL.Models;

namespace COPOL.BLL.Managers
{
    public static class PointsManager
    {
        /// <summary>
        /// сохраняет точки в файл
        /// </summary>
        /// <param name="obj">хэш-таблица с точками контуров</param>
        /// <param name="fileName">имя файла</param>
        public static void SavePoints(Hashtable obj,string fileName)
        {
            var fs = File.Create(fileName);
            var frequences = new List<string>();//массив частот
            foreach (DictionaryEntry de in obj)
            {
                var str = (string)de.Key;
                var split = str.Split(' ', 'F', '=', 'P');
                
                foreach (var s in split)
                {
                    if (string.IsNullOrWhiteSpace(s)) continue;
                    
                    if(!frequences.Contains(s))
                    {
                        frequences.Add(s);
                    }
                    break;
                }
            }
            
            AddText(fs, "!Number of regions = " + frequences.Count);

            // Cортируем массив частот по возрастанию.
            for (var i = frequences.Count - 1 ; i > 0; i--)
            {
                for(var j = 0; j < i;j++)
                {
                    if (!(Convert.ToDouble(frequences[j]) >= Convert.ToDouble(frequences[j + 1]))) 
                        continue;
                    
                    var temp = frequences[j];
                    frequences[j] = frequences[j+1];
                    frequences[j+1] = temp;
                }
            }

            foreach (var t in frequences)
            {
                var freq = CorrectFrequenceForWriting(t);
                freq = freq.Replace(',','.');
                
                AddText(fs, "\r\n Region \r\n");
                AddText(fs, "        Id = " + freq + "\r\n" + "\r\n");
              
                foreach (DictionaryEntry de in obj)//просматриваем всю таблицу
                {
                    var key = (string) de.Key;
                    if (!key.Contains(t)) continue;
                    
                    //пишем следующий текст
                    AddText(fs, "       Segment\r\n" + "               Points = ");
                    var points = (List<Complex>)de.Value;
                    for (var j = 0; j < points.Count; j++)
                    {
                        //здесь надо правильно записывать в файл, чтобы без Е
                        var x = Math
                            .Round(points[j].real, 6)
                            .ToString("F6");
                            
                        x = x.Replace(',', '.');
                        var y = Math
                            .Round(points[j].imaginary, 6)
                            .ToString("F6");
                            
                        y = y.Replace(',', '.');
                        AddText(fs, x + "," + y + ";");
                    }
                    AddText(fs, "\r\n\r\n");
                }
            }
            fs.Close();
        }
        
        public static Hashtable LoadPoints(string fileName)
        {
            var tableOfPointsOfContours = new Hashtable();

            var fs = File.Open(fileName, FileMode.Open, FileAccess.Read);
            int byteFromFile;
            string stringCountOfFrequences = null;
            long positionOfFile = -1;
            for (var i = 0; i <= fs.Length;i++ )
            {
                byteFromFile = fs.ReadByte();
                positionOfFile = i;//для запоминания позиции файла,на которой остановились после прочтения первой строки
                var temp = Convert.ToChar(byteFromFile);
                if (temp != '\r')//пока не конец первой строки
                {
                    stringCountOfFrequences += temp.ToString();
                }
                else
                {
                    break;
                }
            }

            if (stringCountOfFrequences != null)
            {
                var countOfFrequences = Convert.ToInt32(stringCountOfFrequences.Remove(0, 21).Trim());
                for (var i = 0; i < countOfFrequences; i++) //для каждой частоты из файла
                {
                    fs.Position = positionOfFile + 23; //доходим до значения ID
                    string frequence = null; //частота контура
                    for (var j = 0; j < 11; j++) //получаем частоту
                    {
                        byteFromFile = fs.ReadByte();

                        if (Convert.ToChar(byteFromFile) == '\r')
                        {
                            break;
                        }

                        frequence += Convert.ToChar(byteFromFile).ToString();
                    }

                    fs.Position += 8;
                    var gggg = 0;
                    
                    while (true) //получаем точки одного контура
                    {
                        byteFromFile = fs.ReadByte();
                        var pointsOfContour = new List<Complex>(); //сюда записываем точки каждого контура поочередно

                        if (byteFromFile == -1) //проверка на конец файла
                        {
                            break;
                        }

                        if (Convert.ToChar(byteFromFile) == 'R') //при встрече первого следующего слова Region
                        {
                            positionOfFile = fs.Position - 3;
                            break;
                        }

                        if (Convert.ToChar(byteFromFile) != 'S') continue;
                        fs.Position += 32;

                        string number = null;
                        while (true)
                        {
                            byteFromFile = fs.ReadByte();

                            if (Convert.ToChar(byteFromFile) == '\r') //если закончились точки данного массива Points
                            {
                                tableOfPointsOfContours.Add(
                                    "F = " + frequence + " P = undefined" + gggg,
                                    pointsOfContour);
                                
                                gggg++;
                                break;
                            }

                            if (Convert.ToChar(byteFromFile) == ';') //тогда далее следует следующее число
                            {
                                var positionOfComma = number.IndexOf(','); //ищем позицию запятой
                                var onePoint = new Complex
                                {
                                    real = Convert.ToDouble(number.Remove(positionOfComma)
                                        .Replace('.', ',')),
                                    
                                    imaginary = Convert.ToDouble(number.Remove(0, positionOfComma + 1)
                                        .Replace('.', ','))
                                };
                                
                                pointsOfContour.Add(onePoint);
                                number = null;
                            }
                            else
                            {
                                number += Convert.ToChar(byteFromFile).ToString();
                            }
                        }
                    }
                }
            }

            fs.Close();
            return tableOfPointsOfContours;
        }

        /// <summary>
        /// пишет байты в поток
        /// </summary>
        /// <param name="fs"></param>
        /// <param name="value"></param>
        private static void AddText(FileStream fs, string value)
        {
            var info = new UTF8Encoding(true).GetBytes(value);
            fs.Write(info, 0, info.Length);
        }

        /// <summary>
        /// правильная форма записи частоты в файл
        /// </summary>
        /// <param name="freq">входная строка частоты в формате Е+08</param>
        /// <returns>строка в формате без Е</returns>
        private static string CorrectFrequenceForWriting(string freq)
        {
            var freqNew = Convert.ToDouble(freq);
            freqNew /= Math.Pow(10, 9);
            
            return freqNew.ToString(CultureInfo.InvariantCulture);
        }
    }
}