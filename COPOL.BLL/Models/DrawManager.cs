using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using COPOL.BLL.Enums;

namespace COPOL.BLL.Models
{
    public static class DrawManager
    {
        public static void DrawContours(
            Hashtable output,
            Graphics graphics,
            SmithChart smithСhart,
            LoadPull loadPull,
            float z0)
        {
            if (output == null)
            {
                throw new ArgumentException("Не удается произвести построение контуров");
            }

            var penRed = new Pen(Color.Red, 2);
            var penBlue = new Pen(Color.Blue, 3);

            var z00 = new Complex(z0, 0);
            
            // Рисуем точки контуров на диаграмме Смита
            // для каждого массива точек в таблице.
            foreach (DictionaryEntry i in output)
            {
                foreach (var point in (List<Complex>)i.Value)
                {
                    var x = (float)point.real;
                    var y = (float)point.imaginary;
                    
                    // Переводим в экранные координаты.
                    smithСhart.ConvertCoord(CoordinateType.X, graphics, ref x);
                    smithСhart.ConvertCoord(CoordinateType.Y, graphics, ref y);
                    var uCoord = x;
                    var vCoord = y;

                    // Смещение текста относительно точки контура.
                    var freqX = x + 3;
                    var freqY = y - 3;

                    smithСhart.ReverseConvertCoords(graphics, ref uCoord, ref vCoord);
                    smithСhart.ReverseConvertCoords(graphics, ref freqX, ref freqY);
                    
                    if (CheckInsideCircle(uCoord, vCoord))
                    {
                        // Рисуем точку контура.
                        graphics.DrawEllipse(penRed, x, y, 2, 2);
                    }
                }
            }

            // Рисуем точки оптимальной нагрузки.
            foreach (DictionaryEntry i in loadPull.ZOpt)
            {
                var zOptGamma = (((Complex)(i.Value)) - z00) / (((Complex)(i.Value)) + z00);

                var zOptX = (float)zOptGamma.real;
                var zOptY = (float)zOptGamma.imaginary;
                
                //переводим в экранные координаты
                smithСhart.ConvertCoord(CoordinateType.X, graphics, ref zOptX);
                smithСhart.ConvertCoord(CoordinateType.Y, graphics, ref zOptY);

                var textCoordX = zOptX + 8;//для вывода частоты около точки оптимальной нагрузки
                var textCoordY = zOptY - 8;


                var uCoord = zOptX;
                var vCoord = zOptY;
                smithСhart.ReverseConvertCoords(graphics, ref uCoord, ref vCoord);
                smithСhart.ReverseConvertCoords(graphics, ref textCoordX, ref textCoordY);

                if (CheckInsideCircle(uCoord, vCoord))
                {
                    //рисуем точку оптимальной нагрузки
                    graphics.DrawEllipse(penBlue, zOptX, zOptY, 3, 3);
                    var text = (string)i.Key;//частота
                    text = text.Remove(0, 4);//чтобы осталось только значение частоты
                    
                    //выводим значение частоты рядом с точкой оптимальной нагрузки
                    smithСhart.DrawText(
                        graphics,
                        Color.Green
                        ,new Font(
                            "Arial",
                            8,
                            FontStyle.Bold),
                        text,
                        textCoordX,
                        textCoordY);
                }
            }
        }

        private static bool CheckInsideCircle(float u, float v)
        {
            return ((v * v) + (u * u) - 1) <= 0;
        }
    }
}