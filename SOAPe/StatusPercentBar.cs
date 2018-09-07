/*
 * By David Barrett, Dark Bytes Ltd. 2016. Use at your own risk.  No warranties are given.
 * 
 * DISCLAIMER:
 * THIS CODE IS SAMPLE CODE. THESE SAMPLES ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND.
 * MICROSOFT FURTHER DISCLAIMS ALL IMPLIED WARRANTIES INCLUDING WITHOUT LIMITATION ANY IMPLIED WARRANTIES OF MERCHANTABILITY OR OF FITNESS FOR
 * A PARTICULAR PURPOSE. THE ENTIRE RISK ARISING OUT OF THE USE OR PERFORMANCE OF THE SAMPLES REMAINS WITH YOU. IN NO EVENT SHALL
 * MICROSOFT OR ITS SUPPLIERS BE LIABLE FOR ANY DAMAGES WHATSOEVER (INCLUDING, WITHOUT LIMITATION, DAMAGES FOR LOSS OF BUSINESS PROFITS,
 * BUSINESS INTERRUPTION, LOSS OF BUSINESS INFORMATION, OR OTHER PECUNIARY LOSS) ARISING OUT OF THE USE OF OR INABILITY TO USE THE
 * SAMPLES, EVEN IF MICROSOFT HAS BEEN ADVISED OF THE POSSIBILITY OF SUCH DAMAGES. BECAUSE SOME STATES DO NOT ALLOW THE EXCLUSION OR LIMITATION
 * OF LIABILITY FOR CONSEQUENTIAL OR INCIDENTAL DAMAGES, THE ABOVE LIMITATION MAY NOT APPLY TO YOU.
 * */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;


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
