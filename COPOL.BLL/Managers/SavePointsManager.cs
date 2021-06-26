using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace COPOL.BLL.Models
{
    public static class SavePointsManager
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
                    if (Convert.ToDouble(frequences[j]) >= Convert.ToDouble(frequences[j + 1]))
                    {
                        var temp = frequences[j];
                        frequences[j] = frequences[j+1];
                        frequences[j+1] = temp;
                    }
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
                    if(key.Contains(t))
                    {
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
            }
            fs.Close();
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
            
            var output = freqNew.ToString(CultureInfo.InvariantCulture);
            return output;
        }
    }
}