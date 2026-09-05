using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace CRM_App
{
    [ToolboxItem(true)]
    [DesignerCategory("Component")]
    public class RoundedButton : Button
    {
        private int _borderRadius = 8;
        private Color _buttonColor = Color.FromArgb(46, 139, 34);       // #2E8B22 primary green
        private Color _buttonColor2 = Color.FromArgb(35, 115, 25);     // subtle gradient bottom
        private Color _hoverColor = Color.FromArgb(56, 160, 42);        // slightly brighter on hover
        private Color _hoverColor2 = Color.FromArgb(42, 130, 30);
        private Color _pressedColor = Color.FromArgb(28, 95, 20);
        private Color _borderColor = Color.Transparent;
        private int _borderThickness = 0;
        private bool _isHovered = false;
        private bool _isPressed = false;
        private bool _showLoginIcon = false;

        [Category("Appearance")]
        [DefaultValue(8)]
        public int BorderRadius
        {
            get { return _borderRadius; }
            set { _borderRadius = Math.Max(0, value); Invalidate(); }
        }

        [Category("Appearance")]
        public Color ButtonColor
        {
            get { return _buttonColor; }
            set { _buttonColor = value; Invalidate(); }
        }

        [Category("Appearance")]
        public Color ButtonColor2
        {
            get { return _buttonColor2; }
            set { _buttonColor2 = value; Invalidate(); }
        }

        [Category("Appearance")]
        public Color HoverColor
        {
            get { return _hoverColor; }
            set { _hoverColor = value; Invalidate(); }
        }

        [Category("Appearance")]
        public Color HoverColor2
        {
            get { return _hoverColor2; }
            set { _hoverColor2 = value; Invalidate(); }
        }

        [Category("Appearance")]
        public Color PressedColor
        {
            get { return _pressedColor; }
            set { _pressedColor = value; Invalidate(); }
        }

        [Category("Appearance")]
        public Color BorderColor
        {
            get { return _borderColor; }
            set { _borderColor = value; Invalidate(); }
        }

        [Category("Appearance")]
        [DefaultValue(0)]
        public int BorderThickness
        {
            get { return _borderThickness; }
            set { _borderThickness = value; Invalidate(); }
        }

        [Category("Appearance")]
        [DefaultValue(false)]
        public bool ShowLoginIcon
        {
            get { return _showLoginIcon; }
            set { _showLoginIcon = value; Invalidate(); }
        }

        public RoundedButton()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.SupportsTransparentBackColor, true);

            BackColor = Color.Transparent;
            ForeColor = Color.White;
            Font = new Font("Segoe UI", 11f, FontStyle.Bold);
            Size = new Size(125, 42);
            Cursor = Cursors.Hand;
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            _isHovered = true;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _isHovered = false;
            _isPressed = false;
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                _isPressed = true;
                Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            _isPressed = false;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);

            Color topColor;
            Color btmColor;

            if (_isPressed)
            {
                topColor = _pressedColor;
                btmColor = _pressedColor;
            }
            else if (_isHovered)
            {
                topColor = _hoverColor;
                btmColor = _hoverColor2;
            }
            else
            {
                topColor = _buttonColor;
                btmColor = _buttonColor2;
            }

            using (GraphicsPath path = VectorIcons.CreateRoundedRectPath(rect, _borderRadius))
            {
                // Background fill
                if (topColor == btmColor || rect.Height <= 0)
                {
                    using (SolidBrush b = new SolidBrush(topColor))
                    {
                        g.FillPath(b, path);
                    }
                }
                else
                {
                    using (LinearGradientBrush lgb = new LinearGradientBrush(
                        new Point(0, 0), new Point(0, Height), topColor, btmColor))
                    {
                        g.FillPath(lgb, path);
                    }
                }

                // Border if specified
                if (_borderThickness > 0 && _borderColor != Color.Transparent)
                {
                    using (Pen p = new Pen(_borderColor, _borderThickness))
                    {
                        g.DrawPath(p, path);
                    }
                }
            }

            // Draw Icon + Text
            int iconWidth = _showLoginIcon ? 20 : 0;
            int spacing = _showLoginIcon ? 8 : 0;
            SizeF textSize = g.MeasureString(Text, Font);

            int totalContentWidth = iconWidth + spacing + (int)textSize.Width;
            int startX = (Width - totalContentWidth) / 2;

            if (_showLoginIcon)
            {
                int iconH = 18;
                int iconY = (Height - iconH) / 2;
                Rectangle iconRect = new Rectangle(startX, iconY, iconWidth, iconH);
                VectorIcons.DrawLoginArrow(g, iconRect, ForeColor, 2.0f);
            }

            int textX = startX + iconWidth + spacing;
            int textY = (Height - (int)textSize.Height) / 2;

            using (SolidBrush textBrush = new SolidBrush(ForeColor))
            {
                g.DrawString(Text, Font, textBrush, textX, textY);
            }
        }
    }
}

namespace CRM_App.Controls
{
    // Alias to support CRM_App.Controls.RoundedButton seamlessly
    [ToolboxItem(true)]
    [DesignerCategory("Component")]
    public class RoundedButton : CRM_App.RoundedButton
    {
    }
}
