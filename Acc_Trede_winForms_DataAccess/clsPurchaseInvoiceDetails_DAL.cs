using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acc_Trede_winForms_DataAccess.Database
{
    public class clsPurchaseInvoiceDetails_DAL
    {
        /// <summary>
        /// جلب جميع الأسطر والأصناف التابعة لفاتورة مشتريات معينة عبر الـ InvoiceID
        /// </summary>
        public static DataTable GetDetailsByInvoiceID(int invoiceID, out string errorMessage)
        {
            DataTable dt = new DataTable();
            errorMessage = string.Empty;

            // استعلام يجلب تفاصيل الأصناف مع جلب اسم المنتج وكوده للواجهات
            string query = @"SELECT D.PurchaseDetailID, D.InvoiceID, D.ProductID, 
                                    P.Barcode, P.ProductName, D.Quantity, D.UnitPrice,
                                    (D.Quantity * D.UnitPrice) AS TotalLinePrice
                             FROM PurchaseInvoiceDetails D
                             INNER JOIN Products P ON D.ProductID = P.ProductID
                             WHERE D.InvoiceID = @InvoiceID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@InvoiceID", invoiceID);

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
                        errorMessage = "خطأ أثناء جلب تفاصيل الأصناف للفاتورة: " + ex.Message;
                    }
                }
            }
            return dt;
        }

        public static SqlParameter InsertCart(DataTable dt)
        {
            SqlParameter tvpParameter = new SqlParameter();
            tvpParameter.ParameterName = "@Cart";
            tvpParameter.SqlDbType = SqlDbType.Structured;
            tvpParameter.TypeName = "dbo.PurchaseCartType";
            tvpParameter.Value = dt;
            return tvpParameter;
        }
    }
}
