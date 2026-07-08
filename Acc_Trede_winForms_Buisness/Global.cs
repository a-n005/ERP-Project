using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global
{
    public static class GlobalUser
    {
        // تخزين البيانات الأساسية للمستخدم الحالي
        public static int UserID { get; set; }
        public static string Username { get; set; }
        public static string FullName { get; set; }
        public static int Permissions { get; set; }

        // ميثود لتعبئة البيانات فور نجاح تسجيل الدخول
        public static void Initialize(int userId, string username, string fullName, int permissions)
        {
            UserID = userId;
            Username = username;
            FullName = fullName;
            Permissions = permissions;
        }
        // ميثود لتسجيل الخروج وتنظيف البيانات
        public static void LogOut()
        {
            UserID = 0;
            Username = null;
            FullName = null;
            Permissions = 0;
        }

        // دالة ذكية لفحص الصلاحيات بالـ Bitwise Flags تلقائياً من أي شاشة
        public static bool HasPermission(int permissionFlag)
        {
            // فحص باينري (Bitwise AND)
            return (Permissions & permissionFlag) == permissionFlag;
        }
    }
}
