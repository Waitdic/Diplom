using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using COPOL.BLL.Models;

namespace COPOL.BLL.Managers
{
    public class LoadPointsManager
    {
        public Hashtable LoadPoints(string fileName)
        {
            var TableOfPointsOfContours = new Hashtable();

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
                                TableOfPointsOfContours.Add(
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
            return TableOfPointsOfContours;
        }
    }
}