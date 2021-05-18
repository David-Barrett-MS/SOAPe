/*
 * By David Barrett, Microsoft Ltd.  Use at your own risk.  No warranties are given.
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
    public partial class DataGridViewDateTimeEdit : UserControl, IDataGridViewEditingControl
    {
        DataGridView _dataGridView = null;
        private bool _valueChanged = false;
        int _rowIndex;
        private string _dateFormat = "dd/mm/yyyy hh:mm:ss";

        public DataGridViewDateTimeEdit()
        {
            InitializeComponent();
            dateTimeEdit1.ValueChanged += dateTimeEdit1_ValueChanged;
            dateTimeEdit1.FinishEdit += dateTimeEdit1_FinishEdit;
        }

        void dateTimeEdit1_FinishEdit(object sender, EventArgs e)
        {
            if (_dataGridView != null)
            {
                this.EditingControlDataGridView.EndEdit();
            }
        }

        void dateTimeEdit1_ValueChanged(object sender, EventArgs e)
        {
            // Notify the DataGridView that the contents of the cell
            // have changed.
            if (!_valueChanged)
            {
                _valueChanged = true;
                this.EditingControlDataGridView.NotifyCurrentCellDirty(true);
            }
        }

        public DateTime Value
        {
            get
            {
                return dateTimeEdit1.Value;
            }
            set
            {
                dateTimeEdit1.Value = value;
            }
        }

        public string DateFormat
        {
            get
            {
                return _dateFormat;
            }
            set
            {
                _dateFormat = value;
            }
        }
        // Implements the IDataGridViewEditingControl.EditingControlFormattedValue 
        // property.
        public object EditingControlFormattedValue
        {
            get
            {
                return this.Value.ToString(_dateFormat);// (this.CustomFormat);
            }
            set
            {
                try
                {
                    String newValue = value as String;
                    if (newValue != null)
                    {
                        this.Value = DateTime.Parse(newValue);
                    }
                }
                catch { }
            }
        }

        // Implements the 
        // IDataGridViewEditingControl.GetEditingControlFormattedValue method.

        public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
        {
            return EditingControlFormattedValue;
        }

        // Implements the 
        // IDataGridViewEditingControl.ApplyCellStyleToEditingControl method.

        public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
        {
            this.Font = dataGridViewCellStyle.Font;
            dateTimeEdit1.CalendarForeColor = dataGridViewCellStyle.ForeColor;
            dateTimeEdit1.CalendarMonthBackground = dataGridViewCellStyle.BackColor;
        }

        // Implements the IDataGridViewEditingControl.EditingControlRowIndex 
        // property.

        public int EditingControlRowIndex
        {
            get
            {
                return _rowIndex;
            }
            set
            {
                _rowIndex = value;
            }

        }

        // Implements the IDataGridViewEditingControl.EditingControlWantsInputKey 
        // method.

        public bool EditingControlWantsInputKey(Keys key, bool dataGridViewWantsInputKey)
        {
            // Let the DateTimePicker handle the keys listed.
            switch (key & Keys.KeyCode)
            {
                case Keys.Left:
                case Keys.Up:
                case Keys.Down:
                case Keys.Right:
                case Keys.Home:
                case Keys.End:
                case Keys.PageDown:
                case Keys.PageUp:
                case Keys.Tab:
                    return true;

                default:
                    return false;
            }
        }

        // Implements the IDataGridViewEditingControl.PrepareEditingControlForEdit 
        // method.
        public void PrepareEditingControlForEdit(bool selectAll)
        {
            // No preparation needs to be done.
        }

        // Implements the IDataGridViewEditingControl
        // .RepositionEditingControlOnValueChange property.
        public bool RepositionEditingControlOnValueChange
        {
            get
            {
                return false;
            }
        }

        // Implements the IDataGridViewEditingControl
        // .EditingControlDataGridView property.
        public DataGridView EditingControlDataGridView
        {
            get
            {
                return _dataGridView;
            }
            set
            {
                _dataGridView = value;
            }
        }

        // Implements the IDataGridViewEditingControl
        // .EditingControlValueChanged property.
        public bool EditingControlValueChanged
        {
            get
            {
                return _valueChanged;
            }
            set
            {
                _valueChanged = value;
            }
        }

        // Implements the IDataGridViewEditingControl
        // .EditingPanelCursor property.
        public Cursor EditingPanelCursor
        {
            get
            {
                return base.Cursor;
            }
        }

    }



    public class DataGridViewDateTimeCell : DataGridViewTextBoxCell
    {
        public DataGridViewDateTimeCell()
            : base()
        {
            // Use the short date format.
            this.Style.Format = "dd MMM yyyy HH:mm:ss";
        }

        public DataGridViewDateTimeCell(string frm)
            : base()
        {
            // Use the custom format.
            this.Style.Format = frm;
        }

        public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            // Set the value of the editing control to the current cell value.
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
            SOAPe.DataGridViewDateTimeEdit ctl = DataGridView.EditingControl as SOAPe.DataGridViewDateTimeEdit;
            ctl.Value = (DateTime)this.Value;
            ctl.DateFormat = this.Style.Format;
        }

        public override Type EditType
        {
            get
            {
                // Return the type of the editing contol that CalendarCell uses.
                return typeof(SOAPe.DataGridViewDateTimeEdit);
            }
        }

        public override Type ValueType
        {
            get
            {
                // Return the type of the value that CalendarCell contains.
                return typeof(DateTime);
            }
        }

        public override object DefaultNewRowValue
        {
            get
            {
                // Use the current date and time as the default value.
                return DateTime.Now;
            }
        }
    }
}
