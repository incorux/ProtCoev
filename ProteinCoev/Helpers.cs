using System.Collections.Generic;
using System.Windows.Forms;

namespace ProteinCoev
{
    public class ArgumentWrapper
    {
        public TabPage Tab;
        public int Identity;
        public int CreditStart;
        public int CreditGain;
        public int CreditLoss;
        public bool UseBase;
        public bool UseTailing;
    }
    public class Cluster
    {
        public List<int> List = new List<int>();
        public int Count;

        public bool Contains(int i)
        {
            return List.Contains(i);
        }
    }
}
