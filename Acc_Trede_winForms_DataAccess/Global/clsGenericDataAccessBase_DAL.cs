using System;
using System.Data;
using System.Data.SqlClient;

namespace Acc_Trede_winForms_DataAccess.Global
{
    public static class clsGenericDataAccessBase_DAL
    {
        /// <summary>
        /// دالة عامة وآمنة لتنفيذ أي استعلام SELECT (جداول، فيوهات، تقارير) قادم من الـ BLL
        /// </summary>
        /// <param name="query">نص استعلام الـ SQL المُراد تنفيذه</param>
        /// <param name="parameters">مصفوفة اختياريّة من البارامترات لحماية البيانات من الـ SQL Injection</param>
        public static DataTable ExecuteSelectQuery(string query, SqlParameter[] parameters = null)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // إذا أرسل الـ BLL بارامترات للفلترة، نقوم بإضافتها بأمان للـ Command
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows) dt.Load(reader);
                        }
                    }
                    catch (Exception ex)
                    {
                        // يمكنك هنا تسجيل الخطأ في ملف Log أو طباعته للـ Debugging
                        Console.WriteLine("Generic Data Access Error: " + ex.Message);
                    }
                }
            }
            return dt;
        }
    }
}