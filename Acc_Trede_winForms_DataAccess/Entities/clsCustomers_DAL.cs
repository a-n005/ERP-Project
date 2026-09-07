using Acc_Trade_Core;
using Acc_Trede_winForms_DataAccess.Global;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Acc_Trede_winForms_DataAccess.Entities
{
    public class clsCustomers_DAL
    {
        public static Result<int> InsertCustomer(string customerName, string phone,
            string taxNumber, int createdBy)
        {
            int newID = -1;
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_InsertCustomer", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CustomerName", customerName);
                    cmd.Parameters.AddWithValue("@Phone", (object)phone ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@TaxNumber", (object)taxNumber ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CreatedBy", createdBy);

                    try
                    {
                        conn.Open();
                        object result = cmd.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int insertedId))
                        {
                            newID = insertedId;
                        }
                    }
                    catch (Exception ex)
                    {
                        return Result<int>.Failure($"خطأ في الاتصال بقاعدة البيانات: {ex.Message}");
                    }
                }
            }
            return (newID > 0) ? Result<int>.Success(newID) : Result<int>.Failure("فشل إضافة المستخدم: لم يتم إرجاع معرف جديد من قاعدة البيانات.");
        }
        public static Result UpdateCustomer(int customerID, string customerName,
            string phone, string taxNumber, int updatedBy)
        {
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_UpdateCustomer", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CustomerID", customerID);
                    cmd.Parameters.AddWithValue("@CustomerName", customerName);
                    cmd.Parameters.AddWithValue("@Phone", (object)phone ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@TaxNumber", (object)taxNumber ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@UpdatedBy", updatedBy);

                    try
                    {
                        conn.Open();
                        return (cmd.ExecuteNonQuery() > 0) ? Result.Success() : Result.Failure($"لم يتم تحديث بيانات العميل رقم ({customerID})، قد يكون المعرف غير موجود.");
                    }
                    catch (Exception ex)
                    {
                        return Result.Failure($"خطأ في الاتصال بقاعدة البيانات: {ex.Message}");
                    }
                }
            }
        }
        public static Result DeleteCustomerSoft(int customerID)
        {
            string query = @"UPDATE Customers 
                     SET IsActive = 0 
                     WHERE CustomerID = @CustomerID";

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CustomerID", customerID);

                    try
                    {
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return (rowsAffected > 0) ? Result.Success() : Result.Failure($"لم يتم تحديث بيانات العميل رقم ({customerID})، قد يكون المعرف غير موجود.");
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