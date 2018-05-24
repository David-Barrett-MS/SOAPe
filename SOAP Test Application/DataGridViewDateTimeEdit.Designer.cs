namespace SOAPe
{
    partial class DataGridViewDateTimeEdit
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dateTimeEdit1 = new SOAPe.DateTimeEdit();
            this.SuspendLayout();
            // 
            // dateTimeEdit1
            // 
            this.dateTimeEdit1.CalendarForeColor = System.Drawing.SystemColors.ControlText;
            this.dateTimeEdit1.CalendarMonthBackground = System.Drawing.SystemColors.Window;
            this.dateTimeEdit1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dateTimeEdit1.Location = new System.Drawing.Point(0, 0);
            this.dateTimeEdit1.MinimumSize = new System.Drawing.Size(150, 20);
            this.dateTimeEdit1.Name = "dateTimeEdit1";
            this.dateTimeEdit1.Size = new System.Drawing.Size(186, 20);
            this.dateTimeEdit1.TabIndex = 0;
            this.dateTimeEdit1.Value = new System.DateTime(2015, 9, 29, 9, 14, 13, 0);
            // 
            // DataGridViewDateTimeCell
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dateTimeEdit1);
            this.Name = "DataGridViewDateTimeCell";
            this.Size = new System.Drawing.Size(186, 20);
            this.ResumeLayout(false);

        }

        #endregion

        private DateTimeEdit dateTimeEdit1;
    }
}
