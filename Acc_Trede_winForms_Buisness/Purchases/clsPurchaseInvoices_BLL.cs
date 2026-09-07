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
    public class clsPurchaseInvoices_BLL
    {
        #region Enums
        enum _enMode { Add, Update };
        #endregion

        #region Fields
        _enMode _Mode = _enMode.Add;
        private clsSuppliers_BLL _supplier;          
        #endregion

        #region Properties
        public int PurchaseID { get; set; }
        public string SupplierInvoiceNum { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public int? SupplierID { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal Discount { get; set; }
        public decimal NetAmount => (TotalAmount - Discount) + TaxAmount;
        public decimal CashAmount { get; set; }
        public decimal CardAmount { get; set; }
        public decimal RemainingAmount { get; set; } //edit like this => NetAmount - (CashAmount + CardAmount); 
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
        public List<clsPurchaseInvoiceDetails_BLL> Cart { get; set; } = new List<clsPurchaseInvoiceDetails_BLL>();
        #endregion

        #region Cunstructors
        public clsPurchaseInvoices_BLL()
        {
            this.PurchaseID = -1;
            this._Mode = _enMode.Add;
        }
        public clsPurchaseInvoices_BLL(int purchaseID, string supplierInvoiceNum, DateTime createdAt, int createdBy, int? supplierID, decimal total, decimal tax, decimal discount, decimal cash, decimal card,decimal remaining)
        {
            this.PurchaseID = purchaseID;
            this.SupplierInvoiceNum = supplierInvoiceNum;
            this.CreatedAt = createdAt;
            this.CreatedBy = createdBy;
            this.SupplierID = supplierID;
            this.TotalAmount = total;
            this.TaxAmount = tax;
            this.Discount = discount;
            this.CashAmount = cash;
            this.CardAmount = card;
            this.RemainingAmount = remaining;
            this._Mode = _enMode.Update;
        }
        #endregion

        #region Private Method
        Result _Add()
        {
            Result r = new clsPurchaseInvoicesValidator(clsPurchaseInvoicesValidator.enMode.ForAdd).Validate(this).ToResult();
            if (r.IsFailure) return r;

            Result<int> res = clsPurchaseInvoices_DAL.InsertPurchaseInvoice(this.SupplierInvoiceNum, GlobalUser.CurrentUser.UserID, this.SupplierID, this.TotalAmount, this.TaxAmount, this.Discount, this.CashAmount, this.CardAmount,ConvertCartToDataTable(this.Cart));
            if (res.IsFailure)
                return Result.Failure(res.Error);
            this.PurchaseID = res.Value;
            this._Mode = _enMode.Update;
            return Result.Success();
        }
        Result _Update()
        {
            Result r = new clsPurchaseInvoicesValidator(clsPurchaseInvoicesValidator.enMode.ForUpdate).Validate(this).ToResult();
            if (r.IsFailure) return r;

            return clsPurchaseInvoices_DAL.UpdateInvoiceWithSupplierID(this.PurchaseID, this.SupplierID);
        }
        #endregion

        #region Public Method
        public Result Save() => _Mode == _enMode.Add ? _Add() : _Update();
        #endregion

        #region Data Retrieval (Queries)
        public static Result<List<clsPurchaseInvoices_BLL>> GetAllInvoices()
        {

            string query = @"SELECT *
                     FROM PurchaseInvoices 
                     ORDER BY InvoiceDate DESC";
            return clsGenericDataAccessBase_DAL.ExecuteReader(query, mapper);
        }
        public static Result<clsPurchaseInvoices_BLL> Find(int id)
        {
            string query = "select * from purchaseInvoices where purchaseInvoiceID = @PurchaseInvoiceID";
            SqlParameter[] sp = { new SqlParameter("@PurchaseInvoiceID", SqlDbType.Int) { Value = id } };
            return clsGenericDataAccessBase_DAL.ExecuteSingle(query, mapper, sp);
        }
        #endregion

        #region Mapping & Helpers
        private  static DataTable ConvertCartToDataTable(List<clsPurchaseInvoiceDetails_BLL> cart)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Barcode", typeof(string));
            dt.Columns.Add("ProductName", typeof(string));
            dt.Columns.Add("Quantity", typeof(int));
            dt.Columns.Add("CostPrice", typeof(decimal));

            if (cart != null)
            {
                foreach (var item in cart)
                {
                    dt.Rows.Add(item.Product?.Barcode, item.Product?.ProductName, item.Quantity, item.UnitPrice);
                }
            }
            return dt;
        }
        private static Func<SqlDataReader, clsPurchaseInvoices_BLL> mapper = reader => new clsPurchaseInvoices_BLL(
   purchaseID: Convert.ToInt32(reader["PurchaseInvoiceID"]),
   supplierInvoiceNum: reader.GetStringSafe("SupplierInvoiceNumber", string.Empty),
   createdAt: Convert.ToDateTime(reader["InvoiceDate"]),
   createdBy: reader.GetNullable<int>("CreatedBy") ?? 0,
   supplierID: reader.GetNullable<int>("SupplierID"),
   total: Convert.ToDecimal(reader["TotalAmount"]),
   tax: Convert.ToDecimal(reader["TaxAmount"]),
   discount: Convert.ToDecimal(reader["Discount"]),
   cash: Convert.ToDecimal(reader["CashAmount"]),
   card: Convert.ToDecimal(reader["CardAmount"]),
   remaining: Convert.ToDecimal(reader["RemainingAmount"])
);
        #endregion
    }
}
