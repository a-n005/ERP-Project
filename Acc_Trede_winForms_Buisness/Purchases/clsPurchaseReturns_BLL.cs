using Acc_Trade_Core;
using Acc_Trede_winForms.Entities;
using Acc_Trede_winForms_Buisness.Global;
using Acc_Trede_winForms_Buisness.Validation;
using Acc_Trede_winForms_Buisness.Validation.Purchases;
using Acc_Trede_winForms_DataAccess.Global;
using Acc_Trede_winForms_DataAccess.Purchases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Acc_Trede_winForms_Buisness.Purchases
{
    public class clsPurchaseReturns_BLL
    {
        // add later check for quantity when he return twise
        #region Enums
        #endregion

        #region Fields
        private clsSuppliers_BLL _supplier;
        #endregion

        #region Proparties
        public int ReturnID { get; set; }
        public string ReturnNumber { get; set; }
        public int PurchaseInvoiceID { get; set; }
        public int? SupplierID { get; set; }
        public DateTime ReturnDate { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public string Notes { get; set; }
        public int UserID { get; set; }
        public decimal RemainingAmount { get; set; }
        public decimal? CashAmount { get; set; }
        public decimal? CardAmount { get; set; }
        public decimal NetAmount => TotalAmount + TaxAmount;
        public clsSuppliers_BLL Supplier
        {
            get
            {
                if (_supplier == null && SupplierID.HasValue)
                {
                    var result = clsSuppliers_BLL.Find(SupplierID.Value);
                    if (result.IsSuccess)
                    {
                        _supplier = result.Value;
                    }
                }
                return _supplier;
            }
        }
        public List<clsPurchaseReturnDetails_BLL> Cart { get; set; }
        #endregion

        #region Constructors
        public clsPurchaseReturns_BLL()
        {
            this.ReturnID = -1;
            this.ReturnNumber = string.Empty;
            this.PurchaseInvoiceID = -1;
            this.SupplierID = null;
            this.TotalAmount = 0m;
            this.TaxAmount = 0m;
            this.Notes = string.Empty;
            this.UserID = GlobalUser.CurrentUser?.UserID ?? -1;
            this.RemainingAmount = 0m;
            this.CardAmount = null;
            this.CashAmount = null;
            this.Cart = new List<clsPurchaseReturnDetails_BLL>();
        }

        private clsPurchaseReturns_BLL(int returnID, string returnNumber, int purchaseInvoiceID, int? supplierID,
            DateTime returnDate, decimal totalAmount, decimal taxAmount, string notes, int userID, decimal remainingAmount,
            decimal? cashAmount, decimal? cardAmount)
        {
            ReturnID = returnID;
            ReturnNumber = returnNumber;
            PurchaseInvoiceID = purchaseInvoiceID;
            SupplierID = supplierID;
            ReturnDate = returnDate;
            TotalAmount = totalAmount;
            TaxAmount = taxAmount;
            Notes = notes;
            UserID = userID;
            RemainingAmount = remainingAmount;
            CashAmount = cashAmount;
            CardAmount = cardAmount;
        }
        #endregion

        #region Private Method
        private Result _Add()
        {
            Result r = new clsPurchaseReturnsValidator().Validate(this).ToResult();
            if (r.IsFailure) return r;

            Result<int> res = clsPurchaseReturn_DAL.InsertPurchaseReturn(this.PurchaseInvoiceID, this.ReturnNumber, this.SupplierID, this.TotalAmount, this.TaxAmount, this.Notes, GlobalUser.CurrentUser.UserID, this.CashAmount, this.CardAmount, ConvertCartToDataTable(this.Cart));
            if (res.IsFailure)
                return Result.Failure(res.Error);
            this.ReturnID = res.Value;
            return Result.Success();
        }
        #endregion

        #region Public Method
        public Result Save() => _Add();
        #endregion

        #region Data Retrieval (Queries)
        public static Result<List<clsPurchaseReturns_BLL>> GetAllInvoices()
        {
            string query = @"SELECT  * FROM  PurchaseReturns  ORDER BY ReturnDate DESC";
            return clsGenericDataAccessBase_DAL.ExecuteReader(query, mapper);
        }

        public static Result<clsPurchaseReturns_BLL> FindByReturnID(int returnId)
        {
            string query = "select * from SalesReturns where ReturnID=@ID";
            SqlParameter[] sp = { new SqlParameter("@ID", SqlDbType.Int) { Value = returnId } };
            return clsGenericDataAccessBase_DAL.ExecuteSingle(query, mapper, sp);
        }
        public static Result<clsPurchaseReturns_BLL> FindByOriginalID(int originalID)
        {
            string query = "select * from PurchaseReturns where PurchaseInvoiceID=@ID";
            SqlParameter[] sp = { new SqlParameter("@ID", SqlDbType.Int) { Value = originalID } };
            return clsGenericDataAccessBase_DAL.ExecuteSingle(query, mapper, sp);
        }
        #endregion

        #region Mapping & Helpers
        private static DataTable ConvertCartToDataTable(List<clsPurchaseReturnDetails_BLL> cart)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Barcode", typeof(string));
            dt.Columns.Add("ProductName", typeof(string));
            dt.Columns.Add("Quantity", typeof(decimal));
            dt.Columns.Add("CostPrice", typeof(decimal));
            dt.Columns.Add("DetailID", typeof(int));

            if (cart != null)
            {
                foreach (var item in cart)
                {
                    dt.Rows.Add(item.Product?.Barcode, item.Product?.ProductName, item.Quantity, item.UnitPrice,item.DetailID);
                }
            }
            return dt;
        }
        private static Func<SqlDataReader, clsPurchaseReturns_BLL> mapper = reader => new clsPurchaseReturns_BLL(
   returnID: Convert.ToInt32(reader["ReturnID"]),
   returnNumber: reader.GetStringSafe("ReturnNumber", string.Empty),
   purchaseInvoiceID: Convert.ToInt32(reader["PurchaseInvoiceID"]),
   supplierID: reader.GetNullable<int>("SupplierID"),
   returnDate: Convert.ToDateTime(reader["ReturnDate"]),
   totalAmount: Convert.ToDecimal(reader["TotalAmount"]),
   taxAmount: Convert.ToDecimal(reader["TaxAmount"]),
   notes: reader.GetStringSafe("Notes") ?? string.Empty,
   userID: reader.GetNullable<int>("UserID") ?? 0,
   remainingAmount: Convert.ToDecimal(reader["RemainingAmount"]),
   cashAmount: Convert.ToDecimal(reader["CashAmount"]),
   cardAmount: Convert.ToDecimal(reader["CardAmount"])
);
        #endregion
    }
}
