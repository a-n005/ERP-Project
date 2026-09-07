//using Acc_Trede_winForms_DataAccess.Sales;
//using Acc_Trade_Core;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Acc_Trede_winForms_Test.DAL
//{
//    public class clsSalesInvoiceDetailsTest
//    {
//        public readonly ITestOutputHelper _output;
//        public clsSalesInvoiceDetailsTest(ITestOutputHelper output)
//        {
//            _output = output;
//        }
//        [Theory]
//        [InlineData(1)]
//        [InlineData(2)]
//        public void GetDetailsByInvoiceID_Multiple_Success(int invoiceID)
//        {
//            var x = clsSalesInvoiceDetails_DAL.GetDetailsByInvoiceID(invoiceID);
//            _output.WriteLine(x.Value.Rows[0][3].ToString() ?? "It's null");
//            Assert.True(x != null && x.Value.Rows.Count > 0, "Error: The var has null");
//            Assert.True(string.IsNullOrEmpty(x.Error), "Error: " + x.Error);
//        }
//        [Theory]
//        [InlineData(0)]
//        [InlineData(1000)]
//        public void GetDetailsByInvoiceID_Multiple_Failed(int invoiceID)
//        {
//            var x = clsSalesInvoiceDetails_DAL.GetDetailsByInvoiceID(invoiceID);
//            //_output.WriteLine(x.Rows[0][0]?.ToString() ?? "It's null");
//            Assert.False(x != null && x.Value.Rows.Count > 0, "Error: The var has null");
//            Assert.True(string.IsNullOrEmpty(x.Error), "Error: " + x.Error);
//        }
//    }
//}
