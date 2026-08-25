using Acc_Trade_Core;
using Acc_Trede_winForms_DataAccess.Purchases;
using System.Data;

namespace Acc_Trede_winForms_Buisness.Purchases
{
    public class clsPurchaseInvoiceDetails_BLL
    {
        public static Result<DataTable> GetDetails(int invoiceID) => clsPurchaseInvoiceDetails_DAL.GetDetailsByInvoiceID(invoiceID);
    }
}
