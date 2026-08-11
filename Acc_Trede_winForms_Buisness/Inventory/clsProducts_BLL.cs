using Acc_Trade_Core;
using Acc_Trede_winForms_DataAccess.Inventory;
using Global;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace Acc_Trede_winForms_Buisness.Inventory
{
    public class clsProducts_BLL
    {
        private enum _enMode { Add, Update };
        private _enMode _Mode = _enMode.Add;
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
        private Result _AddNewProduct()
        {
            Result<int> res = clsProducts_DAL.InsertProduct(this.Barcode, this.ProductName, this.CostPrice, this.SalePrice, this.Quantity, this.MinQuantityAlert, GlobalUser.CurrentUser.UserID);
            if (res.IsFailure)
                return Result.Failure(res.Error);
            this.ProductID = res.Value;
            this._Mode = _enMode.Update;
            return Result.Success();
        }
        private Result _UpdateProduct() => clsProducts_DAL.UpdateProduct(this.ProductID, this.Barcode, this.ProductName, this.CostPrice, this.SalePrice, this.Quantity, this.MinQuantityAlert, GlobalUser.CurrentUser.UserID);
        public Result Save() => _Mode == _enMode.Add ? _AddNewProduct() : _UpdateProduct();
        public Result Delete() => clsProducts_DAL.DeleteProductSoft(this.ProductID, GlobalUser.CurrentUser.UserID);
        public static Result<DataTable> GetAllProducts() => clsProducts_DAL.GetAllProducts();
        public static Result<DataTable> GetLowQuantity() => clsProducts_DAL.GetLowStockProducts();
        public static Result<clsProducts_BLL> Find(int productID)
        {
            Result<DataTable> res = clsProducts_DAL.GetProductByID(productID);
            if (res.IsFailure)
                return Result<clsProducts_BLL>.Failure(res.Error);
            if (res.Value == null && res.Value.Rows.Count == 0)
                return Result<clsProducts_BLL>.Failure("لم يتم العثور على المنتج المطلوب.");

            DataRow dr = res.Value.Rows[0];
            clsProducts_BLL product = MapFromDataRow(dr);

            return Result<clsProducts_BLL>.Success(product);
        }
        public static Result<clsProducts_BLL> Find(string barcode)
        {
            Result<DataTable> res = clsProducts_DAL.GetProductByBarcode(barcode);
            if (res.IsFailure)
                return Result<clsProducts_BLL>.Failure(res.Error);
            if (res.Value == null && res.Value.Rows.Count == 0)
                return Result<clsProducts_BLL>.Failure("لم يتم العثور على المنتج المطلوب.");

            DataRow dr = res.Value.Rows[0];
            clsProducts_BLL product = MapFromDataRow(dr);

            return Result<clsProducts_BLL>.Success(product);
        }
        private static clsProducts_BLL MapFromDataRow(DataRow dr)
        {
            return new clsProducts_BLL(
                 productID: Convert.ToInt32(dr["ProductID"]),
                 barcode: dr["Barcode"].ToString(),
                 productName: dr["ProductName"].ToString(),
                 costPrice: Convert.ToDecimal(dr["CostPrice"]),
                 salePrice: Convert.ToDecimal(dr["SalePrice"]),
                 quantity: Convert.ToInt32(dr["StockQuantity"]),
                 minQuantityAlert: Convert.ToInt32(dr["MinStockAlert"]),
                 createdAt: Convert.ToDateTime(dr["CreatedAt"]),
                 isActive: Convert.ToBoolean(dr["IsActive"]),
                 createdBy: Convert.ToInt32(dr["CreatedBy"]),
                 updatedBy: dr["UpdatedBy"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["UpdatedBy"]),
                 updatedAt: dr["UpdatedAt"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["UpdatedAt"]),
                 submitterForDelete: Convert.ToBoolean(dr["SubmitterForDelete"])
             );
        }
    }
}
