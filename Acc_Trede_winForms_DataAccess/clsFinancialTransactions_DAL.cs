using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acc_Trede_winForms_DataAccess
{
    public class clsFinancialTransactions_DAL
    {
        /// <summary>
        /// إضافة سند قبض جديد في النظام واسترجاع رسائل الخطأ إن وجدت عبر متغير out
        /// </summary>
        public static bool InsertReceiptVoucher(
            string voucherNumber,
            decimal amount,
            string paymentMethod,
            int? customerID,
            int? supplierID,
            string notes,
            int userID,
            out string errorMessage)
        {
            int rowsAffected = 0;
            errorMessage = string.Empty; // تهيئة مبدئية

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("dbo.sp_InsertReceiptVoucher", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // تمرير البارامترات الأساسية
                    command.Parameters.AddWithValue("@VoucherNumber", voucherNumber);
                    command.Parameters.AddWithValue("@Amount", amount);
                    command.Parameters.AddWithValue("@PaymentMethod", paymentMethod);
                    command.Parameters.AddWithValue("@Notes", string.IsNullOrEmpty(notes) ? (object)DBNull.Value : notes);
                    command.Parameters.AddWithValue("@UserID", userID);

                    // معالجة القيم الاختيارية لـ CustomerID و SupplierID
                    command.Parameters.AddWithValue("@CustomerID", customerID.HasValue ? (object)customerID.Value : DBNull.Value);
                    command.Parameters.AddWithValue("@SupplierID", supplierID.HasValue ? (object)supplierID.Value : DBNull.Value);

                    try
                    {
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        // اقتناص رسائل الـ RAISERROR المكتوبة بالعربية داخل الداتابيز
                        errorMessage = ex.Message;
                        return false;
                    }
                    catch (Exception ex)
                    {
                        // اقتناص أي خطأ عام آخر (مثل انقطاع الاتصال)
                        errorMessage = "خطأ عام في النظام: " + ex.Message;
                        return false;
                    }
                }
            }

            return (rowsAffected > 0);
        }
        /// <summary>
        /// إضافة سند صرف جديد في النظام واسترجاع رسائل الخطأ إن وجدت عبر متغير out
        /// </summary>
        public static bool InsertPaymentVoucher(
            string voucherNumber,
            decimal amount,
            string paymentMethod,
            int? supplierID,
            int? customerID,
            string notes,
            int userID,
            out string errorMessage)
        {
            int rowsAffected = 0;
            errorMessage = string.Empty; // تهيئة مبدئية

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("dbo.sp_InsertPaymentVoucher", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // تمرير البارامترات الأساسية
                    command.Parameters.AddWithValue("@VoucherNumber", voucherNumber);
                    command.Parameters.AddWithValue("@Amount", amount);
                    command.Parameters.AddWithValue("@PaymentMethod", paymentMethod);

                    // استخدام المعامل الثلاثي الذكي للملاحظات لتجنب الـ null في SQL
                    command.Parameters.AddWithValue("@Notes", string.IsNullOrEmpty(notes) ? (object)DBNull.Value : notes);
                    command.Parameters.AddWithValue("@UserID", userID);

                    // معالجة القيم الاختيارية لـ SupplierID و CustomerID (إذا كانت فارغة نمرر DBNull)
                    command.Parameters.AddWithValue("@SupplierID", supplierID.HasValue ? (object)supplierID.Value : DBNull.Value);
                    command.Parameters.AddWithValue("@CustomerID", customerID.HasValue ? (object)customerID.Value : DBNull.Value);

                    try
                    {
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        // اقتناص رسائل التحقق العربية مثل: (خطأ: يجب أن يكون مبلغ السند أكبر من صفر!)
                        errorMessage = ex.Message;
                        return false;
                    }
                    catch (Exception ex)
                    {
                        errorMessage = "خطأ عام في النظام: " + ex.Message;
                        return false;
                    }
                }
            }

            return (rowsAffected > 0);
        }

        /// <summary>
        /// جلب جميع الحركات والسندات المالية المسجلة في النظام
        /// </summary>
        public static DataTable GetAllTransactions(out string errorMessage)
        {
            DataTable dt = new DataTable();
            errorMessage = string.Empty;

            // استعلام مباشر لجلب البيانات (ويمكنك مستقبلاً تحويله لـ Stored Procedure أو View)
            string query = @"SELECT TransactionID, VoucherNumber, TransactionType, Amount, 
                            PaymentMethod, TransactionDate, CustomerID, SupplierID, Notes 
                     FROM FinancialTransactions 
                     ORDER BY TransactionDate DESC";

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
                    }
                    catch (Exception ex)
                    {
                        errorMessage = "خطأ أثناء جلب السندات: " + ex.Message;
                    }
                }
            }

            return dt;
        }

        /// <summary>
        /// البحث عن سند مالي محدد باستخدام رقم السند
        /// </summary>
        public static DataTable GetTransactionByVoucherNumber(string voucherNumber, out string errorMessage)
        {
            DataTable dt = new DataTable();
            errorMessage = string.Empty;

            string query = @"SELECT TransactionID, VoucherNumber, TransactionType, Amount, 
                            PaymentMethod, TransactionDate, CustomerID, SupplierID, Notes, UserID 
                     FROM FinancialTransactions 
                     WHERE VoucherNumber = @VoucherNumber";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@VoucherNumber", voucherNumber);

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
                    }
                    catch (Exception ex)
                    {
                        errorMessage = "خطأ أثناء البحث عن السند: " + ex.Message;
                    }
                }
            }

            return dt;
        }

    }
}
