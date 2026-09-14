using Acc_Trade_Core;
using Acc_Trede_winForms.Entities;
using Acc_Trede_winForms_Buisness.Entities;
using Acc_Trede_winForms_Buisness.Global;
using Acc_Trede_winForms_Buisness.Purchases;
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
    public class clsSalesInvoices_BLL
    {
        #region Fields
        private clsCustomers_BLL _customer;
        #endregion

        #region Properties
        public int InvoiceID { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime Date { get; set; }
        public int CreatedBy { get; private set; }
        public int? CustomerID { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal Discount { get; set; }
        public decimal NetAmount => (TotalAmount - Discount) + TaxAmount;
        public decimal CashAmount { get; set; }
        public decimal CardAmount { get; set; }
        public decimal RemainingAmount { get; private set; }
        public List<clsSalesInvoiceDetails_BLL> SalesCart { get; set; }
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
        public clsSalesInvoices_BLL()
        {
            InvoiceID = -1;
            InvoiceNumber = string.Empty;
            CreatedBy = GlobalUser.CurrentUser?.UserID ?? -1;
            CustomerID = null;
            TotalAmount = 0;
            TaxAmount = 0;
            Discount = 0;
            CashAmount = 0;
            CardAmount = 0;
            SalesCart = new List<clsSalesInvoiceDetails_BLL>();
        }

        private clsSalesInvoices_BLL(int invoiceID, string invoiceNumber, DateTime date, int createdBy, int? customerID, decimal totalAmount, decimal taxAmount, decimal discount, decimal cashAmount, decimal cardAmount, decimal remaining)
        {
            this.InvoiceID = invoiceID;
            this.InvoiceNumber = invoiceNumber;
            this.Date = date;
            this.CreatedBy = createdBy;
            this.CustomerID = customerID;
            this.TotalAmount = totalAmount;
            this.TaxAmount = taxAmount;
            this.Discount = discount;
            this.CashAmount = cashAmount;
            this.CardAmount = cardAmount;
            this.RemainingAmount = remaining;
            this.SalesCart = new List<clsSalesInvoiceDetails_BLL>();
        }
        #endregion

        #region Public Methods
        public Result Create()
        {
            Result r = new clsSalesValidator().Validate(this).ToResult();
            if (r.IsFailure)
                return r;

            Result<int> res = clsSalesInvoices_DAL.InsertSalesInvoice(
                this.InvoiceNumber,
                this.CreatedBy,
                this.CustomerID,
                this.TotalAmount,
                this.Discount,
                this.TaxAmount,
                this.CashAmount,
                this.CardAmount,
                ConvertCartToDataTable(this.SalesCart)
            );

            if (res.IsFailure)
            {
                return res;
            }

            this.InvoiceID = res.Value;
            return Result.Success();
        }
        #endregion

        #region Data Retrieval (Queries)
        public static Result<List<clsSalesInvoices_BLL>> GetAllInvoices()
        {
            string query = @"SELECT *
                             FROM SalesInvoices 
                             ORDER BY InvoiceDate DESC";
            return clsGenericDataAccessBase_DAL.ExecuteReader(query, mapper);
        }

        public static Result<clsSalesInvoices_BLL> Find(int id)
        {
            string query = "SELECT * FROM SalesInvoices WHERE InvoiceID = @ID";
            SqlParameter[] sp = { new SqlParameter("@ID", SqlDbType.Int) { Value = id } };
            return clsGenericDataAccessBase_DAL.ExecuteSingle(query, mapper, sp);
        }
        #endregion

        #region Mapping & Helpers
        private static DataTable ConvertCartToDataTable(List<clsSalesInvoiceDetails_BLL> cart)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ProductID", typeof(int));
            dt.Columns.Add("Quantity", typeof(int));
            dt.Columns.Add("UnitPrice", typeof(decimal));

            if (cart != null)
                foreach (var item in cart)
                    dt.Rows.Add(
                        item.ProductID ,
                        item.Quantity,
                        item.UnitPrice
                    );

            return dt;
        }

        private static Func<SqlDataReader, clsSalesInvoices_BLL> mapper = reader => new clsSalesInvoices_BLL(
            invoiceID: Convert.ToInt32(reader["InvoiceID"]),
            invoiceNumber: reader.GetStringSafe("InvoiceNumber"),
            date: Convert.ToDateTime(reader["InvoiceDate"]),
            createdBy: Convert.ToInt32(reader["UserID"]),
            customerID: reader.GetNullable<int>("CustomerID"),
            totalAmount: Convert.ToDecimal(reader["TotalAmount"]),
            taxAmount: Convert.ToDecimal(reader["TaxAmount"]),
            discount: Convert.ToDecimal(reader["Discount"]),
            cashAmount: Convert.ToDecimal(reader["CashAmount"]),
            cardAmount: Convert.ToDecimal(reader["CardAmount"]),
            remaining: Convert.ToDecimal(reader["RemainingAmount"])
        );
        #endregion
    }
}