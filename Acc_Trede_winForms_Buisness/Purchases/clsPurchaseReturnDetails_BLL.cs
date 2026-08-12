using Acc_Trade_Core;
using Acc_Trede_winForms_DataAccess.Purchases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acc_Trede_winForms_Buisness.Purchases
{
    public class clsPurchaseReturnDetails_BLL
    {
        public Result<DataTable> GetDetails(int returnID) => clsPurchaseReturnDetails_DAL.GetDetails(returnID);
    }
}
