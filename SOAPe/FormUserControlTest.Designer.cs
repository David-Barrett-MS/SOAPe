namespace SOAPe
{
    partial class FormUserControlTest
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.statusPercentBar1 = new SOAPe.StatusPercentBar();
            this.dateTimeEdit1 = new SOAPe.DateTimeEdit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(567, 275);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(368, 26);
            this.textBox1.TabIndex = 1;
            // 
            // statusPercentBar1
            // 
            this.statusPercentBar1.BarColour = System.Drawing.Color.PaleGreen;
            this.statusPercentBar1.Location = new System.Drawing.Point(378, 131);
            this.statusPercentBar1.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.statusPercentBar1.Name = "statusPercentBar1";
            this.statusPercentBar1.PercentComplete = 50D;
            this.statusPercentBar1.Size = new System.Drawing.Size(360, 31);
            this.statusPercentBar1.Status = "fdsfs";
            this.statusPercentBar1.TabIndex = 10;
            // 
            // dateTimeEdit1
            // 
            this.dateTimeEdit1.CalendarForeColor = System.Drawing.SystemColors.ControlText;
            this.dateTimeEdit1.CalendarMonthBackground = System.Drawing.SystemColors.Window;
            this.dateTimeEdit1.DateFormat = "dd/mm/yyyy hh:mm:ss";
            this.dateTimeEdit1.Location = new System.Drawing.Point(567, 235);
            this.dateTimeEdit1.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.dateTimeEdit1.MinimumSize = new System.Drawing.Size(225, 31);
            this.dateTimeEdit1.Name = "dateTimeEdit1";
            this.dateTimeEdit1.Size = new System.Drawing.Size(370, 31);
            this.dateTimeEdit1.TabIndex = 0;
            this.dateTimeEdit1.Value = new System.DateTime(2014, 3, 13, 10, 40, 23, 0);
            this.dateTimeEdit1.ValueChanged += new System.EventHandler(this.dateTimeEdit1_ValueChanged);
            // 
            // FormUserControlTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1066, 387);
            this.Controls.Add(this.statusPercentBar1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.dateTimeEdit1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FormUserControlTest";
            this.Text = "FormUserControlTest";
            this.Load += new System.EventHandler(this.FormUserControlTest_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DateTimeEdit dateTimeEdit1;
        private System.Windows.Forms.TextBox textBox1;
        private StatusPercentBar statusPercentBar1;







    }
}