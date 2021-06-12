using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicWorm.Utils
{
    public static class StatisticsUtil
    {
        public static double StandardDeviation(IEnumerable<double> values)
        {
            double average = values.Average();
            double sumSquares = values.Select(v => (v - average) * (v - average)).Sum();
            return Math.Sqrt(sumSquares / values.Count());
        }
    }
}
