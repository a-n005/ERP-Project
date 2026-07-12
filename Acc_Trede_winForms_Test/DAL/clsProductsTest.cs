using Acc_Trede_winForms_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acc_Trede_winForms_Test
{
    public class clsProductsTest
    {
        [Theory]
        [InlineData("sys-50005-5", "phone", 15.6, 25.99, 100, 3, 2)]
        [InlineData("sys-50005-6", "pc", 27.5, 50, 50, 5, 2)]
        [InlineData("sys-50005-7", "iphone", 10.99, 25.99, 30, 10, 2)]
        public void AddNewProducts_Multiple_ShouldRegisterSuccessfully(
            string barcode, string productname, decimal costPrice, decimal salePrice, int stockQuantity, int minStockAlert, int createdby)
        {
            // Arrange: (المعطيات تأتي تلقائياً من الـ InlineData كبارامترات للدالة)
            //string errMsg;

            // Act: استدعاء الدالة بنفس الترتيب
            bool isInsert = clsProducts_DAL.InsertProduct(barcode, productname, costPrice, salePrice, stockQuantity, minStockAlert, createdby, out string errMsg);

            // Assert: التحقق من النتائج
            // 1. نتأكد أن السيرفر نجح في الإدخال وأعاد معرّفاً تلقائياً أكبر من 0
            Assert.True(isInsert, $"فشل إدخال المستخدم {productname}. رسالة الخطأ المرتجعة: {errMsg}");

            // 2. نتأكد أن رسالة الخطأ فارغة تماماً
            Assert.True(string.IsNullOrEmpty(errMsg), $"حدث خطأ غير متوقع: {errMsg}");
        }
        [Theory]
        [InlineData("sys-50005-7", "name", 1.99, 2.99, 15, 6, 5)]
        [InlineData("sys-50005-55", "name", 1.99, 2.99, 15, 6, 0)]
        public void AddNewProducts_MultipleCases_ShouldNotRegister(
             string barcode, string productname, decimal costPrice, decimal salePrice, int stockQuantity, int minStockAlert, int createdby)
        {
            // Arrange: (المعطيات تأتي تلقائياً من الـ InlineData كبارامترات للدالة)
            //string errMsg;

            // Act: استدعاء الدالة بنفس الترتيب
            bool isInsert = clsProducts_DAL.InsertProduct(barcode, productname, costPrice, salePrice, stockQuantity, minStockAlert, createdby, out string errMsg);

            // Assert: التحقق من النتائج
            // 1. نتأكد أن السيرفر نجح في الإدخال وأعاد معرّفاً تلقائياً أكبر من 0
            Assert.False(isInsert, $"فشل إدخال المستخدم {""}. رسالة الخطأ المرتجعة: {errMsg}");

            // 2. نتأكد أن رسالة الخطأ فارغة تماماً
            Assert.False(string.IsNullOrEmpty(errMsg), $"حدث خطأ غير متوقع: {errMsg}");
        }
        // Update cases
        [Theory]
        [InlineData(1, "sys-50005-18", "x", 17, 30.99, 55, 4, 4)]
        [InlineData(2, "sys-50005-19", "x1", 18, 31.99, 56, 5, 5)]
        [InlineData(3, "sys-50005-20", "x2", 19, 32.99, 57, 6, 6)]
        public void UpdateProducts_MultipleUpdated_ShouldUpdatedSuccessfully(
          int id, string barcode, string productname, decimal costPrice, decimal salePrice, int stockQuantity, int minStockAlert, int updatedBy)
        {
            var x = clsProducts_DAL.UpdateProduct(id, barcode, productname, costPrice, salePrice, stockQuantity, minStockAlert, updatedBy, out string errMsg);

            Assert.True(x, $"Udate failed: {errMsg}");
            Assert.True(string.IsNullOrEmpty(errMsg), $"Update failed: {errMsg}");
        }
        [Theory]
        [InlineData(3, "sys-50005-20", "x2", 19, 32.99, 57, 6, 0)]// this user not exists
        [InlineData(4, "sys-50005-20", "x2", 19, 32.99, 57, 6, 1)]// this barcode exists
        public void UpdateProducts_MultipleCases_ShouldUpdatedFailed(
          int id, string barcode, string productname, decimal costPrice, decimal salePrice, int stockQuantity, int minStockAlert, int updatedBy)
        {
            var x = clsProducts_DAL.UpdateProduct(id, barcode, productname, costPrice, salePrice, stockQuantity, minStockAlert, updatedBy, out string errMsg);

            Assert.False(x, $"Udate failed: {errMsg}");
            Assert.False(string.IsNullOrEmpty(errMsg), $"Update failed: {errMsg}");
        }
        // Get products
        [Fact]
        public void GetLowStockProducts_ReturnDtWithRecurd_ShouldReturnSuccessfully()
        {
            var x = clsProducts_DAL.GetLowStockProducts(out string err);

            Assert.True(x.Rows.Count > 0, $"Error: {err}");
            Assert.True(string.IsNullOrEmpty(err), $"Error return errMsg→ {err}");
        }
        [Fact]
        public void GetAllProducts_ReturnDtWithRecurd_ShouldReturnSuccess()
        {
            var x = clsProducts_DAL.GetAllProducts(out string err);

            Assert.True(x.Rows.Count > 0, $"Error: {err}");
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            Assert.Equal("sys-50005-7", x.Rows[0].Field<string>("Barcode").Trim());
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            Assert.True(string.IsNullOrEmpty(err), $"Error return errMsg→ {err}");
        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void GetProductByID_ReturnDtWithRecurd_ShouldReturnSuccessfully(int productID)
        {
            var x = clsProducts_DAL.GetProductByID(productID, out string err);

            Assert.True(x.Rows.Count > 0, $"Error: {err}");
            Assert.True(string.IsNullOrEmpty(err), $"Error return errMsg→ {err}");
        }
        [Fact]
        public void GetProductByID_ReturnDtWithRecurd_ShouldReturnFailed()
        {
            var x = clsProducts_DAL.GetProductByID(0, out string err);

            Assert.False(x.Rows.Count > 0, $"Error: {err}");
            Assert.True(string.IsNullOrEmpty(err), $"Error return errMsg→ {err}");
        }
        [Theory]
        [InlineData("sys-50005-18")]
        [InlineData("sys-50005-19")]
        public void GetProductByBarcode_ReturnDtWithRecurds_ShouldReturnSuccess(string barcode)
        {
            var x = clsProducts_DAL.GetProductByBarcode(barcode, out string err);
            string s = barcode == "sys-50005-19" ? "sys-50005-19" : "sys-50005-18";
            Assert.True(x.Rows.Count > 0, $"Error: {err}");
            Assert.Equal(s, x.Rows[0].Field<string>("Barcode"));
            Assert.True(string.IsNullOrEmpty(err), $"Error return errMsg→ {err}");
        }
        [Fact]
        public void GetProductByBarcode_Null_Failed()
        {
            var x = clsProducts_DAL.GetProductByBarcode("", out string err);

            Assert.False(x.Rows.Count < 0, $"Error: {err}");
            Assert.True(string.IsNullOrEmpty(err), $"Error return errMsg→ {err}");
        }
        // Delete Product
        [Theory]
        [InlineData(1, 6)]
        [InlineData(2, 6)]
        [InlineData(3, 6)]
        public void DeleteProductSoft_MultipleDelete_ShouldSuccessfully(int id, int updatedby)
        {
            var x = clsProducts_DAL.DeleteProductSoft(id, updatedby, out string errorMessage);
            Assert.True(x, $"Error: {errorMessage}");
            Assert.True(string.IsNullOrEmpty(errorMessage), $"Error, msg: {errorMessage}");
        }
    }
}
