using Acc_Trade_Core;
using Acc_Trede_winForms.Models.CDGV;
using Acc_Trede_winForms.Models.CMessageBox;
using Acc_Trede_winForms_Buisness.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Acc_Trede_winForms.Entities
{
    public partial class ucCustomers : UserControl
    {
        public ucCustomers()
        {
            InitializeComponent();
            this.Resize += (s, e) => CenterAddPanel();
        }
        private void ucCustomers_Load(object sender, EventArgs e)
        {
            pAddInLoad();
            pTopInLoad();
            pBottomInLoad();
            dgvLoad();

        }
        #region Panel Add
        private void pAddInLoad()
        {
            pAdd.Paint += (_, e) => p_Paint(pAdd, e, borderRadius:10,b: true, t: true);
        }
        private void btnAddCustomersClick(object s, EventArgs e)
        {
            // after check if add make pAdd visible false
            pAdd.Visible = false;
        }
        private void CenterAddPanel()
        {
            if (pAdd != null && pAdd.Parent != null)
            {
                int x = (pAdd.Parent.ClientSize.Width - pAdd.Width) / 2;
                int y = (pAdd.Parent.ClientSize.Height - pAdd.Height) / 2;

                pAdd.Location = new Point(Math.Max(0, x), Math.Max(0, y));
            }
        }
        #endregion
        #region Panel Top
        private void pTopInLoad()
        {
            pTop.Paint += (s, e) => p_Paint(pTop, e, null, 2, false, true);
        }
        private void btnAddClick(object s, EventArgs e)
        {
            if (pAdd.Visible)
                return;
            pAdd.Visible = true;
            pAdd.BringToFront();
        }
        #endregion
        #region Panel Bottom
        private void pBottomInLoad()
        {
            pBottom.Paint += (s, e) => p_Paint(pBottom, e,t:true);
        }
        #endregion
        #region Data Grid View 
        private void dgvLoad()
        {
            dgvCustomers.Dock = DockStyle.Fill;
            dgvCustomers.UpdateScrollBar();

            Result<List<clsCustomers_BLL>> r = clsCustomers_BLL.GetAllCustomers();
            if (r.IsFailure)
            {
                CMsgB.Show("", r.Error, 0);
                return;
            }
            dgvCustomers.Grid.DataSource = r.Value;

            dgvCustomers.SortDGV("CustomerID");

            SetDGVLayout(dgvCustomers.Grid);
            refresh();
        }
        private void refresh()
        {
            int c = dgvCustomers.Grid.Rows.Count;
            lblRecords.Text = c > 0 ? $"العملاء: {c}" : "العملاء: 0";
        }
        private void ToHideColumns(DataGridView g, params string[] colsToHide)
        {
            foreach (string colName in colsToHide)
            {
                if (g.Columns.Contains(colName))
                    g.Columns[colName].Visible = false;
            }
        }
        private void SetDGVLayout(DataGridView g)
        {
            ToHideColumns(g, "CreatedAt", "UpdatedAt", "CreatedBy", "UpdatedBy");


            // 3. Set proper Arabic headers and relative column widths
            if (g.Columns.Contains("CustomerID"))
            {
                g.Columns["CustomerID"].HeaderText = "م";
                g.Columns["CustomerID"].FillWeight = 30;
            }

            if (g.Columns.Contains("CustomerName"))
            {
                g.Columns["CustomerName"].HeaderText = "اسم العميل";
                g.Columns["CustomerName"].FillWeight = 120;
            }

            if (g.Columns.Contains("Phone"))
            {
                g.Columns["Phone"].HeaderText = "الهاتف";
                g.Columns["Phone"].FillWeight = 80;
            }

            if (g.Columns.Contains("TaxNumber"))
            {
                g.Columns["TaxNumber"].HeaderText = "الرقم الضريبي";
                g.Columns["TaxNumber"].FillWeight = 80;
            }

            if (g.Columns.Contains("IsActive"))
            {
                g.Columns["IsActive"].HeaderText = "نشط";
                g.Columns["IsActive"].FillWeight = 40;
            }
        }
        #endregion

        #region Private Methods
        private void p_Paint(Panel panel, PaintEventArgs e, Color? color = null, int lineThickness = 2,
                       bool? t = null, bool? b = null, bool? l = null, bool? r = null, int borderRadius = 0, bool? all = null)
        {
            Color lineColor = color ?? Color.FromArgb(108, 92, 231);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int width = panel.ClientSize.Width;
            int height = panel.ClientSize.Height;

            if (width <= 0 || height <= 0) return;

            // 1. قص حواف البانل الخارجية
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

            bool drawT = t ?? (b == null && l == null && r == null);
            bool drawB = b ?? (t == null && l == null && r == null);
            bool drawL = l ?? (t == null && b == null && r == null);
            bool drawR = r ?? (t == null && b == null && e == null);

            using (Pen pen = new Pen(lineColor, lineThickness))
            {
                pen.Alignment = System.Drawing.Drawing2D.PenAlignment.Center;
                int pad = lineThickness / 2;
                Rectangle rect = new Rectangle(pad, pad, width - lineThickness, height - lineThickness);

                if (borderRadius > 0)
                {
                    // إذا كانت جميع الحدود مفعلة، نرسم الإطار كاملاً بشكل مثالي
                    if (drawT && drawB && drawL && drawR)
                    {
                        using (var path = GetRoundedPath(rect, borderRadius))
                        {
                            e.Graphics.DrawPath(pen, path);
                        }
                    }
                    else
                    {
                        // ضبط منطقة القص بدقة لمنع نزول الخط للأسفل عند اختيار (Top) فقط
                        var oldClip = e.Graphics.Clip;
                        using (Region drawRegion = new Region())
                        {
                            drawRegion.MakeEmpty();

                            // ارتفاع/عرض المنطقة المسموح بالرسم فيها يساوي تماماً نصف قطر الانحناء
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
        private System.Drawing.Drawing2D.GraphicsPath GetRoundedPath(Rectangle rect, int radius)
        {
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            int diameter = radius * 2;

            if (diameter > rect.Width) diameter = rect.Width;
            if (diameter > rect.Height) diameter = rect.Height;

            Rectangle arc = new Rectangle(rect.X, rect.Y, diameter, diameter);

            path.AddArc(arc, 180, 90); // Top-Left
            arc.X = rect.Right - diameter;
            path.AddArc(arc, 270, 90); // Top-Right
            arc.Y = rect.Bottom - diameter;
            path.AddArc(arc, 0, 90);   // Bottom-Right
            arc.X = rect.Left;
            path.AddArc(arc, 90, 90);  // Bottom-Left

            path.CloseFigure();
            return path;
        }
        #endregion
    }
}
