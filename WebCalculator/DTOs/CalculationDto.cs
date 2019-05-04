using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebCalculator.DTOs
{
    public class CalculationDto
    {
        public string Time { get; set; }
        public string MathExpression { get; set; }
        public string Result { get; set; }
    }
}