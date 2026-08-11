using Acc_Trede_winForms_DataAccess.Global;
using Acc_Trade_Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acc_Trede_winForms_DataAccess.UserManagement
{
    public class clsUsers_DAL
    {
        /// <summary>
        /// ميثود للتحقق من تسجيل دخول المستخدم وجلب بياناته وصلاحياته
        /// </summary>
        public static Result<DataTable> LoginUser(string username, string passwordHash)
        {
            DataTable dt = new DataTable();

            string query = @"SELECT * 
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
                        return Result<DataTable>.Failure($"خطأ في الاتصال بقاعدة البيانات: {ex.Message}");
                    }
                }
            }
            return (dt.Rows.Count > 0) ? Result<DataTable>.Success(dt) : Result<DataTable>.Failure("اسم المستخدم أو كلمة المرور غير صحيحة.");
        }
        public static Result<int> AddNewUser(string username, string passwordHash, int permissions, string fullName, string phone = null)
        {
            int newUserID = -1;
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_RegisterUser", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);
                    cmd.Parameters.AddWithValue("@Permissions", permissions);
                    cmd.Parameters.AddWithValue("@FullName", fullName);
                    cmd.Parameters.AddWithValue("@Phone", (object)phone ?? DBNull.Value);

                    try
                    {
                        conn.Open();
                        object result = cmd.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int insertedId))
                        {
                            newUserID = insertedId;
                        }
                    }
                    catch (Exception ex)
                    {
                        return Result<int>.Failure($"خطأ في الاتصال بقاعدة البيانات: {ex.Message}");
                    }
                }
            }
            return (newUserID > 0) ? Result<int>.Success(newUserID) : Result<int>.Failure("فشل إضافة المستخدم: لم يتم إرجاع معرف جديد من قاعدة البيانات.");
        }
        public static Result<DataTable> GetAllUsers()
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM Users";

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
                        return Result<DataTable>.Success(dt);
                    }
                    catch (Exception ex)
                    {
                        return Result<DataTable>.Failure($"خطأ في الاتصال بقاعدة البيانات: {ex.Message}");
                    }
                }
            }
        }
        public static Result<DataTable> GetUserByID(int userid)
        {
            DataTable dt = new DataTable();
            string query = @"SELECT * FROM Users where userid= @userid";
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
                        return (dt.Rows.Count > 0) ? Result<DataTable>.Success(dt) : Result<DataTable>.Failure($"عذراً، لم يتم العثور على مستخدم بالرقم المعرف: {userid}");
                    }
                    catch (Exception ex)
                    {
                        return Result<DataTable>.Failure($"خطأ في الاتصال بقاعدة البيانات: {ex.Message}");
                    }
                }
            }
        }
        public static Result UpdateUser(int userID, string username, int permissions, string fullName, bool isActive, int updatedBy, string phone = null)
        {

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_UpdateUser", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UserID", userID);
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Permissions", permissions);
                    cmd.Parameters.AddWithValue("@FullName", fullName);
                    cmd.Parameters.AddWithValue("@Phone", (object)phone ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@IsActive", isActive);
                    cmd.Parameters.AddWithValue("@UpdatedBy", updatedBy);

                    try
                    {
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return (rowsAffected > 0) ? Result.Success() : Result.Failure($"لم يتم تحديث بيانات المستخدم رقم ({userID})، قد يكون المعرف غير موجود.");
                    }
                    catch (Exception ex)
                    {
                        return Result.Failure($"خطأ في الاتصال بقاعدة البيانات: {ex.Message}");
                    }
                }
            }
        }
        /// <summary>
        /// تحديث كلمة المرور فقط (منفصلة لأواعي الأمان)
        /// </summary>
        public static Result UpdatePassword(int userId, string newPasswordHash)
        {
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
                        return cmd.ExecuteNonQuery() > 0 ? Result.Success() : Result.Failure("خطأ: لا يمكن تغيير كلمة المرور!");
                    }
                    catch (Exception ex)
                    {
                        return Result.Failure($"خطأ في الاتصال بقاعدة البيانات: {ex.Message}");
                    }
                }
            }
        }
        /// </summary>
        /// Delete the user by disabling activation.
        /// <returns></returns>
        public static Result DeleteUserSoft(int userID)
        {
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_DeleteUserSoft", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UserID", userID);

                    try
                    {
                        conn.Open();
                        return (cmd.ExecuteNonQuery() > 0) ? Result.Success() : Result.Failure("خطأ: لا يمكن حذف المستخدم حاليا!");

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

