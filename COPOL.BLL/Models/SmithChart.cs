using System;
using System.Drawing;
using System.Windows.Forms;
using COPOL.BLL.Enums;

namespace COPOL.BLL
{
    /// <summary>
    /// Класс для рисование диаграммы Смитта.
    /// </summary>
    public class SmithChart
    {
        /// <summary>Максимальное значение y на диаграмме.</summary>
        /// <remarks>Граничная точка справа по оси x (для смены масштаба).</remarks>
        private float _yMax = 1;        
        
        /// <summary>Максимальное значения x на диаграмме.</summary>
        /// <remarks>Граничная точка сверху по оси y (для смены масштаба).</remarks>
        private float _xMax = 1;   
        
        /// <summary>Минимального значения x на диаграмме.</summary>
        private float _xMin = -1;
        
        /// <summary>Минимального значения y на диаграмме.</summary>
        private float _yMin = -1;
        
        /// <summary>Rол-во осей в сетке координат (по умолчанию по одной оси для x и y).</summary>
        private int _mnAxesCount = 1;
        
        public void DrawGraphics(PaintEventArgs e, float[] arg1, float[] arg2)
        {
            float xCross; 
            float yCross;
            var userPen = new Pen(Color.DarkGray);

            // Рисуем основную окружность.
            for (var i = 0; i < arg1.Length; i++)
            {
                DrawCircleType1(
                    e.Graphics,
                    i == 0 ? new Pen(Color.Black, 3) : userPen,
                    arg1[i]);

                xCross = (arg1[i] - 1) / (arg1[i] + 1);
                yCross = 0;

                DrawText(
                    e.Graphics,
                    Color.Blue,
                    new Font(
                        "Arial",
                        7,
                        FontStyle.Bold),
                    Math.Round(arg1[i], 2).ToString(),
                    xCross,
                    yCross);
            }

            // Рисуем сетку диграммы Смита.
            foreach (var arg in arg2)
            {
                DrawCircleType2(e.Graphics, userPen, arg);
                DrawCircleType2(e.Graphics, userPen, -arg);
                
                xCross = ((arg * arg) - 1) / ((arg * arg) + 1);
                yCross = (2 * arg) / ((arg * arg) + 1);

                DrawText(
                    e.Graphics, Color.Blue,
                    new Font("Arial", 7, FontStyle.Bold),
                    Math.Round(arg, 2).ToString(),
                    xCross,
                    yCross);
                
                DrawText(
                    e.Graphics,
                    Color.Blue,
                    new Font("Arial", 7, FontStyle.Bold),
                    Math.Round(-arg, 2).ToString(),
                    xCross,
                    -yCross);
            }
        }
        
        /// <summary>
        /// Метод для рисования окружностей первого типа.
        /// </summary>
        /// <param name="objGraphics">График.</param>
        /// <param name="userPen">Средство рисования.</param>
        /// <param name="argParam">Центры окружностей.</param>
        private void DrawCircleType1(Graphics objGraphics, Pen userPen, float argParam)
        {
            var nCenterX = argParam / (1 + argParam);   // u - из книги
            var nCenterY = 0f;                               // v - из книги
            var nRadiusX = 1 / (1 + argParam);          // - из книги
            var nRadiusY = 1 / (1 + argParam);          // - из книги

            ConvertCoord(CoordinateType.X, objGraphics, ref nCenterX);
            ConvertCoord(CoordinateType.Y, objGraphics, ref nCenterY);
            ConvertLength(CoordinateType.X, objGraphics, ref nRadiusX);
            ConvertLength(CoordinateType.Y, objGraphics, ref nRadiusY);

            objGraphics.DrawEllipse(
                userPen, 
                nCenterX - nRadiusX,
                nCenterY - nRadiusY,
                nRadiusX * 2,
                nRadiusY * 2);
        }

        /// <summary>
        /// Метод для рисования окружностей второго типа.
        /// </summary>
        /// <param name="objGraphics">График.</param>
        /// <param name="userPen">Средство рисования.</param>
        /// <param name="argParam">Центры окружностей.</param>
        private void DrawCircleType2(Graphics objGraphics, Pen userPen, float argParam)
        {
            var drawRect = new Rectangle();
            
            var nCenterX = 1f;                          // u - из книги
            var nCenterY = 1 / argParam;            // v - из книги
            var nRadiusX = Math.Abs(1 / argParam);  // - из книги
            var nRadiusY = Math.Abs(1 / argParam);  // - из книги

            // Координаты точки пересечения каждой дуги с 1-чной окружностью
            // (относительно осей координат, проведенных через центр окружности 2-го типа)
            var nX = ((argParam * argParam) - 1) / ((argParam * argParam) + 1);
            var nY = (2 * argParam) / ((argParam * argParam) + 1);

            //перевод в координаты U,V
            var nClientX = 1 - nX;
            var nClientY = nX >= 0 
                ? Math.Abs(nY) - nRadiusY 
                : nRadiusY - Math.Abs(nY);
            
            ConvertLength(CoordinateType.X, objGraphics, ref nClientX);
            ConvertLength(CoordinateType.Y, objGraphics, ref nClientY);

            var sweepAngle = Math.Atan(nClientY / nClientX);
            sweepAngle *= (180 / Math.PI);

            double startAngle;
            if (nX >= 0)
            {
                startAngle = nY >= 0 ? 90 : 90 + (90 - sweepAngle);
                sweepAngle += 90;
            }
            else
            {
                startAngle = nY >= 0 ? 90 : 180 + sweepAngle;
                sweepAngle = 90 - sweepAngle;
            }

            ConvertCoord(CoordinateType.X, objGraphics, ref nCenterX);
            ConvertCoord(CoordinateType.Y, objGraphics, ref nCenterY);
            ConvertLength(CoordinateType.X, objGraphics, ref nRadiusX);
            ConvertLength(CoordinateType.Y, objGraphics, ref nRadiusY);

            drawRect.X = (int)(nCenterX - nRadiusX);
            drawRect.Y = (int)(nCenterY - nRadiusY);
            drawRect.Width = (int)(nRadiusX * 2);
            drawRect.Height = (int)(nRadiusY * 2);
            objGraphics.DrawArc(userPen, drawRect, (float)startAngle, (float)sweepAngle);
        }
        
