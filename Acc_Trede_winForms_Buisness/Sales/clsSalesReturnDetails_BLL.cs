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
    public class clsSalesReturnDetails_BLL
    {

        #region Properties
        public int ReturnDetailID { get; set; }
        public int ReturnID { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public int DetailID { get; set; }
        public clsProducts_BLL Product { get; set; }
        public decimal LineTotal => Quantity * UnitPrice;
        #endregion

        #region Constructors
        public clsSalesReturnDetails_BLL(
          int quantity, decimal unitPrice, int detailID, clsProducts_BLL product)
        {
            Quantity = quantity;
            UnitPrice = unitPrice;
            DetailID = detailID;
            Product = product;
        }
        private clsSalesReturnDetails_BLL(
        int returnDetailID, int returnID, int quantity, decimal unitPrice, int detailID, clsProducts_BLL product)
        {
            ReturnDetailID = returnDetailID;
            ReturnID = returnID;
            Quantity = quantity;
            UnitPrice = unitPrice;
            DetailID = detailID;
            Product = product;
        }
        #endregion

        #region Data Retrival (Queries)
        public static Result<List<clsSalesReturnDetails_BLL>> GetDetails(int returnID)
        {
            string query = @"SELECT D.*, P.Barcode, P.ProductName
                             FROM SalesReturnDetails D
                             INNER JOIN Products P ON D.ProductID = P.ProductID
                             WHERE D.ReturnID = @ReturnID";
            SqlParameter[] sp = { new SqlParameter("@ReturnID", SqlDbType.Int) { Value = returnID } };
            return clsGenericDataAccessBase_DAL.ExecuteReader(query,mapper,sp);
        }
        #endregion

        #region Mapping & Helpers
        private static Func<SqlDataReader, clsSalesReturnDetails_BLL> mapper = reader => new clsSalesReturnDetails_BLL(
    returnDetailID: Convert.ToInt32(reader["ReturnDetailID"]),
    returnID: Convert.ToInt32(reader["ReturnID"]),
    quantity: Convert.ToInt32(reader["Quantity"]),
    unitPrice: Convert.ToDecimal(reader["UnitPrice"]),
    detailID: Convert.ToInt32(reader["DetailID"]),
    product: new clsProducts_BLL
    {
        ProductID = Convert.ToInt32(reader["ProductID"]),
        Barcode = reader.GetStringSafe("Barcode"),
        ProductName = reader.GetStringSafe("ProductName")
    }
    );
        #endregion
    }
}
