using Acc_Trade_Core;
using Acc_Trede_winForms_Buisness.Global;
using Acc_Trede_winForms_Buisness.UserManagement;
using System;

namespace Acc_Trede_winForms_Buisness.Global
{
    #region Enums
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

        Admin = ShowUsers | AddUser | UpdateUser | DeleteUser | ManageInvoices | ManageReports
        // total is 63
    }
    #endregion
    public static class GlobalUser
    {


        #region Properties
        public static clsUser_BLL CurrentUser { get; private set; }
        public static bool IsLoggedIn => CurrentUser != null;
        #endregion

        #region Private Method
        private static void Initialize(clsUser_BLL user)
        {
            CurrentUser = user ?? throw new ArgumentNullException(nameof(user), "لا يمكن تهيئة المستخدم بكائن فارغ.");
        }

        private static void Initialize(int userId, string username, string fullName, enPermissions permissions)
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
        #endregion

        #region Public Method
        public static void LogOut()
        {
            CurrentUser = null;
        }
        public static Result Login(string username, string password, bool rememberMe)
        {
            password = Helper.Encrypt(password);
            Result<clsUser_BLL> res = clsUser_BLL.Login(username, password);
            if (res.IsFailure)
            {
                return Result.Failure(res.Error);
            }

            if (res.Value == null || res.Value.UserID == -1)
            {
                return Result.Failure("اسم المستخدم أو كلمة المرور غير صحيحة.");
            }


            if (!res.Value.IsActive)
            {
                return Result.Failure("خطأ: حساب المستخدم معطل، يرجى التواصل مع الإدارة!");
            }
            Initialize(res.Value);

            Helper.Save(username, password, rememberMe);
            return Result.Success();
        }
        #endregion

        // make method to set generate num for invoices
    }
}