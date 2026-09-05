using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace CRM_App
{
    public enum LeadingIconType
    {
        None,
        User,
        Lock
    }

    [ToolboxItem(true)]
    [DesignerCategory("UserControl")]
    public class RoundedTextBox : UserControl
    {
        private TextBox _innerTextBox;
        private int _borderRadius = 8;
        private Color _borderColor = Color.FromArgb(215, 220, 226); // #D7DCE2
        private Color _borderFocusColor = Color.FromArgb(46, 139, 34); // #2E8B22
        private int _borderThickness = 1;
        private bool _isFocused = false;
        private LeadingIconType _leadingIcon = LeadingIconType.None;
        private Color _leadingIconColor = Color.FromArgb(145, 155, 168);
        private bool _showPasswordToggle = false;
        private bool _isPasswordHidden = true;
        private Rectangle _toggleButtonRect;
        private bool _isToggleHovered = false;
        private string _placeholderText = "";

        public event EventHandler PasswordToggleClicked;

        [Category("Appearance")]
        [DefaultValue(8)]
        public int BorderRadius
        {
            get { return _borderRadius; }
            set { _borderRadius = Math.Max(0, value); Invalidate(); }
        }

        [Category("Appearance")]
        public Color BorderColor
        {
            get { return _borderColor; }
            set { _borderColor = value; Invalidate(); }
        }

        [Category("Appearance")]
        public Color BorderFocusColor
        {
            get { return _borderFocusColor; }
            set { _borderFocusColor = value; Invalidate(); }
        }

        [Category("Appearance")]
        [DefaultValue(1)]
        public int BorderThickness
        {
            get { return _borderThickness; }
            set { _borderThickness = value; Invalidate(); }
        }

        [Category("Appearance")]
        [DefaultValue(LeadingIconType.None)]
        public LeadingIconType LeadingIcon
        {
            get { return _leadingIcon; }
            set { _leadingIcon = value; UpdateInnerTextBoxBounds(); Invalidate(); }
        }

        [Category("Appearance")]
        public Color LeadingIconColor
        {
            get { return _leadingIconColor; }
            set { _leadingIconColor = value; Invalidate(); }
        }

        [Category("Behavior")]
        [DefaultValue(false)]
        public bool ShowPasswordToggle
        {
            get { return _showPasswordToggle; }
            set { _showPasswordToggle = value; UpdateInnerTextBoxBounds(); Invalidate(); }
        }

        [Category("Behavior")]
        [DefaultValue(false)]
        public bool UseSystemPasswordChar
        {
            get { return _innerTextBox != null ? _innerTextBox.UseSystemPasswordChar : _isPasswordHidden; }
            set
            {
                if (_innerTextBox != null)
                {
                    _innerTextBox.UseSystemPasswordChar = value;
                }
                _isPasswordHidden = value;
                Invalidate();
            }
        }

        [Category("Appearance")]
        [DefaultValue("")]
        public string PlaceholderText
        {
            get { return _placeholderText; }
            set { _placeholderText = value; Invalidate(); }
        }

        [Category("Appearance")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text
        {
            get { return _innerTextBox != null ? _innerTextBox.Text : ""; }
            set
            {
                if (_innerTextBox != null)
                {
                    _innerTextBox.Text = value;
                    Invalidate();
                }
            }
        }

        [Category("Appearance")]
        public override Font Font
        {
            get { return base.Font; }
            set
            {
                base.Font = value;
                if (_innerTextBox != null)
                {
                    _innerTextBox.Font = value;
                    UpdateInnerTextBoxBounds();
                }
            }
        }

        [Category("Appearance")]
        public override Color ForeColor
        {
            get { return base.ForeColor; }
            set
            {
                base.ForeColor = value;
                if (_innerTextBox != null)
                    _innerTextBox.ForeColor = value;
            }
        }

        public TextBox InnerTextBox
        {
            get { return _innerTextBox; }
        }

        public RoundedTextBox()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.SupportsTransparentBackColor, true);

            BackColor = Color.White;
            ForeColor = Color.FromArgb(40, 49, 59);
            Font = new Font("Segoe UI", 10.5f, FontStyle.Regular);
            Size = new Size(245, 38);
            Padding = new Padding(8);

            _innerTextBox = new TextBox();
            _innerTextBox.BorderStyle = BorderStyle.None;
            _innerTextBox.BackColor = Color.White;
            _innerTextBox.ForeColor = ForeColor;
            _innerTextBox.Font = Font;

            _innerTextBox.GotFocus += InnerTextBox_GotFocus;
            _innerTextBox.LostFocus += InnerTextBox_LostFocus;
            _innerTextBox.TextChanged += InnerTextBox_TextChanged;
            _innerTextBox.KeyDown += InnerTextBox_KeyDown;
            _innerTextBox.KeyPress += InnerTextBox_KeyPress;
            _innerTextBox.KeyUp += InnerTextBox_KeyUp;

            Controls.Add(_innerTextBox);
            UpdateInnerTextBoxBounds();
        }

        private void InnerTextBox_GotFocus(object sender, EventArgs e)
        {
            _isFocused = true;
            Invalidate();
        }

        private void InnerTextBox_LostFocus(object sender, EventArgs e)
        {
            _isFocused = false;
            Invalidate();
        }

        private void InnerTextBox_TextChanged(object sender, EventArgs e)
        {
            OnTextChanged(e);
            Invalidate();
        }

        private void InnerTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            OnKeyDown(e);
        }

        private void InnerTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            OnKeyPress(e);
        }

        private void InnerTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            OnKeyUp(e);
        }

        private void UpdateInnerTextBoxBounds()
        {
            if (_innerTextBox == null) return;

            int leftPadding = (_leadingIcon != LeadingIconType.None) ? 38 : 12;
            int rightPadding = _showPasswordToggle ? 38 : 12;

            int tbHeight = _innerTextBox.PreferredHeight;
            int tbY = (Height - tbHeight) / 2;
            int tbWidth = Math.Max(20, Width - leftPadding - rightPadding);

            _innerTextBox.Location = new Point(leftPadding, tbY);
            _innerTextBox.Width = tbWidth;

            // Toggle button area
            _toggleButtonRect = new Rectangle(Width - 36, (Height - 26) / 2, 28, 26);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateInnerTextBoxBounds();
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            _innerTextBox.Focus();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (_showPasswordToggle)
            {
                bool wasHovered = _isToggleHovered;
                _isToggleHovered = _toggleButtonRect.Contains(e.Location);
                if (_isToggleHovered != wasHovered)
                {
                    Cursor = _isToggleHovered ? Cursors.Hand : Cursors.IBeam;
                    Invalidate(_toggleButtonRect);
                }
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (_isToggleHovered)
            {
                _isToggleHovered = false;
                Cursor = Cursors.Default;
                Invalidate(_toggleButtonRect);
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (_showPasswordToggle && _toggleButtonRect.Contains(e.Location))
            {
                UseSystemPasswordChar = !UseSystemPasswordChar;
                if (PasswordToggleClicked != null)
                {
                    PasswordToggleClicked(this, EventArgs.Empty);
                }
                _innerTextBox.Focus();
                _innerTextBox.SelectionStart = _innerTextBox.Text.Length;
            }
            else
            {
                _innerTextBox.Focus();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            Rectangle rect = new Rectangle(0, 0, Width - 1, Height - 1);
            Color currentBorderColor = _isFocused ? _borderFocusColor : _borderColor;

            using (GraphicsPath path = VectorIcons.CreateRoundedRectPath(rect, _borderRadius))
            {
                // Background
                using (SolidBrush bgBrush = new SolidBrush(BackColor))
                {
                    g.FillPath(bgBrush, path);
                }

                // Border
                using (Pen pen = new Pen(currentBorderColor, _isFocused ? 1.8f : _borderThickness))
                {
                    g.DrawPath(pen, path);
                }
            }

            // Draw Leading Icon
            if (_leadingIcon != LeadingIconType.None)
            {
                Rectangle iconRect = new Rectangle(12, (Height - 20) / 2, 18, 20);
                if (_leadingIcon == LeadingIconType.User)
                {
                    VectorIcons.DrawUserOutline(g, iconRect, _leadingIconColor, 1.6f);
                }
                else if (_leadingIcon == LeadingIconType.Lock)
                {
                    VectorIcons.DrawLockOutline(g, iconRect, _leadingIconColor, 1.6f);
                }
            }

            // Draw Trailing Password Eye Toggle Icon
            if (_showPasswordToggle)
            {
                Rectangle eyeRect = new Rectangle(Width - 32, (Height - 18) / 2, 20, 18);
                Color eyeColor = _isToggleHovered ? Color.FromArgb(46, 139, 34) : Color.FromArgb(120, 130, 142);
                VectorIcons.DrawEyeIcon(g, eyeRect, eyeColor, _isPasswordHidden, 1.6f);
            }

            // Placeholder text if empty and not focused
            if (string.IsNullOrEmpty(_innerTextBox.Text) && !string.IsNullOrEmpty(_placeholderText) && !_isFocused)
            {
                using (SolidBrush placeholderBrush = new SolidBrush(Color.FromArgb(160, 170, 185)))
                {
                    g.DrawString(_placeholderText, Font, placeholderBrush, _innerTextBox.Location.X + 2, _innerTextBox.Location.Y);
                }
            }
        }
    }
}

namespace CRM_App.Controls
{
    // Alias to support CRM_App.Controls.RoundedTextBox seamlessly
    [ToolboxItem(true)]
    [DesignerCategory("UserControl")]
    public class RoundedTextBox : CRM_App.RoundedTextBox
    {
    }

    public enum LeadingIconType
    {
        None,
        User,
        Lock
    }
}
