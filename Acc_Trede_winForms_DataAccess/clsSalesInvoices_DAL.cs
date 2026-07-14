using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acc_Trede_winForms_DataAccess.Database
{
    public class clsSalesInvoices_DAL
    {
        /// <summary>
        /// حفظ فاتورة مبيعات جديدة مع تفاصيلها وتحديث كميات المخزن وأرصدة العملاء الآجلة دفعة واحدة
        /// </summary>
        public static int InsertSalesInvoice(
            string invoiceNumber,
            int userID,
            int? customerID,
            string paymentType,
            decimal totalAmount,
            decimal discount,
            decimal taxAmount,
            decimal netAmount,
            decimal cashAmount,
            decimal cardAmount,
            DataTable salesCartDataTable, // سلة المبيعات DataTable تطابق التايب dbo.SalesCartType
            out string errorMessage)
        {
            int rowsAffected = 0;
            errorMessage = string.Empty;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("dbo.sp_InsertSalesInvoice", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // تمرير البارامترات طبقاً للـ Stored Procedure
                    command.Parameters.AddWithValue("@InvoiceNumber", string.IsNullOrEmpty(invoiceNumber) ? (object)DBNull.Value : invoiceNumber);
                    command.Parameters.AddWithValue("@UserID", userID);
                    command.Parameters.AddWithValue("@CustomerID", customerID.HasValue ? (object)customerID.Value : DBNull.Value);
                    command.Parameters.AddWithValue("@PaymentType", paymentType);
                    command.Parameters.AddWithValue("@TotalAmount", totalAmount);
                    command.Parameters.AddWithValue("@Discount", discount);
                    command.Parameters.AddWithValue("@TaxAmount", taxAmount);
                    command.Parameters.AddWithValue("@NetAmount", netAmount);
                    command.Parameters.AddWithValue("@CashAmount", cashAmount);
                    command.Parameters.AddWithValue("@CardAmount", cardAmount);

                    // تمرير الـ Table-Valued Parameter (TVP) لسلة المبيعات
                    SqlParameter tvpParameter = command.Parameters.AddWithValue("@Cart", salesCartDataTable);
                    tvpParameter.SqlDbType = SqlDbType.Structured;
                    tvpParameter.TypeName = "dbo.SalesCartType"; // يجب أن يطابق اسم الـ Type في السيرفر تماماً

                    try
                    {
                        connection.Open();
                        object res = command.ExecuteScalar();
                        if(res!= null && int.TryParse(res.ToString(), out int id))
                        {
                            rowsAffected=id;
                        }
                    }
                    catch (SqlException ex)
                    {
                        // اقتناص رسائل الـ RAISERROR المخصصة من الـ SQL (مثل: لا يمكن حفظ فاتورة بها متبقٍّ آجل بدون تحديد العميل!)
                        errorMessage = ex.Message;
                        return -1;
                    }
                    catch (Exception ex)
                    {
                        errorMessage = "خطأ عام في النظام: " + ex.Message;
                        return -1;
                    }
                }
            }
            return rowsAffected;
        }

        /// <summary>
        /// جلب جميع فواتير المبيعات المسجلة لعرضها في جدول الإدارة الرئيسي (Dashboard)
        /// </summary>
        /// <returns> I'll change to view and falg enum </returns>
        public static DataTable GetAllSalesInvoices(out string errorMessage)
        {
            DataTable dt = new DataTable();
            errorMessage = string.Empty;

            // استعلام يجلب البيانات الأساسية مع إظهار "عميل نقدي" إذا كان الـ CustomerID خالي
            string query = @"SELECT SI.InvoiceID, SI.InvoiceNumber, SI.InvoiceDate, 
                                    ISNULL(C.CustomerName, N'عميل نقدي') AS CustomerName, 
                                    SI.NetAmount, SI.PaymentType, SI.TotalAmount, SI.Discount, SI.TaxAmount
                             FROM SalesInvoices SI
                             LEFT JOIN Customers C ON SI.CustomerID = C.CustomerID
                             ORDER BY SI.InvoiceDate DESC";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
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
                        errorMessage = "خطأ أثناء جلب فواتير المبيعات: " + ex.Message;
                    }
                }
            }
            return dt;
        }
    }
}
