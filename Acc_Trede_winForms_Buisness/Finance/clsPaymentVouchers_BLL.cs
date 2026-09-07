using Acc_Trade_Core;
using Acc_Trede_winForms_DataAccess.Finance;
using Acc_Trede_winForms_Buisness.Validation;
using Acc_Trede_winForms_Buisness.Validation.Finance;
using Acc_Trede_winForms_Buisness.Global;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Acc_Trede_winForms_DataAccess.Global;

namespace Acc_Trede_winForms_Buisness.Finance
{
    public class clsPaymentVouchers_BLL
    {
        #region Enums
        private enum _enMode { Add, Update }
        #endregion

        #region Fields
        private _enMode _Mode;
        #endregion

        #region Properties
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
        #endregion

        #region Constructors
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
        #endregion

        #region Private Method
        private Result _Add()
        {
            Result r = new clsPaymentValidator().Validate(this).ToResult();
            if (r.IsFailure) return r;

            Result<int> res = clsFinancialTransactions_DAL.InsertPaymentVoucher(this.VoucherNum, this.Amount, this.PaymentMethod, this.SupplierID, this.CustomerID, this.Notes, GlobalUser.CurrentUser.UserID, this.SaleReturnID, this.PurchaseID);
            if (res.IsFailure)
                return Result.Failure(res.Error);
            this.PaymentID = res.Value;
            this._Mode = _enMode.Update;
            return Result.Success();
        }
        // Result _Update()=>clsFinancialTransactions_DAL.
        #endregion

        #region Public Method
        public Result Save() => _Add();
        #endregion

        #region Data Retrieval (Queries)
        public static Result<List<clsPaymentVouchers_BLL>> GetAll()
        {
            string query = @"select * from PaymentVouchers ORDER BY TransactionDate DESC";
            return clsGenericDataAccessBase_DAL.ExecuteReader(query, Mapper);
        }
        public static Result<clsPaymentVouchers_BLL> Find(int id)
        {
            string query = @"SELECT * FROM paymentVouchers  WHERE PaymentID = @paymentID";
            SqlParameter[] parms = { new SqlParameter("@paymentID", SqlDbType.Int) { Value = id } };
            return clsGenericDataAccessBase_DAL.ExecuteSingle(query, Mapper, parms);
        }
        #endregion

        #region Mapping & Helpers
        private static Func<SqlDataReader, clsPaymentVouchers_BLL> Mapper = reader => new clsPaymentVouchers_BLL(
           paymentID: Convert.ToInt32(reader["PaymentID"]),
           voucherNum: reader["VoucherNumber"] != DBNull.Value ? reader["VoucherNumber"].ToString() : string.Empty,
           transactionDate: Convert.ToDateTime(reader["TransactionDate"]),
           amount: Convert.ToDecimal(reader["Amount"]),
           supplierID: reader["SupplierID"] == DBNull.Value ? (Int32?)null : Convert.ToInt32(reader["SupplierID"]),
           customerID: reader["CustomerID"] == DBNull.Value ? (Int32?)null : Convert.ToInt32(reader["CustomerID"]),
           paymentMethod: reader["PaymentMethod"] != DBNull.Value ? reader["PaymentMethod"].ToString() : string.Empty,
           purchaseID: reader["PurchaseInvoiceID"] == DBNull.Value ? (Int32?)null : Convert.ToInt32(reader["PurchaseInvoiceID"]),
           saleReturnID: reader["SalesReturnID"] == DBNull.Value ? (Int32?)null : Convert.ToInt32(reader["SalesReturnID"]),
           notes: reader["Notes"] != DBNull.Value ? reader["Notes"].ToString() : null,
           createdBy: Convert.ToInt32(reader["UserID"])
       );
        #endregion
    }
}
