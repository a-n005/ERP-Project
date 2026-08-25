using Acc_Trade_Core;
using Acc_Trede_winForms_DataAccess.Sales;
using System.Data;

namespace Acc_Trede_winForms_Buisness.Sales
{
    public class clsSalesReturnDetails_BLL
    {
        public static Result<DataTable> GetDetails(int returnID) => clsSalesReturnDetails_DAL.GetDetailsByInvoiceID(returnID);
    }
}
