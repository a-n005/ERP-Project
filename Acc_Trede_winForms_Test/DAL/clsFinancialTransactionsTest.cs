using Acc_Trede_winForms_DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acc_Trede_winForms_Test.DAL
{
    public class clsFinancialTransactionsTest
    {
        [Theory]
        [InlineData("a",5,"Cash",2,null,"hi",1,3,null)]
        [InlineData("s",5,"Cash",4,null,"hi",1,4,null)]
        public static void InsertReceiptVoucher_Multiple_Success(
       string voucherNumber,
       decimal amount,
       string paymentMethod,
       int? customerID,
       int? supplierID,
       string notes,
       int userID,
       int? saleInvoiceID,
       int? purchaseReturnInvoiceID)
        {
            var x = clsFinancialTransactions_DAL.InsertReceiptVoucher(voucherNumber, amount, paymentMethod, customerID,
                supplierID, notes, userID, saleInvoiceID,purchaseReturnInvoiceID, out string errMsg);
            Assert.True(x > 0, "Error: " + errMsg);
            Assert.True(string.IsNullOrEmpty(errMsg), "Error with msg: " + errMsg);
        }

        [Theory]
        [InlineData("aaa", 1, "Cash", null, 2, "hi", 1, null, 3)]
        [InlineData("fdgh", 4, "Cash", null, 2, null, 1, null, 4)]
        public static void  InsertPaymentVoucher_Multiple_Success(
            string voucherNumber,
            decimal amount,
            string paymentMethod,
            int? supplierID,
            int? customerID,
            string? notes,
            int userID,
            int? saleReturnInvoiceID,
            int? purchaseInvoiceID)
        {
            var x = clsFinancialTransactions_DAL.InsertPaymentVoucher(voucherNumber,amount, paymentMethod, customerID,
                supplierID, notes, userID,saleReturnInvoiceID, purchaseInvoiceID, out string errMsg);
            Assert.True(x > 0, "Error: " + errMsg);
            Assert.True(string.IsNullOrEmpty(errMsg), "Error with msg: " + errMsg);
        }
    }
}
