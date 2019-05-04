using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebCalculator.Models
{
    public class Calculation
    {
        public int CalculationId { get; set; }
        public double FirstOperand { get; set; }
        public double SecondOperand { get; set; }
        public double Result { get; set; }
        public DateTime CreationDate { get; set; }
        public CalcOperation Operation { get; set; }
        public int UserId { get; set; }
    }
}