using Acc_Trede_winForms.Models.CScrollB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Acc_Trede_winForms.Models.CDGV
{
    public partial class CDGV : UserControl
    {
        public DataGridView Grid { get; private set; }
        public CScrollBar CustomScrollBar { get; private set; }

        private bool isAscending = true;

        private Point currentHoveredCell = new Point(-1, -1);

        public event Action<string, int> ActionButtonClick;

        public int Count => this.Grid.Rows.Count;


        public CDGV()
        {
            InitializeComponents();
            ApplyDarkTheme();
            BindScrollEvents();
            EnableDoubleBuffering();
        }


        #region Public Methods

        public void FixActionButtonsPosition()
        {
            foreach (DataGridViewColumn col in this.Grid.Columns)
            {
                if (col.Tag is ButtonStyle style)
                {
                    ApplyButtonColumnPosition(col, style.IsLeftmost);
                }
            }
        }

        public void UpdateScrollBar()
        {
            if (this.Width <= 1 || this.Height <= 1) return;

            int visibleRows = this.Grid.DisplayedRowCount(false);

            if (this.Grid.Rows.Count > visibleRows && visibleRows > 0)
            {
                this.CustomScrollBar.Minimum = 0;
                this.CustomScrollBar.Maximum = this.Grid.Rows.Count - 1;
                this.CustomScrollBar.LargeChange = visibleRows;
                this.CustomScrollBar.Visible = true;
                this.CustomScrollBar.BringToFront();
            }
            else
            {
                this.CustomScrollBar.Visible = false;
            }
        }

        #endregion

        #region Dynamic Action Buttons Setup

        public DataGridViewButtonColumn AddActionButton(
            string columnName,
            string headerText,
            string buttonText = "",
            Image icon = null,
            Color? buttonColor = null,
            Color? hoverColor = null,
            bool isLeftmost = false)
        {
            DataGridViewButtonColumn btnColumn = new DataGridViewButtonColumn
            {
                Name = columnName,
                HeaderText = headerText,
                Text = buttonText ?? "",
                UseColumnTextForButtonValue = true,
                FlatStyle = FlatStyle.Flat,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                Width = string.IsNullOrEmpty(buttonText) ? 45 : 90
            };

            btnColumn.DefaultCellStyle.SelectionBackColor = Color.Transparent;
            btnColumn.DefaultCellStyle.SelectionForeColor = Color.Transparent;

            btnColumn.Tag = new ButtonStyle
            {
                NormalColor = buttonColor ?? Color.FromArgb(30, 30, 46),
                HoverColor = hoverColor ?? Color.FromArgb(141, 129, 240),
                TextColor = Color.White,
                Icon = icon,
                IsLeftmost = isLeftmost // حفظ خاصية الموقع
            };

            this.Grid.Columns.Add(btnColumn);
            ApplyButtonColumnPosition(btnColumn, isLeftmost);

            return btnColumn;
        }

        private class ButtonStyle
        {
            public Color NormalColor { get; set; }
            public Color HoverColor { get; set; }
            public Color TextColor { get; set; }
            public Image Icon { get; set; }
            public bool IsLeftmost { get; set; }
        }

        #endregion

        #region Custom Cell Painting (CheckBox & Dynamic Buttons)

        private void Grid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            // 1. رسم CheckBox المخصص
            if (this.Grid.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentForeground);

                bool isChecked = false;
                if (e.Value != null && e.Value != DBNull.Value)
                {
                    isChecked = Convert.ToBoolean(e.Value);
                }

                bool isSelected = (e.State & DataGridViewElementStates.Selected) != 0;

                int size = 16;
                int x = e.CellBounds.X + (e.CellBounds.Width - size) / 2;
                int y = e.CellBounds.Y + (e.CellBounds.Height - size) / 2;
                Rectangle boxRect = new Rectangle(x, y, size, size);

                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                Color backColor = isChecked
                    ? Color.FromArgb(108, 92, 231)
                    : (isSelected ? Color.FromArgb(40, 40, 60) : Color.FromArgb(32, 32, 48));

                Color borderColor = isChecked
                    ? Color.FromArgb(108, 92, 231)
                    : Color.FromArgb(90, 90, 120);

                using (GraphicsPath path = GetRoundedRectanglePath(boxRect, 3))
                {
                    using (SolidBrush bgBrush = new SolidBrush(backColor))
                    {
                        e.Graphics.FillPath(bgBrush, path);
                    }

                    using (Pen borderPen = new Pen(borderColor, 1.5f))
                    {
                        e.Graphics.DrawPath(borderPen, path);
                    }
                }

                if (isChecked)
                {
                    using (Pen checkPen = new Pen(Color.White, 2f))
                    {
                        PointF[] points = new PointF[]
                        {
                            new PointF(x + 3.5f, y + 8.5f),
                            new PointF(x + 6.5f, y + 11.5f),
                            new PointF(x + 12.5f, y + 4.5f)
                        };
                        e.Graphics.DrawLines(checkPen, points);
                    }
                }

                e.Handled = true;
            }

            // 2. رسم الأزرار الديناميكية
            else if (this.Grid.Columns[e.ColumnIndex] is DataGridViewButtonColumn btnCol)
            {
                bool isRowSelected = (e.State & DataGridViewElementStates.Selected) != 0;

                // خلفية الخلية حسب تحديد الصف أو اللون البديل
                Color cellBgColor = isRowSelected ? this.Grid.DefaultCellStyle.SelectionBackColor :
                                   (e.RowIndex % 2 == 0 ? this.Grid.DefaultCellStyle.BackColor : this.Grid.AlternatingRowsDefaultCellStyle.BackColor);

                using (SolidBrush cellBgBrush = new SolidBrush(cellBgColor))
                {
                    e.Graphics.FillRectangle(cellBgBrush, e.CellBounds);
                }

                // رسم خط الحدود السفلي
                using (Pen gridPen = new Pen(this.Grid.GridColor, 1))
                {
                    e.Graphics.DrawLine(gridPen, e.CellBounds.Left, e.CellBounds.Bottom - 1, e.CellBounds.Right, e.CellBounds.Bottom - 1);
                }

                ButtonStyle style = btnCol.Tag as ButtonStyle ?? new ButtonStyle
                {
                    NormalColor = Color.FromArgb(108, 92, 231),
                    HoverColor = Color.FromArgb(128, 112, 251),
                    TextColor = Color.White
                };

                bool hasText = !string.IsNullOrWhiteSpace(btnCol.Text);
                bool hasIcon = style.Icon != null;

                int btnWidth = Math.Min(e.CellBounds.Width - 8, hasText ? 85 : 30);
                int btnHeight = 28;

                int btnX = e.CellBounds.X + (e.CellBounds.Width - btnWidth) / 2;
                int btnY = e.CellBounds.Y + (e.CellBounds.Height - btnHeight) / 2;
                Rectangle btnRect = new Rectangle(btnX, btnY, btnWidth, btnHeight);

                // التحقق من حالة الـ Hover
                bool isHovered = (currentHoveredCell.X == e.ColumnIndex && currentHoveredCell.Y == e.RowIndex);

                // يتغير لون الزر إلى HoverColor إذا كان الماوس فوق الزر أو إذا كان الصف محددًا
                Color fillBtnColor = (isHovered || isRowSelected) ? style.HoverColor : style.NormalColor;

                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                using (GraphicsPath path = GetRoundedRectanglePath(btnRect, 5))
                {
                    using (SolidBrush btnBrush = new SolidBrush(fillBtnColor))
                    {
                        e.Graphics.FillPath(btnBrush, path);
                    }

                    // 1. حالة أيقونة فقط
                    if (hasIcon && !hasText)
                    {
                        int iconSize = 16;
                        int imgX = btnRect.X + (btnRect.Width - iconSize) / 2;
                        int imgY = btnRect.Y + (btnRect.Height - iconSize) / 2;
                        e.Graphics.DrawImage(style.Icon, new Rectangle(imgX, imgY, iconSize, iconSize));
                    }
                    // 2. حالة أيقونة ونص معاً
                    else if (hasIcon && hasText)
                    {
                        int iconSize = 14;
                        int spacing = 4;
                        int imgX = btnRect.X + 6;
                        int imgY = btnRect.Y + (btnRect.Height - iconSize) / 2;

                        e.Graphics.DrawImage(style.Icon, new Rectangle(imgX, imgY, iconSize, iconSize));

                        Rectangle textRect = new Rectangle(
                            imgX + iconSize + spacing,
                            btnRect.Y,
                            btnRect.Width - (iconSize + spacing + 6),
                            btnRect.Height
                        );

                        TextRenderer.DrawText(
                            e.Graphics,
                            btnCol.Text,
                            this.Grid.Font,
                            textRect,
                            style.TextColor,
                            TextFormatFlags.Left | TextFormatFlags.VerticalCenter
                        );
                    }
                    // 3. حالة نص فقط
                    else if (hasText)
                    {
                        TextRenderer.DrawText(
                            e.Graphics,
                            btnCol.Text,
                            this.Grid.Font,
                            btnRect,
                            style.TextColor,
                            TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
                        );
                    }
                }

                e.Handled = true;
            }
        }

        private GraphicsPath GetRoundedRectanglePath(Rectangle rect, int cornerRadius)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = cornerRadius * 2;

            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();

            return path;
        }

        #endregion

        #region Dynamic Sorting

        private void Grid_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var dataSource = this.Grid.DataSource;
            if (dataSource == null) return;

            string propertyName = this.Grid.Columns[e.ColumnIndex].DataPropertyName;
            if (string.IsNullOrEmpty(propertyName)) return;

            Type listType = dataSource.GetType();
            if (!listType.IsGenericType) return;
            Type itemType = listType.GetGenericArguments()[0];

            var items = ((IEnumerable)dataSource).Cast<object>();

            IEnumerable<object> sortedItems;
            if (isAscending)
                sortedItems = items.OrderBy(x => GetPropertyValue(x, propertyName));
            else
                sortedItems = items.OrderByDescending(x => GetPropertyValue(x, propertyName));

            isAscending = !isAscending;

            var castMethod = typeof(Enumerable).GetMethod("Cast").MakeGenericMethod(itemType);
            var toListMethod = typeof(Enumerable).GetMethod("ToList").MakeGenericMethod(itemType);

            var castedItems = castMethod.Invoke(null, new object[] { sortedItems });
            var finalSortedList = toListMethod.Invoke(null, new object[] { castedItems });

            this.Grid.DataSource = finalSortedList;
        }

        private object GetPropertyValue(object obj, string propertyName)
        {
            return obj?.GetType().GetProperty(propertyName)?.GetValue(obj, null);
        }

        #endregion

        #region Helpers

        private void ApplyButtonColumnPosition(DataGridViewColumn column, bool isLeftmost)
        {
            if (this.Grid.Columns.Count == 0) return;

            // في بيئة RightToLeft = Yes:
            // أقصى اليسار = الترتيب الأعلى (Columns.Count - 1)
            // أقصى اليمين = الترتيب الأول (0)
            bool isRtl = this.Grid.RightToLeft == RightToLeft.Yes;

            if (isLeftmost)
            {
                column.DisplayIndex = isRtl ? this.Grid.Columns.Count - 1 : 0;
            }
            else
            {
                column.DisplayIndex = isRtl ? 0 : this.Grid.Columns.Count - 1;
            }
        }

        private void InitializeComponents()
        {
            this.Grid = new DataGridView();
            this.CustomScrollBar = new CScrollBar();

            this.SuspendLayout();

            // 1. Configure Grid
            this.Grid.Dock = DockStyle.Fill;
            this.Grid.ScrollBars = ScrollBars.None;
            this.Grid.BorderStyle = BorderStyle.None;
            this.Grid.RightToLeft = RightToLeft.Yes;

            // 2. Configure Custom VScrollBar
            this.CustomScrollBar.Dock = DockStyle.Left;
            this.CustomScrollBar.Width = 10;
            this.CustomScrollBar.Visible = false;

            // 3. Add to Container
            this.Controls.Add(this.Grid);
            this.Controls.Add(this.CustomScrollBar);

            this.BackColor = Color.FromArgb(24, 24, 36);
            this.ResumeLayout(false);

            this.Grid.StandardTab = true;
            this.Grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            ForceHideNativeScrollbars();
        }

        private void EnableDoubleBuffering()
        {
            PropertyInfo pi = typeof(Control).GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi?.SetValue(this.Grid, true, null);
        }

        private void ApplyDarkTheme()
        {
            // 1. Basic Layout & RTL
            this.Grid.Dock = DockStyle.Fill;
            this.Grid.RightToLeft = RightToLeft.Yes;
            this.Grid.AllowUserToAddRows = false;
            this.Grid.AllowUserToDeleteRows = false;
            this.Grid.AllowUserToResizeRows = false;
            this.Grid.ReadOnly = true;
            this.Grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.Grid.MultiSelect = false;
            this.Grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.Grid.EnableHeadersVisualStyles = false;
            this.Grid.ScrollBars = ScrollBars.None;

            // 2. Background & Border Colors
            this.Grid.BackgroundColor = Color.FromArgb(24, 24, 36);
            this.Grid.BorderStyle = BorderStyle.None;
            this.Grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            this.Grid.GridColor = Color.FromArgb(45, 45, 60);

            // 3. Header Styling
            this.Grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            this.Grid.ColumnHeadersHeight = 40;
            this.Grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            this.Grid.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(32, 32, 48),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                Padding = new Padding(0)
            };

            // 4. Row & Cell Styling
            this.Grid.RowHeadersVisible = false;
            this.Grid.RowTemplate.Height = 35;

            DataGridViewCellStyle rowStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(24, 24, 36),
                ForeColor = Color.Gainsboro,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Regular),
                SelectionBackColor = Color.FromArgb(108, 92, 231),
                SelectionForeColor = Color.White,
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                Padding = new Padding(0)
            };
            this.Grid.DefaultCellStyle = rowStyle;

            // Alternating Row Styling
            this.Grid.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle(rowStyle)
            {
                BackColor = Color.FromArgb(28, 28, 42)
            };
        }

        private void BindScrollEvents()
        {
            #region old
            this.CustomScrollBar.Scroll += (s, e) =>
            {
                if (this.Grid.Rows.Count > 0 && this.CustomScrollBar.Value < this.Grid.Rows.Count)
                {
                    this.Grid.FirstDisplayedScrollingRowIndex = this.CustomScrollBar.Value;
                }
            };

            this.Grid.MouseWheel += (s, e) =>
            {
                if (!this.CustomScrollBar.Visible) return;

                int lines = e.Delta / 120;
                int newValue = this.CustomScrollBar.Value - (lines * 3);

                this.CustomScrollBar.Value = Math.Max(this.CustomScrollBar.Minimum,
                                                     Math.Min(newValue, this.CustomScrollBar.Maximum));
            };

            this.Grid.RowsAdded += (s, e) => UpdateScrollBar();
            this.Grid.RowsRemoved += (s, e) => UpdateScrollBar();
            this.Grid.Resize += (s, e) => UpdateScrollBar();

            // معالجة حركة الماوس لمنع الرمشة والتكرار
            this.Grid.CellMouseMove += (s, e) =>
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && this.Grid.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
                {
                    if (currentHoveredCell.X != e.ColumnIndex || currentHoveredCell.Y != e.RowIndex)
                    {
                        Point oldCell = currentHoveredCell;
                        currentHoveredCell = new Point(e.ColumnIndex, e.RowIndex);

                        if (oldCell.X >= 0 && oldCell.Y >= 0)
                            this.Grid.InvalidateCell(oldCell.X, oldCell.Y);

                        this.Grid.InvalidateCell(e.ColumnIndex, e.RowIndex);
                    }
                }
            };

            this.Grid.CellMouseLeave += (s, e) =>
            {
                if (currentHoveredCell.X >= 0 && currentHoveredCell.Y >= 0)
                {
                    Point oldCell = currentHoveredCell;
                    currentHoveredCell = new Point(-1, -1);
                    this.Grid.InvalidateCell(oldCell.X, oldCell.Y);
                }
            };

            this.Grid.ColumnHeaderMouseClick += Grid_ColumnHeaderMouseClick;
            this.Grid.CellPainting += Grid_CellPainting;
            #endregion

            this.Grid.CellMouseDown += (s, e) =>
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && this.Grid.Columns[e.ColumnIndex] is DataGridViewButtonColumn btnCol)
                {

                    this.Grid.CurrentCell = this.Grid.Rows[e.RowIndex].Cells[e.ColumnIndex];

                    this.Grid.Rows[e.RowIndex].Selected = true;

                    ActionButtonClick?.Invoke(btnCol.Name, e.RowIndex);
                }
            };
        }

        private void ForceHideNativeScrollbars()
        {
            this.Grid.ScrollBars = ScrollBars.None;
            this.Grid.ControlAdded += (s, e) =>
            {
                if (e.Control is ScrollBar)
                {
                    e.Control.Visible = false;
                    e.Control.Enabled = false;
                }
            };
        }

        #endregion

    }
}