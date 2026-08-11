using Acc_Trede_winForms_DataAccess.Purchases;
using Acc_Trade_Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acc_Trede_winForms_Buisness.Purchases
{
    public class clsPurchaseInvoiceDetails_BLL
    {
        public static Result<DataTable> GetDetails(int invoiceID)=>clsPurchaseInvoiceDetails_DAL.GetDetailsByInvoiceID(invoiceID);
    }
}
