using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Acc_Trede_winForms.Models.cToggleSwitch
{
    public enum TextPosition
    {
        Left,
        Right,
        Top,
        Bottom
    }

    [DefaultEvent("CheckedChanged")]
    public class cToggleSwitch : UserControl
    {
        private Panel _containerPanel;
        private Label _label;
        private ToggleSwitchControl _toggleSwitch;
        private TextPosition _labelPosition = TextPosition.Right;
        private int _spacing = 8;
        public event EventHandler CheckedChanged;
        private string _textOff = "Off";
        private string _textOn = "On";

        #region Control Main Properties

        [Category("Custom - Main")]
        [Description("تغيير حجم زر التبديل نفسه")]
        public Size ToggleSwitchSize
        {
            get => _toggleSwitch.Size;
            set
            {
                _toggleSwitch.Size = value;
                UpdateLayout();
            }
        }

        [Category("Custom - Main")]
        [Description("حالة التفعيل")]
        public bool Checked
        {
            get => _toggleSwitch.Checked;
            set => _toggleSwitch.Checked = value;
        }
        #endregion

        #region Control Appearance & Colors

        [Category("Custom - Appearance")]
        [Description("لون حدود المكبس")]
        public Color BorderColor
        {
            get => _toggleSwitch.BorderColor;
            set => _toggleSwitch.BorderColor = value;
        }

        [Category("Custom - Appearance")]
        [Description("سمك حدود المكبس (ضع 0 لإلغاء الحدود)")]
        public int BorderSize
        {
            get => _toggleSwitch.BorderSize;
            set => _toggleSwitch.BorderSize = value;
        }

        [Category("Custom - Appearance")]
        public Color OnColor
        {
            get => _toggleSwitch.OnColor;
            set => _toggleSwitch.OnColor = value;
        }

        public Color OffColor
        {
            get => _toggleSwitch.OffColor;
            set => _toggleSwitch.OffColor = value;
        }

        [Category("Custom - Appearance")]
        [Description("لون دائرة المكبس المتحرك")]
        public Color KnobColor
        {
            get => _toggleSwitch.KnobColor;
            set => _toggleSwitch.KnobColor = value;
        }

        [Category("Custom - Appearance")]
        [Description("حجم الأيقونة المخصصة (ينطبق على On و Off معاً)")]
        public Size IconSize
        {
            get => _toggleSwitch.CustomIconSize;
            set => _toggleSwitch.CustomIconSize = value;
        }

        [Category("Custom - Appearance")]
        [Description("صورة أيقونة التفعيل المخصصة (On)")]
        public Image OnImage
        {
            get => _toggleSwitch.OnImage;
            set => _toggleSwitch.OnImage = value;
        }

        [Category("Custom - Appearance")]
        [Description("صورة أيقونة الإيقاف المخصصة (Off)")]
        public Image OffImage
        {
            get => _toggleSwitch.OffImage;
            set => _toggleSwitch.OffImage = value;
        }
        #endregion

        #region Control Texts & Fonts

        [Category("Custom - Text & Font")]
        [Description("نص العنوان المساعد للزر عند الايقاف")]
        public string TitleTextOff
        {
            get => _textOff;
            set
            {
                _textOff = value;
                UpdateLayout();
            }
        }

        [Category("Custom - Text & Font")]
        [Description("نص العنوان المساعد للزر عند التشغيل")]
        public string TitleTextOn
        {
            get => _textOn;
            set
            {
                _textOn = value;
                UpdateLayout();
            }
        }

        [Category("Custom - Text & Font")]
        [Description("المسافة بين النص والزر")]
        public int Spacing
        {
            get => _spacing;
            set
            {
                _spacing = value;
                UpdateLayout();
            }
        }

        [Category("Custom - Text & Font")]
        [Description("موقع النص بالنسبة لزر التبديل")]
        public TextPosition LabelPosition
        {
            get => _labelPosition;
            set
            {
                _labelPosition = value;
                UpdateLayout();
            }
        }

        [Category("Custom - Text & Font")]
        [Description("النص الظاهر داخل الزر عند التفعيل")]
        public string InsideTextOn
        {
            get => _toggleSwitch.InsideTextOn;
            set => _toggleSwitch.InsideTextOn = value;
        }

        [Category("Custom - Text & Font")]
        [Description("النص الظاهر داخل الزر عند الإيقاف")]
        public string InsideTextOff
        {
            get => _toggleSwitch.InsideTextOff;
            set => _toggleSwitch.InsideTextOff = value;
        }

        [Category("Custom - Text & Font")]
        [Description("لون النص الداخلي للزر")]
        public Color InsideTextColor
        {
            get => _toggleSwitch.InsideTextColor;
            set => _toggleSwitch.InsideTextColor = value;
        }

        [Category("Custom - Text & Font")]
        [Description("خط النص الداخلي للزر")]
        public Font InsideTextFont
        {
            get => _toggleSwitch.InsideTextFont;
            set => _toggleSwitch.InsideTextFont = value;
        }

        [Category("Custom - Text & Font")]
        [Description("خط وحجم نص العنوان")]
        public Font LabelFont
        {
            get => _label.Font;
            set
            {
                _label.Font = value;
                UpdateLayout();
            }
        }

        [Category("Custom - Text & Font")]
        [Description("لون نص العنوان")]
        public Color LabelForeColor
        {
            get => _label.ForeColor;
            set { _label.ForeColor = value; _toggleSwitch.InsideTextColor = value; }
        }
        #endregion

        public cToggleSwitch()
        {
            InitializeComponents();
            UpdateLayout();
            this.Cursor = Cursors.Hand;

            this.SetStyle(ControlStyles.Selectable, true);
            this.TabStop = true;
        }

        private void InitializeComponents()
        {
            this.DoubleBuffered = true;

            _containerPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent
            };

            _toggleSwitch = new ToggleSwitchControl
            {
                Size = new Size(80, 40),
                Cursor = Cursors.Hand,
                TabStop = false
            };
            _toggleSwitch.CheckedChanged += (s, e) =>
            {
                _label.Text = (this.Checked) ? TitleTextOn : TitleTextOff;
                UpdateLayout();
                CheckedChanged?.Invoke(this, e);
            };

            _label = new Label
            {
                AutoSize = true,
                Text = "عنوان الزر",
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent
            };

            _label.Click += (s, e) => { this.Focus(); Checked = !Checked; };
            _containerPanel.Click += (s, e) => { this.Focus(); Checked = !Checked; };

            _containerPanel.Controls.Add(_toggleSwitch);
            _containerPanel.Controls.Add(_label);
            this.Controls.Add(_containerPanel);

            this.Size = new Size(180, 60);
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter || keyData == Keys.Space)
            {
                Checked = !Checked;
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space)
            {
                Checked = !Checked;
                e.Handled = true;
            }
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            _toggleSwitch.Invalidate();
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            _toggleSwitch.Invalidate();
        }

        private void UpdateLayout()
        {
            if (_containerPanel == null || _toggleSwitch == null || _label == null) return;

            _containerPanel.SuspendLayout();
            _label.Text = (this.Checked) ? TitleTextOn : TitleTextOff;

            int spacing = this.Spacing;

            switch (_labelPosition)
            {
                case TextPosition.Right:
                    _toggleSwitch.Location = new Point(0, (_containerPanel.Height - _toggleSwitch.Height) / 2);
                    _label.Location = new Point(_toggleSwitch.Right + spacing, (_containerPanel.Height - _label.Height) / 2);
                    break;

                case TextPosition.Left:
                    _toggleSwitch.Location = new Point(_containerPanel.Width - _toggleSwitch.Width, (_containerPanel.Height - _toggleSwitch.Height) / 2);
                    _label.Location = new Point(_toggleSwitch.Left - spacing - _label.Width, (_containerPanel.Height - _label.Height) / 2);
                    break;

                case TextPosition.Top:
                    _toggleSwitch.Location = new Point((_containerPanel.Width - _toggleSwitch.Width) / 2, _containerPanel.Height - _toggleSwitch.Height);
                    _label.Location = new Point((_containerPanel.Width - _label.Width) / 2, _toggleSwitch.Top - spacing - _label.Height);
                    break;

                case TextPosition.Bottom:
                    _toggleSwitch.Location = new Point((_containerPanel.Width - _toggleSwitch.Width) / 2, 0);
                    _label.Location = new Point((_containerPanel.Width - _label.Width) / 2, _toggleSwitch.Bottom + spacing);
                    break;
            }

            _containerPanel.ResumeLayout();
            this.Invalidate();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateLayout();
        }

        // --- الكلاس الداخلي للرسم المخصص للزر ---
        private class ToggleSwitchControl : Control
        {
            private bool _checked = false;
            private Size _customIconSize = new Size(16, 16);
            private Color _borderColor = Color.Gray;
            private int _borderSize = 1;
            private Color _knobColor = Color.White;

            public string InsideTextOn { get; set; } = "";
            public string InsideTextOff { get; set; } = "";
            public Color InsideTextColor { get; set; }
            public Font InsideTextFont { get; set; } = new Font("Segoe UI", 9f, FontStyle.Bold);

            public Image OnImage { get; set; }
            public Image OffImage { get; set; }
            public Color OnColor { get; set; } = Color.FromArgb(0, 122, 204);
            public Color OffColor { get; set; } = Color.FromArgb(64, 64, 64);

            public Color KnobColor
            {
                get => _knobColor;
                set { _knobColor = value; this.Invalidate(); }
            }

            public Color BorderColor
            {
                get => _borderColor;
                set { _borderColor = value; this.Invalidate(); }
            }

            public int BorderSize
            {
                get => _borderSize;
                set { _borderSize = Math.Max(0, value); this.Invalidate(); }
            }

            public Size CustomIconSize
            {
                get => _customIconSize;
                set { _customIconSize = value; this.Invalidate(); }
            }

            public event EventHandler CheckedChanged;

            public bool Checked
            {
                get => _checked;
                set
                {
                    if (_checked != value)
                    {
                        _checked = value;
                        CheckedChanged?.Invoke(this, EventArgs.Empty);
                        this.Invalidate();
                    }
                }
            }

            public ToggleSwitchControl()
            {
                this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                              ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw |
                              ControlStyles.SupportsTransparentBackColor, true);
                this.BackColor = Color.Transparent;
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);
                Graphics g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                int strokeOffset = _borderSize > 0 ? _borderSize : 1;
                int height = this.Height - (strokeOffset * 2);
                int width = this.Width - (strokeOffset * 2);

                if (height <= 0 || width <= 0) return;

                Rectangle rect = new Rectangle(strokeOffset, strokeOffset, width, height);

                // 1. رسم خلفية الزر والحدود
                using (GraphicsPath path = GetRoundedPath(rect, height))
                {
                    using (SolidBrush bgBrush = new SolidBrush(_checked ? OnColor : OffColor))
                    {
                        g.FillPath(bgBrush, path);
                    }

                    if (_borderSize > 0)
                    {
                        using (Pen borderPen = new Pen(_borderColor, _borderSize))
                        {
                            borderPen.Alignment = PenAlignment.Center;
                            g.DrawPath(borderPen, path);
                        }
                    }

                    // رسم إطار منقط (Dotted Border) حول زر التبديل عند الوقوف عليه بالـ Tab
                    if (this.Parent?.Parent is cToggleSwitch parentControl && parentControl.Focused)
                    {
                        using (Pen focusPen = new Pen(Color.Black, 1.5f))
                        {
                            focusPen.DashStyle = DashStyle.Dot;
                            g.DrawPath(focusPen, path);
                        }
                    }
                }

                // 2. حساب وتحديد دائرة المكبس
                int padding = 3;
                int circleDiameter = height - (padding * 2);
                int circleX = _checked ? (rect.Right - circleDiameter - padding) : (rect.Left + padding);
                Rectangle circleRect = new Rectangle(circleX, rect.Top + padding, circleDiameter, circleDiameter);

                // 3. رسم النص الداخلي في المساحة الفارغة
                string strDraw = _checked ? InsideTextOn : InsideTextOff;
                if (!string.IsNullOrEmpty(strDraw))
                {
                    Rectangle boundsDraw = _checked
                        ? new Rectangle(rect.Left + padding, rect.Top, rect.Width - circleDiameter - (padding * 2), rect.Height)
                        : new Rectangle(circleRect.Right, rect.Top, rect.Width - circleDiameter - (padding * 2), rect.Height);

                    TextFormatFlags textFlags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine;
                    TextRenderer.DrawText(g, strDraw, InsideTextFont, boundsDraw, InsideTextColor, textFlags);
                }

                // 4. رسم دائرة المكبس المتحرك
                using (SolidBrush knobBrush = new SolidBrush(_knobColor))
                {
                    g.FillEllipse(knobBrush, circleRect);
                }

                // 5. رسم الأيقونة أو العلامة فوق دائرة المكبس
                int centerX = circleRect.X + (circleRect.Width / 2);
                int centerY = circleRect.Y + (circleRect.Height / 2);
                Image currentImg = _checked ? OnImage : OffImage;

                if (currentImg != null)
                {
                    int imgX = centerX - (_customIconSize.Width / 2);
                    int imgY = centerY - (_customIconSize.Height / 2);
                    g.DrawImage(currentImg, new Rectangle(imgX, imgY, _customIconSize.Width, _customIconSize.Height));
                }
                else
                {
                    using (Pen iconPen = new Pen(_checked ? OnColor : OffColor, 2f))
                    {
                        iconPen.StartCap = LineCap.Round;
                        iconPen.EndCap = LineCap.Round;

                        int halfW = _customIconSize.Width / 2;
                        int halfH = _customIconSize.Height / 2;

                        if (_checked)
                        {
                            Point[] checkPoints = new Point[]
                            {
                                new Point(centerX - halfW, centerY),
                                new Point(centerX - (halfW / 3), centerY + halfH - 2),
                                new Point(centerX + halfW, centerY - halfH + 2)
                            };
                            g.DrawLines(iconPen, checkPoints);
                        }
                        else
                        {
                            g.DrawLine(iconPen, centerX - halfW, centerY - halfH, centerX + halfW, centerY + halfH);
                            g.DrawLine(iconPen, centerX + halfW, centerY - halfH, centerX - halfW, centerY + halfH);
                        }
                    }
                }
            }

            protected override void OnMouseClick(MouseEventArgs e)
            {
                base.OnMouseClick(e);
                if (e.Button == MouseButtons.Left)
                {
                    if (this.Parent?.Parent is cToggleSwitch parentControl)
                    {
                        parentControl.Focus();
                    }
                    Checked = !Checked;
                }
            }

            private GraphicsPath GetRoundedPath(Rectangle rect, int radius)
            {
                GraphicsPath path = new GraphicsPath();
                int diameter = radius;
                Rectangle arc = new Rectangle(rect.X, rect.Y, diameter, diameter);

                path.AddArc(arc, 90, 180);
                arc.X = rect.Right - diameter;
                path.AddArc(arc, 270, 180);
                path.CloseFigure();
                return path;
            }
        }
    }
}