using Acc_Trade_Core;
using Acc_Trede_winForms_DataAccess.Sales;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acc_Trede_winForms_Buisness.Sales
{
    public class clsSalesReturnDetails_BLL
    {
        public static Result<DataTable> GetDetails(int returnID) => clsSalesReturnDetails_DAL.GetDetailsByInvoiceID(returnID);
    }
}
