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
        public static bool InsertCustomer(string customerName, string phone,
            string taxNumber,int createdBy, out string errMsg)
        {
            bool isInserted = false;
            errMsg = string.Empty;

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
                        // تنفيذ عملية الإدخال
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // إذا تمت العملية بنجاح ستكون الصفوف المتأثرة أكبر من 0
                        isInserted = (rowsAffected > 0);
                    }
                    catch (Exception ex)
                    {
                        // التقاط رسالة الـ RAISERROR من السيرفر إذا كان رقم الجوال مكرراً
                        isInserted = false;
                        errMsg = ex.Message;
                    }
                }
            }
            return isInserted; // تعيد true في حال النجاح و false في حال الفشل
        }
        public static bool UpdateCustomer(int customerID, string customerName, 
            string phone, string taxNumber,int updatedBy, out string errMsg)
        {
            bool isUpdated = false;
            errMsg = string.Empty;

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
                        // تنفيذ عملية التحديث
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // إذا تم التعديل بنجاح ستكون القيمة true
                        isUpdated = (rowsAffected > 0);
                    }
                    catch (Exception ex)
                    {
                        // التقاط رسالة الـ RAISERROR من السيرفر إذا كان رقم الجوال مستخدماً مع عميل آخر
                        isUpdated = false;
                        errMsg = ex.Message;
                    }
                }
            }
            return isUpdated; // تعيد true في حال النجاح و false في حال الفشل
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
