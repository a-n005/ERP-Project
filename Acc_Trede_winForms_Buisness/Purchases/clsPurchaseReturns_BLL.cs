using Acc_Trade_Core;
using Acc_Trede_winForms_Buisness.Validation;
using Acc_Trede_winForms_Buisness.Validation.Purchases;
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

        private Result _Add()
        {
            Result r= new clsPurchaseReturnsValidator().Validate(this).ToResult();
            if (r.IsFailure) return r;

            Result<int> res = clsPurchaseReturn_DAL.InsertPurchaseReturn(this.PurchaseInvoiceID, this.ReturnNumber, this.SupplierID, this.TotalAmount, this.TaxAmount, this.Notes, GlobalUser.CurrentUser.UserID, this.CashAmount, this.CardAmount, this.Cart);
            if (res.IsFailure)
                return Result.Failure(res.Error);
            this.ReturnID = res.Value;
            return Result.Success();
        }
        public Result Save() => _Add();

        public static Result<DataTable> GetAllInvoices() => clsPurchaseReturn_DAL.GetAllSalesInvoices();

        public static Result<clsPurchaseReturns_BLL> FindByReturnID(int returnId)
        {
            Result<DataTable> res = clsPurchaseReturn_DAL.FindByReturnID(returnId);
            if (res.IsFailure)
                return Result<clsPurchaseReturns_BLL>.Failure(res.Error);
            if (res.Value == null && res.Value.Rows.Count == 0)
                return Result<clsPurchaseReturns_BLL>.Failure("لم يتم العثور على الفاتورة المطلوبة.");

            DataRow dr = res.Value.Rows[0];
            clsPurchaseReturns_BLL invoice = MapFromDataRow(dr);

            return Result<clsPurchaseReturns_BLL>.Success(invoice);
        }
        public static Result<clsPurchaseReturns_BLL> FindByOriginalID(int originalID)
        {
            Result<DataTable> res = clsPurchaseReturn_DAL.FindByOriginalID(originalID);
            if (res.IsFailure)
                return Result<clsPurchaseReturns_BLL>.Failure(res.Error);
            if (res.Value == null && res.Value.Rows.Count == 0)
                return Result<clsPurchaseReturns_BLL>.Failure("لم يتم العثور على الفاتورة المطلوبة.");

            DataRow dr = res.Value.Rows[0];
            clsPurchaseReturns_BLL invoice = MapFromDataRow(dr);

            return Result<clsPurchaseReturns_BLL>.Success(invoice);
        }
        private static clsPurchaseReturns_BLL MapFromDataRow(DataRow dr)
        {
            return new clsPurchaseReturns_BLL(
                returnID: Convert.ToInt32(dr["ReturnID"]),
                returnNumber: dr["ReturnNumber"].ToString(),
                purchaseInvoiceID: Convert.ToInt32(dr["PurchaseInvoiceID"]),
                supplierID: dr["SupplierID"] == DBNull.Value ? (Int32?)null : Convert.ToInt32(dr["SupplierID"]),
                returnDate: Convert.ToDateTime(dr["ReturnDate"]),
                totalAmount: Convert.ToDecimal(dr["TotalAmount"]),
                taxAmount: Convert.ToDecimal(dr["TaxAmount"]),
                notes: dr["Notes"].ToString() ?? "",
                userID: Convert.ToInt32(dr["UserID"]),
                remainingAmount: Convert.ToDecimal(dr["RemainingAmount"]),
                cashAmount: Convert.ToDecimal(dr["CashAmount"]),
                cardAmount: Convert.ToDecimal(dr["CardAmount"])
                );

        }
    }
}
