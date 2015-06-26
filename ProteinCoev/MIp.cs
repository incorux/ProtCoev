using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ProteinCoev
{
    public class MIp
    {
        private double[,] MIs;
        private double[,] APCs;
        private double[,] MIps;
        private double[,] Zscores;
        [ThreadStatic]
        private static int j;

        public MIp(double[,] mIs)
        {
            MIs = mIs;
            var length = (int)Math.Sqrt(MIs.Length);
            APCs = new double[length, length];
            MIps = new double[length, length];
        }
        public double[,] GetMIps()
        {
            var averageMI = MIs.Average();
            var length = (int)Math.Sqrt(MIs.Length);
            Parallel.For(0, length, i =>
            {
                for (j = 0; j < length; j++)
                {
                    APCs[i, j] = APCs[j, i] = MIs.AverageColumn(i) * MIs.AverageColumn(j) / averageMI;
                    MIps[i, j] = MIps[j, i] = MIs[i, j] - APCs[i, j];
                }
            });
            Zscores = MIps.CalculateZscore();
            return Zscores;
        }
    }
}