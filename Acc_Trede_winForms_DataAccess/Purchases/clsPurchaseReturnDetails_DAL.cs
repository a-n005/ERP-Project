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
    }
}
