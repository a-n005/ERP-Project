using Acc_Trede_winForms_DataAccess.Database;
using Acc_Trede_winForms_DataAccess.Global;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acc_Trede_winForms_Buisness
{
    public class clsSalesInvoiceDetails_BLL
    {
        public static Result<DataTable> GetDetails(int invoiceID) => clsSalesInvoiceDetails_DAL.GetDetailsByInvoiceID(invoiceID);
    }
}
