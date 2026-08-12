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
    public class clsSalesReturnDetails_DAL
    {
        public static SqlParameter InsertCart(DataTable dt)
        {
            SqlParameter tvpParameter = new SqlParameter();
            tvpParameter.ParameterName = "@Cart";
            tvpParameter.SqlDbType = SqlDbType.Structured;
            tvpParameter.TypeName = "dbo.SalesReturnCartType";
            tvpParameter.Value = dt;
            return tvpParameter;
        }
        public static Result<DataTable> GetDetailsByInvoiceID(int returnID)
        {
            DataTable dt = new DataTable();

            string query = @"SELECT D.DetailID, D.InvoiceID, D.ProductID, 
                                    P.Barcode, P.ProductName, D.Quantity, D.UnitPrice,
                                    D.CostPriceAtSale,
                                    (D.Quantity * D.UnitPrice) AS TotalLinePrice
                             FROM SalesReturnDetails D
                             INNER JOIN Products P ON D.ProductID = P.ProductID
                             WHERE D.ReturnID = @ReturnID";

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
