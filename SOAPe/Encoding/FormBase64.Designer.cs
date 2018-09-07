namespace SOAPe.Encoding
{
    partial class FormBase64
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
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonDecodeToText = new System.Windows.Forms.Button();
            this.buttonDecodeToFile = new System.Windows.Forms.Button();
            this.buttonEncodeText = new System.Windows.Forms.Button();
            this.buttonEncodeFile = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxBase64 = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBoxText = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radioButtonEncodingUTF8 = new System.Windows.Forms.RadioButton();
            this.radioButtonEncodingBinary = new System.Windows.Forms.RadioButton();
            this.radioButtonEncodingUnicode = new System.Windows.Forms.RadioButton();
            this.radioButtonEncodingASCII = new System.Windows.Forms.RadioButton();
            this.checkBoxLineLength = new System.Windows.Forms.CheckBox();
            this.numericUpDownLineLength = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLineLength)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(546, 495);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 6;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonDecodeToText
            // 
            this.buttonDecodeToText.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonDecodeToText.Location = new System.Drawing.Point(523, 213);
            this.buttonDecodeToText.Name = "buttonDecodeToText";
            this.buttonDecodeToText.Size = new System.Drawing.Size(98, 23);
            this.buttonDecodeToText.TabIndex = 2;
            this.buttonDecodeToText.Text = "Decode to text";
            this.buttonDecodeToText.UseVisualStyleBackColor = true;
            this.buttonDecodeToText.Click += new System.EventHandler(this.buttonDecodeToText_Click);
            // 
            // buttonDecodeToFile
            // 
            this.buttonDecodeToFile.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonDecodeToFile.Location = new System.Drawing.Point(523, 242);
            this.buttonDecodeToFile.Name = "buttonDecodeToFile";
            this.buttonDecodeToFile.Size = new System.Drawing.Size(98, 23);
            this.buttonDecodeToFile.TabIndex = 3;
            this.buttonDecodeToFile.Text = "Decode to file...";
            this.buttonDecodeToFile.UseVisualStyleBackColor = true;
            this.buttonDecodeToFile.Click += new System.EventHandler(this.buttonDecodeToFile_Click);
            // 
            // buttonEncodeText
            // 
            this.buttonEncodeText.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonEncodeText.Location = new System.Drawing.Point(523, 271);
            this.buttonEncodeText.Name = "buttonEncodeText";
            this.buttonEncodeText.Size = new System.Drawing.Size(98, 23);
            this.buttonEncodeText.TabIndex = 4;
            this.buttonEncodeText.Text = "Encode text";
            this.buttonEncodeText.UseVisualStyleBackColor = true;
            this.buttonEncodeText.Click += new System.EventHandler(this.buttonEncodeText_Click);
            // 
            // buttonEncodeFile
            // 
            this.buttonEncodeFile.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonEncodeFile.Location = new System.Drawing.Point(523, 300);
            this.buttonEncodeFile.Name = "buttonEncodeFile";
            this.buttonEncodeFile.Size = new System.Drawing.Size(98, 23);
            this.buttonEncodeFile.TabIndex = 5;
            this.buttonEncodeFile.Text = "Encode file...";
            this.buttonEncodeFile.UseVisualStyleBackColor = true;
            this.buttonEncodeFile.Click += new System.EventHandler(this.buttonEncodeFile_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 12);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(505, 506);
            this.splitContainer1.SplitterDistance = 253;
            this.splitContainer1.TabIndex = 7;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxBase64);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(505, 253);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Base 64";
            // 
            // textBoxBase64
            // 
            this.textBoxBase64.AllowDrop = true;
            this.textBoxBase64.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxBase64.Location = new System.Drawing.Point(3, 16);
            this.textBoxBase64.MaxLength = 0;
            this.textBoxBase64.Multiline = true;
            this.textBoxBase64.Name = "textBoxBase64";
            this.textBoxBase64.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxBase64.Size = new System.Drawing.Size(499, 234);
            this.textBoxBase64.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBoxText);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(505, 249);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Text";
            // 
            // textBoxText
            // 
            this.textBoxText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxText.Location = new System.Drawing.Point(3, 16);
            this.textBoxText.MaxLength = 0;
            this.textBoxText.Multiline = true;
            this.textBoxText.Name = "textBoxText";
            this.textBoxText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxText.Size = new System.Drawing.Size(499, 230);
            this.textBoxText.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.numericUpDownLineLength);
            this.groupBox3.Controls.Add(this.checkBoxLineLength);
            this.groupBox3.Controls.Add(this.radioButtonEncodingUTF8);
            this.groupBox3.Controls.Add(this.radioButtonEncodingBinary);
            this.groupBox3.Controls.Add(this.radioButtonEncodingUnicode);
            this.groupBox3.Controls.Add(this.radioButtonEncodingASCII);
            this.groupBox3.Location = new System.Drawing.Point(523, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(98, 160);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Encoding";
            // 
            // radioButtonEncodingUTF8
            // 
            this.radioButtonEncodingUTF8.AutoSize = true;
            this.radioButtonEncodingUTF8.Checked = true;
            this.radioButtonEncodingUTF8.Location = new System.Drawing.Point(6, 42);
            this.radioButtonEncodingUTF8.Name = "radioButtonEncodingUTF8";
            this.radioButtonEncodingUTF8.Size = new System.Drawing.Size(55, 17);
            this.radioButtonEncodingUTF8.TabIndex = 3;
            this.radioButtonEncodingUTF8.TabStop = true;
            this.radioButtonEncodingUTF8.Text = "UTF-8";
            this.radioButtonEncodingUTF8.UseVisualStyleBackColor = true;
            // 
            // radioButtonEncodingBinary
            // 
            this.radioButtonEncodingBinary.AutoSize = true;
            this.radioButtonEncodingBinary.Location = new System.Drawing.Point(6, 88);
            this.radioButtonEncodingBinary.Name = "radioButtonEncodingBinary";
            this.radioButtonEncodingBinary.Size = new System.Drawing.Size(54, 17);
            this.radioButtonEncodingBinary.TabIndex = 2;
            this.radioButtonEncodingBinary.TabStop = true;
            this.radioButtonEncodingBinary.Text = "Binary";
            this.radioButtonEncodingBinary.UseVisualStyleBackColor = true;
            // 
            // radioButtonEncodingUnicode
            // 
            this.radioButtonEncodingUnicode.AutoSize = true;
            this.radioButtonEncodingUnicode.Location = new System.Drawing.Point(6, 65);
            this.radioButtonEncodingUnicode.Name = "radioButtonEncodingUnicode";
            this.radioButtonEncodingUnicode.Size = new System.Drawing.Size(65, 17);
            this.radioButtonEncodingUnicode.TabIndex = 1;
            this.radioButtonEncodingUnicode.Text = "Unicode";
            this.radioButtonEncodingUnicode.UseVisualStyleBackColor = true;
            // 
            // radioButtonEncodingASCII
            // 
            this.radioButtonEncodingASCII.AutoSize = true;
            this.radioButtonEncodingASCII.Location = new System.Drawing.Point(6, 19);
            this.radioButtonEncodingASCII.Name = "radioButtonEncodingASCII";
            this.radioButtonEncodingASCII.Size = new System.Drawing.Size(52, 17);
            this.radioButtonEncodingASCII.TabIndex = 0;
            this.radioButtonEncodingASCII.Text = "ASCII";
            this.radioButtonEncodingASCII.UseVisualStyleBackColor = true;
            // 
            // checkBoxLineLength
            // 
            this.checkBoxLineLength.AutoSize = true;
            this.checkBoxLineLength.Location = new System.Drawing.Point(6, 111);
            this.checkBoxLineLength.Name = "checkBoxLineLength";
            this.checkBoxLineLength.Size = new System.Drawing.Size(81, 17);
            this.checkBoxLineLength.TabIndex = 4;
            this.checkBoxLineLength.Text = "Line length:";
            this.checkBoxLineLength.UseVisualStyleBackColor = true;
            this.checkBoxLineLength.CheckedChanged += new System.EventHandler(this.checkBoxLineLength_CheckedChanged);
            // 
            // numericUpDownLineLength
            // 
            this.numericUpDownLineLength.Enabled = false;
            this.numericUpDownLineLength.Location = new System.Drawing.Point(13, 134);
            this.numericUpDownLineLength.Maximum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.numericUpDownLineLength.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownLineLength.Name = "numericUpDownLineLength";
            this.numericUpDownLineLength.Size = new System.Drawing.Size(45, 20);
            this.numericUpDownLineLength.TabIndex = 5;
            this.numericUpDownLineLength.Value = new decimal(new int[] {
            78,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(63, 136);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "chars";
            // 
            // FormBase64
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 530);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.buttonEncodeFile);
            this.Controls.Add(this.buttonEncodeText);
            this.Controls.Add(this.buttonDecodeToFile);
            this.Controls.Add(this.buttonDecodeToText);
            this.Controls.Add(this.buttonClose);
            this.Name = "FormBase64";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Base64 Encoder/Decoder";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLineLength)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonDecodeToText;
        private System.Windows.Forms.Button buttonDecodeToFile;
        private System.Windows.Forms.Button buttonEncodeText;
        private System.Windows.Forms.Button buttonEncodeFile;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxBase64;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBoxText;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radioButtonEncodingUnicode;
        private System.Windows.Forms.RadioButton radioButtonEncodingASCII;
        private System.Windows.Forms.RadioButton radioButtonEncodingBinary;
        private System.Windows.Forms.RadioButton radioButtonEncodingUTF8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDownLineLength;
        private System.Windows.Forms.CheckBox checkBoxLineLength;
    }
}