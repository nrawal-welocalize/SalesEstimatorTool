using SalesEstimatorTool.Data.Enums;
using System;

namespace SalesEstimatorTool.Data.Models
{
    public class DivisorMapping
    {
        public String Language { get; set; }

        public ActionType Action { get; set; }

        public Decimal Divisor { get; set; }
    }
}
