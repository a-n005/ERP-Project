//using Acc_Trede_winForms_DataAccess.Entities;
//using Acc_Trade_Core;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Numerics;
//using System.Text;
//using System.Threading.Tasks;
//using Xunit.Internal;

//namespace Acc_Trede_winForms_Test.DAL
//{
//    public class clsSuppliersTest
//    {
//        [Theory]
//        [InlineData("an", null, null, null, 1)]
//        [InlineData("ans", "x", "050", "15748513", 1)]
//        public void InsertSupplier_Multiple_Success(string supplierName, string? companyName,
//         string? phone, string? taxNumber, int createdBy)
//        {
//            var x = clsSuppliers_DAL.InsertSupplier(supplierName, companyName, phone, taxNumber, createdBy);

//            Assert.True(x.IsSuccess, $"Error: {x.Error}");
//            Assert.True(string.IsNullOrEmpty(x.Error), $"Error with msg: {x.Error}");
//        }
//        [Theory]
//        [InlineData("an", null, null, null, 1)]
//        [InlineData("anfgs", "x", "050", "15748513", 0)]
//        [InlineData(null, "x", "050", "15748513", 5)]
//        public void InsertSupplier_Multiple_Failed(string? supplierName, string? companyName,
//       string? phone, string? taxNumber, int createdBy)
//        {
//            var x = clsSuppliers_DAL.InsertSupplier(supplierName, companyName, phone, taxNumber, createdBy);

//            Assert.False(x.IsFailure, $"Error: {x.Error}");
//            Assert.False(string.IsNullOrEmpty(x.Error), $"Error with msg: {x.Error}");
//        }
//        // Update 
//        [Theory]
//        [InlineData(1, "aa", null, null, null, 3)]
//        [InlineData(2, "aaa", null, null, null, 3)]
//        public void UpdateSupplier_Multiple_Success(int supplierID, string? supplierName,
//            string? companyName, string? phone, string? taxNumber, int updatedBy)
//        {
//            var x = clsSuppliers_DAL.UpdateSupplier(supplierID, supplierName, companyName, phone, taxNumber, updatedBy);

//            Assert.True(x.IsSuccess, $"Error: {x.Error}");
//            Assert.True(string.IsNullOrEmpty(x.Error), $"Error with msg: {x.Error}");
//        }
//        [Theory]
//        [InlineData(0, "aa", null, null, null, 3)]
//        [InlineData(1, "aaa", null, null, null, 3)]
//        [InlineData(2, "aaa", null, null, null, 0)]
//        [InlineData(2, null, null, null, null, 1)]
//        public void UpdateSupplier_Multiple_Failed(int supplierID, string? supplierName,
//           string? companyName, string? phone, string? taxNumber, int updatedBy)
//        {
//            var x = clsSuppliers_DAL.UpdateSupplier(supplierID, supplierName, companyName, phone, taxNumber, updatedBy);

//            Assert.False(x.IsFailure, $"Error: {x.Error}");
//            Assert.False(string.IsNullOrEmpty(x.Error), $"Error with msg: {x.Error}");
//        }

//        // Delete 
//        [Theory]
//        [InlineData(1)]
//        [InlineData(2)]
//        public void DeleteSupplierSoft_Multiple_Success(int supplierID)
//        {
//            var x = clsSuppliers_DAL.DeleteSupplierSoft(supplierID);

//            Assert.True(x.IsSuccess, $"Error: {x.Error}");
//            Assert.True(string.IsNullOrEmpty(x.Error), $"Error with msg: {x.Error}");
//        }
//        [Theory]
//        [InlineData(0)]
//        public void DeleteSupplierSoft_Multiple_Failed(int supplierID)
//        {
//            var x = clsSuppliers_DAL.DeleteSupplierSoft(supplierID);

//            Assert.False(x.IsFailure, $"Error: {x.Error}");
//            Assert.True(string.IsNullOrEmpty(x.Error), $"Error with msg: {x.Error}");
//        }
//        // Get
//        [Fact]
//        public void GetAllSuppliers_ReturnDt_Success()
//        {
//            var x = clsSuppliers_DAL.GetAllSuppliers();

//            Assert.True(x.Value.Rows.Count>0, $"Error: {x.Error}");
//            Assert.Equal("050", x.Value.Rows[3].Field<string>("Phone"));
//            Assert.True(string.IsNullOrEmpty(x.Error), $"Error with msg: {x.Error}");
//        }
//        [Fact]
//        public void GetSupplierByID_ReturnRecord_Succes()
//        {
//            var x = clsSuppliers_DAL.GetSupplierByID(1);

//            Assert.True(x.Value.Rows.Count > 0, $"Error: {x.Error}");
//            Assert.Null(x.Value.Rows[0].Field<string>("Phone"));
//            Assert.True(string.IsNullOrEmpty(x.Error), $"Error with msg: {x.Error}");
//        }
//        [Fact]
//        public void GetSupplierByID_WithNull_Failed()
//        {
//            var x = clsSuppliers_DAL.GetSupplierByID(0);
//            Assert.False(x.Value.Rows.Count > 0, $"Error: {x.Error}");
//            Assert.True(string.IsNullOrEmpty(x.Error), $"Error with msg: {x.Error}");
//        }
//        [Fact]
//        public void GetSupplierByID_WrongData_Failed()
//        {
//            var x = clsSuppliers_DAL.GetSupplierByID(1);
//            Assert.True(x.Value.Rows.Count > 0, $"Error: {x.Error}");
//             Assert.NotEqual("050", x.Value.Rows[0].Field<string>("Phone"));
//            Assert.True(string.IsNullOrEmpty(x.Error), $"Error with msg: {x.Error}");
//        }
//    }
//}

