using Acc_Trede_winForms_DataAccess.Finance;
using Acc_Trade_Core;
using Global;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acc_Trede_winForms_Buisness.Finance
{
    public class clsReceiptVouchers_BLL
    {
        private enum _enMode { Add, Update }
        private _enMode _Mode;
        public int ReceiptID { get; set; }
        public string VoucherNum { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; } // Cash , Card
        public int? SupplierID { get; set; }
        public int? CustomerID { get; set; }
        public int? PurchaseReturnID { get; set; }
        public int? SaleID { get; set; }
        public string Notes { get; set; }
        public int CreatedBy { get; set; }
        public clsReceiptVouchers_BLL()
        {
            ReceiptID = -1;
            _Mode = _enMode.Add;
        }

        private clsReceiptVouchers_BLL(int receiptID, string voucherNum, DateTime transactionDate, decimal amount, string paymentMethod, int? supplierID, int? customerID, int? purchaseReturnID, int? saleID, string notes, int createdBy)
        {
            ReceiptID = receiptID;
            VoucherNum = voucherNum;
            TransactionDate = transactionDate;
            Amount = amount;
            PaymentMethod = paymentMethod;
            SupplierID = supplierID;
            CustomerID = customerID;
            PurchaseReturnID = purchaseReturnID;
            SaleID = saleID;
            Notes = notes;
            CreatedBy = createdBy;
            _Mode = _enMode.Update;
        }

        private Result _Add()
        {
            Result<int> res = clsFinancialTransactions_DAL.InsertReceiptVoucher(this.VoucherNum, this.Amount, this.PaymentMethod, this.CustomerID, this.SupplierID, this.Notes, GlobalUser.CurrentUser.UserID, this.SaleID, this.PurchaseReturnID);
            if (res.IsFailure)
                return Result.Failure(res.Error);
            this.ReceiptID = res.Value;
            this._Mode = _enMode.Update;
            return Result.Success();
        }
        // Result _Update()=>clsFinancialTransactions_DAL.
        public Result Save()
        {
            return _Add();
        }
        public static Result<DataTable> GetAll() => clsFinancialTransactions_DAL.GetAllTransactionsPayment();
        public static Result<clsReceiptVouchers_BLL> Find(int id)
        {
            Result<DataTable> res = clsFinancialTransactions_DAL.GetReceiptByReceiptID(id);
            if (res.IsFailure)
                return Result<clsReceiptVouchers_BLL>.Failure(res.Error);
            if (res.Value == null && res.Value.Rows.Count == 0)
                return Result<clsReceiptVouchers_BLL>.Failure("لم يتم العثور على االسند المطلوب.");

            DataRow dr = res.Value.Rows[0];
            clsReceiptVouchers_BLL voucher = MapFromDataRow(dr);

            return Result<clsReceiptVouchers_BLL>.Success(voucher);
        }
        private static clsReceiptVouchers_BLL MapFromDataRow(DataRow dr)
        {
            return new clsReceiptVouchers_BLL(
                receiptID: Convert.ToInt32(dr["ReceiptID"]),
                voucherNum: dr["VoucherNumber"].ToString(),
                transactionDate: Convert.ToDateTime(dr["TransactionDate"]),
                amount: Convert.ToDecimal(dr["Amount"]),
                supplierID: dr["SupplierID"] == DBNull.Value ? (Int32?)null : Convert.ToInt32(dr["SupplierID"]),
                customerID: dr["CustomerID"] == DBNull.Value ? (Int32?)null : Convert.ToInt32(dr["CustomerID"]),
                paymentMethod: dr["PaymentMethod"].ToString(),
                purchaseReturnID: dr["PurchaseReturnID"] == DBNull.Value ? (Int32?)null : Convert.ToInt32(dr["PurchaseReturnID"]),
                saleID: dr["SalesInvoiceID"] == DBNull.Value ? (Int32?)null : Convert.ToInt32(dr["SalesInvoiceID"]),
                notes: dr["Notes"].ToString(),
                createdBy: Convert.ToInt32(dr["UserID"])
                );

        }
    }
}
