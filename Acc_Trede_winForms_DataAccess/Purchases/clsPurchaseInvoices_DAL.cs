using Acc_Trade_Core;
using Acc_Trede_winForms_DataAccess.Global;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acc_Trede_winForms_DataAccess.Purchases
{
    public class clsPurchaseInvoices_DAL
    {
        /// <summary>
        /// حفظ فاتورة مشتريات جديدة مع تفاصيلها وتحديث المخازن وأرصدة الموردين دفعة واحدة
        /// </summary>
        public static Result<int> InsertPurchaseInvoice(
            string supplierInvoiceNumber,
            int userID,
            int? supplierID,
            decimal totalAmount,
            decimal discount,
            decimal taxAmount,
            decimal cashAmount,
            decimal cardAmount,
            DataTable cartDataTable)
        {
            int InvoiceID = -1;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("dbo.sp_InsertPurchaseInvoice", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // تمرير البارامترات العادية
                    command.Parameters.AddWithValue("@SupplierInvoiceNumber", string.IsNullOrEmpty(supplierInvoiceNumber) ? (object)DBNull.Value : supplierInvoiceNumber);
                    command.Parameters.AddWithValue("@UserID", userID);
                    command.Parameters.AddWithValue("@SupplierID", supplierID.HasValue ? (object)supplierID.Value : DBNull.Value);
                    command.Parameters.AddWithValue("@TotalAmount", totalAmount);
                    command.Parameters.AddWithValue("@Discount", discount);
                    command.Parameters.AddWithValue("@TaxAmount", taxAmount);
                    command.Parameters.AddWithValue("@CashAmount", cashAmount);
                    command.Parameters.AddWithValue("@CardAmount", cardAmount);

                    command.Parameters.Add(clsPurchaseInvoiceDetails_DAL.InsertCart(cartDataTable));

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int id))
                            InvoiceID = id;
                    }
                    catch (Exception ex)
                    {
                        return Result<int>.Failure($"خطأ في الاتصال بقاعدة البيانات: {ex.Message}");
                    }
                }
            }
            return (InvoiceID > 0) ? Result<int>.Success(InvoiceID) : Result<int>.Failure("فشل إضافة فاتورة: لم يتم إرجاع معرف جديد من قاعدة البيانات.");
        }
        public static Result UpdateInvoiceWithSupplierID(int invoiceID, int? supplierID)
        {
            int rowAffected = -1;

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_UpdateSupplierInPurchaseInvoice", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PurchaseInvoiceID", invoiceID);
                    cmd.Parameters.AddWithValue("@SupplierID", (object)supplierID ?? DBNull.Value);
                    try
                    {
                        conn.Open();
                        object result = cmd.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int res))
                            rowAffected = res;
                    }
                    catch (Exception ex)
                    {
                        return Result.Failure($"خطأ في الاتصال بقاعدة البيانات: {ex.Message}");
                    }
                }
            }
            return rowAffected > 0 ? Result.Success() : Result.Failure($"لم يتم تحديث بيانات الفاتورة رقم ({invoiceID})، قد يكون المعرف غير موجود.");
        }
        /// <summary>
        /// جلب قائمة بجميع فواتير المشتريات المسجلة في النظام (البيانات الأساسية للرأس)
        /// </summary>
        public static Result<DataTable> GetAllPurchaseInvoices()
        {
            DataTable dt = new DataTable();

            string query = @"SELECT PI.PurchaseInvoiceID, PI.SupplierInvoiceNumber, PI.InvoiceDate, 
                            S.SupplierName, (TotalAmount - Discount + TaxAmount) AS NetAmount, PI.PaymentType, PI.TotalAmount, PI.Discount, PI.TaxAmount
                     FROM PurchaseInvoices PI
                     LEFT JOIN Suppliers S ON PI.SupplierID = S.SupplierID
                     ORDER BY PI.InvoiceDate DESC";

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
                        return Result<DataTable>.Success(dt);
                    }
                    catch (Exception ex)
                    {
                        return Result<DataTable>.Failure($"خطأ في الاتصال بقاعدة البيانات: {ex.Message}");
                    }
                }
            }
        }

        public static Result<DataTable> GetPurchaseInvoiceByID(int purchaseInvoiceID)
        {
            DataTable dt = new DataTable();
            string query = "select *,(TotalAmount - Discount + TaxAmount) AS NetAmount from purchaseInvoices where purchaseInvoiceID = @PurchaseInvoiceID";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PurchaseInvoiceID", purchaseInvoiceID);
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
