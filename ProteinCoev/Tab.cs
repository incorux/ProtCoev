using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ProteinCoev
{
    class Tab : TabPage
    {
        public List<Protein> Proteins;
        public List<int> BaseColumns;
        public string Label;
        private RichTextBox alignmentArea;

        public Tab(string label, List<Protein> proteins)
        {
            Label = label;
            Proteins = proteins;
            alignmentArea = new RichTextBox { Font = new Font("Courier New", 12), WordWrap = false, Dock = DockStyle.Fill };
            Controls.Add(alignmentArea);
            DrawAlignments();
            Text = label;
        }

        public override sealed string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        public void Highlight(Color color)
        {
            var seqLength = Proteins.First().Sequence.Length;
            for (var i = 0; i < Proteins.Count; i++)
            {
                foreach (var baseColumn in BaseColumns)
                {
                    alignmentArea.SelectionStart = baseColumn + i * seqLength + i;
                    alignmentArea.SelectionLength = 1;
                    alignmentArea.SelectionBackColor = color;
                }
            }
        }

        public void DrawAlignments(string organism = "All")
        {
            alignmentArea.Text = "";
            foreach (var protein in Proteins.Where(protein => organism.Equals("All") || protein.Organism.Equals(organism)))
            {
                alignmentArea.AppendText(protein.Sequence + "\n");
            }
        }
    }
}
