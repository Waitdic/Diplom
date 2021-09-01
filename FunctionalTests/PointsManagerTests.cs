using System;
using System.Collections;
using COPOL.BLL.Managers;
using COPOL.BLL.Models;
using FunctionalTests.Common;
using NUnit.Framework;

namespace FunctionalTests
{
    [TestFixture]
    public class PointsManagerTests
    {
        private Hashtable _outputPoints;
        
        [TestCase(TestName = "Тест на сохранение точек контуров. Позитивный тест.")]
        public void PointsManager_SavePoints_CorrectResult()
        {
            // SetUp
            SavePoint();

            // Act
            PointsManager.SavePoints(_outputPoints, @"..\..\parameters.rgn");
        }
        
        [TestCase(TestName = "Тест на получение точек контуров. Позитивный тест.")]
        public void PointsManager_LoadPoints_CorrectResult()
        {
            // SetUp
            SavePoint();
            // Act
            PointsManager.SavePoints(_outputPoints, @"..\..\parameters.rgn");
            
            // Assert
            Assert.NotNull(PointsManager.LoadPoints(@"..\..\parameters.rgn"));
        }

        private void SavePoint()
        {
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
            
            var builder = new LoadPull(newParameters, 50);
            _outputPoints = builder.CalculatePoint();
        }
    }
}