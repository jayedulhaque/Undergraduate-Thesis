using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shanon_fano
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader streamReader = new StreamReader("D:\\filetest.txt", Encoding.UTF8);
            string text = streamReader.ReadToEnd();
            streamReader.Close();

            string input = text;
            Shanon_Algo SHA = new Shanon_Algo();

            // Build the Huffman tree
            SHA.Build(input);

            // Encode
            //BitArray encoded = SHA.Encode(input);

            //Console.Write("Encoded: ");
            //foreach (bool bit in encoded)
            //{
            //    Console.Write((bit ? 1 : 0) + "");
            //}
            //Console.WriteLine("\n");
            //Console.WriteLine("Total length:" + " " + encoded.Length / 8 + "byte");
            //Console.WriteLine("\n");

            //// Decode
            //string decoded = SHA.Decode(encoded);

            //Console.WriteLine("Decoded: " + decoded);
            //Console.WriteLine("\n");
            //Console.WriteLine("Total length:" + " " + decoded.Length + "byte");
            //Console.WriteLine("\n");
            //double c = ((decoded.Length - (encoded.Length / 8)) * 100) / decoded.Length;
            //Console.WriteLine("Compressed:" + " " + c + "%");
            //Console.ReadLine();
        }
    }
}
