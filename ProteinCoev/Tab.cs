using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProteinCoev
{
    public class Tab : TabPage
    {
        public List<Protein> Proteins;
        public List<int> BaseColumns = new List<int>();
        public string Label;
        public double[,] MIs;
        public List<int> identities;
        private RichTextBox alignmentArea;
        private Label positionLabel;
        private int lastStart, lastLen;
        private Color color = Color.LightGray;

        private Color selectionColor
        {
            get { return Parent.Parent.Controls.Find("ColorButton", true).First().BackColor; }
        }
        private int seqLength { get { return Proteins.First().Sequence.Length; } }

        public Tab(string label, List<Protein> proteins, Label positionLabel)
        {
            Label = label;
            Proteins = proteins;
            this.positionLabel = positionLabel;
            alignmentArea = new RichTextBox { Font = new Font("Courier New", 12), WordWrap = false, Dock = DockStyle.Fill, BackColor = Color.LightGray };
            Controls.Add(alignmentArea);
            if (proteins.Count < 2000)
                DrawAlignments();
            Text = label;
            alignmentArea.MouseUp += MouseUp;
        }

        public override sealed string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }
        public void HighlightBase(Color color)
        {
            foreach (var baseColumn in BaseColumns)
            {
                HighlightColumn(color, baseColumn);
            }
        }
        private void HighlightRow(Color color, int row)
        {
            alignmentArea.SelectionStart = row * seqLength + row;
            alignmentArea.SelectionLength = seqLength + 1;
            alignmentArea.SelectionBackColor = color;
        }
        private void HighlightColumn(Color color, int column)
        {
            for (var i = 0; i < Proteins.Count; i++)
            {
                alignmentArea.SelectionStart = column + i * seqLength + i;
                alignmentArea.SelectionLength = 1;
                alignmentArea.SelectionBackColor = color;
            }
        }

        private void HighlightBlock(Color color, int start, int length)
        {
            for (var i = 0; i < Proteins.Count; i++)
            {
                alignmentArea.SelectionStart = start + i * seqLength + i;
                alignmentArea.SelectionLength = length;
                alignmentArea.SelectionBackColor = color;
            }
        }

        public void HighlightRanges(IEnumerable<Point> points, int len)
        {
            var start = BaseColumns.FirstOrDefault();
            foreach (var point in points)
            {
                alignmentArea.SelectionStart = start + point.Y + point.X * seqLength + point.X;
                alignmentArea.SelectionLength = len;
                alignmentArea.SelectionBackColor = Color.Tomato;
            }
        }

        public void Compare(Color color, int row1 = 0, int row2 = 0, int column1 = 0, int column2 = 0)
        {
            if (row1 >= 0)
                HighlightRow(color, row1);
            if (row2 >= 0)
                HighlightRow(color, row2);
            if (column1 >= 0)
                HighlightColumn(color, column1);
            if (column2 >= 0)
                HighlightColumn(color, column2);
        }
        public void DrawAlignments(string organism = "All")
        {
            alignmentArea.Text = "";
            var arr = Proteins.ToCharArray();
            for (var i = 0; i < Proteins.Count; i++)
            {
                var sb = new StringBuilder();

                for (var j = 0; j < (seqLength < 2000 ? seqLength : 2000); j++)
                {
                    sb.Append(arr[i, j]);
                }
                alignmentArea.AppendText(sb + "\n");
            }
        }
        private void MouseUp(Object sender, EventArgs e)
        {
            var richTextBox = sender as RichTextBox;
            var len = Math.Abs(richTextBox.SelectionLength % seqLength);
            var caret = richTextBox.SelectionStart;
            var column = caret % (seqLength + 1);
            if (len == 0)
            {
                positionLabel.Text = column.ToString();
                return;
            }
            HighlightBlock(Color.LightGray, lastStart, lastLen);
            HighlightBlock(selectionColor, column, len);

            BaseColumns.Clear();
            for (var i = column; i < column + len; i++)
            {
                BaseColumns.Add(i);
            }

            lastLen = len;
            lastStart = column;

            richTextBox.SelectionStart = caret;
            richTextBox.SelectionLength = len;
        }
    }
}
