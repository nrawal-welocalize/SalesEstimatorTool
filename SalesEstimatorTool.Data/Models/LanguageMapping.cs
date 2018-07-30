using System;
using System.Collections;
using System.Collections.Generic;
using SalesEstimatorTool.Data.Enums;

namespace SalesEstimatorTool.Data.Models
{
    public class LanguageMapping
    {
        public String Language { get; set; }
        public IDictionary<Tier, Decimal> Prices { get; set; }
        public ActionType Action { get; set; }
        public Decimal Divisor { get; set; }
        public IDictionary<Tier, int> TurnaroundTime { get; set; }
        public IDictionary<Rush, Decimal> Rush { get; set; }
    }
}
