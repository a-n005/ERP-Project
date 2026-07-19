using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acc_Trede_winForms_DataAccess
{
    public class clsPurchaseReturn_DAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="invoiceID">The main purchase invoice ID</param>
        /// <returns>Return invoice id</returns>
        public static int InsertPurchaseReturn(
            int invoiceID,
            int? supplier,
            decimal totalAmount,
            decimal taxAmount,
            decimal netAmount,
            string notes,
            int userID,
            decimal? cashAmount,
            decimal? cardAmount,
            DataTable purchaseReturnCart,
            out string errMsg)
        {
            int newID = 0;
            errMsg = null;
            using(SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using(SqlCommand cmd= new SqlCommand("dbo.sp_InsertReturnPurchase",conn))
                {
                    cmd.Parameters.AddWithValue("@InvoiceID", invoiceID);
                    cmd.Parameters.AddWithValue("@SupplierID", supplier.HasValue?(object)supplier.Value:DBNull.Value);
                    cmd.Parameters.AddWithValue("@TotalAmount", totalAmount);
                    cmd.Parameters.AddWithValue("@TaxAmount", taxAmount);
                    cmd.Parameters.AddWithValue("@NetAmount", netAmount);
                    cmd.Parameters.AddWithValue("@Notes", string.IsNullOrEmpty(notes)?(object)DBNull.Value:notes);
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    cmd.Parameters.AddWithValue("@CashAmount", cashAmount.HasValue?(object)cashAmount.Value:DBNull.Value);
                    cmd.Parameters.AddWithValue("@CardAmount", cardAmount.HasValue?(object)cardAmount.Value:DBNull.Value);
                    cmd.Parameters.Add(clsPurchaseReturnDetails_DAL.InsertCart(purchaseReturnCart));
                    try
                    {
                        conn.Open();
                        object res= cmd.ExecuteScalar();
                        if (res != null&& int.TryParse(res.ToString(), out int id))
                        { newID = id; }
                    }
                    catch(SqlException ex)
                    {
                        errMsg = ex.Message;
                        return -1;
                    }
                    catch (Exception ex)
                    {
                        errMsg= ex.Message;
                        return -1;
                    }
                }
            }
            return newID;
        }
    }
}
