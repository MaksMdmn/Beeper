using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebCalculator.Domain.DTOs;
using WebCalculator.Domain.Models;

namespace WebCalculator.Domain.Helpers
{
    public static class CalculationHelper
    {
        static readonly Dictionary<string, CalcOperation> _mathDelimiters = new Dictionary<string, CalcOperation>()
        {
            {"+", CalcOperation.Addition },
            {"-", CalcOperation.Substruction },
            {"*", CalcOperation.Multiplication },
            {"/", CalcOperation.Division }
        };

        public static Calculation ToCalculaton(this string mathExpression)
        {
            Calculation result = new Calculation();
            string mathExpressionCopy = string.Copy(mathExpression);
            double sign = 1D;

            //check if expression has front minus
            if (mathExpressionCopy[0] == '-')
            {
                mathExpressionCopy = mathExpressionCopy.Substring(1);
                sign = -1D;
            }


            foreach (var delimiter in _mathDelimiters.Keys)
            {
                if (mathExpressionCopy.Contains(delimiter))
                {
                    string[] operands = mathExpressionCopy.Split(delimiter.ToCharArray());

                    result.FirstOperand = Convert.ToDouble(operands[0]) * sign;
                    result.SecondOperand = Convert.ToDouble(operands[1]);
                    result.Result = doMath(delimiter, result.FirstOperand, result.SecondOperand);
                    result.Operation = _mathDelimiters[delimiter];

                    return result;
                }
            }

            throw new ArgumentException("Incorrect format of math expression with 2 operands.");
        }

        public static CalculationDto ToCalculationDto(this Calculation calculation)
        {
            return new CalculationDto
            {
                Time = calculation.CreationDate.ToLongTimeString(),
                MathExpression = calculation.ToMathExpression(),
                Result = calculation.Result.ToString()
            };
        }

        public static string ToMathExpression(this Calculation calculation)
        {
            return new StringBuilder()
                .Append(calculation.FirstOperand)
                .Append(_mathDelimiters.FirstOrDefault(pair => pair.Value == calculation.Operation).Key)
                .Append(calculation.SecondOperand)
                .ToString();
        }

        private static double doMath(string delimiter, double firstOperand, double secondOperand)
        {
            switch (_mathDelimiters[delimiter])
            {
                case CalcOperation.Addition:
                    return firstOperand + secondOperand;
                case CalcOperation.Substruction:
                    return firstOperand - secondOperand;
                case CalcOperation.Multiplication:
                    return firstOperand * secondOperand;
                case CalcOperation.Division:
                    return firstOperand / secondOperand;
                default:
                    throw new ArgumentException("Incorrect delimiter");
            }

        }


    }
}