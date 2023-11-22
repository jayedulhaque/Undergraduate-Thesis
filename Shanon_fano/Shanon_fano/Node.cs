using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shanon_fano
{
    class Node
    {
        public char Symbol { get; set; }
        public int Frequency { get; set; }
        public List<bool> bits { get; set; }
    }
}
