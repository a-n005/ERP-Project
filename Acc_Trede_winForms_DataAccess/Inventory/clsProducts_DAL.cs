using Acc_Trade_Core;
using Acc_Trede_winForms_DataAccess.Global;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Acc_Trede_winForms_DataAccess.Inventory
{
    public class clsProducts_DAL
    {
        public static Result<int> InsertProduct(string barcode, string productName, decimal costPrice,
            decimal salePrice, int stockQuantity, int minStockAlert, int createdBy)
        {
            int NewID = -1;
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_InsertProduct", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Barcode", (object)barcode ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ProductName", productName);
                    cmd.Parameters.AddWithValue("@CostPrice", costPrice);
                    cmd.Parameters.AddWithValue("@SalePrice", salePrice);
                    cmd.Parameters.AddWithValue("@StockQuantity", stockQuantity);
                    cmd.Parameters.AddWithValue("@MinStockAlert", minStockAlert);
                    cmd.Parameters.AddWithValue("@CreatedBy", createdBy);

                    try
                    {
                        conn.Open();
                        object result = cmd.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int updatedID))
                        {
                            NewID = updatedID;
                        }
                    }
                    catch (Exception ex)
                    {
                        return Result<int>.Failure($"خطأ في الاتصال بقاعدة البيانات: {ex.Message}");
                    }
                }
            }
            return (Result<int>)((NewID > 0) ? Result<int>.Success() : Result<int>.Failure("فشل إضافة المستخدم: لم يتم إرجاع معرف جديد من قاعدة البيانات."));
        }
        public static Result UpdateProduct(int productID, string barcode, string productName,
            decimal costPrice, decimal salePrice, int stockQuantity, int minStockAlert, int updatedBy)
        {

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_UpdateProduct", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ProductID", productID);
                    cmd.Parameters.AddWithValue("@Barcode", (object)barcode ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ProductName", productName);
                    cmd.Parameters.AddWithValue("@CostPrice", costPrice);
                    cmd.Parameters.AddWithValue("@SalePrice", salePrice);
                    cmd.Parameters.AddWithValue("@StockQuantity", stockQuantity);
                    cmd.Parameters.AddWithValue("@MinStockAlert", minStockAlert);
                    cmd.Parameters.AddWithValue("@UpdatedBy", updatedBy);

                    try
                    {
                        conn.Open();
                        return (cmd.ExecuteNonQuery() > 0) ? Result.Success() : Result.Failure($"لم يتم تحديث بيانات المنتج رقم ({productID})، قد يكون المعرف غير موجود.");
                    }
                    catch (Exception ex)
                    {
                        return Result.Failure($"خطأ في الاتصال بقاعدة البيانات: {ex.Message}");
                    }
                }
            }
        }
        public static Result DeleteProductSoft(int productID, int updatedBy)
        {

            string query = @"UPDATE Products 
                     SET IsActive = 0 , updatedBy= @UpdatedBy ,UpdatedAt = getDate()
                     WHERE ProductID = @ProductID";

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {

                    cmd.Parameters.AddWithValue("@ProductID", productID);
                    cmd.Parameters.AddWithValue("@UpdatedBy", updatedBy);

                    try
                    {
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return (rowsAffected > 0) ? Result.Success() : Result.Failure($"لم يتم تحديث بيانات المنتج رقم ({productID})، قد يكون المعرف غير موجود.");
                    }
                    catch (Exception ex)
                    {
                        return Result.Failure($"خطأ في الاتصال بقاعدة البيانات: {ex.Message}");
                    }
                }
            }
        }
     
    }
}
