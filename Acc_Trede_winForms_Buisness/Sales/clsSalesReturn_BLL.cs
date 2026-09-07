using Acc_Trade_Core;
using Acc_Trede_winForms_Buisness.Entities;
using Acc_Trede_winForms_Buisness.Global;
using Acc_Trede_winForms_Buisness.Validation;
using Acc_Trede_winForms_Buisness.Validation.Sales;
using Acc_Trede_winForms_DataAccess.Global;
using Acc_Trede_winForms_DataAccess.Sales;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Acc_Trede_winForms_Buisness.Sales
{
    public class clsSalesReturn_BLL
    {
        // add later check for quantity when he return twise
        #region Fields
        private clsCustomers_BLL _customer;
        #endregion

        #region Properties
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
        public List<clsSalesReturnDetails_BLL> Cart { get; set; }
        public clsCustomers_BLL Customer
        {
            get
            {
                if (_customer == null && CustomerID.HasValue)
                {
                    var result = clsCustomers_BLL.Find(CustomerID.Value);
                    if (result.IsSuccess)
                    {
                        _customer = result.Value;
                    }
                }
                return _customer;
            }
        }
        #endregion

        #region Constructors
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
            Cart = new List<clsSalesReturnDetails_BLL>();
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
        #endregion

        #region Private Methods
        private Result _Add()
        {
            Result r = new clsSalesReturnValidator().Validate(this).ToResult();
            if (r.IsFailure)
                return r;

            Result<int> res = clsSalesReturn_DAL.InsertSalesReturn(this.InvoiceID, this.ReturnNumber, this.CustomerID, this.TotalAmount, this.TaxAmount, this.Notes, GlobalUser.CurrentUser?.UserID??-1, this.CashAmount, this.CardAmount,ConvertCartToDataTable(this.Cart));
            if (res.IsFailure)
                return Result.Failure(res.Error);
            this.ReturnID = res.Value;
            return Result.Success();
        }
        #endregion

        #region Public Methods
        public Result Save() => _Add();
        #endregion

        #region Data Retrival (Queries)
        public static Result<List<clsSalesReturn_BLL>> GetAllInvoices()
        {
            string query = @"SELECT *
                             FROM  SalesReturns 
                             ORDER BY ReturnDate DESC";
            return clsGenericDataAccessBase_DAL.ExecuteReader(query, mapper);
        }
        public static Result<clsSalesReturn_BLL> FindByReturnID(int returnId)
        {
            string query = "select * from SalesReturns where ReturnID=@ID";
            SqlParameter[] sp = { new SqlParameter("@ID", SqlDbType.Int) { Value = returnId } };
            return clsGenericDataAccessBase_DAL.ExecuteSingle(query,mapper, sp);
        }
        public static Result<clsSalesReturn_BLL> FindByOriginalID(int originalID)
        {
            string query = "select * from SalesReturns where InvoiceID=@ID";
            SqlParameter[] sp = { new SqlParameter("@ID", SqlDbType.Int) { Value = originalID } };
            return clsGenericDataAccessBase_DAL.ExecuteSingle(query, mapper, sp);
        }
        #endregion

        #region Mapping & Helpers
        private static DataTable ConvertCartToDataTable(List<clsSalesReturnDetails_BLL> cart)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ProductID", typeof(int));
            dt.Columns.Add("Quantity", typeof(int));
            dt.Columns.Add("UnitPrice", typeof(decimal));
            dt.Columns.Add("DetailID", typeof(int));

            if (cart != null)
                foreach (var item in cart)
                    dt.Rows.Add(
                        item.Product?.ProductID ?? 0,
                        item.Quantity,
                        item.UnitPrice,
                        item.DetailID
                    );

            return dt;
        }
        private static Func<SqlDataReader, clsSalesReturn_BLL> mapper = reader => new clsSalesReturn_BLL(
                returnID: Convert.ToInt32(reader["ReturnID"]),
                returnNumber: reader.GetStringSafe("ReturnNumber"),
                invoiceID: Convert.ToInt32(reader["InvoiceID"]),
                customerID: reader.GetNullable<int>("CustomerID"),
                returnDate: Convert.ToDateTime(reader["ReturnDate"]),
                totalAmount: Convert.ToDecimal(reader["TotalAmount"]),
                taxAmount: Convert.ToDecimal(reader["TaxAmount"]),
                notes: reader.GetStringSafe("Notes"),
                userID: Convert.ToInt32(reader["UserID"]),
                remainingAmount: Convert.ToDecimal(reader["RemainingAmount"]),
                cashAmount: reader.GetNullable<decimal>("CashAmount"),
                cardAmount: reader.GetNullable<decimal>("CardAmount")
                );
        #endregion
    }
}
