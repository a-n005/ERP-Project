using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acc_Trede_winForms_DataAccess
{
    public class clsPurchaseReturnDetails_DAL
    {
        public static SqlParameter InsertCart(DataTable dt)
        {
            SqlParameter tvpParameter=new SqlParameter("@Cart",SqlDbType.Structured);
            tvpParameter.TypeName = "dbo.PutchaseCartType";
            tvpParameter.Value = dt;
            return tvpParameter;
        }
    }
}
