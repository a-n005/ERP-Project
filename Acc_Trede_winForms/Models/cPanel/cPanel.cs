using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Acc_Trede_winForms.Models.cPanel
{
    [ToolboxItem(true)]
    public class cPanel : Panel
    {
        private readonly List<Control> _disabledControls = new List<Control>();

        //[Category("Behavior")]
        //[Description("تفعيل أو تعطيل ميزة الإغلاق بـ Escape لهذا البنيل")]
        //public bool EnableEscapeClose { get; set; } = false;

        [Category("Action")]
        [Description("هل تعطل كل شي عندما تفتح")]
        public bool IstBackgroundDisabled { get; set; } = false;
        [Category("Action")]
        [Description("يعمل عند الضغط على مفتاح Escape داخل اللوحة")]
        public event EventHandler EscapePressed;

        public cPanel()
        {
            this.SetStyle(ControlStyles.Selectable, true);
            // منع إعادة الرسم المزدوج المشوه مع النص العربي
            this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            this.VisibleChanged += CPanel_VisibleChanged;

        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                // إغلاق خاصية WS_EX_LAYOUTRTL المسببة لإنعكاس النصوص الداخلي والمرآة
                if (this.RightToLeft == RightToLeft.Yes)
                {
                    cp.ExStyle &= ~0x00400000; // إلغاء WS_EX_LAYOUTRTL
                }
                return cp;
            }
        }

        private bool IsDesignMode =>
            LicenseManager.UsageMode == LicenseUsageMode.Designtime ||
            this.DesignMode ||
            (this.Site != null && this.Site.DesignMode);

        private void CPanel_VisibleChanged(object sender, EventArgs e)
        {
            if (IsDesignMode || this.Parent == null || !IstBackgroundDisabled)
                return;

            try
            {
                if (this.Visible)
                {
                    _disabledControls.Clear();

                    foreach (Control ctrl in this.Parent.Controls)
                    {
                        if (ctrl != null && ctrl != this && ctrl.Enabled)
                        {
                            ctrl.Enabled = false;
                            _disabledControls.Add(ctrl);
                        }
                    }

                    this.BeginInvoke(new Action(() =>
                    {
                        Control firstChild = GetFirstValidControl(this);
                        if (firstChild != null)
                        {
                            firstChild.Focus();
                        }
                    }));
                }
                else
                {
                    foreach (var ctrl in _disabledControls)
                    {
                        if (ctrl != null && !ctrl.IsDisposed)
                        {
                            ctrl.Enabled = true;
                        }
                    }
                    _disabledControls.Clear();
                }
            }
            catch
            {
                // منع انهيار المصمم
            }
        }

        private Control GetFirstValidControl(Control parent)
        {
            Control ctrl = parent.GetNextControl(parent, true);
            while (ctrl != null && (!ctrl.Visible || !ctrl.CanFocus || !ctrl.TabStop || !this.Contains(ctrl)))
            {
                ctrl = parent.GetNextControl(ctrl, true);
            }
            return ctrl;
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (this.Visible && (keyData & Keys.KeyCode) == Keys.Tab)
            {
                ContainerControl container = this.GetContainerControl() as ContainerControl;

                if (container != null)
                {
                    Control current = container.ActiveControl;

                    if (current != null && this.Contains(current))
                    {
                        Control next = this.GetNextControl(current, true);

                        while (next != null && (!next.Visible || !next.CanFocus || !next.TabStop || !this.Contains(next)))
                        {
                            next = this.GetNextControl(next, true);
                        }

                        if (next == null || !this.Contains(next))
                        {
                            next = GetFirstValidControl(this);
                        }

                        if (next != null)
                        {
                            next.Focus();
                            return true;
                        }
                    }
                }
            }

            return base.ProcessDialogKey(keyData);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (this.Visible && (keyData & Keys.KeyCode) == Keys.Escape)
            {
                OnEscapePressed();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected virtual void OnEscapePressed()
        {
            EscapePressed?.Invoke(this, EventArgs.Empty);
        }

        public void p_Paint(Panel panel, PaintEventArgs e, Color? color = null, int lineThickness = 2,
                               bool? t = null, bool? b = null, bool? l = null, bool? r = null, int borderRadius = 0, bool? all = null)
        {
            if (panel == null || e == null) return;


        }

        private System.Drawing.Drawing2D.GraphicsPath GetRoundedPath(Rectangle rect, int radius)
        {
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
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
    public static class PanelExtensions
    {
        public static void ApplyBorder(this Panel panel, bool? t = null, bool? b = null, bool? l = null, bool? r = null,
            int borderRadius = 0, Color? color = null, int lineThickness = 2, bool? all = null)
        {
            // إزالة الحدث القديم لمنع تكرار الرسم عند التحديث
            panel.Paint -= Panel_Paint;

            // ربط الحدث تلقائياً بالـ Panel
            panel.Paint += Panel_Paint;

            void Panel_Paint(object sender, PaintEventArgs e)
            {
                Color lineColor = color ?? Color.FromArgb(108, 92, 231);
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                int width = panel.ClientSize.Width;
                int height = panel.ClientSize.Height;

                if (width <= 0 || height <= 0) return;

                if (borderRadius > 0)
                {
                    Rectangle fullRect = new Rectangle(0, 0, width, height);
                    using (var clipPath = GetRoundedPath(fullRect, borderRadius))
                    {
                        panel.Region = new Region(clipPath);
                    }
                }
                else if (panel.Region != null)
                {
                    panel.Region.Dispose();
                    panel.Region = null;
                }

                if (all == true)
                    t = r = b = l = true;

                bool drawT = t ?? false;
                bool drawB = b ?? false;
                bool drawL = l ?? false;
                bool drawR = r ?? false;

                using (Pen pen = new Pen(lineColor, lineThickness))
                {
                    pen.Alignment = System.Drawing.Drawing2D.PenAlignment.Center;
                    int pad = lineThickness / 2;
                    Rectangle rect = new Rectangle(pad, pad, width - lineThickness, height - lineThickness);

                    if (borderRadius > 0)
                    {
                        if (drawT && drawB && drawL && drawR)
                        {
                            using (var path = GetRoundedPath(rect, borderRadius))
                            {
                                e.Graphics.DrawPath(pen, path);
                            }
                        }
                        else
                        {
                            var oldClip = e.Graphics.Clip;
                            using (Region drawRegion = new Region())
                            {
                                drawRegion.MakeEmpty();

                                int clipBound = borderRadius + pad;

                                if (drawT) drawRegion.Union(new Rectangle(0, 0, width, clipBound));
                                if (drawB) drawRegion.Union(new Rectangle(0, height - clipBound, width, clipBound));
                                if (drawL) drawRegion.Union(new Rectangle(0, 0, clipBound, height));
                                if (drawR) drawRegion.Union(new Rectangle(width - clipBound, 0, clipBound, height));

                                e.Graphics.Clip = drawRegion;

                                using (var path = GetRoundedPath(rect, borderRadius))
                                {
                                    e.Graphics.DrawPath(pen, path);
                                }

                                e.Graphics.Clip = oldClip;
                            }
                        }
                    }
                    else
                    {
                        if (drawT && drawB && drawL && drawR)
                        {
                            e.Graphics.DrawRectangle(pen, 0, 0, width - 1, height - 1);
                        }
                        else
                        {
                            if (drawT) e.Graphics.DrawLine(pen, 0, 0, width, 0);
                            if (drawR) e.Graphics.DrawLine(pen, width - 1, 0, width - 1, height);
                            if (drawB) e.Graphics.DrawLine(pen, 0, height - 1, width, height - 1);
                            if (drawL) e.Graphics.DrawLine(pen, 0, 0, 0, height);
                        }
                    }
                }
            }

            panel.Invalidate();
        }
        private static System.Drawing.Drawing2D.GraphicsPath GetRoundedPath(Rectangle rect, int radius)
        {
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
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