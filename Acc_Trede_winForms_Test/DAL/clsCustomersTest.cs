using Acc_Trede_winForms_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Acc_Trede_winForms_Test
{
    public class clsCustomersTest
    {
        [Theory]
        [InlineData("anas", "55", "", 1)]
        [InlineData("an", "00787442", "", 1)]
        [InlineData("aa", "05830692", "10544616541", 1)]
        public void InsertCustomer_MultipleAdd_Success(string name, string phone, string taxNum, int createdBy)
        {
            var x = clsCustomers_DAL.InsertCustomer(name, phone, taxNum, createdBy, out string errMsg);

            Assert.True(x, $"Error: {errMsg}");
            Assert.True(string.IsNullOrEmpty(errMsg), $"حدث خطأ غير متوقع: {errMsg}");
        }
        [Theory]
        [InlineData("anas", "55", "1", 1)]
        [InlineData("anas", "55", "1", 0)]
        public void InsertCustomer_MultipleAdd_Failed(string name, string phone, string taxNum, int createdBy)
        {
            var x = clsCustomers_DAL.InsertCustomer(name, phone, taxNum, createdBy, out string errMsg);

            Assert.False(x, $"Error: {errMsg}");
            Assert.False(string.IsNullOrEmpty(errMsg), $"حدث خطأ غير متوقع: {errMsg}");
        }
        // Update
        [Theory]
        [InlineData(1, "a", "2564", "324", 2)]
        [InlineData(2, "a", "2574", "324", 2)]
        public void UpdateCustomer_Multiple_Success(int customerID, string customerName,
                    string phone, string taxNumber, int updatedBy)
        {
            var x = clsCustomers_DAL.UpdateCustomer(customerID, customerName, phone, taxNumber, updatedBy, out string errMsg);

            Assert.True(x, $"Error: {errMsg}");
            Assert.True(string.IsNullOrEmpty(errMsg), $"حدث خطأ غير متوقع: {errMsg}");
        }
        [Theory]
        [InlineData(2, "a", "2564", "", 2)]// phone is exist
        [InlineData(3, "a", "2564", "", 0)]// updated by 0 not esist
        public void UpdateCustomer_Multiple_Failed(int customerID, string customerName,
                  string phone, string taxNumber, int updatedBy)
        {
            var x = clsCustomers_DAL.UpdateCustomer(customerID, customerName, phone, taxNumber, updatedBy, out string errMsg);

            Assert.False(x, $"Error: {errMsg}");
            Assert.False(string.IsNullOrEmpty(errMsg), $"حدث خطأ غير متوقع: {errMsg}");
        }
        // Delete
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void DeleteCustomerSoft_Multiple_Success(int customerID)
        {
            var x = clsCustomers_DAL.DeleteCustomerSoft(customerID, out string errMsg);

            Assert.True(x, $"Error: {errMsg}");
            Assert.True(string.IsNullOrEmpty(errMsg), $"حدث خطأ غير متوقع: {errMsg}");
        }
        [Theory]
        [InlineData(0)]
        public void DeleteCustomerSoft_Multiple_Failed(int customerID)
        {
            var x = clsCustomers_DAL.DeleteCustomerSoft(customerID, out string errMsg);

            Assert.False(x, $"Error: {errMsg}");
            Assert.True(string.IsNullOrEmpty(errMsg), $"حدث خطأ غير متوقع: {errMsg}");
        }
        // Get Customer
        [Fact]
        public void GetAllCustomers_ReturnDtWithRecords_Success()
        {
            var x = clsCustomers_DAL.GetAllCustomers(out string errMsg);

            Assert.True(x.Rows.Count > 0, $"Error: {errMsg}");
            Assert.Equal("2574", x.Rows[1].Field<string>("Phone"));
            Assert.True(string.IsNullOrEmpty(errMsg), $"حدث خطأ غير متوقع: {errMsg}");
        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetCustomerByID_ReturnDtWithRecords_Success(int customerID)
        {
            var x = clsCustomers_DAL.GetCustomerByID(customerID, out string errMsg);
            string s = customerID == 1 ? "2564" : "2574";
            Assert.True(x.Rows.Count > 0, $"Error: {errMsg}");
            Assert.Equal(s, x.Rows[0].Field<string>("Phone"));
            Assert.True(string.IsNullOrEmpty(errMsg), $"حدث خطأ غير متوقع: {errMsg}");
        }
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public void GetCustomerByID_ReturnDtWithRecords_Failed(int customerID)
        {
            var x = clsCustomers_DAL.GetCustomerByID(customerID, out string errMsg);

            if (x.Rows.Count > 0)
            {
                Assert.True(x.Rows.Count > 0, $"Error: {errMsg}");
                Assert.Equal("2564", x.Rows[0].Field<string>("Phone"));
            }
            else
                Assert.False(x.Rows.Count > 0, $"Error: {errMsg}");
            Assert.True(string.IsNullOrEmpty(errMsg), $"حدث خطأ غير متوقع: {errMsg}");
        }
    }
}
