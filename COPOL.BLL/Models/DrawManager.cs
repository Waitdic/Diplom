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
                    var U_coord = x;
                    var V_coord = y;

                    // Смещение текста относительно точки контура.
                    var freq_x = x + 3;
                    var freq_y = y - 3;

                    smithСhart.ReverseConvertCoords(graphics, ref U_coord, ref V_coord);
                    smithСhart.ReverseConvertCoords(graphics, ref freq_x, ref freq_y);
                    if (CheckInsideCircle(U_coord, V_coord) == true)
                    {
                        // Рисуем точку контура.
                        graphics.DrawEllipse(penRed, x, y, 2, 2);
                    }
                }
            }

            // Рисуем точки оптимальной нагрузки.
            foreach (DictionaryEntry i in loadPull.ZOpt)
            {
                var Z_opt_gamma = (((Complex)(i.Value)) - z00) / (((Complex)(i.Value)) + z00);

                var Z_opt_x = (float)Z_opt_gamma.real;
                var Z_opt_y = (float)Z_opt_gamma.imaginary;
                
                //переводим в экранные координаты
                smithСhart.ConvertCoord(CoordinateType.X, graphics, ref Z_opt_x);
                smithСhart.ConvertCoord(CoordinateType.Y, graphics, ref Z_opt_y);

                var textCoord_x = Z_opt_x + 8;//для вывода частоты около точки оптимальной нагрузки
                var textCoord_y = Z_opt_y - 8;


                var U_coord = Z_opt_x;
                var V_coord = Z_opt_y;
                smithСhart.ReverseConvertCoords(graphics, ref U_coord, ref V_coord);
                smithСhart.ReverseConvertCoords(graphics, ref textCoord_x, ref textCoord_y);

                if (CheckInsideCircle(U_coord, V_coord) == true)
                {
                    //рисуем точку оптимальной нагрузки
                    graphics.DrawEllipse(penBlue, Z_opt_x, Z_opt_y, 3, 3);
                    var text = (string)i.Key;//частота
                    text = text.Remove(0, 4);//чтобы осталось только значение частоты
                    
                    //выводим значение частоты рядом с точкой оптимальной нагрузки
                    smithСhart.DrawText(graphics, Color.Green,new Font("Arial",8,FontStyle.Bold), text, textCoord_x, textCoord_y);
                }
                
            }
        }

        private static bool CheckInsideCircle(float u, float v)
        {
            return ((v * v) + (u * u) - 1) <= 0;
        }
    }
}