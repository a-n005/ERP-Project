using Acc_Trede_winForms_DataAccess.Global;
using Acc_Trade_Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acc_Trede_winForms_DataAccess.Purchases
{
    public class clsPurchaseInvoiceDetails_DAL
    {
        /// <summary>
        /// جلب جميع الأسطر والأصناف التابعة لفاتورة مشتريات معينة عبر الـ InvoiceID
        /// </summary>
        public static Result<DataTable> GetDetailsByInvoiceID(int invoiceID)
        {
            DataTable dt = new DataTable();

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
                        return Result<DataTable>.Success(dt);
                    }
                    catch (Exception ex)
                    {
                        return Result<DataTable>.Failure($"خطأ في الاتصال بقاعدة البيانات: {ex.Message}");
                    }
                }
            }
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
