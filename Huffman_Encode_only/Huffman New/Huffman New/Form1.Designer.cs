namespace Huffman_New
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.BTNopenSRC = new System.Windows.Forms.Button();
            this.BTNSaveOut = new System.Windows.Forms.Button();
            this.BTNshrink = new System.Windows.Forms.Button();
            this.TboxSRC = new System.Windows.Forms.TextBox();
            this.TboxOut = new System.Windows.Forms.TextBox();
            this.ProgBar = new System.Windows.Forms.ProgressBar();
            this.lbl_Compression = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BTNopenSRC
            // 
            this.BTNopenSRC.Location = new System.Drawing.Point(348, 56);
            this.BTNopenSRC.Name = "BTNopenSRC";
            this.BTNopenSRC.Size = new System.Drawing.Size(75, 23);
            this.BTNopenSRC.TabIndex = 0;
            this.BTNopenSRC.Text = "Open File";
            this.BTNopenSRC.UseVisualStyleBackColor = true;
            this.BTNopenSRC.Click += new System.EventHandler(this.BTNopenSRC_Click);
            // 
            // BTNSaveOut
            // 
            this.BTNSaveOut.Location = new System.Drawing.Point(348, 101);
            this.BTNSaveOut.Name = "BTNSaveOut";
            this.BTNSaveOut.Size = new System.Drawing.Size(75, 23);
            this.BTNSaveOut.TabIndex = 0;
            this.BTNSaveOut.Text = "Save To";
            this.BTNSaveOut.UseVisualStyleBackColor = true;
            this.BTNSaveOut.Click += new System.EventHandler(this.BTNSaveOut_Click);
            // 
            // BTNshrink
            // 
            this.BTNshrink.Location = new System.Drawing.Point(348, 146);
            this.BTNshrink.Name = "BTNshrink";
            this.BTNshrink.Size = new System.Drawing.Size(75, 23);
            this.BTNshrink.TabIndex = 0;
            this.BTNshrink.Text = "Compress";
            this.BTNshrink.UseVisualStyleBackColor = true;
            this.BTNshrink.Click += new System.EventHandler(this.BTNshrink_Click);
            // 
            // TboxSRC
            // 
            this.TboxSRC.Location = new System.Drawing.Point(41, 58);
            this.TboxSRC.Name = "TboxSRC";
            this.TboxSRC.Size = new System.Drawing.Size(271, 20);
            this.TboxSRC.TabIndex = 1;
            // 
            // TboxOut
            // 
            this.TboxOut.Location = new System.Drawing.Point(41, 104);
            this.TboxOut.Name = "TboxOut";
            this.TboxOut.Size = new System.Drawing.Size(271, 20);
            this.TboxOut.TabIndex = 1;
            // 
            // ProgBar
            // 
            this.ProgBar.Location = new System.Drawing.Point(41, 205);
            this.ProgBar.Name = "ProgBar";
            this.ProgBar.Size = new System.Drawing.Size(382, 23);
            this.ProgBar.TabIndex = 2;
            // 
            // lbl_Compression
            // 
            this.lbl_Compression.AutoSize = true;
            this.lbl_Compression.Location = new System.Drawing.Point(119, 248);
            this.lbl_Compression.Name = "lbl_Compression";
            this.lbl_Compression.Size = new System.Drawing.Size(0, 13);
            this.lbl_Compression.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 146);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Orginal File Size: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(41, 163);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Encode File Size: ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(41, 180);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Ratio : ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Green;
            this.label4.Location = new System.Drawing.Point(44, 248);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(401, 29);
            this.label4.TabIndex = 7;
            this.label4.Text = "Add a File And save serialize file.";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(486, 294);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbl_Compression);
            this.Controls.Add(this.ProgBar);
            this.Controls.Add(this.TboxOut);
            this.Controls.Add(this.TboxSRC);
            this.Controls.Add(this.BTNshrink);
            this.Controls.Add(this.BTNSaveOut);
            this.Controls.Add(this.BTNopenSRC);
            this.Name = "Form1";
            this.Text = "Huffman";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BTNopenSRC;
        private System.Windows.Forms.Button BTNSaveOut;
        private System.Windows.Forms.Button BTNshrink;
        private System.Windows.Forms.TextBox TboxSRC;
        private System.Windows.Forms.TextBox TboxOut;
        private System.Windows.Forms.ProgressBar ProgBar;
        private System.Windows.Forms.Label lbl_Compression;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}

