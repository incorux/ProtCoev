using System.Drawing;
using System.Windows.Forms;

namespace ProteinCoev
{
    public partial class Form2 : Form
    {
        public Form2(double[,] arr)
        {
            InitializeComponent();
            FillWithArray(arr);
        }
        public void FillWithArray(double[,] arr)
        {
            var len = arr.GetLength(0);
            tableLayoutPanel.RowCount = len + 1;
            tableLayoutPanel.ColumnCount = len + 1;
            tableLayoutPanel.RowStyles.Clear();
            tableLayoutPanel.ColumnStyles.Clear();
            for (var i = 0; i < len + 1; i++)
            {
                tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100 / len));
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100 / len));
            }
            tableLayoutPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            var treshold = arr.GetTreshold(95);
            for (var i = 0; i < len; i++)
            {
                var label = new Label
                {
                    Text = i.ToString(),
                    Dock = DockStyle.Fill,
                    AutoSize = true,
                    Anchor = AnchorStyles.None,
                    BackColor = Color.Gray
                };
                tableLayoutPanel.Controls.Add(label, i + 1, 0);
                var label2 = new Label
                {
                    Text = i.ToString(),
                    Dock = DockStyle.Fill,
                    AutoSize = true,
                    Anchor = AnchorStyles.None,
                    BackColor = Color.Gray
                };
                tableLayoutPanel.Controls.Add(label2, 0, i + 1);
            }
            for (var i = 0; i < len; i++)
            {
                for (var j = 0; j < len; j++)
                {
                    if (i == j) continue;
                    var val = arr[i, j];
                    var label = new Label
                                {
                                    Text = val.ToString("F"),
                                    Dock = DockStyle.Fill,
                                    AutoSize = true,
                                    Anchor = AnchorStyles.None
                                };
                    if (val >= treshold) label.BackColor = Color.LawnGreen;
                    tableLayoutPanel.Controls.Add(label, i + 1, j + 1);
                }
            }
        }
    }
}