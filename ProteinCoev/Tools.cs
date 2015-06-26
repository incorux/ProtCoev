using System;
using System.Collections.Generic;
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
            var Zscores = new double[length, length];
            var flattened = new List<double>();
            var mean = arr.Average();
            for (var i = 0; i < length; i++)
            {
                for (var j = 0; j < length; j++)
                {
                    flattened.Add(arr[i, j]);
                }
            }
            var sd = flattened.StandardDeviation();
            Parallel.For(0, length, i =>
            {
                for (j = 0; j < length; j++)
                {
                    Zscores[i, j] = Math.Abs(mean - arr[i, j]) / sd;
                }
            });
            return Zscores;
        }
    }
}
