using Acc_Trade_Core;
using Acc_Trede_winForms.Models.CDGV;
using Acc_Trede_winForms.Models.CMessageBox;
using Acc_Trede_winForms.Models.cPanel;
using Acc_Trede_winForms.Models.CScrollB;
using Acc_Trede_winForms.Properties;
using Acc_Trede_winForms_Buisness.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Acc_Trede_winForms.Entities
{
    public partial class ucCustomers : UserControl
    {
        #region Constructors & Fields
        List<clsCustomers_BLL> _originalCustomersList = new List<clsCustomers_BLL>();
        clsCustomers_BLL _customer;
        private bool isActive = false;
        public event Action<string, int> ActionButtonClick;
        public ucCustomers()
        {
            InitializeComponent();
            this.Resize += (s, e) => CenterAddPanel();
        }
        #endregion

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
            pAdd.ApplyBorder(borderRadius: 10, b: true, t: true);
            pAdd.IstBackgroundDisabled=true;

            txtCustomerName.Validating += (s, ev) =>
            {
                if (string.IsNullOrWhiteSpace(txtCustomerName.Text.Trim()))
                    errorProvider1.SetError(txtCustomerName, $"خطأ: يجب ادخال اسم المستخدم.");
                else
                    errorProvider1.SetError(txtCustomerName, "");
            };
            txtPhone.KeyPress += (s, ev) =>
            {
                if (!char.IsDigit(ev.KeyChar) && !char.IsControl(ev.KeyChar))
                {
                    errorProvider1.SetError(txtPhone, "خطأ: يجب إدخال أرقام فقط.");
                    ev.Handled = true;
                }
                else
                    errorProvider1.SetError(txtPhone, "");
            };
            cbActive.KeyPress += (s, ev) =>
            {
                if (ev.KeyChar == (char)Keys.Enter)
                    cbActive.Checked = !cbActive.Checked;
            };
            pAdd.EscapePressed += (s, ev) => btnX.PerformClick();
        }
        private void btnAddCustomersClick(object s, EventArgs e)
        {
            if (!this.ValidateChildren()) return;

            if (pAdd.Tag.ToString() == "add")
            {
                _customer.CustomerName = txtCustomerName.Text.Trim();
                _customer.Phone = txtPhone.Text.Trim() == "" ? null : txtPhone.Text.Trim();
                _customer.TaxNumber = txtTaxNumber.Text.Trim() == "" ? null : txtTaxNumber.Text.Trim();
                var r = _customer.Save();

                if (r.IsFailure)
                { CMsgB.Show("خطأ", r.Error, showCancelButton: false); return; }
                CMsgB.Show("ناجح", "تم إضافة العميل بنجاح.", 0);
            }
            else // for update 
            {
                _customer.IsActive = cbActive.Checked;
                _customer.CustomerName = txtCustomerName.Text.Trim();
                _customer.Phone = txtPhone.Text.Trim();
                _customer.TaxNumber = txtTaxNumber.Text.Trim();
                var r = _customer.Save();

                if (r.IsFailure)
                { CMsgB.Show("خطأ", r.Error, showCancelButton: false); return; }
                CMsgB.Show("ناجح", "تم تحديث العميل بنجاح.", 0);
            }
            refresh();
            ResetAndCloseAddPanel();
        }
        private void btnX_Click(object sender, EventArgs e)
        {
            ResetAndCloseAddPanel();
        }
        #endregion

        #region Panel Top
        private void pTopInLoad()
        {
            pTop.Paint += (s, e) => pAdd.p_Paint(pTop, e, null, 2, false, true);
            cToggleSwitch1.CheckedChanged += (s, e) => { isActive = !isActive; ApplyFilter(); };
        }
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }
        private void btnAddClick(object s, EventArgs e)
        {
            if (pAdd.Visible)
                return;

            _customer = new clsCustomers_BLL();
            lblTitel.Text = "إضافة عميل";
            btnAddCustomers.Text = "إضافة العميل";
            pAdd.Tag = "add";

            ResetAndCloseAddPanel();

            pAdd.Visible = true;
            pAdd.BringToFront();

            this.BeginInvoke((Action)(() => txtCustomerName.Focus()));
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            refresh();
        }
        #endregion

        #region Panel Bottom

        private void pBottomInLoad()
        {
            pBottom.Paint += (s, e) => pAdd.p_Paint(pBottom, e, t: true);
        }

        private void refreshRecords() => lblRecords.Text = dgvCustomers.Count > 0 ? $"العملاء: {dgvCustomers.Count}" : "العملاء: 0";

        #endregion

        #region Data Grid View 

        private void dgvLoad()
        {

            dgvCustomers.AddActionButton("Edit", "", icon: Resources.Edit_Pencil, isLeftmost: true);
            dgvCustomers.AddActionButton("Delete", "", icon: Resources.Delete, isLeftmost: true);
            dgvCustomers.Dock = DockStyle.Fill;
            dgvCustomers.UpdateScrollBar();

            dgvCustomers.ActionButtonClick += (s, e) =>
            {
                switch (s)
                {
                    case "Edit":
                        _Update(Find());
                        //dgvCustomers.Grid.ClearSelection();
                        //dgvCustomers.Grid.CurrentCell = null;
                        break;
                    case "Delete": _Delete(Find()); break;
                    default:
                        break;
                }
            };

            refresh();
        }

        private void refresh()
        {
            Result<List<clsCustomers_BLL>> r = clsCustomers_BLL.GetAllCustomers();
            if (r.IsFailure)
            {
                CMsgB.Show("", r.Error, 0);
                return;
            }
            _originalCustomersList = r.Value.OrderBy(c => c.CustomerID).ToList();
            ApplyFilter();
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
            ToHideColumns(g, "CreatedAt", "UpdatedAt", "CreatedBy", "UpdatedBy", "LastUpdate");

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

            dgvCustomers.FixActionButtonsPosition();
        }

        #endregion

        #region Helpers

        private void _Update(Result<clsCustomers_BLL> r)
        {
            if (pAdd.Visible)
                return;

            ClearErrorState();
            if (r.IsFailure)
            { CMsgB.Show("", r.Error); return; }
            _customer = r.Value;

            txtPhone.Text = _customer.Phone;
            txtTaxNumber.Text = _customer.TaxNumber;
            txtCustomerName.Text = _customer.CustomerName;
            cbActive.Checked = _customer.IsActive;

            lblTitel.Text = "تحديث عميل";
            btnAddCustomers.Text = "تحديث العميل";
            pAdd.Tag = "update";

            cbActive.Visible = true;
            pAdd.Visible = true;
            pAdd.BringToFront();
            txtCustomerName.Focus();
        }

        private Result<clsCustomers_BLL> Find()
        {
            return clsCustomers_BLL.Find(Convert.ToInt32(dgvCustomers.Grid.CurrentRow.Cells["CustomerID"].Value));
        }

        private void _Delete(Result<clsCustomers_BLL> r)
        {
            string s = dgvCustomers.Grid.CurrentRow.Cells["CustomerName"].Value.ToString();
            if (CMsgB.Show("تنبية!!!", $"هل انت متأكد من حذف العميل \u200F{{ {s} }}\u200F ؟", isYN: true) == DialogResult.OK)
            {
                if (dgvCustomers.Grid.CurrentRow == null) return;

                if (r.IsFailure)
                {
                    CMsgB.Show("خطأ", r.Error);
                    return;
                }

                var deleteResult = r.Value.Delete();

                if (deleteResult.IsFailure)
                {
                    CMsgB.Show("خطأ", deleteResult.Error);
                    return;
                }

                CMsgB.Show("ناجح", $"تم حذف العميل \u200F{{ {s} }}\u200F بنجاح", 0);
                btnRefresh.PerformClick();
            }
        }

        private void ApplyFilter()
        {
            string searchText = txtSearch.Text.Trim().ToLower();

            var filteredList = _originalCustomersList.AsEnumerable();

            filteredList = this.isActive ? filteredList.Where(x => x.IsActive) : filteredList;

            if (!string.IsNullOrEmpty(searchText))
            {
                filteredList = filteredList.Where(c =>
                  (!string.IsNullOrEmpty(c.CustomerName) && c.CustomerName.ToLower().Contains(searchText)) ||
                  (!string.IsNullOrEmpty(c.Phone) && c.Phone.Contains(searchText)) ||
                  (!string.IsNullOrEmpty(c.TaxNumber) && c.TaxNumber.Contains(searchText))
                );
            }

            var resultList = filteredList.OrderBy(c => c.CustomerID).ToList();

            dgvCustomers.Grid.DataSource = resultList;
            SetDGVLayout(dgvCustomers.Grid);
            refreshRecords();
            dgvCustomers.UpdateScrollBar();
        }

        private void ClearErrorState()
        {
            errorProvider1.SetError(txtCustomerName, "");
            errorProvider1.SetError(txtPhone, "");
        }

        private void ResetAndCloseAddPanel()
        {
            ClearErrorState();

            txtCustomerName.Clear();
            txtPhone.Clear();
            txtTaxNumber.Clear();

            pAdd.Visible = false;
            cbActive.Visible = false;
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

    }
}
