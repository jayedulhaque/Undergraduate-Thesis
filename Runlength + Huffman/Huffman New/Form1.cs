using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Huffman_New
{
    public partial class Form1 : Form
    {
        private OpenFileDialog openFileD;
        private SaveFileDialog saveFileD;
        private HuffmanAlgorithm AL = new HuffmanAlgorithm();
        public Form1()
        {
            InitializeComponent();
            this.openFileD = new System.Windows.Forms.OpenFileDialog();
            this.saveFileD = new System.Windows.Forms.SaveFileDialog();
        }

        private void BTNopenSRC_Click(object sender, EventArgs e)
        {
            openFileD.DefaultExt = "*.jts";
            openFileD.Filter = "Huffman Stream files(*.jts)|*.jts|All files(*.*)|*.*";
            openFileD.Multiselect = false;
            openFileD.CheckPathExists = true;
            openFileD.ShowHelp = false;
            openFileD.Title = "Select source file";
            openFileD.ShowReadOnly = true;
            openFileD.ShowDialog();
            TboxSRC.Text = openFileD.FileName;
        }

        private void BTNSaveOut_Click(object sender, EventArgs e)
        {
            saveFileD.DefaultExt = "*.jts";
            saveFileD.Filter = "Huffman Stream files(*.jts)|*.jts|All files|*.*";
            saveFileD.CheckPathExists = false;
            saveFileD.ShowHelp = false;
            saveFileD.Title = "Select output file";
            saveFileD.ShowDialog();
            TboxOut.Text = saveFileD.FileName;
        }

        private void BTNshrink_Click(object sender, EventArgs e)
        {
            if (!IsSourceAndOutputOK()) return;
            FileStream S = new FileStream(TboxSRC.Text, FileMode.Open);
            //StreamReader streamReader = new StreamReader(TboxSRC.Text, Encoding.UTF8);
            //string s = streamReader.ReadToEnd();
            //streamReader.Close();
            //var input = text;
            var reader = new StreamReader(S, Encoding.UTF8);
            
                string s = reader.ReadToEnd();
                // Do something with the value
            
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
                   sb.AppendFormat("{0}{1}", count, current);
                   count = 1;
                   current = s[i];
               }
           }
           sb.AppendFormat("{0}{1}",count , current);
            //Convert.ToString((char)count, 2).PadLeft(8, '0')
           string str = sb.ToString();
           byte[] byteArray = new byte[str.Length];
           for (int i = 0; i < str.Length; i++)
           {
               byteArray[i] = (byte)str[i];
           }
           MemoryStream stream = new MemoryStream(byteArray);
            AL.ShrinkWithProgress(stream, TboxOut.Text);
            MessageBox.Show("File was Encoded successfully.", "Done Shrinking", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            S.Close();
            ProgBar.Value = 0;
        }

        private bool IsSourceAndOutputOK()
        {
            bool test = true;
            if (Path.GetFileName(TboxOut.Text).Length == 0)
            {
                MessageBox.Show("Invalid output file path", "Output file error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                test = false;
            }
            if (!File.Exists(TboxSRC.Text))
            {
                MessageBox.Show("Invalid source file path", "Source file error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                test = false;
            }
            return test;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //AL.PercentCompleted += new PercentCompletedEventHandler(AL_PercentCompleted);
            //label1.Text = AL.jts.ToString();
        }
        private void AL_PercentCompleted()
        {
            ProgBar.PerformStep();
        }

        private void BTNdecode_Click(object sender, EventArgs e)
        {
            //if (!IsSourceAndOutputOK()) return;
            //FileStream S = new FileStream(TboxSRC.Text, FileMode.Open);
            //if (!AL.IsArchivedStream(S))
            //{
            //    MessageBox.Show("Source file isn't HuffmanStream archived file or corrupted.",
            //        "Bad source file", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    S.Close();
            //    return;
            //}
            //bool IsOk;
            //IsOk = AL.ExtractWithProgress(S, TboxOut.Text);

            //if (IsOk) MessageBox.Show("File was extracted successfully.", "Done extracting", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //ProgBar.Value = 0;
            //S.Close();
        }
    }
}
