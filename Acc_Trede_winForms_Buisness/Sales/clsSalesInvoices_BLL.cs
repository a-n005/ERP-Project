using Acc_Trede_winForms_Buisness.Validation.Sales;
using Acc_Trede_winForms_Buisness.Validation;
using Acc_Trede_winForms_DataAccess.Sales;
using Acc_Trade_Core;
using System.Data;
using Global;
using System;

namespace Acc_Trede_winForms_Buisness.Sales
{
    public class clsSalesInvoices_BLL
    {
        public int InvoiceID { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime Date { get; set; }
        public int CreatedBy { get; private set; }
        public int? CustomerID { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal Discount { get; set; }
        public decimal NetAmount => (TotalAmount - Discount) + TaxAmount;
        public decimal CashAmount { get; set; }
        public decimal CardAmount { get; set; }
        public decimal RemainingAmount { get; private set; }
        public DataTable SalesCart { get; set; }

        public clsSalesInvoices_BLL()
        {
            InvoiceID = -1;
            InvoiceNumber = string.Empty;
            CreatedBy = GlobalUser.CurrentUser?.UserID ?? -1;
            CustomerID = null;
            TotalAmount = 0;
            TaxAmount = 0;
            Discount = 0;
            CashAmount = 0;
            CardAmount = 0;
            SalesCart = new DataTable();
        }

        private clsSalesInvoices_BLL(int invoiceID, string invoiceNumber, DateTime date, int createdBy, int? customerID, decimal totalAmount, decimal taxAmount, decimal discount, decimal cashAmount, decimal cardAmount, decimal remaining)
        {
            this.InvoiceID = invoiceID;
            this.InvoiceNumber = invoiceNumber;
            this.Date = date;
            this.CreatedBy = createdBy;
            this.CustomerID = customerID;
            this.TotalAmount = totalAmount;
            this.TaxAmount = taxAmount;
            this.Discount = discount;
            this.CashAmount = cashAmount;
            this.CardAmount = cardAmount;
            this.RemainingAmount = remaining;
        }

        public Result Create()
        {
            Result r = new clsSalesValidator().Validate(this).ToResult();
            if (r.IsFailure)
                return r;

            Result<int> res = clsSalesInvoices_DAL.InsertSalesInvoice(this.InvoiceNumber, this.CreatedBy, this.CustomerID,
                this.TotalAmount, this.Discount, this.TaxAmount, this.CashAmount, this.CardAmount, this.SalesCart);
            if (res.IsFailure)
            {
                return res;
            }
            this.InvoiceID = res.Value;
            return Result.Success();
        }
        public static Result<DataTable> GetAllInvoices() => clsSalesInvoices_DAL.GetAllSalesInvoices();

        public static Result<clsSalesInvoices_BLL> Find(int id)
        {
            Result<DataTable> res = clsSalesInvoices_DAL.FindByID(id);
            if (res.IsFailure)
                return Result<clsSalesInvoices_BLL>.Failure(res.Error);
            if (res.Value == null && res.Value.Rows.Count == 0)
                return Result<clsSalesInvoices_BLL>.Failure("لم يتم العثور على الفاتورة المطلوبة.");

            DataRow dr = res.Value.Rows[0];
            clsSalesInvoices_BLL invoice = MapFromDataRow(dr);

            return Result<clsSalesInvoices_BLL>.Success(invoice);
        }
        private static clsSalesInvoices_BLL MapFromDataRow(DataRow dr)
        {
            return new clsSalesInvoices_BLL(
                invoiceID: Convert.ToInt32(dr["InvoicesID"]),
                invoiceNumber: dr["InvoiceNumber"].ToString(),
                date: Convert.ToDateTime(dr["InvoiceDate"]),
                createdBy: Convert.ToInt32(dr["UserID"]),
                customerID: dr["CustomerID"] == DBNull.Value ? (Int32?)null : Convert.ToInt32(dr["CustomerID"]),
                totalAmount: Convert.ToDecimal(dr["TotalAmount"]),
                taxAmount: Convert.ToDecimal(dr["TaxAmount"]),
                discount: Convert.ToDecimal(dr["Discount"]),
                cashAmount: Convert.ToDecimal(dr["CashAmount"]),
                cardAmount: Convert.ToDecimal(dr["CardAmount"]),
                remaining: Convert.ToDecimal(dr["RemainingAmount"])
                );

        }


    }
}
