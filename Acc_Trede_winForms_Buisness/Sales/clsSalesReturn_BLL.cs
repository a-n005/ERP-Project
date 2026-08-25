using Acc_Trade_Core;
using Acc_Trede_winForms_Buisness.Validation;
using Acc_Trede_winForms_Buisness.Validation.Sales;
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
            Result r = new clsSalesReturnValidator().Validate(this).ToResult();
            if(r.IsFailure)
                return r;

            Result<int> res = clsSalesReturn_DAL.InsertSalesReturn(this.InvoiceID, this.ReturnNumber, this.CustomerID, this.TotalAmount, this.TaxAmount, this.Notes, GlobalUser.CurrentUser.UserID, this.CashAmount, this.CardAmount, this.Cart);
            if (res.IsFailure)
                return Result.Failure(res.Error);
            this.ReturnID = res.Value;
            return Result.Success();
        }
        public Result Save() => _Add();

        public static Result<DataTable> GetAllInvoices() => clsSalesReturn_DAL.GetAllSalesInvoices();

        public static Result<clsSalesReturn_BLL> FindByReturnID(int returnId)
        {
            Result<DataTable> res = clsSalesReturn_DAL.FindByReturnID(returnId);
            if (res.IsFailure)
                return Result<clsSalesReturn_BLL>.Failure(res.Error);
            if (res.Value == null && res.Value.Rows.Count == 0)
                return Result<clsSalesReturn_BLL>.Failure("لم يتم العثور على الفاتورة المطلوبة.");

            DataRow dr = res.Value.Rows[0];
            clsSalesReturn_BLL invoice = MapFromDataRow(dr);

            return Result<clsSalesReturn_BLL>.Success(invoice);
        }
        public static Result<clsSalesReturn_BLL> FindByOriginalID(int originalID)
        {
            Result<DataTable> res = clsSalesReturn_DAL.FindByOriginalID(originalID);
            if (res.IsFailure)
                return Result<clsSalesReturn_BLL>.Failure(res.Error);
            if (res.Value == null && res.Value.Rows.Count == 0)
                return Result<clsSalesReturn_BLL>.Failure("لم يتم العثور على الفاتورة المطلوبة.");

            DataRow dr = res.Value.Rows[0];
            clsSalesReturn_BLL invoice = MapFromDataRow(dr);

            return Result<clsSalesReturn_BLL>.Success(invoice);
        }
        private static clsSalesReturn_BLL MapFromDataRow(DataRow dr)
        {
            return new clsSalesReturn_BLL(
                returnID: Convert.ToInt32(dr["ReturnID"]),
                returnNumber: dr["ReturnNumber"].ToString(),
                invoiceID: Convert.ToInt32(dr["InvoiceID"]),
                customerID: dr["CustomerID"] == DBNull.Value ? (Int32?)null : Convert.ToInt32(dr["CustomerID"]),
                returnDate: Convert.ToDateTime(dr["ReturnDate"]),
                totalAmount: Convert.ToDecimal(dr["TotalAmount"]),
                taxAmount: Convert.ToDecimal(dr["TaxAmount"]),
                notes: dr["Notes"].ToString()??"",
                userID: Convert.ToInt32(dr["UserID"]),
                remainingAmount: Convert.ToDecimal(dr["RemainingAmount"]),
                cashAmount: Convert.ToDecimal(dr["CashAmount"]),
                cardAmount: Convert.ToDecimal(dr["CardAmount"])
                );

        }
    }
}
