using System.Collections.Generic;

namespace ProteinCoev
{
    public class Cluster
    {
        public List<Aminoacids> AcidList;
        public int Count;

        public bool Contains(Aminoacids acid)
        {
            return AcidList.Contains(acid);
        }
    }
}
