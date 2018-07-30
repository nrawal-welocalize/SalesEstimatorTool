using System;
using System.Linq;

namespace SalesEstimatorTool.Data.Extensions
{
    public static class TimeSpanExtension
    {
        private static readonly Int32[] Weights = { 60 * 60 * 1000, 60 * 1000, 1000, 1 };

        public static TimeSpan ToTimeSpan(this String value)
        {
            return TimeSpan.FromMilliseconds(value.Split('.', ':').Zip(Weights, (n, w) => Convert.ToInt64(n) * w).Sum());
        }

        public static String ToFormattedString(this TimeSpan value)
        {
            return String.Format("{0:00}:{1:00}:{2:00}", Math.Floor(value.TotalHours), value.Minutes, value.Seconds);
        }
    }
}
