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
        [ThreadStatic]
        private static int column2, j, m, l;

        public DI(List<Protein> proteins)
        {
            length = proteins.First().Sequence.Length;
            _count = proteins.Count;
            alg = proteins.ToCharArray();
        }
        public DI(char[,] alg)
        {
            length = alg.GetLength(1);
            _count = alg.GetLength(0);
            this.alg = alg;
        }
        public double[,] getDI()
        {
            const int q = 21;
            var identityTable = new double[_count];
            Parallel.For(0, _count, i =>
            {
                identityTable[i] = GetIdenticalSequences(i);
            });
            var mEff = Math.Round(identityTable.Sum(n => 1 / n), 2);
            MIs = new double[length, length];
            var frequencies = new double[length, q];
            /////////// LOGIC ////////////////////////
            /////////////////////////////// SINGULAR //////////////////////////////////
            Parallel.For(0, length, column1 =>
            {
                for (j = 0; j < _count; j++)
                {
                    var c = alg[j, column1];
                    frequencies[column1, c.ToInt()] += 1 / identityTable[j];
                }
                for (j = 0; j < q; j++)
                {
                    var addItem = mEff / q;
                    var divisor = 2 * mEff;
                    frequencies[column1, j] += addItem;
                    frequencies[column1, j] /= divisor;
                }
            });
            ////////////////////////////// PAIRS //////////////////////////////////////
            Parallel.For(0, length, column1 =>
            {

                // Second column
                for (column2 = column1 + 1; column2 < length; column2++)
                {
                    var frequenciesPairs = new double[q, q];
                    // For each row
                    for (var j = 0; j < _count; j++)
                    {
                        var c1 = alg[j, column1];
                        var c2 = alg[j, column2];
                        var d1 = c1.ToInt();
                        var d2 = c2.ToInt();
                        frequenciesPairs[d1, d2] += 1 / identityTable[j];
                    }
                    // EQ [1]
                    for (j = 0; j < q; j++)
                    {
                        for (l = 0; l < q; l++)
                        {
                            var addItem = mEff / (q * q);
                            var divisor = (2 * mEff);
                            frequenciesPairs[j, l] += addItem;
                            frequenciesPairs[j, l] /= divisor;
                        }
                    }
                    //
                    for (m = 0; m < q; m++)
                    {
                        for (l = 0; l < q; l++)
                        {
                            var number = frequenciesPairs[m, l] /
                                            (frequencies[column1, m] * frequencies[column2, l]);
                            var log = Math.Log(number, Math.E);
                            MIs[column1, column2] += frequenciesPairs[m, l] * log;
                        }
                    }
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
                if (alg[row1, i] == alg[row2, i])
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