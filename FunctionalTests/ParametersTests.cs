using System;
using System.Collections.Generic;
using COPOL.BLL.Models;
using FunctionalTests.Common;
using NUnit.Framework;

namespace FunctionalTests
{
    [TestFixture]
    public class ParametersTests
    {
        [TestCase(TestName = "Тест присваиваения параметров " +
                             "в Parameters. Позитивный тест.")]
        public void Parameters_CorrectValue_CorrectResult()
        {
            // SetUp
            var parameters = ParametersHelper.GetParameters();

            // Assert
            Assert.NotNull(parameters);
            Assert.AreEqual(parameters.Cgs, 20);
            Assert.AreEqual(parameters.Cds, 5);
            Assert.AreEqual(parameters.Cgd, 30);
            Assert.AreEqual(parameters.Ls, 1);
            Assert.AreEqual(parameters.Ld, 20);
            Assert.AreEqual(parameters.Gm, 1000);
            Assert.AreEqual(parameters.Vds0, 3);
            Assert.AreEqual(parameters.Ids0, 500);
            Assert.AreEqual(parameters.N, 3);
            Assert.AreEqual(parameters.Step, 1);
            Assert.AreEqual(parameters.LoopP, 0);
            Assert.AreEqual(parameters.Frequences.Count, 2);
        }

        /// <param name="value">Присваиваемое значение.</param>
        /// <param name="parameter">В какой параметр будет присваиваться значение.</param>
        [TestCase("Cgs",
            TestName = "Тест присваивания -1 в Parameters.{0}. Негативный тест.")]
        [TestCase("Cds",
            TestName = "Тест присваивания -1 в Parameters.{0}. Негативный тест.")]
        [TestCase("Cgd",
            TestName = "Тест присваивания -1 в Parameters.{0}. Негативный тест.")]
        [TestCase("Ls",
            TestName = "Тест присваивания -1 в Parameters.{0}. Негативный тест.")]
        [TestCase("Ld",
            TestName = "Тест присваивания -1 в Parameters.{0}. Негативный тест.")]
        [TestCase("Gm",
            TestName = "Тест присваивания -1 в Parameters.{0}. Негативный тест.")]
        [TestCase("Vds0",
            TestName = "Тест присваивания -1 в Parameters.{0}. Негативный тест.")]
        [TestCase("Ids0",
            TestName = "Тест присваивания -1 в Parameters.{0}. Негативный тест.")]
        [TestCase("N",
            TestName = "Тест присваивания -1 в Parameters.{0}. Негативный тест.")]
        [TestCase("Step",
            TestName = "Тест присваивания -1 в Parameters.{0}. Негативный тест.")]
        [TestCase("LoopP",
            TestName = "Тест присваивания -1 в Parameters.{0}. Негативный тест.")]
        [TestCase("Gm",
            TestName = "Тест присваивания -1 в Parameters.{0}. Негативный тест.")]
        [TestCase("Frequencies",
            TestName = "Тест присваивания -1 в Parameters.{0}. Негативный тест.")]
        public void Parameters_WrongArgument_ThrowsExceptionResult(
            string parameter)
        {
            // SetUp
            var parameters = new Parameters();

            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                // Act
                parameters.Cgs = parameter != "Cgs" ? 20 : -1;
                parameters.Cds = parameter != "Cds" ? 5 : -1;
                parameters.Cgd = parameter != "Cgd" ? 30 : -1;
                parameters.Ls = parameter != "Ls" ? 1 : -1;
                parameters.Ld = parameter != "Ld" ? 20 : -1;
                parameters.Gm = parameter != "Gm" ? 1000 : -1;
                parameters.Vds0 = parameter != "Vds0" ? 3 : -1;
                parameters.Ids0 = parameter != "Ids0" ? 500 : -1;
                parameters.N = parameter != "N" ? 3 : -1;
                parameters.Step = parameter != "Step" ? 1 : -1;
                parameters.LoopP = parameter != "LoopP" ? 0 : -1;
                var temp = new List<float> { parameter != "Frequencies" ? 0.1f : -1 };
                parameters.Frequences = temp;
            });
        }

        [TestCase(TestName = "Тест перевода частоты из списка в строку. Негативный тест.")]
        public void Frequencies_ConvertFromListToString_CorrectResult()
        {
            // SetUp
            var parameters = new Parameters();

            // Act
            parameters.Frequences = new List<float> { (float)(1 * Math.Pow(10, 9)), (float)(0.5f * Math.Pow(10, 9)) };
            var str = parameters.ConvertFrequencesFromListToString();

            // Assert
            Assert.NotNull(str);
            Assert.AreEqual(str, "1; 0,5");
        }

        [TestCase(TestName = "Тест перевода частоты строки в список. Негативный тест.")]
        public void Frequencies_ConvertFromStringToList_CorrectResult()
        {
            // SetUp
            var parameters = new Parameters();

            // Act
            var fr1 = (float)(1 * Math.Pow(10, -9));
            var fr2 = (float)(0.5f * Math.Pow(10, -9));
            var temp = parameters.GetFrequencesFromString(fr1 + "; " + fr2);
            var checkList = new List<float> { 1, 0.5f };

            // Assert
            Assert.NotNull(temp);
            foreach (var a in checkList)
            {
                Assert.IsTrue(temp.Contains(a));
            }
        }
    }

}