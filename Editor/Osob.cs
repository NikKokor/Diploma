using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editor
{
    internal class Osob
    {
        public int id_osob;
        public string name_etap;
        public string name;
        public string type;
        public List<string> str_value;
        public int[] int_value = new int[2];
    }

    internal class OsobAndEtap
    {
        public int id_etap;
        public string name;
        public Osob osob;
    }

    internal class StringAndBool
    {
        public string word;
        public bool flag;
    }
}
