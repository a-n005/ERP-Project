using Acc_Trade_Core;
using Acc_Trede_winForms_Buisness.Global;
using Acc_Trede_winForms_Buisness.Inventory;
using Acc_Trede_winForms_Buisness.Validation;
using Acc_Trede_winForms_DataAccess.Global;
using Acc_Trede_winForms_DataAccess.Purchases;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Acc_Trede_winForms_Buisness.Purchases
{
    public class clsPurchaseInvoiceDetails_BLL
    {


        #region Properties
        public int PurchaseDetailID { get; set; }
        public int InvoiceID { get; set; }
        public int ProductID { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice => UnitPrice * Quantity;
        public clsProducts_BLL Product { get; set; }
        #endregion

        #region Cunstructors
        private clsPurchaseInvoiceDetails_BLL(int purchasedetailID, int invoiceID, int productID, decimal quantity,
            decimal unitPrice, clsProducts_BLL product)
        {
            this.PurchaseDetailID = purchasedetailID;
            this.InvoiceID = invoiceID;
            this.ProductID = productID;
            this.Quantity = quantity;
            this.UnitPrice = unitPrice;
            this.Product = product;
        }
        public clsPurchaseInvoiceDetails_BLL(  decimal quantity, decimal unitPrice, clsProducts_BLL product)
        {
            this.Quantity = quantity;
            this.UnitPrice = unitPrice;
            this.Product = product;
        }
        #endregion

        #region Private Method
        #endregion

        #region Public Method
        #endregion

        #region Data Retrieval (Queries)
        public static Result<List<clsPurchaseInvoiceDetails_BLL>> GetDetails(int invoiceID)
        {
            string query = @"
        SELECT 
            PID.PurchaseDetailID,
            PID.InvoiceID,
            PID.ProductID,
            PID.Quantity,
            PID.UnitPrice,
            P.Barcode,
            P.ProductName
        FROM PurchaseInvoiceDetails PID
        INNER JOIN Products P ON PID.ProductID = P.ProductID
        WHERE PID.InvoiceID = @InvoiceID";

            SqlParameter[] parameters =
            {
                new SqlParameter("@InvoiceID", SqlDbType.Int) { Value = invoiceID }
            };

            return clsGenericDataAccessBase_DAL.ExecuteReader(query, mapper, parameters);
        }
        #endregion

        #region Mapping & Helpers
        private static Func<SqlDataReader, clsPurchaseInvoiceDetails_BLL> mapper = reader => new clsPurchaseInvoiceDetails_BLL(
   purchasedetailID: Convert.ToInt32(reader["PurchaseDetailID"]),
   invoiceID: Convert.ToInt32(reader["InvoiceID"]),
   productID: Convert.ToInt32(reader["ProductID"]),
   quantity: Convert.ToDecimal(reader["Quantity"]),
   unitPrice: Convert.ToDecimal(reader["UnitPrice"]),
product: new clsProducts_BLL
{
    Barcode = reader.GetStringSafe("Barcode"),
    ProductName = reader.GetStringSafe("ProductName")
}
);
        #endregion

    }
}
