using System;
using System.Windows.Forms;
using COPOL.BLL.Repositories;
using FunctionalTests.Common;
using NUnit.Framework;

namespace FunctionalTests
{
    [TestFixture]
    public class ParametersRepositoryTests
    {
        [TestCase(TestName = "Тест для проверки сохранение параметров. Позитивный тест")]
        public void SaveParameters_SetParameters_CorrectResult()
        {
            // SetUp
            var parameters = ParametersHelper.GetParameters();
            
            // Act 
            ParametersRepository.SaveParameters(parameters, @"..\..\parameters.txt");
        }
        
        [TestCase(TestName = "Тест для проверки получение сохраненных" +
                             " параметров. Позитивный тест")]
        public void GetParameters_CorrectResult()
        {
            // SetUp
            var parameters = ParametersHelper.GetParameters();
            ParametersRepository.SaveParameters(parameters, @"..\..\parameters.txt");
            
            // Act 
            var newParameters = ParametersRepository.GetParameters(@"..\..\parameters.txt");
            
            // Assert
            Assert.NotNull(newParameters);
        }
        
        [TestCase(TestName = "Тест для проверки получение сохраненных" +
                             " параметров. Позитивный тест")]
        public void GetParameters_NullParameters_ThrowsException()
        {
            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                // Act
                ParametersRepository.SaveParameters(null, @"..\..\parameters.txt");
            });
        }
    }
}