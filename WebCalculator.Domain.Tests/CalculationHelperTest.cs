using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebCalculator.Domain.DTOs;
using WebCalculator.Domain.Helpers;
using WebCalculator.Domain.Models;

namespace WebCalculator.Domain.Tests
{
    [TestClass]
    public class CalculationHelperTest
    {
        Calculation _calculation;
        string _mathExpression = "-1,5*2";

        [TestInitialize]
        public void Initialize()
        {

            _calculation = new Calculation
            {
                CreationDate = DateTime.Now,
                FirstOperand = -1.5D,
                SecondOperand = 2D,
                Operation = CalcOperation.Multiplication,
                Result = -3D
            };
        }

        [TestMethod]
        public void ToCalculation_MathExprStr_CorrectObjProperties()
        {
            Calculation testCalculation = _mathExpression.ToCalculaton();

            const double expectedFirstOperand = -1.5D;
            const double expectedSecondOperand = 2D;
            const double expectedResult = -3D;
            CalcOperation expectedOperation = CalcOperation.Multiplication;

            Assert.AreEqual(expectedFirstOperand, testCalculation.FirstOperand);
            Assert.AreEqual(expectedSecondOperand, testCalculation.SecondOperand);
            Assert.AreEqual(expectedResult, testCalculation.Result);
            Assert.AreEqual(expectedOperation, testCalculation.Operation);
        }

        [TestMethod]
        public void ToMathExpression_CorrectObjProperties_CorrectMathExprStr()
        {
            string testMathExpression = _calculation.ToMathExpression();
            string _expectedMathExpression = _mathExpression;

            Assert.AreEqual(_expectedMathExpression, testMathExpression);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ToMathExpression_IncorrectCorrectMathExprStr_Exception()
        {
            string testMathExpression = "05";
            testMathExpression.ToCalculaton();
        }
    }
}
