using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace WindowsFormsApplication4
{
    public partial class Form1 : Form
    {
        private OpenFileDialog openFileD;
        private SaveFileDialog saveFileD;
        private BinaryFormatter BinFormat = new BinaryFormatter();
        public Form1()
        {
            InitializeComponent();
            this.openFileD = new System.Windows.Forms.OpenFileDialog();
            this.saveFileD = new System.Windows.Forms.SaveFileDialog();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            openFileD.DefaultExt = "*.jts";
            openFileD.Filter = "Huffman Stream files(*.jts)|*.jts|All files(*.*)|*.*";
            openFileD.Multiselect = false;
            openFileD.CheckPathExists = true;
            openFileD.ShowHelp = false;
            openFileD.Title = "Select source file";
            openFileD.ShowReadOnly = true;
            openFileD.ShowDialog();
            txtBoxOpenFile.Text = openFileD.FileName;
        }

        private void btnSaveFile_Click(object sender, EventArgs e)
        {
            saveFileD.DefaultExt = "*.jts";
            saveFileD.Filter = "Huffman Stream files(*.jts)|*.jts|All files|*.*";
            saveFileD.CheckPathExists = false;
            saveFileD.ShowHelp = false;
            saveFileD.Title = "Select output file";
            saveFileD.ShowDialog();
            txtBoxSaveFile.Text = saveFileD.FileName;
        }
        private bool IsSourceAndOutputOK()
        {
            bool test = true;
            if (Path.GetFileName(txtBoxSaveFile.Text).Length == 0)
            {
                MessageBox.Show("Invalid output file path", "Output file error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                test = false;
            }
            if (!File.Exists(txtBoxOpenFile.Text))
            {
                MessageBox.Show("Invalid source file path", "Source file error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                test = false;
            }
            return test;
        }

        private void btnEncode_Click(object sender, EventArgs e)
        {

            //StreamReader streamReader = new StreamReader(txtBoxOpenFile.Text, Encoding.UTF8);
            //string text = streamReader.ReadToEnd();
            //streamReader.Close();
            //var input = text;

            FileStream S = new FileStream(txtBoxOpenFile.Text, FileMode.Open);
            var reader = new StreamReader(S, Encoding.UTF8);
            string s = reader.ReadToEnd();
            //if (!IsSourceAndOutputOK()) return;
            //FileStream S = new FileStream(txtBoxOpenFile.Text, FileMode.Open);
            Run_Length p = new Run_Length();
            p.Encode(s,txtBoxSaveFile.Text);
            //MessageBox.Show(p.Encode(input));
            //string str = p.Encode(input);
            //byte[] byteArray = new byte[str.Length];

            //for (int i = 0; i < str.Length; i++)
            //{
            //    byteArray[i] = (byte)str[i];
            //}

            //MemoryStream stream = new MemoryStream(byteArray);
            //FileStream tempFS = new FileStream(txtBoxSaveFile.Text, FileMode.Create);
            //for (long i = 0; i < stream.Length; ++i)
            //{
            //    tempFS.WriteByte((Byte)stream.ReadByte());
            //}
            //BinFormat.Serialize(tempFS, 0);
            S.Close();
            MessageBox.Show(" Encoded Successfully");
        }
    }
}
