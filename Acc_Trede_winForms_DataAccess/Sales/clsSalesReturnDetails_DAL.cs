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
    }
}
