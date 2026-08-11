using Acc_Trade_Core;
using Acc_Trede_winForms_DataAccess.Global;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acc_Trede_winForms_DataAccess.Purchases
{
    public class clsPurchaseReturn_DAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="invoiceID">The main purchase invoice ID</param>
        /// <returns>Return invoice id</returns>
        public static Result<int >InsertPurchaseReturn(
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
            using(SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using(SqlCommand cmd= new SqlCommand("dbo.sp_InsertReturnPurchase",conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@InvoiceID", invoiceID);
                    cmd.Parameters.AddWithValue("@ReturnNumber", returnNumber);
                    cmd.Parameters.AddWithValue("@SupplierID", supplier.HasValue?(object)supplier.Value:DBNull.Value);
                    cmd.Parameters.AddWithValue("@TotalAmount", totalAmount);
                    cmd.Parameters.AddWithValue("@TaxAmount", taxAmount);
                    cmd.Parameters.AddWithValue("@Notes", string.IsNullOrEmpty(notes)?(object)DBNull.Value:notes);
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    cmd.Parameters.AddWithValue("@CashAmount", cashAmount.HasValue?(object)cashAmount.Value:DBNull.Value);
                    cmd.Parameters.AddWithValue("@CardAmount", cardAmount.HasValue?(object)cardAmount.Value:DBNull.Value);
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
    }
}
