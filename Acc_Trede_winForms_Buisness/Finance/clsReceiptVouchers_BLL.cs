using Acc_Trade_Core;
using Acc_Trede_winForms_DataAccess.Finance;
using Acc_Trede_winForms_Buisness.Validation;
using Acc_Trede_winForms_Buisness.Validation.Finance;
using System;
using System.Data;
using System.Data.SqlClient;
using Acc_Trede_winForms_DataAccess.Global;
using System.Collections.Generic;
using Acc_Trede_winForms_Buisness.Global;

namespace Acc_Trede_winForms_Buisness.Finance
{
    public class clsReceiptVouchers_BLL
    {
        #region Enums
        private enum _enMode { Add, Update }
        #endregion

        #region Fields
        private _enMode _Mode;
        #endregion
        
        #region Properties
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
        #endregion
        
        #region Constructors
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
        #endregion
        
        #region Private Method
        private Result _Add()
        {
            Result r = new clsReceiptValidator().Validate(this).ToResult();
            if (r.IsFailure) return r;

            Result<int> res = clsFinancialTransactions_DAL.InsertReceiptVoucher(this.VoucherNum, this.Amount, this.PaymentMethod, this.CustomerID, this.SupplierID, this.Notes, GlobalUser.CurrentUser.UserID, this.SaleID, this.PurchaseReturnID);
            if (res.IsFailure)
                return Result.Failure(res.Error);
            this.ReceiptID = res.Value;
            this._Mode = _enMode.Update;
            return Result.Success();
        }
        // Result _Update()=>clsFinancialTransactions_DAL.
        #endregion
        
        #region Public Method
        public Result Save() => _Add();
        #endregion
        
        #region Data Retrieval (Queries)
        public static Result<List<clsReceiptVouchers_BLL>> GetAll()
        {
            string query = @"select * from ReceiptVouchers ORDER BY TransactionDate DESC";
            return clsGenericDataAccessBase_DAL.ExecuteReader(query, Mapper);
        }
        public static Result<clsReceiptVouchers_BLL> Find(int id)
        {
            string query = @"SELECT * FROM receiptVouchers  WHERE receiptID = @receiptID";
            SqlParameter[] parameters = { new SqlParameter("@receiptID", SqlDbType.Int) { Value = id } };
            return clsGenericDataAccessBase_DAL.ExecuteSingle(query, Mapper, parameters);
        }
        #endregion
        
        #region Mapping & Helpers
        private static Func<SqlDataReader, clsReceiptVouchers_BLL> Mapper = reader => new clsReceiptVouchers_BLL(
            receiptID: Convert.ToInt32(reader["ReceiptID"]),
            voucherNum: reader.GetStringSafe("VoucherNumber", string.Empty),
            transactionDate: Convert.ToDateTime(reader["TransactionDate"]),
            amount: Convert.ToDecimal(reader["Amount"]),
            supplierID: reader.GetNullable<int>("SupplierID"),
            customerID: reader.GetNullable<int>("CustomerID"),
            paymentMethod: reader.GetStringSafe("PaymentMethod", string.Empty),
            purchaseReturnID: reader.GetNullable<int>("PurchaseReturnID"),
            saleID: reader.GetNullable<int>("SalesInvoiceID"),
            notes: reader.GetStringSafe("Notes"),
            createdBy: Convert.ToInt32(reader["UserID"])
        );
        #endregion
    }
}
