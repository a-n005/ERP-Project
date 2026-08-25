using Acc_Trade_Core;
using Acc_Trede_winForms_DataAccess.Global;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Acc_Trede_winForms_DataAccess.Purchases
{
    public class clsPurchaseReturn_DAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="invoiceID">The main purchase invoice ID</param>
        /// <returns>Return invoice id</returns>
        public static Result<int> InsertPurchaseReturn(
            int invoiceID,
            string returnNumber,
            int? supplier,
            decimal totalAmount,
            decimal taxAmount,
            string notes,
            int userID,
            decimal? cashAmount,
            decimal? cardAmount,
            DataTable purchaseReturnCart)
        {
            int newID = 0;
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("dbo.sp_InsertReturnPurchase", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@InvoiceID", invoiceID);
                    cmd.Parameters.AddWithValue("@ReturnNumber", returnNumber);
                    cmd.Parameters.AddWithValue("@SupplierID", supplier.HasValue ? (object)supplier.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@TotalAmount", totalAmount);
                    cmd.Parameters.AddWithValue("@TaxAmount", taxAmount);
                    cmd.Parameters.AddWithValue("@Notes", string.IsNullOrEmpty(notes) ? (object)DBNull.Value : notes);
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    cmd.Parameters.AddWithValue("@CashAmount", cashAmount.HasValue ? (object)cashAmount.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@CardAmount", cardAmount.HasValue ? (object)cardAmount.Value : DBNull.Value);
                    cmd.Parameters.Add(clsPurchaseReturnDetails_DAL.InsertCart(purchaseReturnCart));
                    try
                    {
                        conn.Open();
                        object result = cmd.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int id))
                            newID = id;
                    }
                    catch (Exception ex)
                    {
                        return Result<int>.Failure($"خطأ في الاتصال بقاعدة البيانات: {ex.Message}");
                    }
                }
            }
            return (newID > 0) ? Result<int>.Success(newID) : Result<int>.Failure("فشل إضافة فاتورة: لم يتم إرجاع معرف جديد من قاعدة البيانات.");
        }
        public static Result<DataTable> GetAllSalesInvoices()
        {
            DataTable dt = new DataTable();

            string query = @"SELECT  isNull(s.SupplierName,N'مورد غير معروف') AS SupplierName,
                             p.ReturnDate, p.TotalAmount , p.TaxAmount , p.Notes , p.UserID ,
                             p.RemainingAmount , p.CashAmount ,p.CardAmount ,
                             (TotalAmount + TaxAmount) AS NetAmount
                             FROM     PurchaseReturns as p INNER JOIN
                             Suppliers as s ON p.SupplierID = s.SupplierID
                             ORDER BY p.ReturnDate DESC";

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
        public static Result<DataTable> FindByOriginalID(int id)
        {
            DataTable dt = new DataTable();
            string query = "select * from PurchaseReturns where PurchaseInvoiceID=@ID";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
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
                        return Result<DataTable>.Failure(ex.Message);
                    }
                }
            }
        }
        public static Result<DataTable> FindByReturnID(int id)
        {
            DataTable dt = new DataTable();
            string query = "select * from SalesReturns where ReturnID=@ID";
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
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
                        return Result<DataTable>.Failure(ex.Message);
                    }
                }
            }
        }
    }
}
