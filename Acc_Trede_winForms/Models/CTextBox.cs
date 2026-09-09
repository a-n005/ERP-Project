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

        // NEW: Basic missing events
        public event KeyEventHandler _KeyDown;
        public event KeyEventHandler _KeyUp;
        public event EventHandler _ReadOnlyChanged;

        #endregion

        // -> Constructor
        public CTextBox()
        {
            InitializeComponent();
            UpdatePadding();

            this.textBox1.TextChanged += textBox1_TextChanged;
            this.textBox1.Enter += TextBox1_Enter;

            // NEW: Event wireups for input and mouse interaction
            this.textBox1.KeyDown += (s, e) => _KeyDown?.Invoke(this, e);
            this.textBox1.KeyUp += (s, e) => _KeyUp?.Invoke(this, e);
            this.textBox1.ReadOnlyChanged += (s, e) => _ReadOnlyChanged?.Invoke(this, e);
            this.textBox1.MouseMove += TextBox1_MouseMove;
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

        // NEW: Missing Text Selection & ReadOnly Properties
        [Browsable(false)]
        public string SelectedText
        {
            get => textBox1.SelectedText;
            set => textBox1.SelectedText = value;
        }

        [Category("Custom Properties")]
        public bool ReadOnly
        {
            get => textBox1.ReadOnly;
            set => textBox1.ReadOnly = value;
        }

        [Category("Custom Properties")]
        public int MaxLength
        {
            get => textBox1.MaxLength;
            set => textBox1.MaxLength = value;
        }

        [Category("Custom Properties")]
        public HorizontalAlignment TextAlign
        {
            get => textBox1.TextAlign;
            set => textBox1.TextAlign = value;
        }

        [Category("Custom Properties")]
        public Image LeftIcon
        {
            get => leftIcon;
            set
            {
                // Dispose old instance to prevent memory leaks
                if (leftIcon != value)
                {
                    leftIcon?.Dispose();
                    leftIcon = value != null ? new Bitmap(value) : null;
                    UpdatePadding();
                    this.Invalidate();
                }
            }
        }

        [Category("Custom Properties")]
        public Image RightIcon
        {
            get => rightIcon;
            set
            {
                // Dispose old instance to prevent memory leaks
                if (rightIcon != value)
                {
                    rightIcon?.Dispose();
                    rightIcon = value != null ? new Bitmap(value) : null;
                    UpdatePadding();
                    this.Invalidate();
                }
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
                    UpdateControlRegion();
            }
        }

        [Category("Custom Properties")]
        public override string Text
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
            this.textBox1.Focus();
            SelectEnd(); // Unselects text and moves cursor to end
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateControlRegion();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            UpdateControlRegion();
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

            if (this.Width <= 1 || this.Height <= 1 || e.Graphics == null)
                return;

            Graphics graph = e.Graphics;

            // Draw Border Logic
            if (borderRadius > 1) // Rounded TextBox
            {
                var rectBorderSmooth = this.ClientRectangle;
                int smoothSize = borderSize > 0 ? borderSize : 1;

                int inflateValue = -borderSize;
                if (rectBorderSmooth.Width + (inflateValue * 2) <= 0 || rectBorderSmooth.Height + (inflateValue * 2) <= 0)
                    inflateValue = 0;

                var rectBorder = Rectangle.Inflate(rectBorderSmooth, inflateValue, inflateValue);

                int safeRadius = Math.Max(1, Math.Min(borderRadius, Math.Min(this.Width, this.Height) / 2));
                int safeInnerRadius = Math.Max(1, safeRadius - borderSize);

                using (GraphicsPath pathBorderSmooth = GetFigurePath(rectBorderSmooth, safeRadius))
                using (GraphicsPath pathBorder = GetFigurePath(rectBorder, safeInnerRadius))
                using (Pen penBorderSmooth = new Pen(this.Parent != null ? this.Parent.BackColor : this.BackColor, smoothSize))
                using (Pen penBorder = new Pen(borderColor, borderSize))
                {
                    try
                    {
                        // 🛑 تم حذف this.Region من هنا لمنع انهيار الـ Handles
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
                    catch (ArgumentException) { }
                }
            }
            else // Square/Normal TextBox
            {
                using (Pen penBorder = new Pen(borderColor, borderSize))
                {
                    try
                    {
                        penBorder.Alignment = PenAlignment.Inset;
                        if (isFocused) penBorder.Color = borderFocusColor;

                        if (underlinedStyle)
                            graph.DrawLine(penBorder, 0, this.Height - 1, this.Width, this.Height - 1);
                        else
                            graph.DrawRectangle(penBorder, 0, 0, this.Width - 0.5F, this.Height - 0.5F);
                    }
                    catch (ArgumentException) { }
                }
            }
            // Draw Icons Logic (مع أغطية أمان للحماية من الانهيار)
            if (leftIcon != null && iconSize.Width > 0 && iconSize.Height > 0 && this.Height > iconSize.Height)
            {
                try
                {
                    // التأكد من أن الصورة لم يتم التفريغ منها (Not Disposed)
                    if (leftIcon.Width > 0 && leftIcon.Height > 0)
                    {
                        int yPos = (this.Height - iconSize.Height) / 2;
                        graph.DrawImage(leftIcon, basePaddingLeft, yPos, iconSize.Width, iconSize.Height);
                    }
                }
                catch (ArgumentException)
                {
                    // حماية متقدمة في حال تضرر ملف الصورة
                }
            }

            if (rightIcon != null && iconSize.Width > 0 && iconSize.Height > 0 && this.Height > iconSize.Height)
            {
                try
                {
                    if (rightIcon.Width > 0 && rightIcon.Height > 0)
                    {
                        int yPos = (this.Height - iconSize.Height) / 2;
                        int xPos = this.Width - basePaddingRight - iconSize.Width;

                        if (xPos > 0)
                        {
                            graph.DrawImage(rightIcon, xPos, yPos, iconSize.Width, iconSize.Height);
                        }
                    }
                }
                catch (ArgumentException) { }
            }
        }
        #endregion

        #region -> Public Methods (NEW: Operations, Focus & Cleanup)

        // تنظيف ومسح المحتوى وتطبيق الـ Placeholder
        public void Clear()
        {
            this.Text = string.Empty;
        }

        // تحديد كافة النصوص داخل الحقل
        public void SelectAll()
        {
            if (!isPlaceholder && textBox1 != null)
            {
                textBox1.SelectAll();
            }
        }

        // تحديد جزء معين من النص برمجياً
        public void Select(int start, int length)
        {
            if (!isPlaceholder && textBox1 != null)
            {
                textBox1.Select(start, length);
            }
        }

        // إدراج نص في نهاية الحقل مباشرة
        public void AppendText(string text)
        {
            if (isPlaceholder)
            {
                this.Text = text;
            }
            else
            {
                textBox1.AppendText(text);
            }
        }

        // عمليات الحافظة (Clipboard)
        public void Copy() => textBox1.Copy();
        public void Cut() => textBox1.Cut();
        public void Paste() => textBox1.Paste();
        public void Undo() => textBox1.Undo();

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

        private void UpdateControlRegion()
        {
            if (this.Width <= 1 || this.Height <= 1) return;

            // تنظيف الـ Region القديم للحد من تسريب الذاكرة
            this.Region?.Dispose();

            if (borderRadius > 1)
            {
                int safeRadius = Math.Max(1, Math.Min(borderRadius, Math.Min(this.Width, this.Height) / 2));
                using (GraphicsPath pathSmooth = GetFigurePath(this.ClientRectangle, safeRadius))
                {
                    this.Region = new Region(pathSmooth);
                }
            }
            else
            {
                this.Region = new Region(this.ClientRectangle);
            }
        }

        // NEW: Change cursor to Hand when hovering over icons
        private void TextBox1_MouseMove(object sender, MouseEventArgs e)
        {
            Point loc = this.PointToClient(Cursor.Position);

            bool isOverLeftIcon = leftIcon != null && new Rectangle(
                this.Padding.Left - iconSize.Width - 4,
                (this.Height - iconSize.Height) / 2,
                iconSize.Width, iconSize.Height).Contains(loc);

            bool isOverRightIcon = rightIcon != null && new Rectangle(
                this.Width - this.Padding.Right + 4,
                (this.Height - iconSize.Height) / 2,
                iconSize.Width, iconSize.Height).Contains(loc);

            this.Cursor = (isOverLeftIcon || isOverRightIcon) ? Cursors.Hand : Cursors.Default;
        }
        #endregion

        #region -> TextBox events
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            _TextChanged?.Invoke(this, e);
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