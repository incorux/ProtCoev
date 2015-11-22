using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace ProteinCoev
{
    public static class Phylogeny
    {
        public static void ExportToFile(List<Protein> proteins)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("{0} {1}\n", proteins.Count, proteins.First().Sequence.Length);
            foreach (var protein in proteins)
            {
                sb.Append(protein.Organism.Length > 8 ? protein.Organism.Substring(0, 8) : protein.Organism);
                for (var i = 0; i < 8 - protein.Organism.Length; i++)
                {
                    sb.Append(" ");
                }
                sb.Append(" ");
                sb.Append(" ");
                sb.AppendLine(protein.Sequence);
            }
            File.WriteAllText("infile", sb.ToString());

            var psi = new ProcessStartInfo
            {
                FileName = "protdist.exe",
                WorkingDirectory = Environment.CurrentDirectory
            };
            var proc = Process.Start(psi);
            proc.WaitForExit();
            File.Delete("infile");
            File.Move("outfile", "infile");

            var psi2 = new ProcessStartInfo
                      {
                          FileName = "neighbor.exe",
                          WorkingDirectory = Environment.CurrentDirectory
                      };
            var proc2 = Process.Start(psi2);
            proc2.WaitForExit();

            var psi3 = new ProcessStartInfo
            {
                FileName = "drawtree.jar",
                WorkingDirectory = Environment.CurrentDirectory
            };
            var proc3 = Process.Start(psi3);

        }
    }
}
