using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Acc_Trede_winForms_DataAccess.Global
{
    static class clsDataAccessSettings
    {
        // from giminai to add in config
        //  connectionString="Server=.;Database=Acc_Trede;Trusted_Connection=True;TrustServerCertificate=True;"

        //public static readonly string ConnectionString = "Server=.;Database=Acc_Trede;User Id=sa;Password=sa123456;";
        public static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["DB_AccTrede"].ConnectionString;
    }
}
