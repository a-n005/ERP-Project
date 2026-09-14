using Acc_Trede_winForms.Models; // CTextBox
using Acc_Trede_winForms.Models.cPanel;
using Acc_Trede_winForms.Models.CScrollB; // CScrollBar
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Acc_Trede_winForms.Models.cSuggestTextBox
{
    /// <summary>
    /// عنصر يمثل نتيجة واحدة في قائمة الاقتراحات (منتج، متجر، ...الخ)
    /// </summary>
    public class SuggestionItem
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public object Tag { get; set; }

        public override string ToString() => Title ?? string.Empty;
    }

    /// <summary>
    /// تيكست بوكس بحث (CTextBox) + قائمة اقتراحات منسدلة تحته (cPanel) + سكرول بار (CScrollBar)
    /// يبني القائمة فوق الفورم مباشرة حتى تظهر فوق باقي عناصر التحكم.
    /// </summary>
    [DefaultEvent("ItemSelected")]
    public partial class CTxtSuggest : UserControl
    {

        // ---------------- عناصر التحكم ----------------
        public CTextBox TextBox { get; private set; }

        private cPanel.cPanel dropPanel;
        private Panel itemsHost;
        private CScrollBar vScroll;
        private readonly List<Panel> _rowControls = new List<Panel>();

        private List<SuggestionItem> _dataSource = new List<SuggestionItem>();
        private List<SuggestionItem> _filtered = new List<SuggestionItem>();
        private int _selectedIndex = -1;

        private Control _hostSurface; // الفورم (أو أعلى حاوية) الذي ترتسم عليه القائمة
        private IMessageFilter _outsideClickFilter;

        #region Properties
        // ---------------- إعدادات قابلة للتخصيص ----------------
        [Category("Custom - List")] public int ItemHeight { get; set; } = 42;
        [Category("Custom - List")] public int MaxVisibleItems { get; set; } = 6;
        [Category("Custom - List")] public int DropDownWidth { get; set; } = 0; // 0 = نفس عرض حقل البحث
        [Category("Custom - List")] public Color ItemBackColor { get; set; } = Color.FromArgb(30, 30, 46);
        [Category("Custom - List")] public Color ItemHoverColor { get; set; } = Color.FromArgb(141, 129, 240);
        [Category("Custom - List")] public Color ItemTextColor { get; set; } = Color.White;
        [Category("Custom - List")] public Color ItemSubTextColor { get; set; } = Color.Gray;
        [Category("Custom - List")] public Color PanelBorderColor { get; set; } = Color.FromArgb(108, 92, 231);
        [Category("Custom - List")] public Color PanelBackColor { get; set; } = Color.FromArgb(30, 30, 46);

        //----------------- setting txtb ------------------------

        [Category("Custom - Text Box")] public Image LeftIcon { get => TextBox.LeftIcon; set => TextBox.LeftIcon = value; }
        [Category("Custom - Text Box")] public Image RightIcon { get => TextBox.RightIcon; set => TextBox.RightIcon = value; }
        [Category("Custom - Text Box")] public Size IconSize { get => TextBox.IconSize; set => TextBox.IconSize = value; }
        [Category("Custom - Text Box")] public Color BorderColor { get => TextBox.BorderColor; set => TextBox.BorderColor = value; }
        [Category("Custom - Text Box")] public Color BorderFocusColor { get => TextBox.BorderFocusColor; set => TextBox.BorderFocusColor = value; }
        [Category("Custom - Text Box")] public int BorderSize { get => TextBox.BorderSize; set => TextBox.BorderSize = value; }
        [Category("Custom - Text Box")] public bool UnderlinedStyle { get => TextBox.UnderlinedStyle; set => TextBox.UnderlinedStyle = value; }
        [Category("Custom - Text Box")] public Color TxtBackColor { get => TextBox.BackColor; set => TextBox.BackColor = value; }
        [Category("Custom - Text Box")] public override Color ForeColor { get => TextBox.ForeColor; set => TextBox.ForeColor = value; }
        [Category("Custom - Text Box")] public override Font Font { get => TextBox.Font; set => TextBox.Font = value; }
        [Category("Custom - Text Box")] public int BorderRadius { get => TextBox.BorderRadius; set => TextBox.BorderRadius = value; }
        [Category("Custom - Text Box")] public Color PlaceholderColor { get => TextBox.PlaceholderColor; set => TextBox.PlaceholderColor = value; }
        [Category("Custom - Text Box")] public string PlaceholderText { get => TextBox.PlaceholderText; set => TextBox.PlaceholderText = value; }
        // ------- items panel ----------
        [Category("Custom - Item")] public Color ItemColor { get; set; } = Color.Transparent;
        [Category("Custom - Item")] public int ItemRadius { get; set; } = 0;

        #endregion

        /// <summary>دالة فلترة مخصصة، افتراضيًا يبحث داخل العنوان والعنوان الفرعي</summary>
        public Func<SuggestionItem, string, bool> FilterFunc { get; set; }

        public event EventHandler<SuggestionItem> ItemSelected;
        public event EventHandler<string> SearchTextChanged;

        public CTxtSuggest()
        {
            this.BackColor = Color.Transparent;
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.Height = 40;

            TextBox = new CTextBox
            {
                Dock = DockStyle.Fill,
                PlaceholderText = "بحث...",
                BorderRadius = 8
            };
            TextBox._TextChanged += TextBox_TextChanged;
            TextBox._KeyDown += TextBox_KeyDown;
            TextBox.Enter += TextBox_TextEnter;
            this.Controls.Add(TextBox);

            BuildDropdown();

            this.Resize += (s, e) => RepositionDropdown();
            this.LocationChanged += (s, e) => RepositionDropdown();
            this.VisibleChanged += (s, e) => { if (!this.Visible) HideDropdown(); };
            this.RightToLeftChanged += (s, e) => ApplyRightToLeft();
        }
        // ---------------- overwrite -------------

        protected override void OnHandleDestroyed(EventArgs e)
        {
            UnhookOutsideClick();
            dropPanel?.Dispose();
            base.OnHandleDestroyed(e);
        }

        public void Clear()
        {
            TextBox.Text = string.Empty;
        }
        private void BuildDropdown()
        {
            dropPanel = new cPanel.cPanel
            {
                BackColor = PanelBackColor,
                Visible = false,
                IstBackgroundDisabled = false
            };
            dropPanel.ApplyBorder(borderRadius: BorderRadius, lineThickness: 0);

            vScroll = new CScrollBar
            {
                Dock = DockStyle.Right,
                Width = 8,
                Visible = false
            };
            vScroll.Scroll += (s, e) => RepositionRows();

            itemsHost = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                Location = new Point(0, 0)
            };
            itemsHost.MouseWheel += ItemsHost_MouseWheel;


            dropPanel.Controls.Add(itemsHost);
            dropPanel.Controls.Add(vScroll);
            vScroll.BringToFront();
        }

        // ---------------- الأحداث ----------------

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (!dropPanel.Visible) return;

            if (e.KeyCode == Keys.Escape)
            {
                HideDropdown();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Down)
            {
                if (_filtered.Count > 0)
                {
                    _selectedIndex = Math.Min(_filtered.Count - 1, _selectedIndex + 1);
                    EnsureSelectedRowVisible();
                    UpdateRowHighlight();
                }
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Up)
            {
                if (_filtered.Count > 0)
                {
                    _selectedIndex = Math.Max(0, _selectedIndex - 1);
                    EnsureSelectedRowVisible();
                    UpdateRowHighlight();
                }
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Enter && _filtered.Count > 0)
            {
                int index = _selectedIndex >= 0 && _selectedIndex < _filtered.Count ? _selectedIndex : 0;
                SelectItem(_filtered[index]);
                e.Handled = true;
            }
        }

        private void UpdateRowHighlight()
        {
            for (int i = 0; i < _rowControls.Count; i++)
            {
                _rowControls[i].BackColor = (i == _selectedIndex) ? ItemHoverColor : ItemBackColor;
            }
        }

        private void EnsureSelectedRowVisible()
        {
            if (_selectedIndex < 0) return;
            int itemTop = _selectedIndex * ItemHeight;
            int itemBottom = itemTop + ItemHeight;
            int viewTop = vScroll.Visible ? vScroll.Value : 0;
            int viewHeight = itemsHost.Height;

            bool changed = false;
            if (itemTop < viewTop)
            {
                vScroll.Value = Math.Max(vScroll.Minimum, itemTop);
                changed = true;
            }
            else if (itemBottom > viewTop + viewHeight)
            {
                vScroll.Value = Math.Min(vScroll.Maximum, itemBottom - viewHeight);
                changed = true;
            }

            if (changed)
            {
                RepositionRows();
            }
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            string text = TextBox.Text ?? string.Empty;
            SearchTextChanged?.Invoke(this, text);
            Filter(text);
        }
        private void TextBox_TextEnter(object sender,EventArgs e)
        {
            //string text = TextBox.Text ?? string.Empty;
            //SearchTextChanged?.Invoke(this, text);
            //Filter(text);
            Filter(TextBox.Text.Trim());
        }
        // ---------------- البيانات والفلترة ----------------

        /// <summary>حدد قائمة المنتجات/المتاجر الكاملة التي سيبحث فيها العنصر</summary>
        public void SetDataSource(List<SuggestionItem> items)
        {
            _dataSource = items ?? new List<SuggestionItem>();
            if (!string.IsNullOrEmpty(TextBox.Text))
                Filter(TextBox.Text);
        }

        private void Filter(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                HideDropdown();
                return;
            }

            Func<SuggestionItem, string, bool> match = FilterFunc ?? DefaultFilter;
            _filtered = _dataSource.Where(i => match(i, text)).ToList();

            if (_filtered.Count == 0)
            {
                HideDropdown();
                return;
            }

            BuildRows();
            ShowDropdown();
        }

        private bool DefaultFilter(SuggestionItem item, string text)
        {
            return (item.Title ?? "").IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0
                || (item.SubTitle ?? "").IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        // ---------------- بناء صفوف النتائج ----------------

        private void BuildRows()
        {
            foreach (var row in _rowControls) row.Dispose();
            _rowControls.Clear();
            itemsHost.Controls.Clear();
            _selectedIndex = -1;

            foreach (var item in _filtered)
            {
                var row = CreateRow(item);
                itemsHost.Controls.Add(row);
                _rowControls.Add(row);
            }

            int visibleCount = Math.Min(MaxVisibleItems, _filtered.Count);
            int hostHeight = ItemHeight * visibleCount;
            int totalHeight = ItemHeight * _filtered.Count;
            bool needsScroll = totalHeight > hostHeight;

            int width = DropDownWidth > 0 ? DropDownWidth : this.Width;

            itemsHost.Size = new Size(width - (needsScroll ? vScroll.Width : 0), hostHeight);
            dropPanel.Size = new Size(width, hostHeight);

            //// ترك مساحة صغيرة جداً (مثلاً 3 بكسل) لكي لا يغطي itemsHost الزوايا المنحنية للـ dropPanel
            //int borderOffset = 2;

            //itemsHost.Location = new Point(borderOffset, borderOffset-2);
            //itemsHost.Size = new Size(width - (needsScroll ? vScroll.Width : 0) - (borderOffset * 2), hostHeight - (borderOffset * 2));
            //dropPanel.Size = new Size(width, hostHeight);

            vScroll.Visible = needsScroll;
            if (needsScroll)
            {
                vScroll.Minimum = 0;
                vScroll.Maximum = totalHeight - hostHeight;
                vScroll.LargeChange = hostHeight;
                vScroll.Value = 0;
            }

            foreach (var row in _rowControls)
                row.Width = itemsHost.Width;

            RepositionRows();
        }

        private Panel CreateRow(SuggestionItem item)
        {
            bool rtl = this.RightToLeft == RightToLeft.Yes;

            var row = new Panel
            {
                Height = ItemHeight,
                Left = 0,
                BackColor = ItemBackColor,
                Cursor = Cursors.Hand,
                Tag = item
            };
            row.ApplyBorder(lineThickness: 0, borderRadius: ItemRadius);

            var lblTitle = new Label
            {
                Text = item.Title,
                ForeColor = ItemTextColor,
                BackColor = Color.Transparent,
                Font = new Font(this.Font.FontFamily, 9.5f, FontStyle.Regular),
                Dock = DockStyle.Top,
                Height = string.IsNullOrEmpty(item.SubTitle) ? ItemHeight : ItemHeight - 16,
                TextAlign = rtl ? ContentAlignment.MiddleRight : ContentAlignment.MiddleLeft,
                Padding = new Padding(10, 0, 10, 0)
            };
            row.Controls.Add(lblTitle);

            Label lblSub = null;
            if (!string.IsNullOrEmpty(item.SubTitle))
            {
                lblSub = new Label
                {
                    Text = item.SubTitle,
                    ForeColor = ItemSubTextColor,
                    BackColor = Color.Transparent,
                    Font = new Font(this.Font.FontFamily, 8f, FontStyle.Regular),
                    Dock = DockStyle.Bottom,
                    Height = 16,
                    TextAlign = rtl ? ContentAlignment.MiddleRight : ContentAlignment.MiddleLeft,
                    Padding = new Padding(10, 0, 10, 0)
                };
                row.Controls.Add(lblSub);
            }

            EventHandler enter = (s, e) =>
            {
                int idx = _rowControls.IndexOf(row);
                if (idx >= 0)
                {
                    _selectedIndex = idx;
                    UpdateRowHighlight();
                }
            };


            EventHandler click = (s, e) => SelectItem(item);

            row.MouseEnter += enter; lblTitle.MouseEnter += enter; if (lblSub != null) lblSub.MouseEnter += enter;
            row.Click += click; lblTitle.Click += click; if (lblSub != null) lblSub.Click += click;
            row.MouseWheel += ItemsHost_MouseWheel;

            return row;
        }

        private void RepositionRows()
        {
            int offset = vScroll.Visible ? vScroll.Value : 0;
            for (int i = 0; i < _rowControls.Count; i++)
                _rowControls[i].Top = i * ItemHeight - offset;
        }

        private void ItemsHost_MouseWheel(object sender, MouseEventArgs e)
        {
            if (!vScroll.Visible) return;
            int delta = e.Delta > 0 ? -ItemHeight : ItemHeight;
            vScroll.Value = Math.Max(vScroll.Minimum, Math.Min(vScroll.Maximum, vScroll.Value + delta));
        }

        private void SelectItem(SuggestionItem item)
        {
            TextBox.Text = item.Title;
            TextBox.SelectEnd();
            HideDropdown();
            ItemSelected?.Invoke(this, item);
        }

        // ---------------- إظهار / إخفاء القائمة فوق الفورم ----------------

        private void ShowDropdown()
        {
            EnsureHostSurface();
            if (_hostSurface == null) return;

            if (dropPanel.Parent != _hostSurface)
            {
                dropPanel.Parent?.Controls.Remove(dropPanel);
                _hostSurface.Controls.Add(dropPanel);
            }

            ApplyRightToLeft();
            RepositionDropdown();
            dropPanel.Visible = true;
            dropPanel.BringToFront();

            HookOutsideClick();
        }

        private void HideDropdown()
        {
            if (dropPanel.Visible)
                dropPanel.Visible = false;
            UnhookOutsideClick();
        }

        private void EnsureHostSurface()
        {
            if (_hostSurface != null) return;
            _hostSurface = this.TopLevelControl ?? this.FindForm();
        }

        private void RepositionDropdown()
        {
            if (dropPanel.Parent == null) return;

            Point screenPoint = TextBox.PointToScreen(new Point(0, TextBox.Height));
            Point relative = dropPanel.Parent.PointToClient(screenPoint);

            int width = DropDownWidth > 0 ? DropDownWidth : this.Width;
            dropPanel.Location = relative;
            dropPanel.Width = width;
        }

        private void ApplyRightToLeft()
        {
            dropPanel.RightToLeft = this.RightToLeft;
            itemsHost.RightToLeft = this.RightToLeft;
        }

        // ---------------- إغلاق القائمة عند الضغط خارجها ----------------

        private void HookOutsideClick()
        {
            if (_outsideClickFilter != null) return;
            _outsideClickFilter = new OutsideClickFilter(this);
            Application.AddMessageFilter(_outsideClickFilter);
        }

        private void UnhookOutsideClick()
        {
            if (_outsideClickFilter == null) return;
            Application.RemoveMessageFilter(_outsideClickFilter);
            _outsideClickFilter = null;
        }

        private bool IsPointInsideControl(Point screenPoint)
        {
            if (dropPanel.Visible)
            {
                var dropRect = new Rectangle(dropPanel.PointToScreen(Point.Empty), dropPanel.Size);
                if (dropRect.Contains(screenPoint)) return true;
            }

            var searchRect = new Rectangle(this.PointToScreen(Point.Empty), this.Size);
            return searchRect.Contains(screenPoint);
        }

        private class OutsideClickFilter : IMessageFilter
        {
            private const int WM_LBUTTONDOWN = 0x0201;
            private readonly CTxtSuggest _owner;

            public OutsideClickFilter(CTxtSuggest owner) { _owner = owner; }

            public bool PreFilterMessage(ref Message m)
            {
                if (m.Msg == WM_LBUTTONDOWN && !_owner.IsPointInsideControl(Cursor.Position))
                {
                    _owner.HideDropdown();
                }
                return false;
            }
        }

    }
}