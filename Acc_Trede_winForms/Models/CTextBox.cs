using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Acc_Trede_winForms.Models
{
    [DefaultEvent("_TextChanged")]
    public partial class CTextBox : UserControl
    {
        #region -> Fields
        // Fields
        private Color borderColor = Color.MediumSlateBlue;
        private Color borderFocusColor = Color.HotPink;
        private int borderSize = 2;
        private bool underlinedStyle = false;
        private bool isFocused = false;

        private int borderRadius = 0;
        private Color placeholderColor = Color.DarkGray;
        private string placeholderText = "";
        private bool isPlaceholder = false;
        private bool isPasswordChar = false;
        // Icon Fields
        private Image leftIcon = null;
        private Image rightIcon = null;
        private Size iconSize = new Size(20, 20);

        // Base padding (left, top, right, bottom) without icons
        private int basePaddingLeft = 10;
        private int basePaddingRight = 10;

        // Events
        public event EventHandler _TextChanged;
        public event EventHandler LeftIconClick;
        public event EventHandler RightIconClick;

        #endregion

        // -> Constructor
        public CTextBox()
        {
            InitializeComponent();
            UpdatePadding();

            this.textBox1.Enter += TextBox1_Enter;
        }

        #region -> Properties
        // Add these properties inside CTextBox.cs
        [Browsable(false)]
        public int SelectionStart
        {
            get => textBox1.SelectionStart;
            set => textBox1.SelectionStart = value;
        }

        [Browsable(false)]
        public int SelectionLength
        {
            get => textBox1.SelectionLength;
            set => textBox1.SelectionLength = value;
        }


        [Category("Custom Properties")]
        public Image LeftIcon
        {
            get => leftIcon;
            set
            {
                leftIcon = value;
                UpdatePadding();
                this.Invalidate();
            }
        }

        [Category("Custom Properties")]
        public Image RightIcon
        {
            get => rightIcon;
            set
            {
                rightIcon = value;
                UpdatePadding();
                this.Invalidate();
            }
        }

        [Category("Custom Properties")]
        public Size IconSize
        {
            get => iconSize;
            set
            {
                iconSize = value;
                UpdatePadding();
                this.Invalidate();
            }
        }

        [Category("Custom Properties")]
        public Color BorderColor
        {
            get { return borderColor; }
            set
            {
                borderColor = value;
                this.Invalidate();
            }
        }

        [Category("Custom Properties")]
        public Color BorderFocusColor
        {
            get { return borderFocusColor; }
            set { borderFocusColor = value; }
        }

        [Category("Custom Properties")]
        public int BorderSize
        {
            get { return borderSize; }
            set
            {
                if (value >= 1)
                {
                    borderSize = value;
                    this.Invalidate();
                }
            }
        }

        [Category("Custom Properties")]
        public bool UnderlinedStyle
        {
            get { return underlinedStyle; }
            set
            {
                underlinedStyle = value;
                this.Invalidate();
            }
        }

        [Category("Custom Properties")]
        public bool PasswordChar
        {
            get { return isPasswordChar; }
            set
            {
                isPasswordChar = value;
                if (!isPlaceholder)
                    textBox1.UseSystemPasswordChar = value;
            }
        }

        [Category("Custom Properties")]
        public bool Multiline
        {
            get { return textBox1.Multiline; }
            set { textBox1.Multiline = value; }
        }

        [Category("Custom Properties")]
        public override Color BackColor
        {
            get { return base.BackColor; }
            set
            {
                base.BackColor = value;
                textBox1.BackColor = value;
            }
        }

        [Category("Custom Properties")]
        public override Color ForeColor
        {
            get { return base.ForeColor; }
            set
            {
                base.ForeColor = value;
                textBox1.ForeColor = value;
            }
        }

        [Category("Custom Properties")]
        public override Font Font
        {
            get { return base.Font; }
            set
            {
                base.Font = value;
                textBox1.Font = value;
                if (this.DesignMode)
                    UpdateControlHeight();
            }
        }

        [Category("Custom Properties")]
        public string Texts
        {
            get
            {
                // Return empty string if currently displaying placeholder
                return isPlaceholder ? string.Empty : textBox1.Text;
            }
            set
            {
                textBox1.Text = value;

                // If assigned string is NOT empty, clear placeholder state & reset text color
                if (!string.IsNullOrEmpty(value))
                {
                    isPlaceholder = false;
                    textBox1.ForeColor = ForeColor;
                    if (this.isPasswordChar)
                    {
                        textBox1.UseSystemPasswordChar = true;
                    }
                }
                else
                {
                    // Re-apply placeholder state if empty
                    isPlaceholder = true;
                    textBox1.Text = placeholderText;
                    textBox1.ForeColor = placeholderColor;
                    if (this.isPasswordChar)
                    {
                        textBox1.UseSystemPasswordChar = false;
                    }
                }
            }
        }

        [Category("Custom Properties")]
        public int BorderRadius
        {
            get { return borderRadius; }
            set
            {
                if (value >= 0)
                {
                    borderRadius = value;
                    this.Invalidate();
                }
            }
        }

        [Category("Custom Properties")]
        public Color PlaceholderColor
        {
            get { return placeholderColor; }
            set
            {
                placeholderColor = value;
                if (isPlaceholder)
                    textBox1.ForeColor = value;
            }
        }

        [Category("Custom Properties")]
        public string PlaceholderText
        {
            get { return placeholderText; }
            set
            {
                placeholderText = value;
                textBox1.Text = "";
                SetPlaceholder();
            }
        }

        #endregion

        #region -> Overridden methods
        // Inside CTextBox.cs
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            textBox1.Focus();
            SelectEnd(); // Unselects text and moves cursor to end
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (this.DesignMode)
                UpdateControlHeight();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            UpdateControlHeight();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            // Handle Left Icon Click Event
            if (leftIcon != null)
            {
                Rectangle leftIconBounds = new Rectangle(
                    this.Padding.Left - iconSize.Width - 4,
                    (this.Height - iconSize.Height) / 2,
                    iconSize.Width,
                    iconSize.Height);

                if (leftIconBounds.Contains(e.Location))
                    LeftIconClick?.Invoke(this, EventArgs.Empty);
            }

            // Handle Right Icon Click Event
            if (rightIcon != null)
            {
                Rectangle rightIconBounds = new Rectangle(
                    this.Width - this.Padding.Right + 4,
                    (this.Height - iconSize.Height) / 2,
                    iconSize.Width,
                    iconSize.Height);

                if (rightIconBounds.Contains(e.Location))
                    RightIconClick?.Invoke(this, EventArgs.Empty);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics graph = e.Graphics;

            // Draw Border Logic
            if (borderRadius > 1) // Rounded TextBox
            {
                var rectBorderSmooth = this.ClientRectangle;
                var rectBorder = Rectangle.Inflate(rectBorderSmooth, -borderSize, -borderSize);
                int smoothSize = borderSize > 0 ? borderSize : 1;

                using (GraphicsPath pathBorderSmooth = GetFigurePath(rectBorderSmooth, borderRadius))
                using (GraphicsPath pathBorder = GetFigurePath(rectBorder, borderRadius - borderSize))
                using (Pen penBorderSmooth = new Pen(this.Parent != null ? this.Parent.BackColor : this.BackColor, smoothSize))
                using (Pen penBorder = new Pen(borderColor, borderSize))
                {
                    this.Region = new Region(pathBorderSmooth);
                    if (borderRadius > 15) SetTextBoxRoundedRegion();
                    graph.SmoothingMode = SmoothingMode.AntiAlias;
                    penBorder.Alignment = PenAlignment.Center;
                    if (isFocused) penBorder.Color = borderFocusColor;

                    if (underlinedStyle)
                    {
                        graph.DrawPath(penBorderSmooth, pathBorderSmooth);
                        graph.SmoothingMode = SmoothingMode.None;
                        graph.DrawLine(penBorder, 0, this.Height - 1, this.Width, this.Height - 1);
                    }
                    else
                    {
                        graph.DrawPath(penBorderSmooth, pathBorderSmooth);
                        graph.DrawPath(penBorder, pathBorder);
                    }
                }
            }
            else // Square/Normal TextBox
            {
                using (Pen penBorder = new Pen(borderColor, borderSize))
                {
                    this.Region = new Region(this.ClientRectangle);
                    penBorder.Alignment = PenAlignment.Inset;
                    if (isFocused) penBorder.Color = borderFocusColor;

                    if (underlinedStyle)
                        graph.DrawLine(penBorder, 0, this.Height - 1, this.Width, this.Height - 1);
                    else
                        graph.DrawRectangle(penBorder, 0, 0, this.Width - 0.5F, this.Height - 0.5F);
                }
            }

            // Draw Icons Logic
            if (leftIcon != null)
            {
                int yPos = (this.Height - iconSize.Height) / 2;
                graph.DrawImage(leftIcon, basePaddingLeft, yPos, iconSize.Width, iconSize.Height);
            }

            if (rightIcon != null)
            {
                int yPos = (this.Height - iconSize.Height) / 2;
                int xPos = this.Width - basePaddingRight - iconSize.Width;
                graph.DrawImage(rightIcon, xPos, yPos, iconSize.Width, iconSize.Height);
            }
        }
        #endregion

        #region -> Private methods
        private void TextBox1_Enter(object sender, EventArgs e)
        {
            // Prevent WinForms from auto-highlighting the text on focus
            this.BeginInvoke(new Action(() =>
            {
                if (this.textBox1 != null)
                {
                    this.textBox1.SelectionStart = this.textBox1.Text.Length;
                    this.textBox1.SelectionLength = 0;
                }
            }));
        }
        // Method to place cursor at the end without highlighting text
        public void SelectEnd()
        {
            if (textBox1 != null)
            {
                textBox1.SelectionStart = textBox1.Text.Length;
                textBox1.SelectionLength = 0;
            }
        }
        private void UpdatePadding()
        {
            int left = basePaddingLeft + (leftIcon != null ? iconSize.Width + 6 : 0);
            int right = basePaddingRight + (rightIcon != null ? iconSize.Width + 6 : 0);
            this.Padding = new Padding(left, 7, right, 7);
        }

        private void SetPlaceholder()
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) && placeholderText != "")
            {
                isPlaceholder = true;
                textBox1.Text = placeholderText;
                textBox1.ForeColor = placeholderColor;
                if (isPasswordChar)
                    textBox1.UseSystemPasswordChar = false;
            }
        }

        private void RemovePlaceholder()
        {
            if (isPlaceholder && placeholderText != "")
            {
                isPlaceholder = false;
                textBox1.Text = "";
                textBox1.ForeColor = this.ForeColor;
                if (isPasswordChar)
                    textBox1.UseSystemPasswordChar = true;
            }
        }

        private GraphicsPath GetFigurePath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            float curveSize = radius * 2F;

            path.StartFigure();
            path.AddArc(rect.X, rect.Y, curveSize, curveSize, 180, 90);
            path.AddArc(rect.Right - curveSize, rect.Y, curveSize, curveSize, 270, 90);
            path.AddArc(rect.Right - curveSize, rect.Bottom - curveSize, curveSize, curveSize, 0, 90);
            path.AddArc(rect.X, rect.Bottom - curveSize, curveSize, curveSize, 90, 90);
            path.CloseFigure();
            return path;
        }

        private void SetTextBoxRoundedRegion()
        {
            GraphicsPath pathTxt;
            if (Multiline)
            {
                pathTxt = GetFigurePath(textBox1.ClientRectangle, borderRadius - borderSize);
                textBox1.Region = new Region(pathTxt);
            }
            else
            {
                pathTxt = GetFigurePath(textBox1.ClientRectangle, borderSize * 2);
                textBox1.Region = new Region(pathTxt);
            }
            pathTxt.Dispose();
        }

        private void UpdateControlHeight()
        {
            if (textBox1.Multiline == false)
            {
                int txtHeight = TextRenderer.MeasureText("Text", this.Font).Height + 1;
                textBox1.Multiline = true;
                textBox1.MinimumSize = new Size(0, txtHeight);
                textBox1.Multiline = false;

                this.Height = textBox1.Height + this.Padding.Top + this.Padding.Bottom;
            }
        }
        #endregion

        #region -> TextBox events
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (_TextChanged != null)
                _TextChanged.Invoke(sender, e);
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            this.OnClick(e);
        }

        private void textBox1_MouseEnter(object sender, EventArgs e)
        {
            this.OnMouseEnter(e);
        }

        private void textBox1_MouseLeave(object sender, EventArgs e)
        {
            this.OnMouseLeave(e);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.OnKeyPress(e);
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            isFocused = true;
            this.Invalidate();
            RemovePlaceholder();
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            isFocused = false;
            this.Invalidate();
            SetPlaceholder();
        }
        #endregion

    }
}