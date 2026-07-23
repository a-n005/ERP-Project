using Acc_Trede_winForms_Buisness;
using Acc_Trede_winForms_DataAccess;
using Acc_Trede_winForms_DataAccess.Global;
using System;
using System.Data;

namespace Global
{
    [Flags] // تتيح دمج أكثر من صلاحية برقم واحد (Bitwise Operations)
    public enum enPermissions
    {
        None = 0,             // 0
        ShowUsers = 1,        // 1 << 0
        AddUser = 2,          // 1 << 1
        UpdateUser = 4,       // 1 << 2
        DeleteUser = 8,       // 1 << 3
        ManageInvoices = 16,  // 1 << 4
        ManageReports = 32,   // 1 << 5

        // دمج كامل الصلاحيات القائمة بدلاً من -1 لتجنب مشاكل المعالجة بالـ Bitwise
        All = ShowUsers | AddUser | UpdateUser | DeleteUser | ManageInvoices | ManageReports
    }

    public static class GlobalUser
    {
        // تخزين البيانات الأساسية للمستخدم الحالي (Default is null)
        public static clsUser_BLL CurrentUser { get; private set; }

        // الخيار الأفضل 1: تهيئة الجلسة بتمرير كائن BLL مكتمل ومجلوب من قاعدة البيانات
        public static void Initialize(clsUser_BLL user)
        {
            CurrentUser = user ?? throw new ArgumentNullException(nameof(user), "لا يمكن تهيئة المستخدم بكائن فارغ.");
        }

        // الخيار 2: إذا كنت تريد تعبئة كائن جديد يدوياً من القيم
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
            CurrentUser = null; // مسح مرجع الكائن بالكامل
        }
        public static bool IsLoggedIn => CurrentUser != null;
        public static Result Login(string username, string passwordHash)
        {
            Result<DataTable> res = clsUsers_DAL.LoginUser(username, passwordHash);
            if (res.IsFailure)
            {
                return Result.Failure(res.Error);
            }

            if (res.Value == null || res.Value.Rows.Count == 0)
            {
                return Result.Failure("لم يتم العثور على المستخدم المطلوب.");
            }

            DataRow dr = res.Value.Rows[0];

            clsUser_BLL user = clsUser_BLL.Initialize(dr);
            Initialize(user);
            return(user.IsActive) ? Result.Success():Result.Failure("خطأ: المستخدم غير نشط يجب التواصل مع الادارة!");
        }
    }
}