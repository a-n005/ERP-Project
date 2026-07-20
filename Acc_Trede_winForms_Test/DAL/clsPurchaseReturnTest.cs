using Acc_Trede_winForms_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acc_Trede_winForms_Test.DAL
{
    public class clsPurchaseReturnTest
    {
        private static DataTable InitializeCartTable(string barcode,string productName, int quantity, decimal costPrice,int detailID)
        {
            DataTable dtCart = new DataTable();

            // يجب أن تطابق هذه الأعمدة ترتيب وأنواع الحقول في dbo.PurchaseCartType تماماً
            dtCart.Columns.Add("Barcode", typeof(string));
            dtCart.Columns.Add("ProductName", typeof(string));
            dtCart.Columns.Add("Quantity", typeof(int));
            dtCart.Columns.Add("UnitPrice", typeof(decimal));
            dtCart.Columns.Add("DetailID", typeof(int));
            dtCart.Rows.Add(barcode,productName, quantity, costPrice,detailID);

            return dtCart;
        }
        public static IEnumerable<object[]> GetPurchaseReturnInvoiceTestData()
        {
            // تهيئة الجداول الثلاثة التي كتبتها في كودك
            DataTable dt1 = InitializeCartTable("sys-50005-18", "", 2, 10,2);   // valid product
            //DataTable dt2 = InitializeCartTable(1, 5, 15);  // invalid product name 
            //DataTable dt3 = InitializeCartTable(1, 5, 10); // new barcode
            //DataTable dt4 = InitializeCartTable(1,  10, 10); // new barcode
            DataTable dt5 = InitializeCartTable("syx-50005-18", "",2, 10,4); // new barcode
            //dt5.Rows.Add("test", null, 5, 15,6);

            // كل سطر هنا يمثل حالة فحص كاملة (تطابق بارامترات دالة التست)
            yield return new object[] { 2, "RIP-5934-3", 1, 20.0m, 3m, 23m, "with tax", 1, 3m, 20m, dt1 };
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            //yield return new object[] { "test", 1, null, 50.0m, 0.0m, 0.0m, 50.0m, 50.0m, 0.0m, dt2 };
            //yield return new object[] { "526", 1, 2, 50.0m, 5.0m, 1.0m, 46.0m, 50.0m, 50.0m, dt3 };
            //yield return new object[] { "test", 1, 4, 100.0m, 10.0m, 15.0m, 105.0m, 50.0m, 50.0m, dt4 };
            yield return new object[] { 4, "RIP-5934-5", null, 150m, 22.5m, 172.5m, null, 1, 50.0m, 100m, dt5 };
#pragma warning restore CS8625 

        }

        [Theory]
        [MemberData(nameof(GetPurchaseReturnInvoiceTestData))]
        public static void InsertPurchaseReturn(
          int invoiceID,
          string returnNumber,
          int? supplier,
          decimal totalAmount,
          decimal taxAmount,
          decimal netAmount,
          string notes,
          int userID,
          decimal? cashAmount,
          decimal? cardAmount,
          DataTable purchaseReturnCart)
        {
            var x = clsPurchaseReturn_DAL.InsertPurchaseReturn(invoiceID, returnNumber, supplier, totalAmount, taxAmount, netAmount, notes, userID, cashAmount, cardAmount, purchaseReturnCart, out string errMsg);
            Assert.True(x > 0, $"Error: {errMsg}");
            Assert.True(string.IsNullOrEmpty(errMsg), $"Error with msg: {errMsg}");
        }
    }
}
