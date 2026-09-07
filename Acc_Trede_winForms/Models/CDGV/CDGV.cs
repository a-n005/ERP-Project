using Acc_Trede_winForms.Models.CScrollB;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Acc_Trede_winForms.Models.CDGV
{
    public class CDGV : UserControl
    {
        public DataGridView Grid { get; private set; }
        public CScrollBar CustomScrollBar { get; private set; }

        public CDGV()
        {
            InitializeComponents();
            ApplyDarkTheme();
            BindScrollEvents();
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
            this.CustomScrollBar.Dock = DockStyle.Left; // Set to Left for RTL layout
            this.CustomScrollBar.Width = 10;
            this.CustomScrollBar.Visible = false;

            // 3. Add to Container
            this.Controls.Add(this.Grid);
            this.Controls.Add(this.CustomScrollBar);

            this.BackColor = Color.FromArgb(24, 24, 36);
            this.ResumeLayout(false);

            ForceHideNativeScrollbars();
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

            // 3. Header Styling (Centered Alignment)
            this.Grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            this.Grid.ColumnHeadersHeight = 40;
            this.Grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            this.Grid.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(32, 32, 48),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter, // Force MiddleCenter
                Padding = new Padding(0) // Remove left padding to keep perfectly centered
            };

            // 4. Row & Cell Styling (Centered Alignment)
            this.Grid.RowHeadersVisible = false;
            this.Grid.RowTemplate.Height = 35;

            DataGridViewCellStyle rowStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(24, 24, 36),
                ForeColor = Color.Gainsboro,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Regular),
                SelectionBackColor = Color.FromArgb(108, 92, 231),
                SelectionForeColor = Color.White,
                Alignment = DataGridViewContentAlignment.MiddleCenter, // Force MiddleCenter
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
            // Scroll grid when dark scrollbar updates
            this.CustomScrollBar.Scroll += (s, e) =>
            {
                if (this.Grid.Rows.Count > 0 && this.CustomScrollBar.Value < this.Grid.Rows.Count)
                {
                    this.Grid.FirstDisplayedScrollingRowIndex = this.CustomScrollBar.Value;
                }
            };

            // Connect Mouse Wheel to ScrollBar
            this.Grid.MouseWheel += (s, e) =>
            {
                if (!this.CustomScrollBar.Visible) return;

                int lines = e.Delta / 120; // Standard wheel notch step
                int newValue = this.CustomScrollBar.Value - (lines * 3); // Move 3 rows per notch

                this.CustomScrollBar.Value = Math.Max(this.CustomScrollBar.Minimum,
                                             Math.Min(newValue, this.CustomScrollBar.Maximum));
            };

            this.Grid.RowsAdded += (s, e) => UpdateScrollBar();
            this.Grid.RowsRemoved += (s, e) => UpdateScrollBar();
            this.Grid.Resize += (s, e) => UpdateScrollBar();
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

        public void SortDGV(string colName)
        {
            if (this.Grid.Columns.Contains(colName))
            {
                this.Grid.Sort(this.Grid.Columns[colName], ListSortDirection.Ascending);
            }
        }
    }
}