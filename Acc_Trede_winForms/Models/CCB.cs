using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Acc_Trede_winForms.Models
{
    // ==========================================
    // CUSTOM CHECKBOX (CCheckBox)
    // ==========================================
    public class CCB : CheckBox
    {
        private Color _checkedColor = Color.FromArgb(108, 92, 231);
        private Color _uncheckedBorderColor = Color.FromArgb(108, 92, 231);
        private Color _boxBackColor = Color.FromArgb(30, 30, 46);
        private Color _focusBorderColor = Color.FromArgb(108, 92, 231);
        private int _focusBorderRadius = 6; // انحناء إطار التركيز
        private int _boxSize = 18;

        [Category("Custom Appearance")]
        public Color CheckedColor
        {
            get => _checkedColor;
            set { _checkedColor = value; this.Invalidate(); }
        }

        [Category("Custom Appearance")]
        public Color UncheckedBorderColor
        {
            get => _uncheckedBorderColor;
            set { _uncheckedBorderColor = value; this.Invalidate(); }
        }

        [Category("Custom Appearance")]
        public Color BoxBackColor
        {
            get => _boxBackColor;
            set { _boxBackColor = value; this.Invalidate(); }
        }

        [Category("Custom Appearance")]
        public Color FocusBorderColor
        {
            get => _focusBorderColor;
            set { _focusBorderColor = value; this.Invalidate(); }
        }

        [Category("Custom Appearance")]
        public int FocusBorderRadius
        {
            get => _focusBorderRadius;
            set { _focusBorderRadius = value; this.Invalidate(); }
        }

        [Category("Custom Appearance")]
        public int BoxSize
        {
            get => _boxSize;
            set { _boxSize = value; this.Invalidate(); }
        }

        public CCB()
        {
            this.AutoSize = false;
            this.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            this.ForeColor = Color.White;
            this.Cursor = Cursors.Hand;
            this.Size = new Size(150, 24);
            this.MinimumSize = new Size(_boxSize + 10, _boxSize + 6);

            // إعدادات الرسم والسلوك عند التركيز
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        protected override bool ShowFocusCues => false;

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            this.Invalidate();
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            Graphics g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // 1. مسح الخلفية
            using (SolidBrush bgBrush = new SolidBrush(this.Parent?.BackColor ?? Color.FromArgb(30, 30, 46)))
            {
                g.FillRectangle(bgBrush, this.ClientRectangle);
            }

            // 2. رسم إطار التركيز البنفسجي بإنحناء (Rounded) حول الأداة عند التحديد (Focused)
            if (this.Focused)
            {
                Rectangle focusRect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
                using (Pen focusPen = new Pen(_focusBorderColor, 1.5f))
                {
                    focusPen.DashStyle = DashStyle.DashDotDot;
                    g.DrawRoundedRectangle(focusPen, focusRect, _focusBorderRadius);
                }
            }

            bool isRtl = this.RightToLeft == RightToLeft.Yes;
            int boxY = (this.Height - _boxSize) / 2;

            // تحديد مكان الصندوق
            int boxX = isRtl ? (this.Width - _boxSize - 4) : 4;
            Rectangle boxRect = new Rectangle(boxX, boxY, _boxSize, _boxSize);

            // 3. رسم مربع الاختيار
            if (this.Checked)
            {
                using (SolidBrush fillBrush = new SolidBrush(_checkedColor))
                {
                    g.FillRoundedRectangle(fillBrush, boxRect, 4);
                }

                // رسم علامة الصح
                using (Pen checkPen = new Pen(Color.White, 2f))
                {
                    PointF[] checkPoints = new PointF[]
                    {
                        new PointF(boxRect.X + (_boxSize * 0.25f), boxRect.Y + (_boxSize * 0.5f)),
                        new PointF(boxRect.X + (_boxSize * 0.45f), boxRect.Y + (_boxSize * 0.7f)),
                        new PointF(boxRect.X + (_boxSize * 0.75f), boxRect.Y + (_boxSize * 0.3f))
                    };
                    g.DrawLines(checkPen, checkPoints);
                }
            }
            else
            {
                using (SolidBrush boxBrush = new SolidBrush(_boxBackColor))
                using (Pen borderPen = new Pen(_uncheckedBorderColor, 1.5f))
                {
                    g.FillRoundedRectangle(boxBrush, boxRect, 4);
                    g.DrawRoundedRectangle(borderPen, boxRect, 4);
                }
            }

            // 4. تحديد مساحة النص ورسمه
            int textX = isRtl ? 4 : (_boxSize + 10);
            int textWidth = this.Width - (_boxSize + 14);
            Rectangle textRect = new Rectangle(textX, 0, Math.Max(1, textWidth), this.Height);

            TextFormatFlags flags = TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak;
            if (isRtl)
            {
                flags |= TextFormatFlags.Right | TextFormatFlags.RightToLeft;
            }
            else
            {
                flags |= TextFormatFlags.Left;
            }

            TextRenderer.DrawText(g, this.Text, this.Font, textRect, this.ForeColor, flags);
        }
    }
}