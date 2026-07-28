using Acc_Trede_winForms_DataAccess.Global;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acc_Trede_winForms_DataAccess
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
                    // تحديد نوع الأمر كـ Stored Procedure
                    cmd.CommandType = CommandType.StoredProcedure;

                    // تمرير البارامترات المطلوبة للإجراء المخزن مع معالجة القيم الفارغة
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
            int isUpdated = -1;
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_UpdateCustomer", conn))
                {
                    // تحديد نوع الأمر كـ Stored Procedure
                    cmd.CommandType = CommandType.StoredProcedure;

                    // تمرير البارامترات المطلوبة للإجراء المخزن
                    cmd.Parameters.AddWithValue("@CustomerID", customerID);
                    cmd.Parameters.AddWithValue("@CustomerName", customerName);
                    cmd.Parameters.AddWithValue("@Phone", (object)phone ?? DBNull.Value); // معالجة الجوال الفارغ
                    cmd.Parameters.AddWithValue("@TaxNumber", (object)taxNumber ?? DBNull.Value); // معالجة الرقم الضريبي الفارغ
                    cmd.Parameters.AddWithValue("@UpdatedBy", updatedBy);

                    try
                    {
                        conn.Open();
                        object result = cmd.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int updatedID))
                        {
                            isUpdated = updatedID;
                        }
                    }
                    catch (Exception ex)
                    {
                        return Result<int>.Failure($"خطأ في الاتصال بقاعدة البيانات: {ex.Message}");
                    }
                }
            }
            return (isUpdated > 0) ? Result.Success() : Result.Failure("فشل إضافة المستخدم: لم يتم إرجاع معرف جديد من قاعدة البيانات.");
        }
        public static bool DeleteCustomerSoft(int customerID, out string errMsg)
        {
            bool isDeleted = false;
            errMsg = string.Empty;

            // كويري مباشر لتحديث حالة العميل إلى غير نشط
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
                        isDeleted = (rowsAffected > 0);
                    }
                    catch (Exception ex)
                    {
                        isDeleted = false;
                        errMsg = ex.Message;
                    }
                }
            }
            return isDeleted;
        }
        public static DataTable GetAllCustomers(out string errMsg)
        {
            DataTable dt = new DataTable();
            errMsg = string.Empty;

            // جلب بيانات العملاء حسب حالتهم (نشط / أرشيف) مع جلب الرصيد الحالي
            string query = @"SELECT *
                     FROM Customers 
                     ORDER BY CustomerName ASC";

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {

                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                    catch (Exception ex)
                    {
                        errMsg = ex.Message;
                    }
                }
            }
            return dt;
        }
        public static DataTable GetCustomerByID(int customerID, out string errMsg)
        {
            DataTable dt = new DataTable();
            errMsg = string.Empty;

            string query = @"SELECT *
                     FROM Customers 
                     WHERE CustomerID = @CustomerID";

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CustomerID", customerID);

                    try
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                    catch (Exception ex)
                    {
                        errMsg = ex.Message;
                    }
                }
            }
            return dt;
        }

    }
}
