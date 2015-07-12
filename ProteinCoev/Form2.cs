using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ProteinCoev
{
    public partial class Form2 : Form
    {
        private SeriesChartType _chartType = SeriesChartType.RangeColumn;
        private int _amount = 2000000;
        private bool All { get { return _amount == 2000000; } }
        private bool _sort;
        private double[,] _arr = null, _arr2 = null;

        public Form2(double[,] arr)
        {
            _arr = arr;
            InitializeComponent();
            GetOptions(arr.Length);
            FillWithArray(arr);
        }
        public Form2(double[,] arr, double[,] arr2)
        {
            _arr = arr;
            _arr2 = arr2;
            InitializeComponent();
            GetOptions(Math.Min(arr.Length, arr2.Length));
            FillWithArray(arr, arr2);
        }

        private void GetOptions(int max)
        {
            var options = new GraphOptions(max);
            options.ShowDialog();
            _chartType = options.Type;
            _amount = options.All ? _amount : options.Amount;
            _sort = options.Sort;
        }

        public void FillWithArray(double[,] arr, double[,] arr2 = null)
        {
            double treshold = 0.0, treshold2 = 0.0;
            if (!All)
            {
                treshold = arr.GetTreshold(_amount);
            }

            var xValues = new List<double>();
            var yValues = new List<double>();

            chart1.Series.Add(new Series("Zscores") { Enabled = true, ChartType = _chartType });

            for (var i = 0; i < arr.GetLength(0); i++)
            {
                // Search only half of the matrix, because it's doubled [i,j] = [j,i]
                for (var j = 0; j < i; j++)
                {
                    if (All && !(arr[i, j] > treshold)) continue;
                    xValues.Add(i);
                    yValues.Add(arr[i, j]);
                }
            }
            if (_sort)
            {
                yValues.Sort();
                yValues.Reverse();
            }
            /*            xValues = xValues.GetRange(0, Amount);
                        yValues = yValues.GetRange(0, Amount);*/
            chart1.Series["Zscores"].Points.DataBindXY(xValues, "Columns", yValues, "Zscores");

            if (arr2 == null) return;

            if (!All)
            {
                treshold2 = arr2.GetTreshold(_amount);
            }

            var xValues2 = new List<double>();
            var yValues2 = new List<double>();

            chart1.Series.Add(new Series("Zscores2") { Enabled = true, ChartType = _chartType });

            for (var i = 0; i < arr2.GetLength(0); i++)
            {
                for (var j = 0; j < i; j++)
                {
                    if (!All && !(arr2[i, j] > treshold2)) continue;
                    xValues2.Add(i);
                    yValues2.Add(arr2[i, j]);
                }
            }

            if (_sort)
            {
                yValues2.Sort();
                yValues2.Reverse();
            }
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
            
/*            var cor = chart1.DataManipulator.Statistics.Correlation("Zscores", "Zscores2");
            var cov = chart1.DataManipulator.Statistics.Covariance("Zscores", "Zscores2");

            chart1.Legends.Add(new Legend("Tests") { Docking = Docking.Top, Enabled = true });
            chart1.Legends["Tests"].Alignment = StringAlignment.Center;
            chart1.Legends["Tests"].CustomItems.Add(new LegendItem(String.Format("Ftest: f-value : {0}", ftest.FValue.ToString("0.00")), Color.Transparent, ""));
            chart1.Legends["Tests"].CustomItems.Add(new LegendItem(String.Format("Correlation     : {0}", cor.ToString("0.00")), Color.Transparent, ""));
            chart1.Legends["Tests"].CustomItems.Add(new LegendItem(String.Format("Covariance     : {0}", cov.ToString("0.00")), Color.Transparent, ""));*/
        }

        private void GridBtnClick(object sender, EventArgs e)
        {
            var form = new ArrayVisualizer(_arr, _arr2);
            form.ShowDialog();
        }
    }
}