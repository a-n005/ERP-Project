//using Acc_Trede_winForms_DataAccess.Sales;
//using Acc_Trade_Core;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Acc_Trede_winForms_Test.DAL
//{
//    public class clsSalesReturnTest
//    {
//        private static DataTable InitializeCartTable(int productID, int quantity, decimal costPrice,int detailID)
//        {
//            DataTable dtCart = new DataTable();

//            // يجب أن تطابق هذه الأعمدة ترتيب وأنواع الحقول في dbo.PurchaseCartType تماماً
//            dtCart.Columns.Add("ProductID", typeof(int));
//            dtCart.Columns.Add("Quantity", typeof(int));
//            dtCart.Columns.Add("UnitPrice", typeof(decimal));
//            dtCart.Columns.Add("DetailID", typeof(int));
//            dtCart.Rows.Add(productID, quantity, costPrice,detailID);

//            return dtCart;
//        }
//        public static IEnumerable<object[]> GetSalesReturnInvoiceTestData()
//        {
//            // تهيئة الجداول الثلاثة التي كتبتها في كودك
//            DataTable dt1 = InitializeCartTable(4, 5, 10m, 3);   // valid product
//            //DataTable dt2 = InitializeCartTable(1, 5, 15);  // invalid product name 
//            //DataTable dt3 = InitializeCartTable(1, 5, 10); // new barcode
//            //DataTable dt4 = InitializeCartTable(1,  10, 10); // new barcode
//            DataTable dt5 = InitializeCartTable(4, 5, 15m, 5); // new barcode
//            dt5.Rows.Add(1, 5, 15, 6);

//            // كل سطر هنا يمثل حالة فحص كاملة (تطابق بارامترات دالة التست)
//            yield return new object[] { 3,"RIS-5934-4", 1, 50.0m, 7.5m, 57.5m, "with tax", 1, 37.5m, 20m, dt1 };
//#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
//            //yield return new object[] { "test", 1, null, 50.0m, 0.0m, 0.0m, 50.0m, 50.0m, 0.0m, dt2 };
//            //yield return new object[] { "526", 1, 2, 50.0m, 5.0m, 1.0m, 46.0m, 50.0m, 50.0m, dt3 };
//            //yield return new object[] { "test", 1, 4, 100.0m, 10.0m, 15.0m, 105.0m, 50.0m, 50.0m, dt4 };
//            yield return new object[] { 5, "RIS-5934-5", null, 150m, 22.5m, 172.5m, null, 1, 50.0m, 100m, dt5 };
//#pragma warning restore CS8625 

//        }

//        [Theory]
//        [MemberData(nameof(GetSalesReturnInvoiceTestData))]
//        public static void InsertSalesReturn(
//          int invoiceID,
//          string returnNumber,
//          int? customerID,
//          decimal totalAmount,
//          decimal taxAmount,
//          decimal netAmount,
//          string notes,
//          int userID,
//          decimal? cashAmount,
//          decimal? cardAmount,
//          DataTable salesCart)
//        {
//            var x = clsSalesReturn_DAL.InsertSalesReturn(invoiceID,returnNumber, customerID, totalAmount, taxAmount, notes, userID, cashAmount, cardAmount, salesCart);
//            Assert.True(x.Value > 0, $"Error: {x.Error}");
//            Assert.True(string.IsNullOrEmpty(x.Error), $"Error with msg: {x.Error}");
//        }
//    }
//}
