using Acc_Trade_Core;
using Acc_Trede_winForms_DataAccess.Purchases;
using Global;
using System;
using System.Data;

namespace Acc_Trede_winForms_Buisness.Purchases
{
    public class clsPurchaseInvoices_BLL
    {
        enum _enMode { Add, Update };
        _enMode _Mode = _enMode.Add;
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
        public decimal RemainingAmount { get; private set; }
        public DataTable Cart { get; set; }
        public clsPurchaseInvoices_BLL()
        {
            this.PurchaseID = -1;
            this._Mode = _enMode.Add;
        }
        public clsPurchaseInvoices_BLL(int purchaseID, string supplierInvoiceNum, DateTime createdAt, int createdBy, int? supplierID, decimal total, decimal tax, decimal discount, decimal cash, decimal card, decimal remaining)
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
        Result _Add()
        {
            Result<int> res = clsPurchaseInvoices_DAL.InsertPurchaseInvoice(this.SupplierInvoiceNum, GlobalUser.CurrentUser.UserID, this.SupplierID, this.TotalAmount, this.TaxAmount, this.Discount, this.CashAmount, this.CardAmount, this.Cart);
            if (res.IsFailure)
                return Result.Failure(res.Error);
            this.PurchaseID = res.Value;
            this._Mode = _enMode.Update;
            return Result.Success();
        }
        Result _Update() => clsPurchaseInvoices_DAL.UpdateInvoiceWithSupplierID(this.PurchaseID, this.SupplierID);
        public Result Save() => _Mode == _enMode.Add ? _Add() : _Update();
        public static Result<DataTable> GetAllInvoices() => clsPurchaseInvoices_DAL.GetAllPurchaseInvoices();
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
