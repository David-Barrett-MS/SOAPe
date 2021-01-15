using System.Drawing;
using System.Windows.Forms;

namespace SOAPe
{
    public class GroupBoxHighlight: GroupBox
    {
        private Color _highlightColour = Color.Red;
        private Color _baseColour;
        private bool _highlighted = false;
        private Font _baseFont;

        public GroupBoxHighlight()
        {
            _baseFont = this.Font;
            _baseColour = this.ForeColor;
        }

        public Color HighlightColour
        {
            get { return _highlightColour; }
            set
            {
                _highlightColour = value;
                this.Refresh();
            }
        }

        public bool Highlighted
        {
            get { return _highlighted; }
            set
            {
                _highlighted = value;
                this.Refresh();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (!_highlighted)
            {
                base.OnPaint(e);
                return;
            }

            Font highlightFont = new Font(_baseFont, FontStyle.Bold);

            Size tSize = TextRenderer.MeasureText(this.Text, highlightFont);
            Rectangle borderRect = e.ClipRectangle;
            borderRect.Y += tSize.Height / 2;
            borderRect.Height -= tSize.Height / 2;
            ControlPaint.DrawBorder(e.Graphics, borderRect, _highlightColour, 2, ButtonBorderStyle.Solid, _highlightColour, 2, ButtonBorderStyle.Solid, _highlightColour, 2, ButtonBorderStyle.Solid, _highlightColour, 2, ButtonBorderStyle.Solid);

            Rectangle textRect = e.ClipRectangle;
            textRect.X += 6;
            textRect.Width = tSize.Width;
            textRect.Height = tSize.Height;
            e.Graphics.FillRectangle(new SolidBrush(this.BackColor), textRect);
            e.Graphics.DrawString(this.Text, highlightFont, new SolidBrush(_highlightColour), textRect);
        }
    }
}
