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
            //_tab.HighlightBase(Color.Gray);
            _pb.Value = 100;
        }

        public void Run(ArgumentWrapper wrapper)
        {
            _tab = (Tab)wrapper.Tab;
            RunWorkerAsync(wrapper);
        }

        private void WorkerProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            _pb.Value = e.ProgressPercentage;
        }
        private void GetBase(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            var wrapper = (ArgumentWrapper)doWorkEventArgs.Argument;
            var tab = (Tab)wrapper.Tab;
            var proteins = tab.Proteins;
            var baseColumns = new List<int>();
            var seqLength = proteins.First().Sequence.Length;
            var seqNum = proteins.Count;
            var minimum = Math.Ceiling((decimal)(seqNum * wrapper.Identity / 100));
            var identityTable = new int[seqLength];
            for (var i = 0; i < seqLength; i++)
            {
                var spaces = 0;
                var clusters = Blosum.GetClusters();
                for (var j = 0; j < seqNum; j++)
                {
                    try
                    {
                        var c = proteins[j].Sequence[i];
                        var acid = c.ToAcid();
                        if (acid == null) spaces++;
                        else clusters.AddAcidCount(acid.Value);
                    }
                    catch (KeyNotFoundException) { }
                }
                var max = clusters.Max(n => n.Count);
                ///////////// IDENTITY ////////////////////
                if (max >= minimum)
                    baseColumns.Add(i);

                identityTable[i] = max;

                float dividend = i;
                var progress = dividend / seqLength * 100;
                ReportProgress((int)progress);
            }
            tab.identities = identityTable.ToList();
            tab.BaseColumns = baseColumns;
            ////////////// BASE  //////////////////////
            if (!wrapper.UseBase) return;
            var credit = wrapper.CreditStart;
            var bestCluster = new Cluster { Count = -1 };
            var currentCluster = new Cluster();
            var lastGain = baseColumns.First();
            var bestLastGain = 0;
            for (var j = baseColumns.First(); j < seqLength; j++)
            {
                currentCluster.List.Add(j);
                currentCluster.Count++;
                if (baseColumns.Contains(j))
                {
                    credit += wrapper.CreditGain;
                    lastGain = j;
                }
                else
                {
                    credit -= wrapper.CreditLoss;
                }
                if (credit >= 0) continue;
                if (currentCluster.Count > bestCluster.Count)
                {
                    bestCluster = currentCluster;
                    bestLastGain = lastGain;
                }
                credit = wrapper.CreditStart;
                currentCluster = new Cluster();
            }
            ///////////////////////////// BASE TAIL  ///////////////////////////////////////
            if (wrapper.UseTailing)
            {
                var tailLen = bestCluster.List.Last() - bestLastGain;
                bestCluster.List.RemoveRange(bestCluster.List.Count - tailLen, tailLen);
                while (tailLen > 0)
                {
                    tailLen--;
                    var first = bestCluster.List.First();
                    var last = bestCluster.List.Last();
                    if (first == 0)
                    {
                        for (var i = 0; i < tailLen; i++)
                        {
                            bestCluster.List.Add(last + i);
                        }
                    }
                    else if (last == seqNum)
                    {
                        for (var i = 0; i < tailLen; i++)
                        {
                            bestCluster.List.Add(first - i);
                        }
                    }
                    var beforeSpaces = identityTable[first - 1];
                    var afterSpaces = identityTable[last + 1];
                    if (beforeSpaces < afterSpaces)
                        bestCluster.List.Add(last + 1);
                    else bestCluster.List.Insert(0, first - 1);
                }
            }
            tab.BaseColumns = bestCluster.List;
        }
    }
}
