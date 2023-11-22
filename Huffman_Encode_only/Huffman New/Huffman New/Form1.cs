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
            openFileD.DefaultExt = "*.txt";
            openFileD.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
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
            this.Text = "Processing.... Please Wait....";
            if (!IsSourceAndOutputOK()) return;
            FileStream S = new FileStream(TboxSRC.Text, FileMode.Open);
            if (AL.IsArchivedStream(S))
            {
                label3.Text += AL.GetFileSize(S);
            }
            else
            {
                Int32 En_Size = AL.ShrinkWithProgress(S, TboxOut.Text)/8;
                label1.Text += S.Length;
                label2.Text += En_Size;
                FileStream S2 = new FileStream(TboxOut.Text, FileMode.Open);
                Int32 OrginalSize=Convert.ToInt32(AL.GetFileSize(S2));
                label3.Text += ((OrginalSize - En_Size)*100)/OrginalSize+"%";
                S.Close();
            }
            ProgBar.Value = 100;
            this.Text="Done";
            label4.Text = "File Compressed succesfully......";

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
            AL.PercentCompleted += new PercentCompletedEventHandler(AL_PercentCompleted);
        }
        private void AL_PercentCompleted()
        {
            ProgBar.PerformStep();
        }
    }
}
