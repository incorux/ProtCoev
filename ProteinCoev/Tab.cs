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
        public List<int> BaseColumns;
        public string Label;
        public double[,] MIs;
        public List<int> identities;
        private RichTextBox alignmentArea;
        private Label positionLabel;
        private int seqLength { get { return Proteins.First().Sequence.Length; } }

        public Tab(string label, List<Protein> proteins, Label positionLabel)
        {
            Label = label;
            Proteins = proteins;
            this.positionLabel = positionLabel;
            alignmentArea = new RichTextBox { Font = new Font("Courier New", 12), WordWrap = false, Dock = DockStyle.Fill };
            Controls.Add(alignmentArea);
            if (proteins.Count < 2000)
                DrawAlignments();
            Text = label;
            alignmentArea.SelectionChanged += BoxSelectionChanged;
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

                for (var j = 0; j < seqLength; j++)
                {
                    sb.Append(arr[i, j]);
                }
                alignmentArea.AppendText(sb + "\n");
            }
        }
        private void BoxSelectionChanged(Object sender, EventArgs e)
        {
            var richTextBox = sender as RichTextBox;
            var caret = richTextBox.SelectionStart;
            var column = caret % (seqLength + 1);
            try
            {
                var identity = identities[column];
                if (richTextBox != null)
                    positionLabel.Text = String.Format("Column: {0},\n identity: {1}", column, identity);
            }
            catch (NullReferenceException)
            {
                positionLabel.Text = String.Format("Column: {0}", column);
            }

        }
    }
}
