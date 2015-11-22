using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

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
            switch (c)
            {
                case 'A': return 0;
                case 'R': return 1;
                case 'N': return 2;
                case 'D': return 3;
                case 'C': return 4;
                case 'Q': return 5;
                case 'E': return 6;
                case 'G': return 7;
                case 'H': return 8;
                case 'I': return 9;
                case 'L': return 10;
                case 'K': return 11;
                case 'M': return 12;
                case 'F': return 13;
                case 'P': return 14;
                case 'S': return 15;
                case 'T': return 16;
                case 'W': return 17;
                case 'Y': return 18;
                case 'V': return 19;
                default: return 20;
            }
        }
        public static char ToAcid(this int integer)
        {
            return Enum.GetName(typeof(Aminoacids), (Aminoacids)integer)[0];
        }
        public static Aminoacids? ToAcid(this char c)
        {
            if (Enum.GetNames(typeof(Aminoacids)).Contains(c.ToString()))
                return (Aminoacids)Enum.Parse(typeof(Aminoacids), c.ToString());
            return null;
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
                    var str = protein.Sequence.Trim();
                    arr[index, i] = str[i];
                }
            }
            return arr;
        }
        public static char[,] ToCharArrayRestricted(this List<Protein> proteins, List<int> columns)
        {
            var len = columns.Count == 0 ? proteins.First().Sequence.Length : columns.Count;
            var arr = new char[proteins.Count, len];
            var fullArray = proteins.ToCharArray();
            for (var j = 0; j < proteins.Count; j++)
            {
                for (int k = 0; k < len; k++)
                {
                    var index = columns.Count == 0 ? k : columns[k];
                    arr[j, k] = fullArray[j, index];
                }
            }
            return arr;
        }

        public static double GetTreshold(this double[,] arr, int amount)
        {
            var len = arr.GetLength(0);
            var arr2 = new double[len * len];
            Buffer.BlockCopy(arr, 0, arr2, 0, len * len * sizeof(double));
            Array.Sort(arr2);
            var treshold = amount;
            return arr2[len * len - treshold];
        }

        public static IEnumerable<T> GetRow<T>(this T[,] array, int index)
        {
            for (var i = 0; i < array.GetLength(1); i++)
            {
                yield return array[index, i];
            }
        }

        public static IEnumerable<int> GetAllIndexes(this string source, string matchString)
        {
            matchString = Regex.Escape(matchString);
            return from Match match in Regex.Matches(source, matchString) select match.Index;
        }

        public static List<T> Flatten<T>(this T[,] source)
        {
            var length = (int)Math.Sqrt(source.Length);
            var list = new List<T>();
            for (var i = 0; i < length; i++)
            {
                for (var j = 0; j < i; j++)
                {
                    list.Add(source[i, j]);
                }
            }
            return list;
        }
    }
}