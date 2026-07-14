using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
namespace Acc_Trede_winForms_Test.Global
{
    public class clsConnectionStringTest
    {
        private readonly ITestOutputHelper _output;

        // 2. Inject it through the constructor (xUnit does this automatically)
        public clsConnectionStringTest(ITestOutputHelper output)
        {
            _output = output;
        }
        [Fact]
        public void ConnectionString_Access_Success()
        {
            string x= ConfigurationManager.ConnectionStrings["DB_AccTrede"].ConnectionString;
            _output.WriteLine("the var value: " + x);
            Assert.False(string.IsNullOrEmpty(x), "Error a var not have a value.");
            
        }
    }
}
