using Acc_Trade_Core;
using Acc_Trede_winForms_DataAccess.Global;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Acc_Trede_winForms_DataAccess.Purchases
{
    public class clsPurchaseInvoiceDetails_DAL
    {
        public static SqlParameter InsertCart(DataTable dt)
        {
            SqlParameter tvpParameter = new SqlParameter();
            tvpParameter.ParameterName = "@Cart";
            tvpParameter.SqlDbType = SqlDbType.Structured;
            tvpParameter.TypeName = "dbo.PurchaseCartType";
            tvpParameter.Value = dt;
            return tvpParameter;
        }
    }
}
