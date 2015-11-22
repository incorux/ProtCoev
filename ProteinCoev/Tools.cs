using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProteinCoev
{
    public static class Tools
    {
        [ThreadStatic]
        private static int j;
        public static double[,] CalculateZscore(this double[,] arr)
        {
            var length = (int)Math.Sqrt(arr.Length);
            var zscores = new double[length, length];
            var flattened = arr.Flatten();
            var mean = flattened.Average();
            var sd = flattened.StandardDeviation();
            Parallel.For(0, length, i =>
            {
                for (j = 0; j < length; j++)
                {
                    zscores[j, i] = zscores[i, j] = Math.Abs(mean - arr[i, j]) / sd;
                }
            });
            return zscores;
        }
    }
}
