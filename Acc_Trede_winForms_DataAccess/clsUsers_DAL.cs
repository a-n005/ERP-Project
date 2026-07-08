using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acc_Trede_winForms_DataAccess
{
    public class clsUsers_DAL
    {
        /// <summary>
        /// ميثود للتحقق من تسجيل دخول المستخدم وجلب بياناته وصلاحياته
        /// </summary>
        public static DataTable LoginUser(string username, string passwordHash, out string errorMessage)
        {
            errorMessage = string.Empty;
            DataTable dt = new DataTable();

            string query = @"SELECT UserID, Username, Permissions, FullName, IsActive 
                     FROM Users 
                     WHERE Username = @Username AND PasswordHash = @PasswordHash";

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);

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
                        errorMessage = ex.Message;
                    }
                }
            }
            return dt; // ترجع الجدول والـ BLL هو من سيتصرف به لاحقاً!
        }

        /// <summary>
        /// إضافة مستخدم جديد للنظام
        /// </summary>
        public static int RegisterUser(string username, string passwordHash, int permissions, string fullName,
            string phone, bool isActive, out string errMsg)
        {
            int newUserID = -1;
            errMsg = string.Empty;
            // استدعاء اسم الإجراء المخزن الفعلي في قاعدتك
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_RegisterUser", conn))
                {
                    // إعلام السيرفر أننا نستدعي إجرائاً مخزناً وليس نص كويري
                    cmd.CommandType = CommandType.StoredProcedure;

                    // تمرير البارامترات للإجراء المخزن
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);
                    cmd.Parameters.AddWithValue("@Permissions", permissions);
                    cmd.Parameters.AddWithValue("@FullName", fullName);
                    cmd.Parameters.AddWithValue("@Phone", (object)phone ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@IsActive", isActive);

                    try
                    {
                        conn.Open();
                        // جلب قيمة الـ SCOPE_IDENTITY() الراجعة من الإجراء المخزن
                        object result = cmd.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int insertedId))
                        {
                            newUserID = insertedId;
                        }
                    }
                    catch (Exception ex)
                    {
                        // معالجة الأخطاء عند الحاجة
                        errMsg = ex.Message;
                    }
                }
            }
            return newUserID; // يعيد المعرف التلقائي الجديد للمستخدم
        }

        /// <summary>
        /// جلب جميع المستخدمين لعرضهم في الـ DataGrid داخل الـ WPF
        /// </summary>
        public static DataTable GetAllUsers(out string errorMessage)
        {
            errorMessage = string.Empty;
            DataTable dt = new DataTable();
            string query = "SELECT UserID, Username, Permissions, FullName, Phone," +
                " IsActive, CreatedAt FROM Users";

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
                        errorMessage = ex.Message;
                    }
                }
            }
            return dt;
        }
        // <summary>
        // Get user by ID
        // <summary>
        public static DataTable GetUserByID(int userid, out string errMsg)
        {
            errMsg = string.Empty;
            DataTable dt = new DataTable();
            string query = @"SELECT UserID, Username, Permissions, FullName, Phone,
                IsActive, CreatedAt FROM Users where userid= @userid";
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@userid", userid);
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
        // <summary>
        // Get user by Username
        // <summary>
        public static DataTable GetUserByUserName(int username, out string errMsg)
        {
            errMsg = string.Empty;
            DataTable dt = new DataTable();
            string query = @"SELECT UserID, Username, Permissions, FullName, Phone,
                IsActive, CreatedAt FROM Users where username= @username";
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
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
        /// <summary>
        /// تحديث بيانات المستخدم وصلاحياته
        /// </summary>
        public static bool UpdateUser(int userID, string username, int permissions, string fullName, string phone, bool isActive, out string errMsg)
        {
            bool isUpdated = false;
            errMsg = string.Empty;

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_UpdateUser", conn))
                {
                    // تحديد نوع الأمر كـ Stored Procedure
                    cmd.CommandType = CommandType.StoredProcedure;

                    // تمرير البارامترات المطلوبة للإجراء المخزن
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Permissions", permissions);
                    cmd.Parameters.AddWithValue("@FullName", fullName);
                    cmd.Parameters.AddWithValue("@Phone", (object)phone ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@IsActive", isActive);

                    try
                    {
                        conn.Open();
                        // نستخدم ExecuteNonQuery لأننا نقوم بعملية تحديث ولا ننتظر راجعاً كـ ID أو جدول
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // في حال نجاح التعديل ستكون الصفوف المتأثرة أكبر من 0
                        isUpdated = (rowsAffected > 0);
                    }
                    catch (Exception ex)
                    {
                        // التقاط خطأ الـ RAISERROR من السيرفر إذا كان اسم المستخدم مكرراً
                        isUpdated = false;
                        errMsg = ex.Message;
                    }
                }
            }
            return isUpdated; // تعيد true في حال النجاح و false في حال الفشل
        }

        /// <summary>
        /// تحديث كلمة المرور فقط (منفصلة لأواعي الأمان)
        /// </summary>
        public static bool UpdatePassword(int userId, string newPasswordHash, out string errorMessage)
        {
            errorMessage = string.Empty;
            string query = "UPDATE Users SET PasswordHash = @PasswordHash WHERE UserID = @UserID";

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    cmd.Parameters.AddWithValue("@PasswordHash", newPasswordHash);

                    try
                    {
                        conn.Open();
                        return cmd.ExecuteNonQuery() > 0;
                    }
                    catch (Exception ex)
                    {
                        errorMessage = ex.Message;
                        return false;
                    }
                }
            }
        }

        /// </summary>
        /// Delete the user by disabling activation.
        /// <returns></returns>
        public static bool DeleteUserSoft(int userID, out string errMsg)
        {
            bool isDeleted = false;
            errMsg = string.Empty;

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_DeleteUserSoft", conn))
                {
                    // تحديد نوع الأمر كـ Stored Procedure
                    cmd.CommandType = CommandType.StoredProcedure;

                    // تمرير المعرف الخاص بالمستخدم المراد إيقافه
                    cmd.Parameters.AddWithValue("@UserID", userID);

                    try
                    {
                        conn.Open();
                        // تنفيذ العملية ومعرفة عدد الصفوف المتأثرة
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // إذا تم التحديث بنجاح ستكون القيمة true
                        isDeleted = (rowsAffected > 0);
                    }
                    catch (Exception ex)
                    {
                        isDeleted = false;
                        errMsg = ex.Message;
                    }
                }
            }
            return isDeleted; // تعيد true في حال النجاح و false في حال حدوث أي خلل
        }
    }
}

