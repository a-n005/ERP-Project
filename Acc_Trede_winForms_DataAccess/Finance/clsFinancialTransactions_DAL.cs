using Acc_Trade_Core;
using Acc_Trede_winForms_DataAccess.Global;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Acc_Trede_winForms_DataAccess.Finance
{
    public class clsFinancialTransactions_DAL
    {
        /// <summary>
        /// إضافة سند قبض جديد في النظام واسترجاع رسائل الخطأ إن وجدت عبر متغير out
        /// </summary>
        public static Result<int> InsertReceiptVoucher(
            string voucherNumber,
            decimal amount,
            string paymentMethod,
            int? customerID,
            int? supplierID,
            string notes,
            int userID,
            int? saleInvoiceID,
            int? purchaseReturnInvoiceID)
        {
            int receiptID = -1;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("dbo.sp_InsertReceiptVoucher", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@VoucherNumber", voucherNumber);
                    command.Parameters.AddWithValue("@Amount", amount);
                    command.Parameters.AddWithValue("@PaymentMethod", paymentMethod);
                    command.Parameters.AddWithValue("@Notes", string.IsNullOrEmpty(notes) ? (object)DBNull.Value : notes);
                    command.Parameters.AddWithValue("@UserID", userID);
                    command.Parameters.AddWithValue("@CustomerID", customerID.HasValue ? (object)customerID.Value : DBNull.Value);
                    command.Parameters.AddWithValue("@SupplierID", supplierID.HasValue ? (object)supplierID.Value : DBNull.Value);
                    command.Parameters.AddWithValue("@SaleInvoiceID", saleInvoiceID.HasValue ? (object)saleInvoiceID.Value : DBNull.Value);
                    command.Parameters.AddWithValue("@PurchaseReturnID", purchaseReturnInvoiceID.HasValue ? (object)purchaseReturnInvoiceID.Value : DBNull.Value);

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int id))
                            receiptID = id;
                    }
                    catch (Exception ex)
                    {
                        return Result<int>.Failure($"خطأ في الاتصال بقاعدة البيانات: {ex.Message}");
                    }
                }
            }
            return (receiptID > 0) ? Result<int>.Success(receiptID) : Result<int>.Failure("فشل إضافة فاتورة: لم يتم إرجاع معرف جديد من قاعدة البيانات.");
        }
        /// <summary>
        /// إضافة سند صرف جديد في النظام واسترجاع رسائل الخطأ إن وجدت عبر متغير out
        /// </summary>
        public static Result<int> InsertPaymentVoucher(
            string voucherNumber,
            decimal amount,
            string paymentMethod,
            int? supplierID,
            int? customerID,
            string notes,
            int userID,
            int? saleReturnInvoiceID,
            int? purchaseInvoiceID)
        {
            int paymentID = -1;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("dbo.sp_InsertPaymentVoucher", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@VoucherNumber", voucherNumber);
                    command.Parameters.AddWithValue("@Amount", amount);
                    command.Parameters.AddWithValue("@PaymentMethod", paymentMethod);
                    command.Parameters.AddWithValue("@Notes", string.IsNullOrEmpty(notes) ? (object)DBNull.Value : notes);
                    command.Parameters.AddWithValue("@UserID", userID);
                    command.Parameters.AddWithValue("@SupplierID", supplierID.HasValue ? (object)supplierID.Value : DBNull.Value);
                    command.Parameters.AddWithValue("@CustomerID", customerID.HasValue ? (object)customerID.Value : DBNull.Value);
                    command.Parameters.AddWithValue("@SaleReturnInvoiceID", saleReturnInvoiceID.HasValue ? (object)saleReturnInvoiceID.Value : DBNull.Value);
                    command.Parameters.AddWithValue("PurchaseInvoiceID", purchaseInvoiceID.HasValue ? (object)purchaseInvoiceID.Value : DBNull.Value);

                    try
                    {
                        connection.Open();
                        object result = command.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int id))
                            paymentID = id;
                    }
                    catch (Exception ex)
                    {
                        return Result<int>.Failure($"خطأ في الاتصال بقاعدة البيانات: {ex.Message}");
                    }
                }
            }
            return (paymentID > 0) ? Result<int>.Success(paymentID) : Result<int>.Failure("فشل إضافة فاتورة: لم يتم إرجاع معرف جديد من قاعدة البيانات.");
        }

        /// <summary>
        /// جلب جميع الحركات والسندات المالية المسجلة في النظام - سوف يعدل ليجلب الفيو
        /// </summary>
        // not work for now the new tables are paymentVoucher and receiptVoucher
        public static Result<DataTable> GetAllTransactionsPayment()
        {
            DataTable dt = new DataTable();

            // استعلام مباشر لجلب البيانات (ويمكنك مستقبلاً تحويله لـ Stored Procedure أو View)
            string query = @"select * from PaymentVouchers ORDER BY TransactionDate DESC";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                dt.Load(reader); // شحن البيانات داخل الـ DataTable
                            }
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
        public static Result<DataTable> GetAllTransactionsReceipt()
        {
            DataTable dt = new DataTable();

            // استعلام مباشر لجلب البيانات (ويمكنك مستقبلاً تحويله لـ Stored Procedure أو View)
            string query = @"select * from ReceiptVouchers ORDER BY TransactionDate DESC";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                dt.Load(reader); // شحن البيانات داخل الـ DataTable
                            }
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

        /// <summary>
        /// البحث عن سند مالي محدد باستخدام رقم السند - سوف يعدل ليجلب الفيو
        /// </summary>
        public static Result<DataTable> GetReceiptByReceiptID(int receiptID)
        {
            DataTable dt = new DataTable();


            string query = @"SELECT * FROM receiptVouchers  WHERE receiptID = @receiptID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@receiptID", receiptID);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                dt.Load(reader);
                            }
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
        public static Result<DataTable> GetPaymentByPaymentID(int paymentID)
        {
            DataTable dt = new DataTable();

            string query = @"SELECT * FROM paymentVouchers  WHERE paymentID = @paymentID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@paymentID", paymentID);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                dt.Load(reader);
                            }
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
