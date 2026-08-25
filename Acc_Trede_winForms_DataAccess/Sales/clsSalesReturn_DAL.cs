using Acc_Trade_Core;
using Acc_Trede_winForms_DataAccess.Global;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Acc_Trede_winForms_DataAccess.Sales
{
    public class clsSalesReturn_DAL
    {
        /// <summary>
        /// Return from sales
        /// </summary>
        /// <param name="invoiceID"> From the main invoice </param>
        /// <returns>Invoice ID from Return</returns>
        public static Result<int> InsertSalesReturn(
            int invoiceID,
            string ReturnNumber,
            int? customerID,
            decimal totalAmount,
            decimal taxAmount,
            string notes,
            int userID,
            decimal? cashAmount,
            decimal? cardAmount,
            DataTable salesCart)
        {
            int newID = 0;
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("dbo.sp_InsertReturnSale", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@InvoiceID", invoiceID);
                    cmd.Parameters.AddWithValue("@ReturnNumber", ReturnNumber);
                    cmd.Parameters.AddWithValue("@CustomerID", customerID.HasValue ? (object)customerID.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@TotalAmount", totalAmount);
                    cmd.Parameters.AddWithValue("@TaxAmount", taxAmount);
                    cmd.Parameters.AddWithValue("@Notes", string.IsNullOrEmpty(notes) ? (object)DBNull.Value : notes);
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    cmd.Parameters.AddWithValue("@CashAmount", cashAmount.HasValue ? (object)cashAmount.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@CardAmount", cardAmount.HasValue ? (object)cardAmount : DBNull.Value);
                    cmd.Parameters.Add(clsSalesReturnDetails_DAL.InsertCart(salesCart));
                    try
                    {
                        conn.Open();
                        object res = cmd.ExecuteScalar();
                        if (res != null && int.TryParse(res.ToString(), out int id))
                        {
                            newID = id;
                        }
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

            string query = @"SELECT r.ReturnID, r.ReturnNumber, r.InvoiceID, r.CustomerID,
                             r.ReturnDate, r.TotalAmount, r.TaxAmount, r.Notes, r.UserID, r.RemainingAmount,
                             r.CashAmount, r.CardAmount, isNull(c.CustomerName,N'عميل نقدي') as CustomerName,
                             (TotalAmount + TaxAmount) AS NetAmount
                             FROM     Customers AS c INNER JOIN
                             SalesReturns AS r ON c.CustomerID = r.CustomerID
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
            string query = "select * from SalesReturns where InvoiceID=@ID";
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
