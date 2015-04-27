using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ProteinCoev
{
    class Worker : BackgroundWorker
    {
        private ProgressBar _pb;
        private Tab _tab;
        private decimal _identity;
        public Worker(ProgressBar pb)
        {
            _pb = pb;
            DoWork += GetBase;
            ProgressChanged += WorkerProgressChanged;
            RunWorkerCompleted += WorkerRunWorkerCompleted;
            WorkerReportsProgress = true;
            WorkerSupportsCancellation = true;
        }

        void WorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _tab.Highlight(Color.Gray);
            _pb.Value = 100;
        }

        public void Run(Tab tab, decimal identity)
        {
            _identity = identity;
            _tab = tab;
            RunWorkerAsync(tab);
        }

        private void WorkerProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            _pb.Value = e.ProgressPercentage;
        }
        private void GetBase(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            var tab = (Tab)doWorkEventArgs.Argument;
            var proteins = tab.Proteins;
            var baseColumns = new List<int>();
            var seqLength = proteins.First().Sequence.Length;
            var seqNum = proteins.Count;
            var minimum = Math.Ceiling(seqNum * _identity / 100);
            for (var i = 0; i < seqLength; i++)
            {
                var spaces = 0;
                var acids = AminoAcids.GetAminoAcidDictionary();
                var clusters = Blosum.GetClusters();
                for (var j = 0; j < seqNum; j++)
                {
                    try
                    {
                        var c = proteins[j].Sequence[i];
                        if (c == '-') spaces++;
                        else clusters.AddAcidCount(c.ToAcid());
                    }
                    catch (KeyNotFoundException) { }
                }
                var max = clusters.Max(n => n.Count);
                if (max >= minimum)
                {
                    baseColumns.Add(i);
                }
                ///////////////////////////////////
                /*for (var j = 0; j < seqNum; j++)
                {
                    try
                    {
                        var c = proteins[j].Sequence[i];
                        if (c == '-') spaces++;
                        else acids[c]++;
                    }
                    catch (KeyNotFoundException) { }
                }
                var readChars = 0;
                while (seqNum - readChars - spaces >= minimum)
                {
                    var max = acids.Values.Max();
                    var c = acids.Where(n => n.Value == max).Select(n => n.Key).First();
                    var row = (int)c.ToAcid();
                    var columnsIndices = new List<int>();
                    for (var j = 0; j < 20; j++)
                    {
                        if (Blosum.matrix[row][j] > 0)
                            columnsIndices.Add(j);
                    }
                    var count = 0;
                    foreach (var columnsIndex in columnsIndices)
                    {
                        var acid = columnsIndex.ToAcid();
                        count += acids[acid];
                        acids.Remove(acid);

                    }
                    if (count >= minimum)
                    {
                        baseColumns.Add(i);
                        break;
                    }
                    else
                    {
                        readChars += count;
                    }
                }*/

                float dividend = i;
                var progress = dividend / seqLength * 100;
                ReportProgress((int)progress);
            }
            tab.BaseColumns = baseColumns;
        }
    }
}
