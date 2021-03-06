﻿using System;

namespace WebCalculator.Domain.Models
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