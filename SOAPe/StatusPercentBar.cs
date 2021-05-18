/*
 * By David Barrett, Dark Bytes Ltd. 2016. Use at your own risk.  No warranties are given.
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
using System.Drawing;
using System.Windows.Forms;


namespace SOAPe
{
    public partial class StatusPercentBar : UserControl
    {
        private double _percentComplete = 0;
        private string _status = String.Empty;
        //private Font _statusFont = new Font(FontFamily.GenericSansSerif, 8);
        private Color _barColour = Color.Green;
        private Brush _textColour = Brushes.Black;
        private int _lastBarWidth = 0;

        public StatusPercentBar()
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }


        public Color BarColour
        {
            get { return _barColour; }
            set 
            { 
                _barColour = value;
                this.Invalidate();
            }
        }

        public double PercentComplete
        {
            get { return _percentComplete; }
            set
            {
                if (value > 100)
                {
                    _percentComplete = 100;
                }
                else if (value < 0)
                {
                    _percentComplete = 0;
                }
                else
                    _percentComplete = value;
                if ((int)(((double)this.ClientRectangle.Width * _percentComplete) / 100) != _lastBarWidth)
                    this.Invalidate();
            }
        }

        public String Status
        {
            get { return _status; }
            set
            {
                if (value == _status)
                    return;
                _status = value;
                this.Invalidate();
            }
        }

        protected override void OnResize(EventArgs e)
        {
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                SolidBrush brush = new SolidBrush(_barColour);
                Rectangle rect = this.ClientRectangle;

                // Draw progress bar
                _lastBarWidth = (int)(((double)rect.Width * _percentComplete) / 100);
                rect.Width = _lastBarWidth;
                e.Graphics.FillRectangle(brush, rect);

                using (Font statusFont = new Font(FontFamily.GenericSansSerif, 8))
                {
                    if (!String.IsNullOrEmpty(_status))
                    {
                        SizeF len = e.Graphics.MeasureString(_status, statusFont);
                        Point location = new Point(Convert.ToInt32((this.Width - len.Width) / 2), Convert.ToInt32((this.Height - len.Height) / 2));
                        e.Graphics.DrawString(_status, statusFont, _textColour, location);
                    }
                }

                System.Windows.Forms.ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle, Color.Black,ButtonBorderStyle.Solid);

                brush.Dispose();
            }
            catch { }
        }


    }
}
