 using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Huffman_New
{

    public delegate void PercentCompletedEventHandler();
    public class HuffmanAlgorithm : IDisposable
    {

        public int jts;
        #region Internal classes

        [Serializable]
        public class FrequencyTable
        {
            public Byte[] FoundBytes;
            public uint[] Frequency;
        }
        public class TreeNode
        {
            #region Members

            public TreeNode

                Lson = null,
                Rson = null,
                ///<summery> Pointer to the parent of the node.</summery>
                Parent = null;
            /// The Byte value of a leaf, it is relevant only when the node is actualy a leaf.
            public Byte ByteValue;

            /// <summary>
            /// This is the frequency value of the node
            /// </summary>
            public ulong Value;

            #endregion
        }
        public class HuffmanTree
        {
            #region Members

            public readonly TreeNode[] Leafs;
            public readonly FrequencyTable FT;
            private ArrayList OrphanNodes = new ArrayList();
            public readonly TreeNode RootNode;

            #endregion

            /// <summary>Build a Huffman tree out of a frequency table.</summary>
            public HuffmanTree(FrequencyTable FT)
            {
                ushort Length = (ushort)FT.FoundBytes.Length;
                this.FT = FT;
                Leafs = new TreeNode[Length];
                if (Length > 1)
                {
                    for (ushort i = 0; i < Length; ++i)
                    {
                        Leafs[i] = new TreeNode();
                        Leafs[i].ByteValue = FT.FoundBytes[i];
                        Leafs[i].Value = FT.Frequency[i];
                    }
                    OrphanNodes.AddRange(Leafs);
                    RootNode = BuildTree();
                }
                else
                {//No need to create a tree (only one node below rootnode)
                    TreeNode TempNode = new TreeNode();
                    TempNode.ByteValue = FT.FoundBytes[0];
                    TempNode.Value = FT.Frequency[0];
                    RootNode = new TreeNode();
                    RootNode.Lson = RootNode.Rson = TempNode;
                }
                OrphanNodes.Clear();
                OrphanNodes = null;

            }
            private TreeNode BuildTree()
            {
                TreeNode small, smaller, NewParentNode = null;
                /*stop when the tree is fully build( only one root )*/
                while (OrphanNodes.Count > 1)
                {
                    FindSmallestOrphanNodes(out smaller, out  small);
                    NewParentNode = new TreeNode();
                    NewParentNode.Value = small.Value + smaller.Value;
                    NewParentNode.Lson = smaller;
                    NewParentNode.Rson = small;
                    smaller.Parent = small.Parent = NewParentNode;
                    OrphanNodes.Add(NewParentNode);
                }
                //returning the root of the tree (always the last new parent)
                return NewParentNode;
            }

            private void FindSmallestOrphanNodes(out TreeNode Smallest, out TreeNode Small)
            {
                Smallest = Small = null;
                //Scanning backward
                ulong Tempvalue = 18446744073709551614;
                TreeNode TempNode = null;
                int i, j = 0;
                int ArrSize = OrphanNodes.Count - 1;
                //scanning for the smallest value orphan node
                for (i = ArrSize; i != -1; --i)
                {
                    TempNode = (TreeNode)OrphanNodes[i];
                    if (TempNode.Value < Tempvalue)
                    {
                        Tempvalue = TempNode.Value;
                        Smallest = TempNode;
                        j = i;
                    }
                }
                OrphanNodes.RemoveAt(j);
                --ArrSize;

                Tempvalue = 18446744073709551614;
                //scanning for the second smallest value orphan node
                for (i = ArrSize; i > -1; --i)
                {
                    TempNode = (TreeNode)OrphanNodes[i];
                    if (TempNode.Value < Tempvalue)
                    {
                        Tempvalue = TempNode.Value;
                        Small = TempNode;
                        j = i;
                    }
                }
                OrphanNodes.RemoveAt(j);

            }
        }
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

        //-------------------------------------------------------------------------------		
        /// <summary>
        /// This is the file/stream header that attached to each archived file or stream at the begining.
        /// </summary>
        [Serializable]
        public class FileHeader
        {

            /// <summary>The version of the archiving code.</summary>
            public readonly byte version;

            /// <summary>The frequency table of the archived data.</summary>
            public readonly FrequencyTable FT;


            /// <summary>The size of the data before archiving it.</summary>
            public readonly long OriginalSize;


            /// <summary>Number of extra bits added to the last  byte of the data.</summary>
            public readonly byte ComplementsBits;


            /// <summary>Security key to the archived stream\file.</summary


            public FileHeader(Byte ver, FrequencyTable T, ref long OrgSize,
                byte BitsToFill)
            {
                version = ver; FT = T; OriginalSize = OrgSize; ComplementsBits = BitsToFill;

            }
        }
        #endregion
        //-------------------------------------------------------------------------------
        #region Members
        //-------------------------------------------------------------------------------
        /// <summary>
        /// This is a temporary array to sign  where it's location in the 
        /// <c>BuildFrequencyTable</c> function (the value is the location.
        /// </summary>
        private Byte[] ByteLocation = new Byte[256];
        /// <summary>
        /// This array indicated if the byte with the value that correspond
        /// to the index of the array (0-255) was found or not in the stream.
        /// </summary>
        private bool[] IsByteExist;

        /// <summary>Holds the bytes that where found.</summary>
        private ArrayList BytesList = new ArrayList();

        /// <summary>Holds the amount of repetitions of byte.</summary>
        private ArrayList AmountList = new ArrayList();

        /// <summary>I use this list to write the reverse path to a Byte.</summary>
        private ArrayList BitsList = new ArrayList();

        /// <summary>Uses to write and read the Headers to and from a stream.</summary>
        private BinaryFormatter BinFormat = new BinaryFormatter();

        /// <summary>This stack is used to write extracted and shrinked bytes.</summary>
        private BitsStack Stack = new BitsStack();


        private PercentCompletedEventHandler OnPercentCompleted;

        #endregion
        //-------------------------------------------------------------------------------
        #region Public Functions
        public void ShrinkWithProgress(Stream Data, string OutputFile)
        {
            //Generating the header data from the stream and creating a HuffmanTree
            HuffmanTree HT = new HuffmanTree(BuildFrequencyTable(Data));
            //Creating temporary file
            FileStream tempFS = new FileStream(OutputFile, FileMode.Create);
            //Writing  header
            //WriteHeader(tempFS, HT.FT, Data.Length, 11, GetComplementsBits(HT));
            long DataSize = Data.Length;
            TreeNode TempNode = null;
            Byte Original; //the byte we read from the original stream

            int j; int k;
            for (long i = 0; i < DataSize; ++i)
            {
                Original = (Byte)Data.ReadByte();
                TempNode = HT.Leafs[ByteLocation[Original]];
                while (TempNode.Parent != null)
                {
                    //If I'm left sone of my parent push 1 else push 0
                    BitsList.Add(TempNode.Parent.Lson == TempNode);
                    TempNode = TempNode.Parent;//Climb up the tree.
                }
                //BitsList.Reverse();
                //k = BitsList.Count;
                //for (j = 0; j < k; ++j)
                //{
                //    Stack.PushFlag((bool)BitsList[j]);
                //    if (Stack.IsFull())
                //    {
                //        tempFS.WriteByte(Stack.Container);
                //        Stack.Empty();
                //    }
                //}
                //BitsList.Clear();
            }

            ////Writing the last byte if the stack wasn't compleatly full.
            //if (!Stack.IsEmpty())
            //{
            //    Byte BitsToComplete = (Byte)(8 - Stack.NumOfBits());
            //    for (byte Count = 0; Count < BitsToComplete; ++Count)//complete to full 8 bits
            //        Stack.PushFlag(false);
            //    tempFS.WriteByte(Stack.Container);
            //    Stack.Empty();
            //}
            //tempFS.Seek(0, SeekOrigin.Begin);
            //tempFS.Close();
            BitsList.Reverse();
            k = BitsList.Count;
            StringBuilder sb = new StringBuilder();
            int count = 1;
            bool current = (bool)BitsList[0];
            for (j = 1; j < k; j++)
            {
                if (current == (bool)BitsList[j])
                {
                    count++;
                }
                else
                {
                    //sb.AppendFormat("{0}{1}", count, current ? 1 : 0);
                    sb.AppendFormat("{0}{1}", Convert.ToString((char)count, 2).PadLeft(8, '0'), current ? 1 : 0);
                    count = 1;
                    current = (bool)BitsList[j];
                }

            }
            sb.AppendFormat("{0}{1}", Convert.ToString((char)count, 2).PadLeft(8, '0'), current ? 1 : 0);
            string str = sb.ToString();
            //byte[] byteArray = new byte[str.Length];
            //ArrayList Blist = new ArrayList();
            bool [] ar = new bool[str.Length];
            for (int i = 0; i < str.Length; i++)
            {
                ar[i]=(str[i]=='1');
            }
            k = str.Length;
            for (j = 0; j < k; ++j)
            {
                Stack.PushFlag((bool)ar[j]);
                if (Stack.IsFull())
                {
                    tempFS.WriteByte(Stack.Container);
                    Stack.Empty();
                }
            }
            //Blist.Clear();
            if (!Stack.IsEmpty())
            {
                Byte BitsToComplete = (Byte)(8 - Stack.NumOfBits());
                for (byte Count = 0; Count < BitsToComplete; ++Count)//complete to full 8 bits
                    Stack.PushFlag(false);
                tempFS.WriteByte(Stack.Container);
                Stack.Empty();
            }
            //tempFS.Seek(0, SeekOrigin.Begin);
            //MemoryStream stream = new MemoryStream(byteArray);
            //for (long i = 0; i < stream.Length; ++i)
            //{
            //    tempFS.WriteByte((Byte)stream.ReadByte());
            //}
            BinFormat.Serialize(tempFS, 0);
            tempFS.Seek(0, SeekOrigin.Begin);
            tempFS.Close();
            MessageBox.Show("file was Encoded successfully");

        }



        public bool IsArchivedStream(Stream Data)
        {
            Data.Seek(0, SeekOrigin.Begin);
            bool test = true;
            try
            {
                FileHeader Header = (FileHeader)BinFormat.Deserialize(Data);
                Header = null;
            }
            catch (Exception)
            {
                //if header wasn't found
                test = false;
            }
            finally
            {
                Data.Seek(0, SeekOrigin.Begin);
            }
            return test;
        }

        #endregion
        //-------------------------------------------------------------------------------
        #region Public events
        /// <summary>
        /// This is Asynchronic event and accures when the <c>Extract</c> function returns
        /// on wrong password error.
        /// Invoked whenever attempt to extract password protected file\stream, by
        /// using the wrong password(Fatal error). In case this event isn't handaled by the users
        /// an exeption will be thrown(in password error case).
        /// </summary>
        [Description("Invoked whenever attempt to extract password protected file\\stream " +
             "by using the wrong password. In case this event isn't handaled by the users " +
             "an exeption will be thrown(in password error case).")
        ]
        [Category("Behavior")]


        //-------------------------------------------------------------------------------
        /// <summary>
        /// This is Asynchronic event and invoked only from xxxxWithProgress functions.
        /// Invoked whenever another 1 percent of the function is done.
        /// </summary>
        //[Description("This is Asynchronic event and invoked only from xxxxWithProgress " +
        //     "functions. Invoked whenever another 1 percent of the function is done.")
        //]
        //[Category("Action")]
        //public event PercentCompletedEventHandler PercentCompleted
        //{
        //    add { OnPercentCompleted += value; }
        //    remove { OnPercentCompleted -= value; }
        //}

        #endregion
        //-------------------------------------------------------------------------------
        #region Private Functions
        private FrequencyTable BuildFrequencyTable(Stream DataSource)
        {
            long OriginalPosition = DataSource.Position;
            FrequencyTable FT = new FrequencyTable();
            IsByteExist = new bool[256]; //false by default

            Byte bTemp;
            //Counting bytes and saving them
            for (long i = 0; i < DataSource.Length; ++i)
            {
                bTemp = (Byte)DataSource.ReadByte();
                if (IsByteExist[bTemp]) //If the byte was found before increase the repeatition
                    AmountList[ByteLocation[bTemp]] = (uint)AmountList[ByteLocation[bTemp]] + 1;
                else/*If new byte*/
                {
                    IsByteExist[bTemp] = true; //Mark as found
                    ByteLocation[bTemp] = (Byte)BytesList.Count; //Save the new location of the byte in the bouth ArrayLists
                    AmountList.Add(1u); //Marking that one was found
                    BytesList.Add(bTemp);
                }
            }
            int ArraySize = BytesList.Count;
            FT.FoundBytes = new byte[ArraySize];
            FT.Frequency = new uint[ArraySize];
            short ArraysSize = (short)ArraySize;
            //Copy the list to arrays;
            for (short i = 0; i < ArraysSize; ++i)
            {
                FT.FoundBytes[i] = (Byte)BytesList[i];
                FT.Frequency[i] = (uint)AmountList[i];
            }
            SortArrays(FT.Frequency, FT.FoundBytes, ArraysSize);
            IsByteExist = null;
            BytesList.Clear();
            AmountList.Clear();
            DataSource.Seek(OriginalPosition, SeekOrigin.Begin);
            return FT;
        }
        private void SortArrays(uint[] SortTarget, Byte[] TweenArray, short size)
        {
            --size;
            bool TestSwitch = false;
            Byte BTemp;
            uint uiTemp;
            short i, j;
            for (i = 0; i < size; ++i)
            {
                for (j = 0; j < size; ++j)
                {
                    if (SortTarget[j] < SortTarget[j + 1])
                    {
                        TestSwitch = true;//Making switch action
                        uiTemp = SortTarget[j];
                        SortTarget[j] = SortTarget[j + 1];
                        SortTarget[j + 1] = uiTemp;
                        //Doing same to corresponding array
                        BTemp = TweenArray[j];
                        TweenArray[j] = TweenArray[j + 1];
                        TweenArray[j + 1] = BTemp;
                    }
                }//end of for
                if (!TestSwitch) break;//if no switch action in this round, no need for more.
                TestSwitch = false;
            }//end of for
            for (i = 0; i < SortTarget.Length; ++i)
                ByteLocation[TweenArray[i]] = (Byte)i;

        }
        private void WriteHeader(Stream St, FrequencyTable FT, long OriginalSize,
            Byte version, Byte ComplementsBits)
        {
            FileHeader Header = new FileHeader(version, FT, ref OriginalSize,
                ComplementsBits);
            BinFormat.Serialize(St, Header);
        }

        private Byte GetComplementsBits(HuffmanTree HT)
        {
            //Getting the deapth of each leaf in the huffman tree
            short i = (short)HT.Leafs.Length;
            ushort[] NodesDeapth = new ushort[i];
            long SizeInOfBits = 0;
            while (--i != -1)
            {
                TreeNode TN = HT.Leafs[i];
                while (TN.Parent != null)
                {
                    TN = TN.Parent;
                    ++NodesDeapth[i];
                }
                SizeInOfBits += NodesDeapth[i] * HT.FT.Frequency[i];
            }
            return (byte)(8 - SizeInOfBits % 8);
        }

        #endregion
        //-------------------------------------------------------------------------------
        #region IDisposable Members

        public void Dispose()
        {
            BytesList = null;
            IsByteExist = null;
            AmountList = null;
            BinFormat = null;
            BitsList = null;
            ByteLocation = null;
            OnPercentCompleted = null;
        }

        #endregion
        //-----------------------------------------------------------------------------
        /////////////////////////////////////////////////////////////////////////
        public Stream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
        static Stream GenerateStreamFromByteArray(Byte[] bytes)
        {
            MemoryStream stream = new MemoryStream(bytes);

            //var writer = new BinaryWriter(stream);
            //writer.Write(bytes);
            return stream;
        }
        //public static void CopyStream(Stream input, Stream output)
        //{
        //    byte[] buffer = new byte[16*1024];
        //    int read;
        //    while((read = input.Read (buffer, 0, buffer.Length) > 0)
        //        output.Write (buffer, 0, read);

        //}
    }
}
