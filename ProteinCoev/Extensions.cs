using System;
using System.Collections.Generic;
using System.Linq;

namespace ProteinCoev
{
    public static class Extensions
    {
        public static char GetChar(this Aminoacids acid)
        {
            return Enum.GetName(typeof(Aminoacids), acid)[0];
        }

        public static char ToAcid(this int integer)
        {
            return Enum.GetName(typeof(Aminoacids), (Aminoacids)integer)[0];
        }

        public static Aminoacids ToAcid(this char c)
        {
            return (Aminoacids)Enum.Parse(typeof(Aminoacids), c.ToString());
        }

        public static void AddAcidCount(this List<Cluster> clusters, Aminoacids acid)
        {
            foreach (var cluster in clusters.Where(cluster => cluster.Contains(acid)))
            {
                cluster.Count++;
                return;
            }
        }
    }
}