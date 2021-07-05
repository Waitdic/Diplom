using COPOL.BLL.Models;
using NUnit.Framework;
using System.Drawing;
using System.Windows.Forms;

namespace FunctionalTests
{
    [TestFixture]
    public class SmithChartTests
    {
        private readonly PictureBox _graphics = new();
        private SmithChart _chartManager = new ();
        
        // Центры горизонтальных окружностей. 
        private static readonly float[] _arg1 = {
            0.0f, 0.2f, 0.4f, 0.6f,
            0.8f, 1.0f, 1.4f, 1.8f, 
            2.2f, 3.0f, 4.0f, 5.0f, 10.0f
        };
        
        // Центры Вертикальных окружностей. 
        private static readonly float[] _arg2 = { 
            0.2f, 0.4f, 0.6f, 0.8f, 1.0f,
            1.2f, 1.4f, 1.6f, 1.9f, 2.3f,
            3.0f, 4.0f, 5.0f, 10.0f
        };
        
        private void Initial()   
        {
            _graphics.BackColor = Color.White;
            _graphics.Location = new Point(12, 7);
            _graphics.Name = "SmithChart";
            _graphics.Size = new Size(540, 540);
            _graphics.TabIndex = 11;
            _graphics.TabStop = false;
        }
        
        [TestCase(TestName = "Тест для проверки рисования диаграммы Смита.")]
        public void SmithChart_DrawGraphics_CorrectResult()
        {
            // SetUp
            Initial();

            // Act
            _chartManager.DrawGraphics(_graphics.CreateGraphics(), _arg1, _arg2);
        }
        
        [TestCase(20, 20,
            TestName = "Тест для проверки рисования точек с сопротивлеями {0} и {1}" +
                       " на диаграмме Смита.")]
        [TestCase(0, 0,
            TestName = "Тест для проверки рисования точек с сопротивлеями {0} и {1}" +
                       " на диаграмме Смита.")]
        [TestCase(-70, 20,
            TestName = "Тест для проверки рисования точек с сопротивлеями {0} и {1}" +
                       " на диаграмме Смита.")]
        public void SmithChart_DrawUserCircles_CorrectResult(int r, int x)
        {
            // SetUp
            Initial();

            // Act
            _chartManager.DrawUserCircles(_graphics.CreateGraphics(), 50, r, x);
        }
    }
}