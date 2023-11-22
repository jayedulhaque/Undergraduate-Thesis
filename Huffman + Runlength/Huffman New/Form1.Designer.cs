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
            this.BTNdecode = new System.Windows.Forms.Button();
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
            this.BTNshrink.Text = "Encode";
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
            this.ProgBar.Location = new System.Drawing.Point(41, 245);
            this.ProgBar.Name = "ProgBar";
            this.ProgBar.Size = new System.Drawing.Size(382, 23);
            this.ProgBar.TabIndex = 2;
            // 
            // BTNdecode
            // 
            this.BTNdecode.Location = new System.Drawing.Point(348, 189);
            this.BTNdecode.Name = "BTNdecode";
            this.BTNdecode.Size = new System.Drawing.Size(75, 22);
            this.BTNdecode.TabIndex = 3;
            this.BTNdecode.Text = "Decode";
            this.BTNdecode.UseVisualStyleBackColor = true;
            this.BTNdecode.Click += new System.EventHandler(this.BTNdecode_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(486, 294);
            this.Controls.Add(this.BTNdecode);
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
        private System.Windows.Forms.Button BTNdecode;
    }
}

