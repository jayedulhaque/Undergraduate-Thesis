using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Shanon_fano
{
    class Shanon_Algo
    {
        private List<Node> nodes = new List<Node>();
        public Node Root { get; set; }
        public Dictionary<char, int> Frequencies = new Dictionary<char, int>();
        List<Node> orderedNodes = new List<Node>();
        public void Build(string source)
        {
            for (int i = 0; i < source.Length; i++)
            {
                if (!Frequencies.ContainsKey(source[i]))
                {
                    Frequencies.Add(source[i], 0);
                }

                Frequencies[source[i]]++;
            }
            //foreach (KeyValuePair<char, int> symbol in Frequencies)
            //{
            //    Console.WriteLine(symbol.Key + " " + symbol.Value);

            //}
            //Console.WriteLine("\n");
            //Console.ReadKey();
            foreach (KeyValuePair<char, int> symbol in Frequencies)
            {
                nodes.Add(new Node() { Symbol = symbol.Key, Frequency = symbol.Value });
            }
            
            //while (nodes.Count > 1)
            //{
            orderedNodes = nodes.OrderBy(node => node.Frequency).ToList<Node>();

            //}
            orderedNodes.Reverse();
            //orderedNodes.ElementAt(0).bits.Add(false);
            Shannon(0,orderedNodes.Count);//only for 2 character
            for (int i = 0; i < orderedNodes.Count; i++)
            {
                Console.WriteLine(orderedNodes.ElementAt(i).Symbol.ToString() 
                    + " " + orderedNodes.ElementAt(i).Frequency.ToString() 
                    + " " + orderedNodes.ElementAt(i).bits.ToList());

            }
            Console.ReadKey();
        }
        public void Shannon(int l,int h)
        {
            if ((l + 1) == h || l == h || l > h)
            {
                if (l == h || l > h)
                    return;
                orderedNodes.ElementAt(l).bits.Add(false);
                orderedNodes.ElementAt(h).bits.Add(true);
                return;
            }
            else
            {

            }

        }
    }
}
