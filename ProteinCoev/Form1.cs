using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ProteinCoev
{
    public partial class Form1 : Form
    {
        // GLOBAL VARIABLES
        private Color _highlightColor = Color.Gray;
        private List<string> OrganismList = new List<string>();
        private Worker _worker;
        //
        public Form1()
        {
            InitializeComponent();
            AlignmentTabs.Controls.Clear();
            ColorButton.BackColor = _highlightColor;
            comboOrganisms.DropDownStyle = ComboBoxStyle.DropDown;
            comboOrganisms.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboOrganisms.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private void LoadFileButtonClick(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                ParseFile(ofd.FileName);
            }
        }

        private void ParseFile(string fileName)
        {
            var proteins = new List<Protein>();
            var rawFileName = fileName.Split('\\').Last().Split('.').First();
            var fileReader = new StreamReader(fileName);
            string line;
            while ((line = fileReader.ReadLine()) != null)
            {
                if (line[0] == '>')
                {
                    var index = line.IndexOf("_", StringComparison.Ordinal);
                    var organism = line.Substring(index + 1, line.IndexOf("/", StringComparison.Ordinal) - index - 1);
                    proteins.Add(new Protein
                                 {
                                     Organism = organism,
                                     FileName = rawFileName
                                 });
                    if (!OrganismList.Contains(organism))
                        OrganismList.Add(organism);
                }
                else
                {
                    proteins.Last().Sequence = String.Concat(proteins.Last().Sequence, line.Trim());
                }
            }
            var newTab = new Tab(rawFileName, proteins);
            AlignmentTabs.Controls.Add(newTab);
            LoadCombo();
        }
        private void LoadCombo()
        {
            comboOrganisms.Items.Clear();
            comboOrganisms.Items.Add("All");

            foreach (var org in OrganismList)
            {
                comboOrganisms.Items.Add(org);
            }
            comboOrganisms.SelectedText = "All";
        }

        private void ButtonApplyClick(object sender, EventArgs e)
        {
            if (_worker == null)
                _worker = new Worker(progressBar);
            if (_worker.IsBusy) return;

            ((Tab)AlignmentTabs.SelectedTab).DrawAlignments();

            _worker.Run(new ArgumentWrapper
                        {
                            Tab = AlignmentTabs.SelectedTab,
                            Identity = (int)numericIdentity.Value,
                            CreditStart = (int)numericStartingCredit.Value,
                            CreditGain = (int)numericCreditGain.Value,
                            CreditLoss = (int)numericCreditLoss.Value,
                            UseBase = checkBase.Checked,
                            UseTailing = checkTailing.Checked
                        });
        }

        private void ColorButtonClick(object sender, EventArgs e)
        {
            var cd = new ColorDialog();
            if (cd.ShowDialog() != DialogResult.OK) return;
            _highlightColor = cd.Color;
            ColorButton.BackColor = cd.Color;
            ((Tab)AlignmentTabs.SelectedTab).Highlight(cd.Color);
        }

        private void ComboOrganismsSelectedIndexChanged(object sender, EventArgs e)
        {
            var tab = ((Tab)AlignmentTabs.SelectedTab);
            tab.DrawAlignments(comboOrganisms.SelectedItem.ToString());
        }
    }
}
