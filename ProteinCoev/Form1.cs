using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace ProteinCoev
{
    public partial class Form1 : Form
    {
        // GLOBAL VARIABLES
        private Color _highlightColor = Color.Gray;
        private List<string> OrganismList = new List<string>();
        private Worker _worker;
        private String rawFileName;
        private List<Protein> proteins { get { return ((Tab)AlignmentTabs.SelectedTab).Proteins; } }
        //
        public Form1()
        {
            InitializeComponent();
            AlignmentTabs.Controls.Clear();
            ColorButton.BackColor = _highlightColor;
            //            comboOrganisms.DropDownStyle = ComboBoxStyle.DropDown;
            //            comboOrganisms.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            //            comboOrganisms.AutoCompleteSource = AutoCompleteSource.ListItems;
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
            rawFileName = fileName.Split('\\').Last().Split('.').First();
            var fileReader = new StreamReader(fileName);
            var i = 0;
            string line;
            while ((line = fileReader.ReadLine()) != null)
            {
                if (line[0] == '>')
                {
                    var index = line.IndexOf("_", StringComparison.Ordinal);
                    //var organism = line.Substring(index + 1, line.IndexOf("/", StringComparison.Ordinal) - index - 1);
                    var organism = line.Substring(1);
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
                    //proteins.Add(new Protein { Organism = "All", FileName = rawFileName });
                    proteins.Last().Sequence = String.Concat(proteins.Last().Sequence, line.Trim());
                }
            }
            var newTab = new Tab(rawFileName, proteins, labelPosition);
            AlignmentTabs.Controls.Add(newTab);
            AlignmentTabs.SelectedIndex = AlignmentTabs.TabCount - 1;
            //LoadCombo();
        }

        /*        private void LoadCombo()
                {
                    comboOrganisms.Items.Clear();
                    comboOrganisms.Items.Add("All");

                    foreach (var org in OrganismList)
                    {
                        comboOrganisms.Items.Add(org);
                    }
                    comboOrganisms.SelectedText = "All";
                }*/

        private void ButtonApplyClick(object sender, EventArgs e)
        {
            if (_worker == null)
                _worker = new Worker(progressBar);
            if (_worker.IsBusy) return;

            var tab = ((Tab)AlignmentTabs.SelectedTab);
            tab.DrawAlignments();

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
        }

        private void ComboOrganismsSelectedIndexChanged(object sender, EventArgs e)
        {
            var tab = ((Tab)AlignmentTabs.SelectedTab);
            //tab.DrawAlignments(comboOrganisms.SelectedItem.ToString());
        }

        private void MIClick(object sender, EventArgs e)
        {
            var tab = ((Tab)AlignmentTabs.SelectedTab);
            var proteins = tab.Proteins;
            var mi = new MI(proteins, tab.BaseColumns);
            var sw = new Stopwatch();
            sw.Start();
            var zscores = mi.GetZscores();
            var form = new Form2(zscores);
            form.Show();
            WriteToFile(zscores, String.Concat(tab.Label + "_", "MI"));
        }

        private void MIpClick(object sender, EventArgs e)
        {
            var tab = ((Tab)AlignmentTabs.SelectedTab);
            var proteins = tab.Proteins;
            var mi = new MI(proteins, tab.BaseColumns);
            var mis = mi.GetZscores();
            var MIp = new MIp(mis);
            var zscores = MIp.GetMIps();
            var form = new Form2(zscores);
            form.Show();
            WriteToFile(zscores, String.Concat(tab.Label + "_", "MIp"));
        }

        private void DIClick(object sender, EventArgs e)
        {
            var tab = ((Tab)AlignmentTabs.SelectedTab);
            var di = new DI(proteins, tab.BaseColumns);
            var zscores = di.getDI();
            var form = new Form2(zscores);
            form.Show();
            WriteToFile(zscores, String.Concat(tab.Label + "_", "DI"));
        }

        private void PsicovClick(object sender, EventArgs e)
        {
            var tab = ((Tab)AlignmentTabs.SelectedTab);
            var arr = proteins.ToCharArrayRestricted(tab.BaseColumns);
            var zscores = new Psicov(arr).GetPsicov();
            var form = new Form2(zscores);
            form.Show();
            WriteToFile(zscores, String.Concat(tab.Label + "_", "Psicov"));
        }

        private void RunAllClick(object sender, EventArgs e)
        {
            foreach (Tab tab in AlignmentTabs.Controls)
            {
                var proteins = tab.Proteins;
                var mi = new MI(proteins, tab.BaseColumns);
                var MIZscores = mi.GetZscores();
                WriteToFile(MIZscores, String.Concat(tab.Label + "_", "MI"), false);

                var mis = MIZscores;
                var MIp = new MIp(mis);
                var MIpZscores = MIp.GetMIps();
                WriteToFile(MIpZscores, String.Concat(tab.Label + "_", "MIp"), false);

                var di = new DI(proteins);
                var diZscores = di.getDI();
                WriteToFile(diZscores, String.Concat(tab.Label + "_", "DI"), false);

                var arr = proteins.ToCharArrayRestricted(tab.BaseColumns);
                var psicovZscores = new Psicov(arr).GetPsicov();
                if (psicovZscores == null) continue;
                WriteToFile(psicovZscores, String.Concat(tab.Label + "_", "Psicov"), false);
            }
        }
        private void WriteToFile(double[,] arr, string suffix, bool showMessage = true)
        {
            using (var fileStream = new FileStream(suffix, FileMode.Create))
            {
                var bf = new BinaryFormatter();
                var bw = new BinaryWriter(fileStream);
                var memoryStream = new MemoryStream();
                bf.Serialize(memoryStream, arr);
                bw.Write(memoryStream.ToArray());
            }
            if (showMessage)
                MessageBox.Show(String.Format("File: {0} was saved", suffix));
        }

        private void CoevBtnClick(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            var bf = new BinaryFormatter();
            if (ofd.ShowDialog() != DialogResult.OK) return;
            var file1 = ofd.FileName;
            var ofd1 = new OpenFileDialog();
            if (ofd1.ShowDialog() != DialogResult.OK) return;
            var file2 = ofd1.FileName;

            var arr1 = (double[,])bf.Deserialize(new FileStream(file1, FileMode.Open));
            var arr2 = (double[,])bf.Deserialize(new FileStream(file2, FileMode.Open));

            var form2 = new Form2(arr1, arr2);
            form2.ShowDialog();
        }

        private void SearchForStringButtonClick(object sender, EventArgs e)
        {
            var searchStr = SearchSequenceTextBox.Text.Trim();
            var tab = (Tab)AlignmentTabs.SelectedTab;
            var sequenceRange = proteins.ToCharArrayRestricted(tab.BaseColumns);

            var indices = new List<Point>();

            for (var i = 0; i < sequenceRange.GetLength(0); i++)
            {
                var str = new string(sequenceRange.GetRow(i).ToArray());
                indices.AddRange(str.GetAllIndexes(searchStr).Select(index => new Point(i, index)));
            }

            tab.HighlightRanges(indices, searchStr.Length);
        }

        private void CreateTreeButtonClick(object sender, EventArgs e)
        {
            Phylogeny.ExportToFile(proteins);
        }
    }
}