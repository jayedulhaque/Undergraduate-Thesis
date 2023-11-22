using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;

namespace WindowsFormsApplication4
{
    public class Run_Length
    {
        private BinaryFormatter BinFormat = new BinaryFormatter();

        
        public struct BitsStack
        {

            public Byte Container;
            private byte Amount;
            public bool IsFull()
            {
                return Amount == 8;
            }
            public bool IsEmpty()
            {
                return Amount == 0;
            }
            public Byte NumOfBits()
            {
                return Amount;
            }
            public void Empty() { Amount = Container = 0; }
            public void PushFlag(bool Flag)
            {
                if (Amount == 8) throw new Exception("Stack is full");
                Container >>= 1;
                if (Flag) Container |= 128;
                ++Amount;
            }
            public bool PopFlag()
            {
                if (Amount == 0) throw new Exception("Stack is empty");
                bool t = (Container & 1) != 0;
                --Amount;
                Container >>= 1;
                return t;
            }
            public void FillStack(Byte Data)
            {
                Container = Data;
                Amount = 8;
            }

        }
        /// <summary>This stack is used to write extracted and shrinked bytes.</summary>
        private BitsStack Stack = new BitsStack();
        public void Encode(string input, string OutputFile)
       {
           string s = input;
           FileStream tempFS = new FileStream(OutputFile, FileMode.Create);
           StringBuilder sb = new StringBuilder();
           int count = 1;
           char current =s[0];
           for(int i = 1; i < s.Length;i++)
           {
               if (current == s[i])
               {
                   count++;
               }
               else
               {
                   sb.AppendFormat("{0}{1}", (char)count, current);
                   count = 1;
                   current = s[i];
               }
           }
           sb.AppendFormat("{0}{1}",(char)count , current);
            //Convert.ToString((char)count, 2).PadLeft(8, '0')
           string str = sb.ToString();
           byte[] byteArray = new byte[str.Length];
           for (int i = 0; i < str.Length; i++)
           {
               byteArray[i] = (byte)str[i];
           }
           MemoryStream stream = new MemoryStream(byteArray);
           for (long i = 0; i < stream.Length; ++i)
           {
               tempFS.WriteByte((Byte)stream.ReadByte());
           }
           BinFormat.Serialize(tempFS, 0);
           tempFS.Seek(0, SeekOrigin.Begin);
           tempFS.Close();
           MessageBox.Show("file was Encoded successfully");
       }
        public static string Decode(string s)
        {
            string a = "";
            int count = 0;
            StringBuilder sb = new StringBuilder();
            char current = char.MinValue;
            for (int i = 0; i < s.Length; i++)
            {
                current = s[i];
                if (char.IsDigit(current))
                    a += current;
                else
                {
                    count = int.Parse(a);
                    a = "";
                    for (int j = 0; j < count; j++)
                        sb.Append(current);
                }
            }
            return sb.ToString();
        }
        public Byte[] GetBytesFromBinaryString(String binary)
        {
            var list = new List<Byte>();

            for (int i = 0; i < binary.Length; i += 8)
            {
                String t = binary.Substring(i, 8);

                list.Add(Convert.ToByte(t, 2));
            }

            return list.ToArray();
        }
        public string AsciiToBinary (string input)
        {
                string converted = string.Empty;
                // convert string to byte
                byte[] byteArray = Encoding.ASCII.GetBytes(input);
      

                for (int i = 0; i < byteArray.Length; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        converted += (byteArray[i] & 0x80) > 0 ? "1" : "0";
                        byteArray[i] <<= 1;
                    }
                }
            return converted;
        }
        public char DeciToChar(int count)
        {
            int i = count;
            char a = (char)i;
            return a;
        }
        public string chex(int e)                  // Convert a byte to a string representing that byte in hexadecimal
        {
            int value = e;
            // Convert the decimal value to a hexadecimal value in string form. 
            string hexOutput = String.Format("{0:X}", value);
            return hexOutput;
        }
        public byte[] StreamToByte(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
      }

    
}
