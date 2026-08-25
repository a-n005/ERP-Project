using Acc_Trade_Core;
using Acc_Trede_winForms_DataAccess.Sales;
using Global;
using System;
using System.Data;

namespace Acc_Trede_winForms_Buisness.Sales
{
    public class clsSalesReturn_BLL
    {
        // add later check for quantity when he return twise

        public int ReturnID { get; set; }
        public string ReturnNumber { get; set; }
        public int InvoiceID { get; set; }
        public int? CustomerID { get; set; }
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
        public clsSalesReturn_BLL()
        {
            this.ReturnID = -1;
            InvoiceID = -1;
            this.ReturnNumber = string.Empty;
            this.UserID = GlobalUser.CurrentUser?.UserID ?? -1;
            CustomerID = null;
            TotalAmount = 0;
            TaxAmount = 0;
            CashAmount = 0;
            CardAmount = 0;
            Cart = new DataTable();
        }
        private clsSalesReturn_BLL(int returnID, string returnNumber, int invoiceID, int? customerID, DateTime returnDate,
            decimal totalAmount, decimal taxAmount, string notes, int userID, decimal remainingAmount, decimal? cashAmount, decimal? cardAmount)
        {
            ReturnID = returnID;
            ReturnNumber = returnNumber;
            InvoiceID = invoiceID;
            CustomerID = customerID;
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
            Result<int> res = clsSalesReturn_DAL.InsertSalesReturn(this.InvoiceID, this.ReturnNumber, this.CustomerID, this.TotalAmount, this.TaxAmount, this.Notes, GlobalUser.CurrentUser.UserID, this.CashAmount, this.CardAmount, this.Cart);
            if (res.IsFailure)
                return Result.Failure(res.Error);
            this.ReturnID = res.Value;
            return Result.Success();
        }
        public Result Save() => _Add();
    }
}
