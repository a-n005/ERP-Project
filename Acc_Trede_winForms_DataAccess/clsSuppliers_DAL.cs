using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acc_Trede_winForms_DataAccess
{
    public class clsSuppliers_DAL
    {
        public static bool InsertSupplier(string supplierName, string companyName,
            string phone, string taxNumber,int createdBy, out string errMsg)
        {
            bool isInserted = false;
            errMsg = string.Empty;

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_InsertSupplier", conn))
                {
                    // تحديد نوع الأمر كـ Stored Procedure
                    cmd.CommandType = CommandType.StoredProcedure;

                    // تمرير البارامترات المطلوبة للإجراء المخزن مع معالجة القيم الفارغة
                    cmd.Parameters.AddWithValue("@SupplierName", supplierName);
                    cmd.Parameters.AddWithValue("@CompanyName", (object)companyName ?? DBNull.Value);
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
                        isInserted = false;
                        errMsg = ex.Message;
                    }
                }
            }
            return isInserted;
        }
        public static bool UpdateSupplier(int supplierID, string supplierName,
            string companyName, string phone, string taxNumber,int updatedBy, out string errMsg)
        {
            bool isUpdated = false;
            errMsg = string.Empty;

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_UpdateSupplier", conn))
                {
                    // تحديد نوع الأمر كـ Stored Procedure
                    cmd.CommandType = CommandType.StoredProcedure;

                    // تمرير البارامترات المطلوبة للإجراء المخزن
                    cmd.Parameters.AddWithValue("@SupplierID", supplierID);
                    cmd.Parameters.AddWithValue("@SupplierName", supplierName);
                    cmd.Parameters.AddWithValue("@CompanyName", (object)companyName ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Phone", (object)phone ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@TaxNumber", (object)taxNumber ?? DBNull.Value);
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
                        isUpdated = false;
                        errMsg = ex.Message;
                    }
                }
            }
            return isUpdated; // تعيد true في حال النجاح و false في حال الفشل
        }
        public static bool DeleteSupplierSoft(int supplierID, out string errMsg)
        {
            bool isDeleted = false;
            errMsg = string.Empty;

            // كويري مباشر لتحديث حالة المورد إلى غير نشط بدلاً من مسحه نهائياً
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
        public static DataTable GetAllSuppliers(out string errMsg)
        {
            DataTable dt = new DataTable();
            errMsg = string.Empty;

            // جلب بيانات الموردين بالتفصيل مع ترتيبهم أبجدياً حسب اسم الشركة أو المورد
            string query = @"SELECT *
                     FROM Suppliers 
                     ORDER BY CompanyName ASC, SupplierName ASC";

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
        public static DataTable GetSupplierByID(int supplierID, out string errMsg)
        {
            DataTable dt = new DataTable();
            errMsg = string.Empty;

            // كويري دقيق لجلب بيانات مورد واحد فقط بناءً على الـ ID
            string query = @"SELECT *
                     FROM Suppliers 
                     WHERE SupplierID = @SupplierID";

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // تمرير الـ ID كبارامتر آمن لحماية الكويري
                    cmd.Parameters.AddWithValue("@SupplierID", supplierID);

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
            return dt; // سيعيد الجدول بصف واحد للمورد، أو فارغاً إذا لم يعثر على الـ ID
        }
    }
}
