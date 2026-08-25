using Acc_Trade_Core;
using Acc_Trede_winForms_DataAccess.Purchases;
using Global;
using System;
using System.Data;

namespace Acc_Trede_winForms_Buisness.Purchases
{
    public class clsPurchaseReturns_BLL
    {
        // add later check for quantity when he return twise

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
        public DataTable Cart { get; set; }
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
            this.Cart = new DataTable();
        }
        private Result _Add()
        {
            Result<int> res = clsPurchaseReturn_DAL.InsertPurchaseReturn(this.PurchaseInvoiceID, this.ReturnNumber, this.SupplierID, this.TotalAmount, this.TaxAmount, this.Notes, GlobalUser.CurrentUser.UserID, this.CashAmount, this.CardAmount, this.Cart);
            if (res.IsFailure)
                return Result.Failure(res.Error);
            this.ReturnID = res.Value;
            return Result.Success();
        }
        public Result Save() => _Add();
    }
}
