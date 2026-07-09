using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acc_Trede_winForms_DataAccess.Database
{
    public class clsSalesInvoicesDetails_DAL
    {
        /// <summary>
        /// جلب جميع الأسطر والأصناف التابعة لفاتورة مبيعات معينة عبر الـ InvoiceID (مع جلب الـ Barcode)
        /// </summary>
        public static DataTable GetDetailsByInvoiceID(int invoiceID, out string errorMessage)
        {
            DataTable dt = new DataTable();
            errorMessage = string.Empty;

            // استعلام يجلب تفاصيل الأصناف المباعة مع جلب اسم المنتج والباركود الخاص به للواجهات
            // لاحظ أننا نقرأ CostPriceAtSale المخرن لحظة البيع لحساب الأرباح بدقة لاحقاً في الـ BLL
            string query = @"SELECT D.InvoiceDetailID, D.InvoiceID, D.ProductID, 
                                    P.Barcode, P.ProductName, D.Quantity, D.UnitPrice,
                                    D.CostPriceAtSale,
                                    (D.Quantity * D.UnitPrice) AS TotalLinePrice
                             FROM SalesInvoiceDetails D
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
                        errorMessage = "خطأ أثناء جلب تفاصيل الأصناف لفاتورة المبيعات: " + ex.Message;
                    }
                }
            }
            return dt;
        }
    }
}
