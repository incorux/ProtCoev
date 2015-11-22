using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProteinCoev
{
    class DI
    {
        private double[,] MIs;
        private int length;
        private int _count;
        private char[,] alg;
        private List<int> columns;
        [ThreadStatic]
        private static int column2, i, j, m, l;

        public DI(List<Protein> proteins, List<int> columns = null)
        {
            alg = proteins.ToCharArray();
            length = columns == null ? alg.GetLength(1) : columns.Count;
            this.columns = columns;
            _count = alg.GetLength(0);
        }
        public double[,] getDI()
        {
            const int q = 21;
            var identityTable = new double[_count];
            Parallel.For(0, _count, i =>
            {
                identityTable[i] = GetIdenticalSequences(i);
            });
            var rawsum = identityTable.Sum(n => 1 / n);
            var mEff = Math.Round(rawsum, 2);
            MIs = new double[length, length];
            var frequencies = new double[length, q];
            /////////// LOGIC ////////////////////////
            /////////////////////////////// SINGULAR EQ [2] & [1] //////////////////////////////////
            Parallel.For(0, length, i =>
            {
                // Take a row from base if supplied
                var index1 = columns != null ? columns[i] : i;

                for (j = 0; j < _count; j++)
                {
                    var c = alg[j, index1];
                    frequencies[i, c.ToInt()] += 1 / identityTable[j];
                }
                for (j = 0; j < q; j++)
                {
                    if (frequencies[i, j] == 0) continue;
                    var addItem = mEff / q;
                    // Assume gamma == mEff
                    var divisor = 2 * mEff;
                    frequencies[i, j] += addItem;
                    frequencies[i, j] /= divisor;
                }
            });
            ////////////////////////////// PAIRS //////////////////////////////////////
            Parallel.For(0, length, column1 =>
            {
                // Second column
                for (column2 = column1 + 1; column2 < length; column2++)
                {
                    // Take from base if supplied
                    int index1, index2;
                    if (columns != null)
                    {
                        index1 = columns[column1];
                        index2 = columns[column2];
                    }
                    else
                    {
                        index1 = column1;
                        index2 = column2;
                    }

                    var frequenciesPairs = new double[q, q];
                    // For each row
                    for (var j = 0; j < _count; j++)
                    {
                        var c1 = alg[j, index1];
                        var c2 = alg[j, index2];
                        var d1 = c1.ToInt();
                        var d2 = c2.ToInt();
                        frequenciesPairs[d1, d2] += 1 / identityTable[j];
                    }
                    // EQ [1]
                    for (j = 0; j < q; j++)
                    {
                        for (l = 0; l < q; l++)
                        {
                            if (frequenciesPairs[j, l] == 0) continue;
                            var addItem = mEff / (q * q);
                            var divisor = (2 * mEff);
                            frequenciesPairs[j, l] += addItem;
                            frequenciesPairs[j, l] /= divisor;
                        }
                    }
                    // EQ [3]
                    // For every aminoacids combination
                    for (m = 0; m < q; m++)
                    {
                        for (l = 0; l < q; l++)
                        {
                            if (frequenciesPairs[m, l] == 0) continue;
                            var number = frequenciesPairs[m, l] /
                                            (frequencies[column1, m] * frequencies[column2, l]);
                            var log = Math.Log(number, Math.E);
                            MIs[column1, column2] += frequenciesPairs[m, l] * log;
                        }
                    }
                    MIs[column2, column1] = MIs[column1, column2];
                }
            });
            var Zscores = MIs.CalculateZscore();
            return Zscores;
        }

        /// <summary>
        /// Iterates through columns of two rows: row1 and row2 finding matches.
        /// </summary>
        /// <param name="row1"></param>
        /// <param name="row2"></param>
        /// <param name="identityTreshold"></param>
        /// <returns>True if there are enough matches for the treshold</returns>
        private bool CheckSequenceIdentity(int row1, int row2, double identityTreshold)
        {
            var same = 0.0;
            for (var i = 0; i < length; i++)
            {
                var index1 = columns != null ? columns[i] : i;

                if (alg[row1, index1] == alg[row2, index1])
                    same++;
            }
            var fraction = same / length;
            return fraction > identityTreshold;
        }
        /// <summary>
        /// Calculates how many sequences are identical in identityTreshold fraction percentage.
        /// </summary>
        /// <returns></returns>
        private int GetIdenticalSequences(int sequenceIndex, double identityTreshold = 0.8)
        {
            var count = 0;
            for (var i = 0; i < _count; i++)
            {
                if (CheckSequenceIdentity(sequenceIndex, i, identityTreshold)) count++;
            }
            return count;
        }
    }
}