        /// <summary>
        /// Метод для перевода координаты, посчитанные по формулам, в координаты не компоненте для рисования.
        /// </summary>
        /// <param name="coordType"></param>
        /// <param name="objGraphics"></param>
        /// <param name="nCoord"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void ConvertCoord(CoordinateType coordType, Graphics objGraphics, ref float nCoord)
        {
            var drawRect = objGraphics.VisibleClipBounds;
            switch(coordType)
            {
                case CoordinateType.X:
                    nCoord = (drawRect.Width - 15) * ((nCoord - _xMin) / (_xMax - _xMin));
                    break;
                case CoordinateType.Y:
                    nCoord = (drawRect.Height - 15) * ((_yMax - nCoord) / (_yMax - _yMin));
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Ошибка. " + nameof(coordType));
            }
        }
        
        /// <summary>
        /// Метод для перевода длины в длины на компоненте рисования.
        /// </summary>
        /// <param name="coordType"></param>
        /// <param name="objGraphics"></param>
        /// <param name="nLength"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private void ConvertLength(CoordinateType coordType, Graphics objGraphics, ref float nLength)
        {
            var drawRect = objGraphics.VisibleClipBounds;
            switch(coordType)
            {
                case CoordinateType.X:
                    nLength = (drawRect.Width - 15) * (nLength / (_xMax - _xMin));
                    break;
                case CoordinateType.Y:
                    nLength = (drawRect.Height - 15) * (nLength / (_yMax - _yMin));
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Ошибка. " + nameof(coordType));
            }
        }

        /// <summary>
        /// Метод отрисовки текста.
        /// </summary>
        /// <param name="objGraphics"></param>
        /// <param name="color"></param>
        /// <param name="textFont"></param>
        /// <param name="text"></param>
        /// <param name="x">X координата текста.</param>
        /// <param name="y">Y координата текста.</param>
        public void DrawText(
            Graphics objGraphics,
            Color color,
            Font textFont,
            string text,
            float x,
            float y)
        {
            var myBrush = new SolidBrush(color);
            ConvertCoord(CoordinateType.X, objGraphics, ref x);
            ConvertCoord(CoordinateType.Y, objGraphics, ref y);
            objGraphics.DrawString(text, textFont, myBrush, x, y);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objGraphics"></param>
        /// <param name="waveR"></param>
        /// <param name="activeR"></param>
        /// <param name="reactiveR"></param>
        public void DrawUserCircles(Graphics objGraphics, int waveR, int activeR, int reactiveR)
        {
            var r = activeR / (float)waveR;
            var x = reactiveR / (float)waveR;

            var userPen = new Pen(Color.MediumPurple) { Width = 3 };
            
            float v;
            double beta;

            var angle = Math.Atan((1 + r) / x);
            if (angle > 0)
            {
                beta = Math.PI - 2 * angle;
                v = (float)(Math.Sin(beta) / (1 + r));
            }
            else 
            {
                beta = Math.PI - 2 * Math.Abs(angle);
                v = -(float)(Math.Sin(beta) / (1 + r));
            }
            
            var u = (float)(Math.Cos(beta) / (1 + r));
            u = r / (1 + r) - u;

            ConvertCoord(CoordinateType.X, objGraphics, ref u);
            ConvertCoord(CoordinateType.Y, objGraphics, ref v);
 
            // отнимаем 1 чтобы центр эллипса совпадал с серединой искомой точки
            objGraphics.DrawEllipse(userPen, u, v, 6, 6);
        }
        
        // переводим координаты пикселей в значения действит. и мнимой частей к-та отражения
        public void ReverseConvertCoords(Graphics objGraphics, ref float nX, ref float nY)
        {
            var drawRect = objGraphics.VisibleClipBounds;
            nX = (nX / drawRect.Width) * (_xMax - _xMin) + _xMin;
            nY = -(nY / drawRect.Height) * (_yMax - _yMin) + _yMax;
        }
    }
}
