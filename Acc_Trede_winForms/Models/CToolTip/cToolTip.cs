using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Acc_Trede_winForms.Models.CToolTip
{
    /// <summary>موقع الأيقونة بالنسبة للنص داخل التلميح</summary>
    public enum ToolTipIconAlign
    {
        Left,
        Right
    }

    /// <summary>
    /// تولتيب مخصص بالكامل (خلفية، حدود دائرية، أيقونة اختيارية) بنفس هوية التصميم.
    /// الألوان الافتراضية: خلفية (30,30,46) ونص/حد (108,92,231).
    /// </summary>
    public class CToolTip : ToolTip
    {
        private readonly Dictionary<Control, Image> _controlIcons = new Dictionary<Control, Image>();

        // ---------------- ألوان وشكل عام ----------------
        public Color BackgroundColor { get; set; } = Color.FromArgb(30, 30, 46);
        public Color TextColor { get; set; } = Color.FromArgb(108, 92, 231);
        public Color BorderColor { get; set; } = Color.FromArgb(108, 92, 231);
        public int BorderSize { get; set; } = 1;
        public int BorderRadius { get; set; } = 6;
        public Font TooltipFont { get; set; } = new Font("Segoe UI", 9f, FontStyle.Regular);
        public Padding ContentPadding { get; set; } = new Padding(10, 6, 10, 6);

        // ---------------- الأيقونة ----------------
        /// <summary>أيقونة افتراضية تُستخدم لكل العناصر التي لم تحدد أيقونة خاصة بها</summary>
        public Image DefaultIcon { get; set; } = null;
        public Size IconSize { get; set; } = new Size(16, 16);
        public ToolTipIconAlign IconAlignment { get; set; } = ToolTipIconAlign.Left;
        public int IconTextSpacing { get; set; } = 6;

        /// <summary>لو true (الافتراضي)، يعكس تلقائيًا اتجاه النص وموقع الأيقونة عندما يكون العنصر المرتبط RightToLeft = Yes</summary>
        public bool AutoRightToLeft { get; set; } = true;

        public CToolTip()
        {
            this.OwnerDraw = true;
            this.Popup += CToolTip_Popup;
            this.Draw += CToolTip_Draw;
        }

        /// <summary>يضبط نص التلميح، مع إمكانية تحديد أيقونة مختلفة لهذا العنصر تحديدًا (اختياري)</summary>
        public void SetToolTip(Control control, string caption, Image icon)
        {
            base.SetToolTip(control, caption);

            if (icon != null)
                _controlIcons[control] = icon;
            else
                _controlIcons.Remove(control);
        }

        private Image GetIconFor(Control control)
        {
            if (control != null && _controlIcons.TryGetValue(control, out var icon))
                return icon;
            return DefaultIcon;
        }

        private void CToolTip_Popup(object sender, PopupEventArgs e)
        {
            string text = GetToolTip(e.AssociatedControl) ?? string.Empty;
            Image icon = GetIconFor(e.AssociatedControl);

            Size textSize = TextRenderer.MeasureText(text, TooltipFont);

            int width = ContentPadding.Left + ContentPadding.Right + textSize.Width;
            int height = Math.Max(textSize.Height, icon != null ? IconSize.Height : 0)
                         + ContentPadding.Top + ContentPadding.Bottom;

            if (icon != null)
                width += IconSize.Width + IconTextSpacing;

            e.ToolTipSize = new Size(Math.Max(width, 24), Math.Max(height, 20));
        }

        private void CToolTip_Draw(object sender, DrawToolTipEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle bounds = e.Bounds;

            using (GraphicsPath path = GetRoundedPath(bounds, BorderRadius))
            using (SolidBrush bgBrush = new SolidBrush(BackgroundColor))
            {
                g.FillPath(bgBrush, path);
            }

            if (BorderSize > 0)
            {
                Rectangle borderRect = Rectangle.Inflate(bounds, -BorderSize, -BorderSize);
                using (GraphicsPath borderPath = GetRoundedPath(borderRect, Math.Max(0, BorderRadius - BorderSize)))
                using (Pen borderPen = new Pen(BorderColor, BorderSize))
                {
                    borderPen.Alignment = PenAlignment.Center;
                    g.DrawPath(borderPen, borderPath);
                }
            }

            bool rtl = AutoRightToLeft && e.AssociatedControl != null
                       && e.AssociatedControl.RightToLeft == RightToLeft.Yes;

            var effectiveAlign = IconAlignment;
            if (rtl)
                effectiveAlign = IconAlignment == ToolTipIconAlign.Left ? ToolTipIconAlign.Right : ToolTipIconAlign.Left;

            Image icon = GetIconFor(e.AssociatedControl);
            int contentHeight = bounds.Height - ContentPadding.Top - ContentPadding.Bottom;
            int textX = bounds.X + ContentPadding.Left;
            int textWidth = bounds.Width - ContentPadding.Left - ContentPadding.Right;

            if (icon != null)
            {
                int iconY = bounds.Y + ContentPadding.Top + (contentHeight - IconSize.Height) / 2;
                textWidth -= IconSize.Width + IconTextSpacing;

                if (effectiveAlign == ToolTipIconAlign.Left)
                {
                    g.DrawImage(icon, textX, iconY, IconSize.Width, IconSize.Height);
                    textX += IconSize.Width + IconTextSpacing;
                }
                else // Right
                {
                    int iconX = bounds.Right - ContentPadding.Right - IconSize.Width;
                    g.DrawImage(icon, iconX, iconY, IconSize.Width, IconSize.Height);
                }
            }

            Rectangle textRect = new Rectangle(textX, bounds.Y + ContentPadding.Top, Math.Max(0, textWidth), contentHeight);

            TextFormatFlags flags = TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis;
            flags |= rtl ? (TextFormatFlags.Right | TextFormatFlags.RightToLeft) : TextFormatFlags.Left;

            TextRenderer.DrawText(g, e.ToolTipText, TooltipFont, textRect, TextColor, flags);
        }

        private GraphicsPath GetRoundedPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();

            if (radius <= 0 || rect.Width <= 0 || rect.Height <= 0)
            {
                path.AddRectangle(rect);
                return path;
            }

            int diameter = radius * 2;
            if (diameter > rect.Width) diameter = rect.Width;
            if (diameter > rect.Height) diameter = rect.Height;

            Rectangle arc = new Rectangle(rect.X, rect.Y, diameter, diameter);
            path.AddArc(arc, 180, 90);
            arc.X = rect.Right - diameter;
            path.AddArc(arc, 270, 90);
            arc.Y = rect.Bottom - diameter;
            path.AddArc(arc, 0, 90);
            arc.X = rect.Left;
            path.AddArc(arc, 90, 90);
            path.CloseFigure();

            return path;
        }
    }
}