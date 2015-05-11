using System;
using System.Collections.Generic;
using System.Linq;

namespace ProteinCoev
{
    public static class Extensions
    {
        public static char GetChar(this Aminoacids acid)
        {
            return Enum.GetName(typeof(Aminoacids), acid)[0];
        }
        public static int ToInt(this char c)
        {
            if (c == '-') return 20;
            return (int)c.ToAcid();
        }
        public static char ToAcid(this int integer)
        {
            return Enum.GetName(typeof(Aminoacids), (Aminoacids)integer)[0];
        }
        public static Aminoacids ToAcid(this char c)
        {
            return (Aminoacids)Enum.Parse(typeof(Aminoacids), c.ToString());
        }
        public static void AddAcidCount(this List<Cluster> clusters, Aminoacids acid)
        {
            foreach (var cluster in clusters.Where(cluster => cluster.Contains((int)acid)))
            {
                cluster.Count++;
                return;
            }
        }
        public static double Average(this double[,] array)
        {
            var sum = 0.0;
            var len = Math.Sqrt(array.Length);
            for (var i = 0; i < len; i++)
            {
                for (var j = 0; j < len; j++)
                {
                    sum += array[i, j];
                }
            }
            return sum / array.Length;
        }
        public static double StandardDeviation(this IEnumerable<double> valueList)
        {
            var M = 0.0;
            var S = 0.0;
            var k = 1;
            foreach (var value in valueList)
            {
                var tmpM = M;
                M += (value - tmpM) / k;
                S += (value - tmpM) * (value - M);
                k++;
            }
            return Math.Sqrt(S / (k - 1));
        }
        public static double AverageColumn(this double[,] array, int c)
        {
            var sum = 0.0;
            for (var i = 0; i < Math.Sqrt(array.Length); i++)
            {
                sum += array[i, c];
            }
            return sum / array.Length;
        }
        public static char[,] ToCharArray(this List<Protein> proteins)
        {
            var len = proteins.First().Sequence.Length;
            var arr = new char[proteins.Count, len];
            for (var index = 0; index < proteins.Count; index++)
            {
                var protein = proteins[index];
                for (var i = 0; i < len; i++)
                {
                    arr[index, i] = protein.Sequence[i];
                }
            }
            return arr;
        }
        public static char[,] ToCharArrayRestricted(this List<Protein> proteins, List<int> columns)
        {
            var len = columns == null ? proteins.First().Sequence.Length : columns.Count;
            var arr = new char[proteins.Count, len];
            var fullArray = proteins.ToCharArray();
            for (var j = 0; j < proteins.Count; j++)
            {
                var i = 0;
                for (int k = 0; k < len; k++)
                {
                    arr[j, i++] = fullArray[j, k];
                }
            }
            return arr;
        }
    }
}