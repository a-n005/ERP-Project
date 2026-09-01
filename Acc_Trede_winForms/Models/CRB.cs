// ==========================================
// CUSTOM RADIOBUTTON (CRadioButton)
// ==========================================
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Acc_Trede_winForms.Models
{
    public class CRB : RadioButton
    {
        private Color _checkedColor = Color.FromArgb(108, 92, 231);
        private Color _uncheckedBorderColor = Color.FromArgb(108, 92, 231);
        private Color _boxBackColor = Color.FromArgb(30, 30, 46);
        private int _radioSize = 18;

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
        public int RadioSize
        {
            get => _radioSize;
            set { _radioSize = value; this.Invalidate(); }
        }

        public CRB()
        {
            this.AutoSize = false;
            this.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            this.ForeColor = Color.White;
            this.Cursor = Cursors.Hand;
            this.Size = new Size(150, 24);
            this.MinimumSize = new Size(_radioSize + 10, _radioSize + 6);

            // Disable default Windows focus rectangle drawing
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        // Prevents Windows from drawing focus cues (the blue shadow/line)
        protected override bool ShowFocusCues => false;

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            Graphics g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // مسح الخلفية
            using (SolidBrush bgBrush = new SolidBrush(this.Parent?.BackColor ?? Color.FromArgb(30, 30, 46)))
            {
                g.FillRectangle(bgBrush, this.ClientRectangle);
            }

            bool isRtl = this.RightToLeft == RightToLeft.Yes;
            int radioY = (this.Height - _radioSize) / 2;

            // تحديد مكان الدائرة
            int radioX = isRtl ? (this.Width - _radioSize - 2) : 2;
            Rectangle radioRect = new Rectangle(radioX, radioY, _radioSize, _radioSize);

            // رسم الدائرة الخارجية
            using (SolidBrush fillBrush = new SolidBrush(_boxBackColor))
            using (Pen borderPen = new Pen(this.Checked ? _checkedColor : _uncheckedBorderColor, 1.5f))
            {
                g.FillEllipse(fillBrush, radioRect);
                g.DrawEllipse(borderPen, radioRect);
            }

            // رسم النقطة الداخلية عند الاختيار
            if (this.Checked)
            {
                int dotMargin = _radioSize / 4;
                Rectangle dotRect = new Rectangle(
                    radioRect.X + dotMargin,
                    radioRect.Y + dotMargin,
                    _radioSize - (dotMargin * 2),
                    _radioSize - (dotMargin * 2)
                );

                using (SolidBrush dotBrush = new SolidBrush(_checkedColor))
                {
                    g.FillEllipse(dotBrush, dotRect);
                }
            }

            // تحديد مساحة النص بناءً على الحجم المحدد يدوياً
            int textX = isRtl ? 0 : (_radioSize + 8);
            int textWidth = this.Width - (_radioSize + 10);
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