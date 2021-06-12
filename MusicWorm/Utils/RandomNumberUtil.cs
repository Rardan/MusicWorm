using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicWorm.Utils
{
    public static class RandomNumberUtil
    {
        public static int GenerateFromRange(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
    }
}
