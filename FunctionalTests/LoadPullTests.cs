using System;
using COPOL.BLL.Models;
using FunctionalTests.Common;
using NUnit.Framework;

namespace FunctionalTests
{
    [TestFixture]
    public class LoadPullTests
    {
        [TestCase(TestName = "Тест на правильный расчет точек. Позитивный тест.")]
        public void LoadPull_CalculatePoint_CorrectResult()
        {
            // SetUp
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
                Step = parameters.Step,
            };
            
            // Act
            var builder = new LoadPull(newParameters, 50);
            var outputPoints = builder.CalculatePoint();
            
            // Assert
            Assert.NotNull(outputPoints);
            Assert.NotNull(builder.PMaxOutput);
        }
        
        [TestCase(TestName = "Тест присваиваения неправильных" +
                             " данных для расчета точек. Негативный тест.")]
        public void LoadPull_SetWrongParameters_CorrectResult()
        {
            // SetUp
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
                Step = 200,
            };

            Assert.Throws<ArgumentException>(() =>
            {
                // Act
                var builder = new LoadPull(newParameters, 50);
                builder.CalculatePoint();
            });
        }
    }
}