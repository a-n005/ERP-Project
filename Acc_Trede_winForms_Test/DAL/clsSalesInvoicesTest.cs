//using Acc_Trede_winForms_DataAccess.Sales;
//using System.Data;

//namespace Acc_Trede_winForms_Test.DAL
//{
//    public class clsSalesInvoicesTest
//    {
//        private readonly ITestOutputHelper _outputHelper;
//        public clsSalesInvoicesTest(ITestOutputHelper outputHelper)
//        {
//            _outputHelper = outputHelper;
//        }

//        private static DataTable InitializeCartTable(int productID, int quantity, decimal unitPrice)
//        {
//            DataTable dtCart = new DataTable();

//            // يجب أن تطابق هذه الأعمدة ترتيب وأنواع الحقول في dbo.PurchaseCartType تماماً
//            dtCart.Columns.Add("ProductID", typeof(int));
//            dtCart.Columns.Add("Quantity", typeof(int));
//            dtCart.Columns.Add("CostPrice", typeof(decimal));
//            dtCart.Rows.Add(productID, quantity, unitPrice);

//            return dtCart;
//        }
//        public static IEnumerable<object[]> GetSalesInvoiceTestData()
//        {
//            // تهيئة الجداول الثلاثة التي كتبتها في كودك
//            DataTable dt1 = InitializeCartTable(4, 5, 10);   // valid product
//            DataTable dt2 = InitializeCartTable(4, 5, 10);  // invalid product name 
//            DataTable dt3 = InitializeCartTable(4, 5, 10); // new barcode
//            DataTable dt4 = InitializeCartTable(4, 10, 10); // new barcode
//            DataTable dt5 = InitializeCartTable(4, 10, 15); // new barcode
//            dt5.Rows.Add(1, 10, 15);

//            // كل سطر هنا يمثل حالة فحص كاملة (تطابق بارامترات دالة التست)
//            yield return new object[] { "5", 1, 1, 50.0m, 0.0m, 0.0m, 50.0m, 50.0m, 0.0m, dt1 };
//#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
//            yield return new object[] { "6", 1, null, 50.0m, 0.0m, 0.0m, 50.0m, 0m, 50.0m, dt2 };
//            yield return new object[] { "7", 1, 2, 50.0m, 5.0m, 1.0m, 46.0m, 30.0m, 0.0m, dt3 };
//            yield return new object[] { "8", 1, 4, 100.0m, 10.0m, 15.0m, 105.0m, 50.0m, 50.0m, dt4 };
//            yield return new object[] { "9", 1, null, 300.0m, 50.0m, 20.0m, 270.0m, 135.0m, 135m, dt5 };
//#pragma warning restore CS8625 

//        }
//        [Theory]
//        [MemberData(nameof(GetSalesInvoiceTestData))]
//        public void InsertSalesInvoice_Multiple_Success(
//         string invoiceNumber, int userID, int? customerID,
//         decimal totalAmount, decimal discount, decimal taxAmount,
//         decimal cashAmount, decimal cardAmount, DataTable salesCartDataTable)
//        {
//            var x = clsSalesInvoices_DAL.InsertSalesInvoice(invoiceNumber, userID, customerID,
//                totalAmount, discount, taxAmount, cashAmount, cardAmount, salesCartDataTable);
//            _outputHelper.WriteLine("The value: " + x.ToString());
//            _outputHelper.WriteLine(x.Error ?? "The msg is null.");
//            Assert.True(x.Value > 0, "Error: " + x.Error);
//            Assert.True(string.IsNullOrEmpty(x.Error), "Error with msg: " + x.Error);
//        }
//        public static IEnumerable<object[]> GetSalesInvoiceTestData1()
//        {
//            // تهيئة الجداول الثلاثة التي كتبتها في كودك
//            DataTable dt1 = InitializeCartTable(0, 5, 10);
//            DataTable dt2 = InitializeCartTable(1, 1000, 10);
//            DataTable dt3 = InitializeCartTable(1, 5, 10);
//            DataTable dt4 = InitializeCartTable(4, 10, 10);
//            DataTable dt5 = InitializeCartTable(4, 10, 15);
//            dt5.Rows.Add(1, 10, 15);

//            // كل سطر هنا يمثل حالة فحص كاملة (تطابق بارامترات دالة التست)
//            yield return new object[] { "s", 1, 1, 50.0m, 0.0m, 0.0m, 50.0m, 50.0m, 0.0m, dt1 };
//#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
//            yield return new object[] { "12", 1, null, 50.0m, 0.0m, 0.0m, 50.0m, 5.0m, 0.0m, dt1 };// has credit, null suppid
//            yield return new object[] { null, 1, null, 50.0m, 0.0m, 0.0m, 50.0m, 50.0m, 0.0m, dt2 };// invalid invoice num
//            yield return new object[] { "14", null, 2, 50.0m, 5.0m, 1.0m, 46.0m, 50.0m, 50.0m, dt3 };// invalid userid
//            yield return new object[] { "15", 1, 4, 100.0m, 10.0m, 15.0m, 105.0m, 1000.0m, 50.0m, dt4 };
//            yield return new object[] { "35", 1, null, 300.0m, 50.0m, 20.0m, 270.0m, 1035.0m, 135m, dt5 };
//#pragma warning restore CS8625 

//        }
//        [Theory]
//        [MemberData(nameof(GetSalesInvoiceTestData1))]
//        public void InsertSalesInvoice_Multiple_Failed(
//         string invoiceNumber, int userID, int? customerID,
//         decimal totalAmount, decimal discount, decimal taxAmount,
//         decimal cashAmount, decimal cardAmount, DataTable salesCartDataTable)
//        {
//            var x = clsSalesInvoices_DAL.InsertSalesInvoice(invoiceNumber, userID, customerID,
//                totalAmount, discount, taxAmount, cashAmount, cardAmount, salesCartDataTable);
//            _outputHelper.WriteLine("The value: " + x.ToString());
//            _outputHelper.WriteLine(x.Error ?? "The msg is null.");
//            Assert.False(x.Value > 0, "Error: " + x.Error);
//            Assert.False(string.IsNullOrEmpty(x.Error), "Error with msg: " + x.Error);
//        }

//    }
//}
