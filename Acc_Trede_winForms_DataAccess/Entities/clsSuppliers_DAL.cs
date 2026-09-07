using Acc_Trade_Core;
using Acc_Trede_winForms_DataAccess.Global;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Acc_Trede_winForms_DataAccess.Entities
{
    public class clsSuppliers_DAL
    {
        public static Result<int> InsertSupplier(string supplierName, string companyName,
            string phone, string taxNumber, int createdBy)
        {
            int newID = -1;

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_InsertSupplier", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@SupplierName", supplierName);
                    cmd.Parameters.AddWithValue("@CompanyName", (object)companyName ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Phone", (object)phone ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@TaxNumber", (object)taxNumber ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CreatedBy", createdBy);

                    try
                    {
                        conn.Open();
                        object res = cmd.ExecuteScalar();

                        newID = (res != null) && int.TryParse(res.ToString(), out int ID) ? ID : -1;
                    }
                    catch (Exception ex)
                    {
                        return Result<int>.Failure($"خطأ في الاتصال بقاعدة البيانات: {ex.Message}");
                    }
                }
            }
            return (newID > 0) ? Result<int>.Success(newID) : Result<int>.Failure("فشل إضافة المستخدم: لم يتم إرجاع معرف جديد من قاعدة البيانات.");
        }
        public static Result UpdateSupplier(int supplierID, string supplierName,
            string companyName, string phone, string taxNumber, int updatedBy)
        {

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_UpdateSupplier", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@SupplierID", supplierID);
                    cmd.Parameters.AddWithValue("@SupplierName", supplierName);
                    cmd.Parameters.AddWithValue("@CompanyName", (object)companyName ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Phone", (object)phone ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@TaxNumber", (object)taxNumber ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@UpdatedBy", updatedBy);

                    try
                    {
                        conn.Open();
                        return (cmd.ExecuteNonQuery() > 0) ? Result.Success() : Result.Failure($"لم يتم تحديث بيانات المورد رقم ({supplierID})، قد يكون المعرف غير موجود.");
                    }
                    catch (Exception ex)
                    {
                        return Result.Failure($"خطأ في الاتصال بقاعدة البيانات: {ex.Message}");
                    }
                }
            }
        }
        public static Result DeleteSupplierSoft(int supplierID)
        {
            string query = @"UPDATE Suppliers 
                     SET IsActive = 0 
                     WHERE SupplierID = @SupplierID";

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SupplierID", supplierID);

                    try
                    {
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return (rowsAffected > 0) ? Result.Success() : Result.Failure($"لم يتم تحديث بيانات المستخدم رقم ({supplierID})، قد يكون المعرف غير موجود.");
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
