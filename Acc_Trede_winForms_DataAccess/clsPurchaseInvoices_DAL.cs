using Acc_Trede_winForms_DataAccess.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acc_Trede_winForms_DataAccess
{
    public class clsPurchaseInvoices_DAL
    {
        /// <summary>
        /// حفظ فاتورة مشتريات جديدة مع تفاصيلها وتحديث المخازن وأرصدة الموردين دفعة واحدة
        /// </summary>
        public static int InsertPurchaseInvoice(
            string supplierInvoiceNumber,
            int userID,
            int? supplierID,
            string paymentType,
            decimal totalAmount,
            decimal discount,
            decimal taxAmount,
            decimal netAmount,
            decimal cashAmount,
            decimal cardAmount,
            DataTable cartDataTable, // هنا نمرر سلة الأصناف كـ DataTable تطابق التايب PurchaseCartType
            out string errorMessage)
        {
            int InvoiceID = -1;
            errorMessage = string.Empty;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("dbo.sp_InsertPurchaseInvoice", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // تمرير البارامترات العادية
                    command.Parameters.AddWithValue("@SupplierInvoiceNumber", string.IsNullOrEmpty(supplierInvoiceNumber) ? (object)DBNull.Value : supplierInvoiceNumber);
                    command.Parameters.AddWithValue("@UserID", userID);
                    command.Parameters.AddWithValue("@SupplierID", supplierID.HasValue ? (object)supplierID.Value : DBNull.Value);
                    command.Parameters.AddWithValue("@PaymentType", paymentType);
                    command.Parameters.AddWithValue("@TotalAmount", totalAmount);
                    command.Parameters.AddWithValue("@Discount", discount);
                    command.Parameters.AddWithValue("@TaxAmount", taxAmount);
                    command.Parameters.AddWithValue("@NetAmount", netAmount);
                    command.Parameters.AddWithValue("@CashAmount", cashAmount);
                    command.Parameters.AddWithValue("@CardAmount", cardAmount);

                    command.Parameters.Add(clsPurchaseInvoiceDetails_DAL.InsertCart(cartDataTable));

                    try
                    {
                        connection.Open();
                        // الـ ExecuteNonQuery هنا ستعود بعدد الأسطر المتأثرة في الفواتير، التفاصيل، والمخازن
                        object result = command.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int id))
                            InvoiceID = id;
                    }
                    catch (SqlException ex)
                    {
                        // اقتناص رسائل الـ RAISERROR المخصصة مثل: (خطأ: لا يمكن إدخال مبالغ سالبة!)
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

            // بما أن العملية تحتوي على Insert و Update لجداول متعددة، فإذا نجحت سيكون الـ rowsAffected أكبر من 0 حتماً
            return InvoiceID;
        }
        public static bool UpdateInvoiceWithSupplierID(int invoiceID, int supplierID, out string errMsg)
        {

            errMsg = string.Empty;
            int rowAffected = -1;

            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_UpdateSupplierInPurchaseInvoice", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PurchaseInvoiceID", invoiceID);
                    cmd.Parameters.AddWithValue("@SupplierID", supplierID);
                    try
                    {
                        conn.Open();
                        object result = cmd.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int res))
                            rowAffected = res;
                    }
                    catch (Exception ex)
                    {
                        errMsg = ex.Message;
                        return false;
                    }
                }
            }
            return rowAffected > 0;
        }
        /// <summary>
        /// جلب قائمة بجميع فواتير المشتريات المسجلة في النظام (البيانات الأساسية للرأس)
        /// </summary>
        public static DataTable GetAllPurchaseInvoices(out string errorMessage)
        {
            DataTable dt = new DataTable();
            errorMessage = string.Empty;

            // استعلام يجلب الفواتير مع اسم المورد بدلاً من معرّفه الرقمي لراحة المستخدم
            string query = @"SELECT PI.PurchaseInvoiceID, PI.SupplierInvoiceNumber, PI.InvoiceDate, 
                            S.SupplierName, PI.NetAmount, PI.PaymentType, PI.TotalAmount, PI.Discount, PI.TaxAmount
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
                    }
                    catch (Exception ex)
                    {
                        errorMessage = "خطأ أثناء جلب فواتير المشتريات: " + ex.Message;
                    }
                }
            }
            return dt;
        }

        public static DataTable GetPurchaseInvoiceByID(int purchaseInvoiceID, out string errorMessage)
        {
            DataTable dt = new DataTable();
            errorMessage = string.Empty;
            string query = "select * from purchaseInvoices where purchaseInvoiceID = @PurchaseInvoiceID";
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
                    }
                    catch (Exception ex) { errorMessage = "خطأ أثناء جلب فواتير المشتريات: " + ex.Message; }
                }
            }
            return dt;
        }
    }
}
