using Acc_Trade_Core;
using Acc_Trede_winForms_Buisness.Inventory;
using Acc_Trede_winForms_Buisness.Validation;
using Acc_Trede_winForms_DataAccess.Global;
using Acc_Trede_winForms_DataAccess.Sales;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Acc_Trede_winForms_Buisness.Sales
{
    public class clsSalesInvoiceDetails_BLL
    {
        #region Properties
        public int DetailID { get; set; }
        public int InvoiceID { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal CostPriceAtSale { get; set; }
        public int Quantity { get; set; }
        public clsProducts_BLL Product { get; set; }
        public decimal LineTotal => Quantity * UnitPrice;
        #endregion

        #region Constructors
        public clsSalesInvoiceDetails_BLL(int detailID, int invoiceID, decimal unitPrice, decimal costPriceAtSale, int quantity, clsProducts_BLL product)
        {
            DetailID = detailID;
            InvoiceID = invoiceID;
            UnitPrice = unitPrice;
            CostPriceAtSale = costPriceAtSale;
            Quantity = quantity;
            Product = product;
        }
        public clsSalesInvoiceDetails_BLL(int quantity,decimal unitPrice, clsProducts_BLL product)
        {
            this.Quantity = quantity;
            this.UnitPrice = unitPrice;
            this.Product = product ?? new clsProducts_BLL();
        }
        #endregion

        #region Data Retrival (Queries)
        public static Result<List<clsSalesInvoiceDetails_BLL>> GetDetails(int invoiceID)
        {
            string query = @"SELECT D.DetailID, D.InvoiceID, D.ProductID, 
                                    P.Barcode, P.ProductName, D.Quantity, D.UnitPrice,
                                    D.CostPriceAtSale
                             FROM SalesInvoiceDetails D
                             INNER JOIN Products P ON D.ProductID = P.ProductID
                             WHERE D.InvoiceID = @InvoiceID";
            SqlParameter[] sp = { new SqlParameter("@InvoiceID", SqlDbType.Int) { Value = invoiceID } };
            return clsGenericDataAccessBase_DAL.ExecuteReader(query,mapper,sp);
        }
        #endregion

        #region Mapping & Helpers
        private static Func<SqlDataReader, clsSalesInvoiceDetails_BLL> mapper = reader => new clsSalesInvoiceDetails_BLL(
            detailID: Convert.ToInt32(reader["DetailID"]),
            invoiceID: Convert.ToInt32(reader["InvoiceID"]),
            quantity: Convert.ToInt32(reader["Quantity"]),
            unitPrice: Convert.ToDecimal(reader["UnitPrice"]),
            costPriceAtSale: Convert.ToDecimal(reader["CostPriceAtSale"]),
            product: new clsProducts_BLL{
                ProductID= Convert.ToInt32(reader["ProductID"]),
                Barcode = reader.GetStringSafe("Barcode"),
                ProductName = reader.GetStringSafe("ProductName")
            }
            );
        #endregion
    }
}
