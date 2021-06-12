using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicWorm.Utils
{
    public static class StringGeneratorUtil
    {
        private const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private const int length = 20;
        public static string GenerateRandomString()
        {
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, random.Next(1, length))
                .Select(c => c[random.Next(c.Length)]).ToArray());
        }
    }
}
