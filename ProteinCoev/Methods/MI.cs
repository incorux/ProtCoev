using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProteinCoev
{
    public class MI
    {
        [ThreadStatic]
        private static int j, k, l;
        private double[,] MIs;
        private int _length;
        private int _count;
        private List<int> columns;
        private double[,] Zscores;
        private char[,] alg;
        public MI(List<Protein> proteins, List<int> columns)
        {
            alg = proteins.ToCharArray();
            _count = alg.GetLength(0);
            _length = alg.GetLength(1);
            if (columns.Count == 0) return;
            this.columns = columns;
            _length = columns.Count;
        }
        public MI(char[,] alg, List<int> columns)
        {
            this.alg = alg;
            _count = alg.GetLength(0);
            _length = alg.GetLength(1);
            if (columns.Count == 0) return;
            this.columns = columns;
            _length = columns.Count;
        }
        public double[,] GetZscores()
        {
            /////////// MIs
            MIs = new double[_length, _length];
            Parallel.For(0, _length, i =>
            {
                for (j = i + 1; j < _length; j++)
                {
                    // ROWS
                    int index1, index2;
                    if (columns != null)
                    {
                        index1 = columns[i];
                        index2 = columns[j];
                    }
                    else
                    {
                        index1 = i;
                        index2 = j;
                    }
                    var d1 = AminoAcids.GetAminoAcidDictionaryDouble();
                    var d2 = AminoAcids.GetAminoAcidDictionaryDouble();
                    var pairs = new double[20, 20];
                    var total = 0;
                    for (var k = 0; k < _count; k++)
                    {
                        var c1 = alg[k, index1];
                        var c2 = alg[k, index2];
                        if (c1.ToInt() == 20 || c2.ToInt() == 20)
                            continue;
                        total++;
                        pairs[c1.ToInt(), c2.ToInt()]++;
                        d1[c1]++;
                        d2[c2]++;
                    }
                    var sumHx = d1.Values.Sum(n => n == 0.0 ? 0 : n / total * (Math.Log(n / total, 20)));
                    var sumHy = d2.Values.Sum(n => n == 0.0 ? 0 : n / total * (Math.Log(n / total, 20)));
                    var sumHxy = 0.0;
                    for (k = 0; k < 20; k++)
                    {
                        for (l = 0; l < 20; l++)
                        {
                            if (pairs[k, l] == 0.0) continue;
                            sumHxy += pairs[k, l] / total * (Math.Log(pairs[k, l] / total) / Math.Log(20));
                        }
                    }
                    // Normalization:  / -sumHxy
                    if (i == j || sumHxy == 0) MIs[i, i] = 0;
                    else MIs[i, j] = MIs[j, i] = (-sumHx - sumHy + sumHxy) / -sumHxy;
                }
            });
            var zscores = MIs.CalculateZscore();
            return zscores;
        }
    }
}