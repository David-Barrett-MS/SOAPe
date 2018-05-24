namespace SOAPe
{
    partial class DateTimeEdit
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
            this.dateTimePickerTime = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerDate = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // dateTimePickerTime
            // 
            this.dateTimePickerTime.Dock = System.Windows.Forms.DockStyle.Right;
            this.dateTimePickerTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePickerTime.Location = new System.Drawing.Point(113, 0);
            this.dateTimePickerTime.Name = "dateTimePickerTime";
            this.dateTimePickerTime.ShowUpDown = true;
            this.dateTimePickerTime.Size = new System.Drawing.Size(73, 20);
            this.dateTimePickerTime.TabIndex = 0;
            this.dateTimePickerTime.ValueChanged += new System.EventHandler(this.DTValueChanged);
            // 
            // dateTimePickerDate
            // 
            this.dateTimePickerDate.Dock = System.Windows.Forms.DockStyle.Left;
            this.dateTimePickerDate.Location = new System.Drawing.Point(0, 0);
            this.dateTimePickerDate.Name = "dateTimePickerDate";
            this.dateTimePickerDate.Size = new System.Drawing.Size(114, 20);
            this.dateTimePickerDate.TabIndex = 1;
            this.dateTimePickerDate.ValueChanged += new System.EventHandler(this.DTValueChanged);
            // 
            // DateTimeEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dateTimePickerDate);
            this.Controls.Add(this.dateTimePickerTime);
            this.MinimumSize = new System.Drawing.Size(150, 20);
            this.Name = "DateTimeEdit";
            this.Size = new System.Drawing.Size(186, 20);
            this.Load += new System.EventHandler(this.DateTimeEdit_Load);
            this.SizeChanged += new System.EventHandler(this.DateTimeEdit_SizeChanged);
            this.Leave += new System.EventHandler(this.DateTimeEdit_Leave);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePickerTime;
        private System.Windows.Forms.DateTimePicker dateTimePickerDate;

    }
}
