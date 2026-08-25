using Acc_Trade_Core;
using Acc_Trede_winForms_DataAccess.Finance;
using Global;
using System;
using System.Data;

namespace Acc_Trede_winForms_Buisness.Finance
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
        public int CreatedBy { get; set; }
        public clsPaymentVouchers_BLL()
        {
            PaymentID = -1;
            _Mode = _enMode.Add;
        }

        private clsPaymentVouchers_BLL(int paymentID, string voucherNum, DateTime transactionDate, decimal amount, string paymentMethod, int? supplierID, int? customerID, int? purchaseID, int? saleReturnID, string notes, int createdBy)
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
            CreatedBy = createdBy;
            _Mode = _enMode.Update;
        }

        private Result _Add()
        {
            Result<int> res = clsFinancialTransactions_DAL.InsertPaymentVoucher(this.VoucherNum, this.Amount, this.PaymentMethod, this.SupplierID, this.CustomerID, this.Notes, GlobalUser.CurrentUser.UserID, this.SaleReturnID, this.PurchaseID);
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
        public static Result<clsPaymentVouchers_BLL> Find(int id)
        {
            Result<DataTable> res = clsFinancialTransactions_DAL.GetPaymentByPaymentID(id);
            if (res.IsFailure)
                return Result<clsPaymentVouchers_BLL>.Failure(res.Error);
            if (res.Value == null && res.Value.Rows.Count == 0)
                return Result<clsPaymentVouchers_BLL>.Failure("لم يتم العثور على االسند المطلوب.");

            DataRow dr = res.Value.Rows[0];
            clsPaymentVouchers_BLL voucher = MapFromDataRow(dr);

            return Result<clsPaymentVouchers_BLL>.Success(voucher);
        }
        private static clsPaymentVouchers_BLL MapFromDataRow(DataRow dr)
        {
            return new clsPaymentVouchers_BLL(
                paymentID: Convert.ToInt32(dr["PaymentID"]),
                voucherNum: dr["VoucherNumber"].ToString(),
                transactionDate: Convert.ToDateTime(dr["TransactionDate"]),
                amount: Convert.ToDecimal(dr["Amount"]),
                supplierID: dr["SupplierID"] == DBNull.Value ? (Int32?)null : Convert.ToInt32(dr["SupplierID"]),
                customerID: dr["CustomerID"] == DBNull.Value ? (Int32?)null : Convert.ToInt32(dr["CustomerID"]),
                paymentMethod: dr["PaymentMethod"].ToString(),
                purchaseID: dr["PurchaseInvoiceID"] == DBNull.Value ? (Int32?)null : Convert.ToInt32(dr["PurchaseInvoiceID"]),
                saleReturnID: dr["SalesReturnID"] == DBNull.Value ? (Int32?)null : Convert.ToInt32(dr["SalesReturnID"]),
                notes: dr["Notes"].ToString(),
                createdBy: Convert.ToInt32(dr["UserID"])
                );

        }
    }
}
