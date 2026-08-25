using Acc_Trade_Core;
using Acc_Trede_winForms_DataAccess.Global;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Acc_Trede_winForms_DataAccess.Sales
{
    public class clsSalesInvoiceDetails_DAL
    {
        /// <summary>
        /// جلب جميع الأسطر والأصناف التابعة لفاتورة مبيعات معينة عبر الـ InvoiceID (مع جلب الـ Barcode)
        /// </summary>
        /// <returns>I'll change it to view </returns>
        public static Result<DataTable> GetDetailsByInvoiceID(int invoiceID)
        {
            DataTable dt = new DataTable();

            string query = @"SELECT D.DetailID, D.InvoiceID, D.ProductID, 
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
                        return Result<DataTable>.Success(dt);
                    }
                    catch (Exception ex)
                    {
                        return Result<DataTable>.Failure($"خطأ في الاتصال بقاعدة البيانات: {ex.Message}");
                    }
                }
            }
        }
    }
}
