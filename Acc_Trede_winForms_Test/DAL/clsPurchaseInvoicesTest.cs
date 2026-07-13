using Acc_Trede_winForms_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acc_Trede_winForms_Test.DAL
{
    public class clsPurchaseInvoicesTest
    {
        private static DataTable InitializeCartTable(string barcode,string productName,int quantity,decimal costPrice)
        {
            DataTable dtCart = new DataTable();

            // يجب أن تطابق هذه الأعمدة ترتيب وأنواع الحقول في dbo.PurchaseCartType تماماً
            dtCart.Columns.Add("Barcode", typeof(string));
            dtCart.Columns.Add("ProductName", typeof(string));
            dtCart.Columns.Add("Quantity", typeof(int));
            dtCart.Columns.Add("CostPrice", typeof(decimal));
            dtCart.Rows.Add(barcode, productName, quantity, costPrice);

            return dtCart;
        }
        public static IEnumerable<object[]> GetPurchaseInvoiceTestData()
        {
            // تهيئة الجداول الثلاثة التي كتبتها في كودك
            DataTable dt1 = InitializeCartTable("sys-50005-18", "x", 5, 10);   // valid product
            DataTable dt2 = InitializeCartTable("sys-50005-18", "ss", 5, 10);  // invalid product name 
            DataTable dt3 = InitializeCartTable("sya-50005-18", "pin", 5, 10); // new barcode
            DataTable dt4 = InitializeCartTable("syx-50005-18", "test", 10, 10); // new barcode
            DataTable dt5 = InitializeCartTable("test-50005-18", "test1", 10, 15); // new barcode
            dt5.Rows.Add("test", "tt", 10, 15);

            // كل سطر هنا يمثل حالة فحص كاملة (تطابق بارامترات دالة التست)
            yield return new object[] { "first", 1, 1, "Cash", 50.0m, 0.0m, 0.0m, 50.0m, 50.0m, 0.0m, dt1 };
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            yield return new object[] { "test", 1, null, "Card", 50.0m, 0.0m, 0.0m, 50.0m, 50.0m, 0.0m, dt2 };
            yield return new object[] { "526", 1, 2, "Credit", 50.0m, 5.0m, 1.0m, 46.0m, 50.0m, 50.0m, dt3 };
            yield return new object[] { "test", 1, 4, "Mixed", 100.0m, 10.0m, 15.0m, 105.0m, 50.0m,50.0m, dt4 };
            yield return new object[] { "test", 1, null, "Mixed", 300.0m, 50.0m, 20.0m, 270.0m, 135.0m, 135m, dt5 };
#pragma warning restore CS8625 

        }

        [Theory]
        [MemberData(nameof(GetPurchaseInvoiceTestData))]
        public void InsertPurchaseInvoice_Multiple_Success(
            string supplierInvoiceNumber,
            int userID,
            int? supplierID,
            string paymentType,
            decimal totalAmount,
            decimal discount,
            decimal taxAmount,
            decimal netAmount,
            decimal cashAmount,
            decimal cardAmount,
            DataTable cartDataTable) // هنا نمرر سلة الأصناف كـ DataTable تطابق التايب PurchaseCartType )
        {

            int id = clsPurchaseInvoices_DAL.InsertPurchaseInvoice(supplierInvoiceNumber, userID, supplierID,
                paymentType, totalAmount, discount, taxAmount, netAmount,
                cashAmount, cardAmount, cartDataTable, out string errorMessage);

            Assert.True(id > 0, $"Error: {errorMessage}");
            Assert.True(string.IsNullOrEmpty(errorMessage), $"Error with msg: {errorMessage}");
        }
        public static IEnumerable<object[]> GetPurchaseInvoiceTestData1()
        {
            // تهيئة الجداول الثلاثة التي كتبتها في كودك
            DataTable dt1 = InitializeCartTable("sys-50005-18", "x", 5, 10);   // valid product
            DataTable dt2 = InitializeCartTable("sys-50005-18", "ss", 5, 10);  // invalid product name 
            DataTable dt3 = InitializeCartTable("sya-50005-18", "pin", 5, 10); // new barcode
            DataTable dt4 = InitializeCartTable("syx-50005-18", "test", 10, 10); // new barcode
            DataTable dt5 = InitializeCartTable("test-50005-18", "test1", 10, 15); // new barcode
            dt5.Rows.Add("test", "tt", 10, 15);

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            // كل سطر هنا يمثل حالة فحص كاملة (تطابق بارامترات دالة التست)
            yield return new object[] { "first", 1, null, "Cash", 50.0m, 0.0m, 0.0m, 50.0m, 5.0m, 0.0m, dt1 };// has credit, null suppid
            yield return new object[] { null, 1, null, "Card", 50.0m, 0.0m, 0.0m, 50.0m, 50.0m, 0.0m, dt2 };// invalid invoice num
            yield return new object[] { "526", null, 2, "Credit", 50.0m, 5.0m, 1.0m, 46.0m, 50.0m, 50.0m, dt3 };// invalid userid
            yield return new object[] { "test", 1, 4, "Mixed", 100.0m, 10.0m, 15.0m, 105.0m, 1000.0m, 50.0m, dt4 };
            yield return new object[] { "test", 1, null, "Mixed", 300.0m, 50.0m, 20.0m, 270.0m, 1035.0m, 135m, dt5 };
#pragma warning restore CS8625 

        }
        [Theory]
        [MemberData(nameof(GetPurchaseInvoiceTestData1))]
        public void InsertPurchaseInvoice_Multiple_Failed(
          string supplierInvoiceNumber,
          int userID,
          int? supplierID,
          string paymentType,
          decimal totalAmount,
          decimal discount,
          decimal taxAmount,
          decimal netAmount,
          decimal cashAmount,
          decimal cardAmount,
          DataTable cartDataTable) // هنا نمرر سلة الأصناف كـ DataTable تطابق التايب PurchaseCartType )
        {

            int id = clsPurchaseInvoices_DAL.InsertPurchaseInvoice(supplierInvoiceNumber, userID, supplierID,
                paymentType, totalAmount, discount, taxAmount, netAmount,
                cashAmount, cardAmount, cartDataTable, out string errorMessage);

            Assert.False(id > 0, $"Error: {errorMessage}");
            Assert.False(string.IsNullOrEmpty(errorMessage), $"Error with msg: {errorMessage}");
        }
        [Theory]
        [InlineData(11,4)]
        [InlineData(14,4)]
       public void UpdateInvoiceWithSupplierID_Multiple_Successfully(int invoiceID, int supplierID)
        {
            bool x = clsPurchaseInvoices_DAL.UpdateInvoiceWithSupplierID(invoiceID, supplierID, out string errMsg);

            Assert.True(x, $"Error: {errMsg}");
            Assert.True(string.IsNullOrEmpty(errMsg), $"Error with msg: {errMsg}");
        }
        [Theory]
        [InlineData(0,2)]
        [InlineData(1,10)]
        [InlineData(1,3)]
        public void UpdateInvoiceWithSupplierID_Multiple_Failed(int invoiceID, int supplierID)
        {
            bool x = clsPurchaseInvoices_DAL.UpdateInvoiceWithSupplierID(invoiceID, supplierID, out string errMsg);

            Assert.False(x, $"Error: {errMsg}");
            Assert.False(string.IsNullOrEmpty(errMsg), $"Error with msg: {errMsg}");
        }
        [Fact]
        public void GetAllPurchaseInvoices()
        {
            var x= clsPurchaseInvoices_DAL.GetAllPurchaseInvoices(out string  errMsg);
            bool r = x.AsEnumerable().Any(row => row.Field<string>("SupplierInvoiceNumber") == "525");
            Assert.True(x.Rows.Count > 0, $"Error: {errMsg}");
            Assert.True(r, "Error: not found 525");
            Assert.True(string.IsNullOrEmpty(errMsg), $"Error with msg : {errMsg}");
        }
        [Fact]
        public void GetPurchaseInvoiceByID_ReturnDtWithRecord_Success()
        {
            var x= clsPurchaseInvoices_DAL.GetPurchaseInvoiceByID(purchaseInvoiceID: 1, out string errMsg);
            bool r = x.AsEnumerable().Any(row => row.Field<string>("SupplierInvoiceNumber") == "524");
            Assert.True(x.Rows.Count > 0, $"Error: {errMsg}");
            Assert.True(r, "Error: not found 524");
            Assert.True(string.IsNullOrEmpty(errMsg), $"Error with msg : {errMsg}");
        }
        [Fact]
        public void GetPurchaseInvoiceByID_ReturnDtWithRecord_Failed()
        {
            var x = clsPurchaseInvoices_DAL.GetPurchaseInvoiceByID(purchaseInvoiceID: 0, out string errMsg);
            bool r = x.AsEnumerable().Any(row => row.Field<string>("SupplierInvoiceNumber") == "524");
            Assert.False(x.Rows.Count > 0, $"Error: {errMsg}");
            Assert.False(r, "Error: not found 524");
            Assert.True(string.IsNullOrEmpty(errMsg), $"Error with msg : {errMsg}");
        }
    }
}
