using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acc_Trede_winForms_DataAccess
{
    public class clsProducts_DAL
    {
        public static bool InsertProduct(string barcode, string productName, decimal costPrice,
            decimal salePrice, int stockQuantity, int minStockAlert, int createdBy, out string errMsg)
        {
            bool isInserted = false;
            errMsg = string.Empty;

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_InsertProduct", conn))
                {
                    // تحديد نوع الأمر كـ Stored Procedure
                    cmd.CommandType = CommandType.StoredProcedure;

                    // تمرير البارامترات المطلوبة للإجراء المخزن مع معالجة القيم الفارغة للباركود
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
                        // تنفيذ عملية الإدخال
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // إذا تمت العملية بنجاح ستكون الصفوف المتأثرة أكبر من 0
                        isInserted = (rowsAffected > 0);
                    }
                    catch (Exception ex)
                    {
                        // التقاط رسالة الـ RAISERROR من السيرفر إذا كان الباركود مكرراً
                        isInserted = false;
                        errMsg = ex.Message;
                    }
                }
            }
            return isInserted; // تعيد true في حال النجاح و false في حال الفشل
        }
        public static bool UpdateProduct(int productID, string barcode, string productName,
            decimal costPrice, decimal salePrice, int stockQuantity, int minStockAlert, int updatedBy, out string errMsg)
        {
            bool isUpdated = false;
            errMsg = string.Empty;

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_UpdateProduct", conn))
                {
                    // تحديد نوع الأمر كـ Stored Procedure
                    cmd.CommandType = CommandType.StoredProcedure;

                    // تمرير البارامترات المطلوبة للإجراء المخزن
                    cmd.Parameters.AddWithValue("@ProductID", productID);
                    cmd.Parameters.AddWithValue("@Barcode", (object)barcode ?? DBNull.Value); // معالجة الباركود الفارغ
                    cmd.Parameters.AddWithValue("@ProductName", productName);
                    cmd.Parameters.AddWithValue("@CostPrice", costPrice);
                    cmd.Parameters.AddWithValue("@SalePrice", salePrice);
                    cmd.Parameters.AddWithValue("@StockQuantity", stockQuantity);
                    cmd.Parameters.AddWithValue("@MinStockAlert", minStockAlert);
                    cmd.Parameters.AddWithValue("@UpdatedBy", updatedBy);

                    try
                    {
                        conn.Open();
                        // تنفيذ عملية التحديث وننتظر عدد الصفوف المتأثرة
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // إذا تم التعديل بنجاح ستكون القيمة true
                        isUpdated = (rowsAffected > 0);
                    }
                    catch (Exception ex)
                    {
                        // التقاط رسالة الـ RAISERROR من السيرفر إذا كان الباركود مستخدم مع منتج آخر
                        isUpdated = false;
                        errMsg = ex.Message;
                    }
                }
            }
            return isUpdated; // تعيد true في حال النجاح و false في حال الفشل
        }
        public static DataTable GetLowStockProducts(out string errMsg)
        {
            DataTable dt = new DataTable();
            errMsg = string.Empty;

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetLowStockProducts", conn))
                {
                    // تحديد نوع الأمر كـ Stored Procedure
                    cmd.CommandType = CommandType.StoredProcedure;

                    try
                    {
                        conn.Open();
                        // استخدام SqlDataReader لقراءة صفوف المنتجات النواقص وتعبئتها في الـ DataTable
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
            return dt; // تعيد الجدول ممتلئاً بالمنتجات النواقص، أو فارغاً في حال لا توجد نواقص أو حدث خطأ
        }
        public static bool DeleteProductSoft(int productID, int updatedBy, out string errMsg)
        {
            bool isDeleted = false;
            errMsg = string.Empty;

            // نص الكويري المباشر لتحديث حالة المنتج إلى غير نشط
            string query = @"UPDATE Products 
                     SET IsActive = 0 , updatedBy= @UpdatedBy ,UpdatedAt = getDate()
                     WHERE ProductID = @ProductID";

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // لا نحدد CommandType.StoredProcedure لأنها كويري نصية عادية (Text)

                    // تمرير البارامتر بشكل آمن
                    cmd.Parameters.AddWithValue("@ProductID", productID);
                    cmd.Parameters.AddWithValue("@UpdatedBy", updatedBy);

                    try
                    {
                        conn.Open();
                        // تنفيذ الكويري في السيرفر
                        isDeleted = cmd.ExecuteNonQuery() > 0;
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
        public static DataTable GetAllProducts(out string errMsg)
        {
            DataTable dt = new DataTable();
            errMsg = string.Empty;

            // نص الكويري المباشر لجلب المنتجات النشطة وترتيبها
            string query = @"SELECT *
                     FROM Products 
                     ORDER BY ProductName ASC";

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    try
                    {
                        conn.Open();
                        // استخدام SqlDataReader لقراءة البيانات وتفريغها في الـ DataTable
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
            return dt; // تعيد الجدول بالمنتجات أو فارغاً في حال حدوث خطأ
        }
        public static DataTable GetProductByID(int productID, out string errMsg)
        {
            DataTable dt = new DataTable();
            errMsg = string.Empty;

            // كويري مباشر لجلب بيانات منتج واحد محدد بالـ ID
            string query = @"SELECT *
                     FROM Products 
                     WHERE ProductID = @ProductID";

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // تمرير المعرف بشكل آمن تماماً لحماية البيانات
                    cmd.Parameters.AddWithValue("@ProductID", productID);

                    try
                    {
                        conn.Open();
                        // قراءة البيانات وتعبئتها في الجدول
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
            return dt; // سيعيد جدولاً يحتوي على صف واحد فقط في حال النجاح
        }
        public static DataTable GetProductByBarcode(string barcode, out string errMsg)
        {
            DataTable dt = new DataTable();
            errMsg = string.Empty;

            // كويري دقيق ومحدد الحقول لجلب منتج واحد مطابق للباركود ونشط
            string query = @"SELECT *
                     FROM Products 
                     WHERE Barcode = @Barcode";

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // تمرير الباركود كبارامتر آمن لحماية الكويري
                    cmd.Parameters.AddWithValue("@Barcode", barcode);

                    try
                    {
                        conn.Open();
                        // قراءة الصف وتفريغه سريعاً في الـ DataTable
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
            return dt; // سيعيد الجدول بصف واحد للمنتج، أو فارغاً إذا لم يعثر على الباركود
        }
    }
}
