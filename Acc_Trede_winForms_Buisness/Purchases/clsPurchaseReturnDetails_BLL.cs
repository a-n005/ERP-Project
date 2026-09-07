using Acc_Trade_Core;
using Acc_Trede_winForms_Buisness.Inventory;
using Acc_Trede_winForms_Buisness.Validation;
using Acc_Trede_winForms_DataAccess.Global;
using Acc_Trede_winForms_DataAccess.Purchases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Acc_Trede_winForms_Buisness.Purchases
{
    public class clsPurchaseReturnDetails_BLL
    {
        #region Properties
        public int ReturnDetailID { get; set; }
        public int ReturnID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public int DetailID { get; set; }
        public decimal LineTotal => Quantity * UnitPrice;
        public clsProducts_BLL Product { get; set; }
        #endregion

        #region Constructors
        public clsPurchaseReturnDetails_BLL(int quantity,decimal unitPrice, int detailID, clsProducts_BLL product)
        {
            this.Quantity = quantity;
            this.UnitPrice = unitPrice;
            this.DetailID = detailID;
            this.Product = product ?? new clsProducts_BLL();
        }
        private clsPurchaseReturnDetails_BLL(int returnDetailID, int returnID, int productID, int quantity,
            decimal unitPrice, int detailID, clsProducts_BLL product)
        {
            this.ReturnDetailID = returnDetailID;
            this.ReturnID = returnID;
            this.ProductID = productID;
            this.Quantity = quantity;
            this.UnitPrice = unitPrice;
            this.DetailID = detailID;
            this.Product = product;
        }
        #endregion

        #region Data Retrieval (Queries)
        public Result<List<clsPurchaseReturnDetails_BLL>> GetDetails(int returnID)
        {
            string query = @"SELECT * FROM PurchaseReturnDetails  WHERE ReturnID = @returnID";
            SqlParameter[] parameters = { new SqlParameter("@ReturnID", SqlDbType.Int) { Value = returnID } };
            return clsGenericDataAccessBase_DAL.ExecuteReader(query, mapper, parameters);
        }
        #endregion

        #region Mapping & Helpers
        private static Func<SqlDataReader, clsPurchaseReturnDetails_BLL> mapper = reader => new clsPurchaseReturnDetails_BLL(
returnDetailID: Convert.ToInt32(reader["ReturnDetailID"]),
returnID: Convert.ToInt32(reader["ReturnID"]),
productID: Convert.ToInt32(reader["ProductID"]),
quantity: Convert.ToInt32(reader["Quantity"]),
unitPrice: Convert.ToDecimal(reader["UnitPrice"]),
detailID: Convert.ToInt32(reader["DetailID"]),
product: new clsProducts_BLL
{
    Barcode = reader.GetStringSafe("Barcode"),
    ProductName = reader.GetStringSafe("ProductName")
}
);
        #endregion
    }
}
