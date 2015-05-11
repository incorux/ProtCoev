using System.Collections.Generic;
using System.Linq;

namespace ProteinCoev
{
    public static class AminoAcids
    {
        public static Dictionary<char, int> GetAminoAcidDictionary()
        {
            return typeof(Aminoacids).GetEnumNames().ToDictionary(enumName => enumName[0], enumName => 0);
        }
        public static Dictionary<char, double> GetAminoAcidDictionaryDouble()
        {
            return typeof(Aminoacids).GetEnumNames().ToDictionary(enumName => enumName[0], enumName => 0.0);
        }
    }

    public enum Aminoacids
    {
        A = 0,
        R = 1,
        N = 2,
        D = 3,
        C = 4,
        Q = 5,
        E = 6,
        G = 7,
        H = 8,
        I = 9,
        L = 10,
        K = 11,
        M = 12,
        F = 13,
        P = 14,
        S = 15,
        T = 16,
        W = 17,
        Y = 18,
        V = 19
    }
}
