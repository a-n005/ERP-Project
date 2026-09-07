using Acc_Trade_Core;
using Acc_Trede_winForms_DataAccess.Inventory;
using Acc_Trede_winForms_Buisness.Validation;
using Acc_Trede_winForms_Buisness.Validation.Inventory;
using Acc_Trede_winForms_Buisness.Global;
using System;
using System.Data;
using System.Collections.Generic;
using Acc_Trede_winForms_DataAccess.Global;
using System.Data.SqlClient;

namespace Acc_Trede_winForms_Buisness.Inventory
{
    public class clsProducts_BLL
    {
        #region Enums
        private enum _enMode { Add, Update };
        #endregion

        #region Fields
        private _enMode _Mode = _enMode.Add;
        #endregion

        #region Properties
        public int ProductID { get; set; }
        public string Barcode { get; set; }
        public string ProductName { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SalePrice { get; set; }
        public int Quantity { get; set; }
        public int MinQuantityAlert { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool SubmitterForDelete { get; set; }
        #endregion

        #region Cunstructors
        public clsProducts_BLL()
        {
            this.ProductID = -1;
            this._Mode = _enMode.Add;
        }
        private clsProducts_BLL(int productID, string barcode, string productName, decimal costPrice,
            decimal salePrice, int quantity, int minQuantityAlert, DateTime createdAt, bool isActive,
            int createdBy, int? updatedBy, DateTime? updatedAt, bool submitterForDelete)
        {
            ProductID = productID;
            Barcode = barcode;
            ProductName = productName;
            CostPrice = costPrice;
            SalePrice = salePrice;
            Quantity = quantity;
            MinQuantityAlert = minQuantityAlert;
            CreatedAt = createdAt;
            IsActive = isActive;
            CreatedBy = createdBy;
            UpdatedBy = updatedBy;
            UpdatedAt = updatedAt;
            SubmitterForDelete = submitterForDelete;
            this._Mode = _enMode.Update;
        }
        #endregion

        #region Private Method
        private Result _AddNewProduct()
        {
            Result r = new clsProductsValidator().Validate(this).ToResult();
            if (r.IsFailure) return r;

            Result<int> res = clsProducts_DAL.InsertProduct(this.Barcode, this.ProductName, this.CostPrice, this.SalePrice, this.Quantity, this.MinQuantityAlert, GlobalUser.CurrentUser.UserID);
            if (res.IsFailure)
                return Result.Failure(res.Error);
            this.ProductID = res.Value;
            this._Mode = _enMode.Update;
            return Result.Success();
        }
        private Result _UpdateProduct()
        {
            Result r = new clsProductsValidator().Validate(this).ToResult();
            if (r.IsFailure) return r;

            return clsProducts_DAL.UpdateProduct(this.ProductID, this.Barcode, this.ProductName,
                 this.CostPrice, this.SalePrice, this.Quantity, this.MinQuantityAlert, GlobalUser.CurrentUser.UserID);
        }
        #endregion

        #region Public Method
        public Result Save() => _Mode == _enMode.Add ? _AddNewProduct() : _UpdateProduct();
        // ask about del want del now or when the quantity finsh.
        public Result Delete() => clsProducts_DAL.DeleteProductSoft(this.ProductID, GlobalUser.CurrentUser.UserID);
        #endregion

        #region Data Retrieval (Queries)
        public static Result<List<clsProducts_BLL>> GetAllProducts()
        {
            string query = @"SELECT *
                     FROM Products 
                     ORDER BY ProductName ASC";
            return clsGenericDataAccessBase_DAL.ExecuteReader(query, mapper);
        }
        public static Result<List<clsProducts_BLL>> GetLowQuantity()=> 
            clsGenericDataAccessBase_DAL.ExecuteReader("sp_GetLowStockProducts", mapper, commandType: CommandType.StoredProcedure);
        public static Result<clsProducts_BLL> Find(int productID)
        {
            string query = @"SELECT *
                     FROM Products 
                     WHERE ProductID = @ProductID";
            SqlParameter[] parameters = { new SqlParameter("@ProductID", SqlDbType.Int) { Value = productID } };
            return clsGenericDataAccessBase_DAL.ExecuteSingle(query, mapper, parameters);
        }
        public static Result<clsProducts_BLL> Find(string barcode)
        {
            string query = @"SELECT *
                     FROM Products 
                     WHERE Barcode = @Barcode";
            SqlParameter[] parameters = { new SqlParameter("@Barcode", SqlDbType.NVarChar) { Value = barcode } };
            return clsGenericDataAccessBase_DAL.ExecuteSingle(query, mapper, parameters);
        }
        #endregion

        #region Mapping & Helpers
        private static Func<SqlDataReader, clsProducts_BLL> mapper = reader => new clsProducts_BLL(
                 productID: Convert.ToInt32(reader["ProductID"]),
                 barcode: reader.GetStringSafe("Barcode", string.Empty),
                 productName: reader.GetStringSafe("ProductName", string.Empty),
                 costPrice: Convert.ToDecimal(reader["CostPrice"]),
                 salePrice: Convert.ToDecimal(reader["SalePrice"]),
                 quantity: Convert.ToInt32(reader["StockQuantity"]),
                 minQuantityAlert: Convert.ToInt32(reader["MinStockAlert"]),
                 createdAt: Convert.ToDateTime(reader["CreatedAt"]),
                 isActive: Convert.ToBoolean(reader["IsActive"]),
                 createdBy: Convert.ToInt32(reader["CreatedBy"]),
                 updatedBy: reader.GetNullable<int>("UpdatedBy"),
                 updatedAt: reader.GetNullable<DateTime>("UpdatedAt"),
                 submitterForDelete: Convert.ToBoolean(reader["SubmitterForDelete"])
             );
        #endregion
    }
}
