using Acc_Trade_Core;
using Acc_Trede_winForms_DataAccess.Purchases;
using System.Data;

namespace Acc_Trede_winForms_Buisness.Purchases
{
    public class clsPurchaseReturnDetails_BLL
    {
        public Result<DataTable> GetDetails(int returnID) => clsPurchaseReturnDetails_DAL.GetDetails(returnID);
    }
}
