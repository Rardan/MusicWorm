using MusicWorm.Models;
using MusicWorm.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicWorm.Utils
{
    public static class ResultUtil
    {
        public static ResultViewModel PrepareResult(String name, PartialResult result1, PartialResult result10, PartialResult result20, PartialResult result50, PartialResult result100)
        {
            ResultViewModel result = new ResultViewModel()
            {
                Name = name,
                Result1 = result1,
                Result10 = result10,
                Result20 = result20,
                Result50 = result50,
                Result100 = result100
            };
            return result;
        }

        public static PartialResult CreatePartialResult(IEnumerable<double> values)
        {
            PartialResult result = new PartialResult
            {
                Time = Math.Round(values.Average(), 3),
                Deviation = Math.Round(StatisticsUtil.StandardDeviation(values), 3)
            };
            return result;
        }
    }
}
