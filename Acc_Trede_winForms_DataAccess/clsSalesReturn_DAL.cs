using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acc_Trede_winForms_DataAccess
{
    public class clsSalesReturn_DAL
    {
        /// <summary>
        /// Return from sales
        /// </summary>
        /// <param name="invoiceID"> From the main invoice </param>
        /// <returns>Invoice ID from Return</returns>
        public static int InsertSalesReturn(
            int invoiceID,
            int? customerID,
            decimal totalAmount,
            decimal taxAmount,
            decimal netAmount,
            string notes,
            int userID,
            decimal? cashAmount,
            decimal? cardAmount,
            DataTable salesCart,
            out string errMsg)
        {
            int newID = 0;
            errMsg = null;
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("dbo.sp_InsertReturnSale", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@InvoiceID", invoiceID);
                    cmd.Parameters.AddWithValue("@CustomerID", customerID.HasValue ? (object)customerID.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@TaxAmount", taxAmount);
                    cmd.Parameters.AddWithValue("@NetAmount", netAmount);
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
                        { newID = id; }
                    }
                    catch (SqlException ex)
                    {
                        errMsg = ex.Message;
                        return -1;
                    }
                    catch (Exception ex)
                    {
                        errMsg = ex.Message;
                        return -1;
                    }
                }
            }
            return newID;
        }

        // add get records 
    }
}
