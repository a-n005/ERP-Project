using Acc_Trade_Core;
using Acc_Trede_winForms_DataAccess.Global;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        // add get records 
    }
}
