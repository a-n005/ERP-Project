using Acc_Trede_winForms.Models.CMessageBox;
using Acc_Trede_winForms.Models.cPanel;
using Acc_Trede_winForms.Models.cSuggestTextBox;
using Acc_Trede_winForms.Properties;
using Acc_Trede_winForms_Buisness.Inventory;
using Acc_Trede_winForms_Buisness.Sales;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Acc_Trede_winForms.Cashier
{
    public partial class ucCashier : UserControl
    {
        private List<clsProducts_BLL> products = new List<clsProducts_BLL>();
        private List<SuggestionItem> suggestion = new List<SuggestionItem>();
        private BindingList<clsSalesInvoiceDetails_BLL> cart = new BindingList<clsSalesInvoiceDetails_BLL>();
        public ucCashier()
        {
            InitializeComponent();
        }
        private clsSalesInvoiceDetails_BLL toAdd(int quantity, decimal unitPrice, clsProducts_BLL product) =>
             new clsSalesInvoiceDetails_BLL(quantity, unitPrice, product);

        private void ucCashier_Load(object sender, EventArgs e)
        {
            dgvOnLoad();
            leftOnLoad();
            TopOnLoad();




        }

        #region DGV 

        private void dgvOnLoad()
        {
            refresh();

            dgvCart.Grid.ReadOnly = false;

            SetDGVLayout(dgvCart.Grid);

            dgvCart.Grid.CellParsing += (s, e) =>
            {
                string colName = dgvCart.Grid.Columns[e.ColumnIndex].Name;

                if ((colName == "Quantity" || colName == "SalePrice")
                    && string.IsNullOrWhiteSpace(e.Value?.ToString()))
                {
                    e.Value = dgvCart.Grid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    e.ParsingApplied = true;
                }
            };

            dgvCart.Grid.EditingControlShowing += (s, e) =>
            {
                string colName = dgvCart.Grid.Columns[dgvCart.Grid.CurrentCell.ColumnIndex].Name;

                if (e.Control is TextBox txt)
                {
                    txt.KeyPress -= Cell_KeyPress;

                    if (colName == "Quantity" || colName == "SalePrice")
                    {
                        txt.KeyPress += Cell_KeyPress;
                    }
                }
            };

            dgvCart.ActionButtonClick += (s, e) =>
            {
                string barcode = dgvCart.Grid.CurrentRow.Cells["Barcode"].Value?.ToString();
                var itemRemove = cart.FirstOrDefault(c => c.Barcode == barcode);
                if (itemRemove != null)
                    cart.Remove(itemRemove);

                UpdateDetails();
            };
        }

        #endregion

        #region Left Panel

        private void leftOnLoad()
        {
            pDetails.ApplyBorder(r: true);

            dgvCart.Grid.CellValueChanged += (s, e) =>
            {
                UpdateDetails();
            };


        }

        #endregion

        #region Top Panel

        private void TopOnLoad()
        {
            pTop.ApplyBorder(b: true);

            txtSearch.SetDataSource(suggestion);
            // edit how to add to save 
            txtSearch.ItemSelected += (s, ev) =>
            {
                txtSearch.Clear();
                var p = ev.Tag as clsProducts_BLL;
                cart.Add(new clsSalesInvoiceDetails_BLL(1, p.SalePrice, p));
                UpdateDetails();
                dgvCart.Focus();
                txtSearch.Focus();
            };
            btnAdd.Click += (s, ev) =>
            {
                var x = cart.AsEnumerable();
                //List<clsSalesInvoiceDetails_BLL> c = (List<clsSalesInvoiceDetails_BLL>)dgvCart.Grid.DataSource;
                //string x = "";
            };
        }

        #endregion

        #region Helpers

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

            ToHideColumns(g, clsSalesInvoiceDetails_BLL.HideColumns);


            if (g.Columns.Contains("Barcode"))
            {
                g.Columns["Barcode"].HeaderText = "الباركود";
                g.Columns["Barcode"].DisplayIndex = 1;
                g.Columns["Barcode"].ReadOnly = true;
            }

            if (g.Columns.Contains("ProductName"))
            {
                g.Columns["ProductName"].HeaderText = "اسم المنتج";
                g.Columns["ProductName"].DisplayIndex = 2;
                g.Columns["ProductName"].ReadOnly = true;
            }

            if (g.Columns.Contains("UnitPrice"))
            {
                g.Columns["UnitPrice"].HeaderText = "السعر";
                g.Columns["UnitPrice"].DefaultCellStyle.Format = "N2";
                g.Columns["UnitPrice"].DisplayIndex = 3;
                g.Columns["UnitPrice"].ReadOnly = false;
            }
            if (g.Columns.Contains("Tax"))
            {
                g.Columns["Tax"].HeaderText = "الضريبة";
                g.Columns["Tax"].DisplayIndex = 4;
                g.Columns["Tax"].ReadOnly = true;
                g.Columns["Tax"].DefaultCellStyle.Format = "N2";
            }

            g.Columns.Remove("Quantity");
            dgvCart.AddSpinnerColumn("Quantity", "الكمية", 4, 1);

            if (g.Columns.Contains("LineTotal"))
            {
                g.Columns["LineTotal"].HeaderText = "الإجمالي";
                g.Columns["LineTotal"].DisplayIndex = 6;
                g.Columns["LineTotal"].ReadOnly = true;
                g.Columns["LineTotal"].DefaultCellStyle.Format = "N2";
            }
            dgvCart.AddActionButton("Remove", "", icon: Resources.Delete);

            dgvCart.FixActionButtonsPosition();

        }

        private void UpdateDetails()
        {
            lblTax.Text = cart.Sum(sx => sx.Tax).ToString("N2");

            var discount = 0m;
            var total = cart.Sum(sx => sx.LineTotal);
            txtDiscount._TextChanged += (s, e) =>
            {
                if (!string.IsNullOrWhiteSpace(txtDiscount.Text))
                {
                    discount = Convert.ToDecimal(txtDiscount.Text);
                    CalculateInvoiceTotals(discount);
                    if (discount > total)
                        errorProvider1.SetError(txtDiscount, "لا يمكن اضافة خصم اكبر من الإجمالي.");
                    else
                        errorProvider1.SetError(txtDiscount, "");
                }
            };
            // when he leave the txt return value to 0.00 if he set dis grather than total
            // key press to pass digit and controls only 
            // set the cul 
            lblTotal.Text = (total).ToString("N2");

        }

        private void refresh()
        {
            var l = clsProducts_BLL.GetAllProducts();
            if (l.IsFailure)
                CMsgB.Show("", l.Error, false);

            products = l.Value;
            suggestion = products.Select(p => new SuggestionItem { Title = p.ProductName, SubTitle = p.Barcode, Tag = p }).ToList();
            dgvCart.Grid.DataSource = cart;
        }
        private void Cell_KeyPress(object sender, KeyPressEventArgs e)
        {
            string colName = dgvCart.Grid.Columns[dgvCart.Grid.CurrentCell.ColumnIndex].Name;

            if (char.IsControl(e.KeyChar)) return;

            bool isInvalid = false;
            string txtEror = "يجب ادخال ارقام فقط.";
            if (colName == "Quantity" && !char.IsDigit(e.KeyChar))
            {
                isInvalid = true;
            }
            else if (colName == "UnitPrice")
            {
                TextBox txt = sender as TextBox;
                if ((e.KeyChar == '.' && txt.Text.Contains(".")))
                {
                    isInvalid = true;
                    txtEror = "لا يمكن وضع اكثر من فاصله.";
                }
                if ((!char.IsDigit(e.KeyChar) && e.KeyChar != '.'))
                {
                    isInvalid = true;
                }
            }

            if (isInvalid)
            {
                e.Handled = true;

                if (sender is Control ctrl)
                {
                    toolTip1.Show(txtEror, ctrl, 0, -30, 1500);
                }
            }
        }

        private void CalculateInvoiceTotals(decimal globalDiscount)
        {
            // 1. حساب إجمالي الفاتورة قبل الخصم (Subtotal)
            decimal subtotal = cart.Sum(item => item.LineTotal);

            // حماية ضد القسمة على صفر
            if (subtotal <= 0)
            {
                lblTotal.Text = "0.00";
                txtDiscount.Text = "0.00";
                lblTax.Text = "0.00";
                lblNetTotal.Text = "0.00";
                return;
            }

            decimal totalTax = 0;
            decimal totalNetAfterDiscount = 0;

            foreach (var item in cart)
            {
                // إجمالي السطر الحالي
                decimal lineTotal = item.Quantity * item.UnitPrice;

                // نصيب هذا السطر من الخصم العام (توزيع تناسبي)
                decimal lineShareOfDiscount = (lineTotal / subtotal) * globalDiscount;

                // صافي السطر بعد الخصم المخصص له
                decimal lineNet = lineTotal - lineShareOfDiscount;
                totalNetAfterDiscount += lineNet;

                // ضريبة هذا السطر بناءً على صافي بعد الخصم
                decimal lineTax = lineNet * 0.15m;
                totalTax += lineTax;
            }

            // الإجمالي النهائي الشامل للضريبة
            decimal grandTotal = totalNetAfterDiscount + totalTax;

            // عرض النتائج في الواجهة
            lblTotal.Text = subtotal.ToString("N2");
            txtDiscount.Text = globalDiscount.ToString("N2");
            lblTax.Text = totalTax.ToString("N2");
            lblNetTotal.Text = grandTotal.ToString("N2");
        }
        #endregion
    }
}
