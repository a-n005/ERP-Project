using Acc_Trade_Core;
using Acc_Trede_winForms_DataAccess.Global;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Acc_Trede_winForms_DataAccess.Purchases
{
    public class clsPurchaseReturnDetails_DAL
    {
        public static SqlParameter InsertCart(DataTable dt)
        {
            SqlParameter tvpParameter = new SqlParameter("@Cart", SqlDbType.Structured);
            tvpParameter.TypeName = "dbo.PurchaseReturnCartType";
            tvpParameter.Value = dt;
            return tvpParameter;
        }
        public static Result<DataTable> GetDetails(int returnID)
        {
            DataTable dt = new DataTable();

            string query = @"SELECT D.ReturnDetailID, D.DetailID, D.ReturnID, D.ProductID, 
                                    P.Barcode, P.ProductName, D.Quantity, D.UnitPrice,
                                    D.LineTotal
                             FROM PurchaseReturnDetails D
                             INNER JOIN Products P ON D.ProductID = P.ProductID
                             WHERE D.ReturnID = @returnID";

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ReturnID", returnID);

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
