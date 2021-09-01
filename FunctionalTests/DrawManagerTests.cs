using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using COPOL.BLL.Managers;
using COPOL.BLL.Models;
using FunctionalTests.Common;
using NUnit.Framework;

namespace FunctionalTests
{
    [TestFixture]
    public class DrawManagerTests
    { 
        private readonly PictureBox _graphics = new();
        private SmithChart _chartManager = new ();
        private LoadPull _builder;
        private Hashtable _outputs;

        private void Initial()   
        {
            _graphics.BackColor = Color.White;
            _graphics.Location = new Point(12, 7);
            _graphics.Name = "SmithChart";
            _graphics.Size = new Size(540, 540);
            _graphics.TabIndex = 11;
            _graphics.TabStop = false;
            
            var parameters = ParametersHelper.GetParameters();
            var newParameters = new Parameters
            {
                Vds0 = parameters.Vds0,
                Ids0 = (float) (parameters.Ids0 * Math.Pow(10, -3)),
                Cgs = (float) (parameters.Cgs * Math.Pow(10, -12)),
                Cds = (float) (parameters.Cds * Math.Pow(10, -12)),
                Cgd = (float) (parameters.Cgd * Math.Pow(10, -12)),
                Ls = (float) (parameters.Ls * Math.Pow(10, -9)),
                Ld = (float) (parameters.Ld * Math.Pow(10, -9)),
                Gm = (float) (parameters.Gm * Math.Pow(10, -3)),
                Frequences = parameters.Frequences,
                LoopP = parameters.LoopP,
                N = parameters.N,
                Step = 4,
            };
            
            _builder = new LoadPull(newParameters, 50);
            _outputs = _builder.CalculatePoint();
        }
        
        [TestCase(TestName = "Тест для проверки рисования контуров. Позитивный тест.")]
        public void DrawManager_DrawContours_CorrectResult()
        {
            // SetUp
            Initial();

            // Act
            DrawManager.DrawContours(
                _outputs,
                _graphics.CreateGraphics(),
                _chartManager,
                _builder,
                50);
        }
        
        [TestCase(TestName = "Тест для проверки рисования контуров второго вида." +
                             " Позитивный тест")]
        public void DrawContours2_SetWrongParameters_ThrowException()
        {
            // SetUp
            Initial();

            // Act
            DrawManager.DrawContours2(
                _outputs,
                _graphics.CreateGraphics(),
                _chartManager);
        }
        
        [TestCase(TestName = "Тест для проверки рисования контуров без точек." +
                             " Негативный тест")]
        public void DrawContours_SetWrongParameters_ThrowException()
        {
            // SetUp
            Initial();

            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                // Act
                DrawManager.DrawContours(
                    null,
                    _graphics.CreateGraphics(),
                    _chartManager,
                    _builder,
                    50);
            });
        }
    }
}