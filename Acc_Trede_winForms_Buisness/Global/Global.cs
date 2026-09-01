using Acc_Trade_Core;
using Acc_Trede_winForms_Buisness.Global;
using Acc_Trede_winForms_Buisness.UserManagement;
using System;
using System.Data;

namespace Global
{
    [Flags]
    public enum enPermissions
    {
        None = 0,             // 0
        ShowUsers = 1,        // 1 << 0
        AddUser = 2,          // 1 << 1
        UpdateUser = 4,       // 1 << 2
        DeleteUser = 8,       // 1 << 3
        ManageInvoices = 16,  // 1 << 4
        ManageReports = 32,   // 1 << 5

        All = ShowUsers | AddUser | UpdateUser | DeleteUser | ManageInvoices | ManageReports
    }

    public static class GlobalUser
    {
        // make method to set generate num for invoices

        public static clsUser_BLL CurrentUser { get; private set; }

        public static void Initialize(clsUser_BLL user)
        {
            CurrentUser = user ?? throw new ArgumentNullException(nameof(user), "لا يمكن تهيئة المستخدم بكائن فارغ.");
        }

        public static void Initialize(int userId, string username, string fullName, enPermissions permissions)
        {
            CurrentUser = new clsUser_BLL
            {
                UserID = userId,
                UserName = username,
                FullName = fullName,
                Permissions = permissions,
                IsActive = true
            };
        }

        public static void LogOut()
        {
            CurrentUser = null;
        }

        public static bool IsLoggedIn => CurrentUser != null;

        public static Result Login(string username, string password,bool rememberMe)
        {
            Result<DataTable> res = clsUser_BLL.LoginUser(username, password);
            if (res.IsFailure)
            {
                return Result.Failure(res.Error);
            }

            if (res.Value == null || res.Value.Rows.Count == 0)
            {
                return Result.Failure("اسم المستخدم أو كلمة المرور غير صحيحة.");
            }

            DataRow dr = res.Value.Rows[0];

            clsUser_BLL user = clsUser_BLL.MapFromDataRow(dr);

            if (!user.IsActive)
            {
                return Result.Failure("خطأ: حساب المستخدم معطل، يرجى التواصل مع الإدارة!");
            }
            Initialize(user);

            HelperRegistre.Save(username, password, rememberMe);
            return Result.Success();
        }
    }
}