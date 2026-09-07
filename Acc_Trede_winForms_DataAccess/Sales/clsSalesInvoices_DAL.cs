using Acc_Trade_Core;
using Acc_Trede_winForms_DataAccess.Global;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Acc_Trede_winForms_DataAccess.Sales
{
    public class clsSalesInvoices_DAL
    {
        /// <summary>
        /// حفظ فاتورة مبيعات جديدة مع تفاصيلها وتحديث كميات المخزن وأرصدة العملاء الآجلة دفعة واحدة
        /// </summary>
        public static Result<int> InsertSalesInvoice(
            string invoiceNumber,
            int userID,
            int? customerID,
            decimal totalAmount,
            decimal discount,
            decimal taxAmount,
            decimal cashAmount,
            decimal cardAmount,
            DataTable salesCartDataTable)
        {
            int newBill = -1;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("dbo.sp_InsertSalesInvoice", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@InvoiceNumber", string.IsNullOrEmpty(invoiceNumber) ? (object)DBNull.Value : invoiceNumber);
                    command.Parameters.AddWithValue("@UserID", userID);
                    command.Parameters.AddWithValue("@CustomerID", customerID.HasValue ? (object)customerID.Value : DBNull.Value);
                    command.Parameters.AddWithValue("@TotalAmount", totalAmount);
                    command.Parameters.AddWithValue("@Discount", discount);
                    command.Parameters.AddWithValue("@TaxAmount", taxAmount);
                    command.Parameters.AddWithValue("@CashAmount", cashAmount);
                    command.Parameters.AddWithValue("@CardAmount", cardAmount);

                    SqlParameter tvpParameter = command.Parameters.AddWithValue("@Cart", salesCartDataTable);
                    tvpParameter.SqlDbType = SqlDbType.Structured;
                    tvpParameter.TypeName = "dbo.SalesCartType";

                    try
                    {
                        connection.Open();
                        object res = command.ExecuteScalar();
                        if (res != null && int.TryParse(res.ToString(), out int id))
                        {
                            newBill = id;
                        }
                    }
                    catch (Exception ex)
                    {
                        return Result<int>.Failure($"خطأ في الاتصال بقاعدة البيانات: {ex.Message}");
                    }
                }
            }
            return (newBill > 0) ? Result<int>.Success(newBill) : Result<int>.Failure("فشل إضافة فاتورة: لم يتم إرجاع معرف جديد من قاعدة البيانات.");
        }
    }
}
