using Acc_Trede_winForms_DataAccess;
using Acc_Trede_winForms_DataAccess.Global;
using Global;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acc_Trede_winForms_Buisness
{
    public class clsPaymentVouchers_BLL
    {
        private enum _enMode { Add, Update }
        private _enMode _Mode;
        public int PaymentID { get; set; }
        public string VoucherNum { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; } // Cash , Card
        public int? SupplierID { get; set; }
        public int? CustomerID { get; set; }
        public int? PurchaseID { get; set; }
        public int? SaleReturnID { get; set; }
        public string Notes { get; set; }
        public clsPaymentVouchers_BLL()
        {
            PaymentID = -1;
            _Mode = _enMode.Add;
        }

        private clsPaymentVouchers_BLL(int paymentID, string voucherNum, DateTime transactionDate, decimal amount, string paymentMethod, int? supplierID, int? customerID, int? purchaseID, int? saleReturnID, string notes)
        {
            PaymentID = paymentID;
            VoucherNum = voucherNum;
            TransactionDate = transactionDate;
            Amount = amount;
            PaymentMethod = paymentMethod;
            SupplierID = supplierID;
            CustomerID = customerID;
            PurchaseID = purchaseID;
            SaleReturnID = saleReturnID;
            Notes = notes;
            _Mode = _enMode.Update;
        }

        private Result _Add()
        {
            Result<int> res = clsFinancialTransactions_DAL.InsertPaymentVoucher(this.VoucherNum, this.Amount, this.PaymentMethod, this.SupplierID, this.CustomerID, this.Notes,GlobalUser.CurrentUser.UserID, this.SaleReturnID, this.PurchaseID);
            if (res.IsFailure)
                return Result.Failure(res.Error);
            this.PaymentID = res.Value;
            this._Mode = _enMode.Update;
            return Result.Success();
        }
       // Result _Update()=>clsFinancialTransactions_DAL.
       public Result Save()
        {
            return _Add();
        }
        public static Result<DataTable> GetAll() => clsFinancialTransactions_DAL.GetAllTransactionsPayment();
              public static Result<clsPurchaseInvoices_BLL> Find(int id)
        {
            Result<DataTable> res = clsPurchaseInvoices_DAL.GetPurchaseInvoiceByID(id);
            if (res.IsFailure)
                return Result<clsPurchaseInvoices_BLL>.Failure(res.Error);
            if (res.Value == null && res.Value.Rows.Count == 0)
                return Result<clsPurchaseInvoices_BLL>.Failure("لم يتم العثور على المنتج المطلوب.");

            DataRow dr = res.Value.Rows[0];
            clsPurchaseInvoices_BLL invoice = MapFromDataRow(dr);

            return Result<clsPurchaseInvoices_BLL>.Success(invoice);
        }
        private static clsPurchaseInvoices_BLL MapFromDataRow(DataRow dr)
        {
            return new clsPurchaseInvoices_BLL(
                purchaseID: Convert.ToInt32(dr["PurchaseInvoiceID"]),
                supplierInvoiceNum: dr["SupplierInvoiceNumber"].ToString(),
                createdAt: Convert.ToDateTime(dr["InvoiceDate"]),
                createdBy: Convert.ToInt32(dr["UserID"]),
                supplierID: dr["SupplierID"] == DBNull.Value ? (Int32?)null : Convert.ToInt32(dr["SupplierID"]),
                total: Convert.ToDecimal(dr["TotalAmount"]),
                tax: Convert.ToDecimal(dr["TaxAmount"]),
                discount: Convert.ToDecimal(dr["Discount"]),
                cash: Convert.ToDecimal(dr["CashAmount"]),
                card: Convert.ToDecimal(dr["CardAmount"]),
                remaining: Convert.ToDecimal(dr["RemainingAmount"])
                );

        }
    }
}
