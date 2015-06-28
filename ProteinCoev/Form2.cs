using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ProteinCoev
{
    public partial class Form2 : Form
    {
        private const SeriesChartType ChartType = SeriesChartType.RangeColumn;
        private int Amount = 2000000;

        public Form2(double[,] arr)
        {
            InitializeComponent();
            FillWithArray(arr);
        }
        public Form2(double[,] arr, double[,] arr2)
        {
            InitializeComponent();
            FillWithArray(arr, arr2);
        }

        public void FillWithArray(double[,] arr, double[,] arr2 = null, int tresholdPercentage = 90)
        {
            var treshold = arr.GetTreshold(tresholdPercentage);

            var xValues = new List<double>();
            var yValues = new List<double>();

            chart1.Series.Add(new Series("Zscores") { Enabled = true, ChartType = ChartType });

            for (var i = 0; i < arr.GetLength(0); i++)
            {
                // Search only half of the matrix, as it's doubled [i,j] = [j,i]
                for (var j = 0; j < i; j++)
                {
                    if (!(arr[i, j] > treshold)) continue;
                    xValues.Add(i);
                    yValues.Add(arr[i, j]);
                }
            }
            yValues.Sort();
            yValues.Reverse();
            /*            xValues = xValues.GetRange(0, Amount);
                        yValues = yValues.GetRange(0, Amount);*/
            chart1.Series["Zscores"].Points.DataBindXY(xValues, "Columns", yValues, "Zscores");

            if (arr2 == null) return;
            var treshold2 = arr2.GetTreshold(tresholdPercentage);

            var xValues2 = new List<double>();
            var yValues2 = new List<double>();

            chart1.Series.Add(new Series("Zscores2") { Enabled = true, ChartType = ChartType });

            for (var i = 0; i < arr2.GetLength(0); i++)
            {
                for (var j = 0; j < i; j++)
                {
                    if (!(arr2[i, j] > treshold2)) continue;
                    xValues2.Add(i);
                    yValues2.Add(arr2[i, j]);
                }
            }

            yValues2.Sort();
            yValues2.Reverse();
            if (yValues.Count < yValues2.Count)
            {
                yValues2 = yValues2.GetRange(0, yValues.Count);
                xValues2 = xValues2.GetRange(0, yValues.Count);
            }
            //            xValues2 = xValues2.GetRange(0, Amount);
            //            yValues2 = yValues2.GetRange(0, Amount);

            chart1.Series["Zscores2"].Points.DataBindXY(xValues2, "Columns", yValues2, "Zscores");

            chart1.Series[0].YAxisType = AxisType.Primary;
            chart1.Series[1].YAxisType = AxisType.Secondary;

            chart1.ChartAreas[0].AxisY2.LineColor = Color.Transparent;
            chart1.ChartAreas[0].AxisY2.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisY2.Enabled = AxisEnabled.True;
            chart1.ChartAreas[0].AxisY2.IsStartedFromZero = chart1.ChartAreas[0].AxisY.IsStartedFromZero;

            var ftest = chart1.DataManipulator.Statistics.FTest(0.05, "Zscores", "Zscores2");
            var cor = chart1.DataManipulator.Statistics.Correlation("Zscores", "Zscores2");
            var cov = chart1.DataManipulator.Statistics.Covariance("Zscores", "Zscores2");

            chart1.Legends.Add(new Legend("Tests") { Docking = Docking.Top, Enabled = true });
            chart1.Legends["Tests"].Alignment = StringAlignment.Center;
            chart1.Legends["Tests"].CustomItems.Add(new LegendItem(String.Format("Ftest: f-value : {0}", ftest.FValue.ToString("0.00")), Color.Transparent, ""));
            chart1.Legends["Tests"].CustomItems.Add(new LegendItem(String.Format("Correlation     : {0}", cor.ToString("0.00")), Color.Transparent, ""));
            chart1.Legends["Tests"].CustomItems.Add(new LegendItem(String.Format("Covariance     : {0}", cov.ToString("0.00")), Color.Transparent, ""));
        }
    }
}