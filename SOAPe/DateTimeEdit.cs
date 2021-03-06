﻿/*
 * By David Barrett, Microsoft Ltd. Use at your own risk.  No warranties are given.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 * */

using System;
using System.Windows.Forms;

namespace SOAPe
{
    public partial class DateTimeEdit : UserControl
    {
        public event EventHandler ValueChanged;
        public event EventHandler FinishEdit;
        private string _format = "dd/mm/yyyy hh:mm:ss";
        public DateTimeEdit()
        {
            InitializeComponent();
            UpdateSize();
        }

        protected virtual void OnValueChanged(EventArgs e)
        {
            EventHandler handler = this.ValueChanged;
            if (handler != null)
                handler(this, e);
        }

        private void DTValueChanged(object sender, EventArgs e)
        {
            this.OnValueChanged(EventArgs.Empty);
        }

        protected virtual void OnFinishEdit(EventArgs e)
        {
            EventHandler handler = this.FinishEdit;
            if (handler != null)
                handler(this, e);
        }

        public DateTime Value
        {
            get
            {
                return new DateTime(dateTimePickerDate.Value.Year, dateTimePickerDate.Value.Month, dateTimePickerDate.Value.Day,
                    dateTimePickerTime.Value.Hour, dateTimePickerTime.Value.Minute, dateTimePickerTime.Value.Second);
            }
            set
            {
                dateTimePickerDate.Value = value;
                dateTimePickerTime.Value = value;
            }
        }

        public string DateFormat
        {
            get
            {
                return _format;
            }
            set
            {
                _format = value;
            }
        }

        public System.Drawing.Color CalendarForeColor
        {
            get { return dateTimePickerDate.CalendarForeColor; }
            set
            {
                dateTimePickerDate.CalendarForeColor = value;
                dateTimePickerTime.CalendarForeColor = value;
            }
        }

        public System.Drawing.Color CalendarMonthBackground
        {
            get { return dateTimePickerDate.CalendarMonthBackground; }
            set
            {
                dateTimePickerDate.CalendarMonthBackground = value;
                dateTimePickerTime.CalendarMonthBackground = value;
            }
        }

        private void DateTimeEdit_Load(object sender, EventArgs e)
        {

        }

        private void DateTimeEdit_SizeChanged(object sender, EventArgs e)
        {
            UpdateSize();
        }

        private void UpdateSize()
        {
            int iWidth = this.Width / 2;
            dateTimePickerDate.Width = iWidth;
            dateTimePickerTime.Left = dateTimePickerDate.Right;
            dateTimePickerTime.Width = iWidth;
        }


        private void DateTimeEdit_Leave(object sender, EventArgs e)
        {
            this.OnFinishEdit(EventArgs.Empty);
        }

    }
}
