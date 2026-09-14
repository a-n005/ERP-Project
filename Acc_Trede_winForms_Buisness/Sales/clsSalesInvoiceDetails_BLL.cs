using Acc_Trade_Core;
using Acc_Trede_winForms_Buisness.Inventory;
using Acc_Trede_winForms_Buisness.Validation;
using Acc_Trede_winForms_DataAccess.Global;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;

namespace Acc_Trede_winForms_Buisness.Sales
{
    public class clsSalesInvoiceDetails_BLL : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #region Backing Fields & Properties

        public int DetailID { get; set; }
        public int InvoiceID { get; set; }
        public int ProductID { get; set; }
        public decimal CostPriceAtSale { get; set; }

        public string Barcode { get; set; }
        public string ProductName { get; set; }

        private int _quantity;
        public int Quantity
        {
            get => _quantity;
            set
            {
                if (_quantity != value)
                {
                    _quantity = value;
                    OnPropertyChanged(nameof(Quantity));
                    OnPropertyChanged(nameof(LineTotal));
                    OnPropertyChanged(nameof(Tax));
                }
            }
        }

        private decimal _unitPrice;
        public decimal UnitPrice
        {
            get => _unitPrice;
            set
            {
                if (_unitPrice != value)
                {
                    _unitPrice = value;
                    OnPropertyChanged(nameof(UnitPrice));
                    OnPropertyChanged(nameof(LineTotal));
                    OnPropertyChanged(nameof(Tax));
                }
            }
        }
        public decimal LineTotal => Quantity * UnitPrice;
        public decimal Tax => (UnitPrice * 0.15m)* Quantity;

        public static string[] HideColumns => new string[] { "DetailID", "InvoiceID", "CostPriceAtSale", "ProductID" };
        #endregion

        #region Constructors
        public clsSalesInvoiceDetails_BLL(int detailID, int invoiceID, decimal unitPrice, decimal costPriceAtSale, int quantity, int productID, string barcode, string productName)
        {
            DetailID = detailID;
            InvoiceID = invoiceID;
            UnitPrice = unitPrice;
            CostPriceAtSale = costPriceAtSale;
            Quantity = quantity;
            ProductID = productID;
            Barcode = barcode;
            ProductName = productName;
        }

        public clsSalesInvoiceDetails_BLL(int quantity, decimal unitPrice, clsProducts_BLL product)
        {
            Quantity = quantity;
            UnitPrice = unitPrice;
            ProductID = product.ProductID;
            Barcode = product.Barcode;
            ProductName = product.ProductName;

        }
        #endregion

        #region Data Retrieval (Queries)
        public static Result<List<clsSalesInvoiceDetails_BLL>> GetDetails(int invoiceID)
        {
            string query = @"SELECT D.DetailID, D.InvoiceID, D.ProductID,  
                                    P.Barcode, P.ProductName, D.Quantity, D.UnitPrice,
                                    D.CostPriceAtSale, P.SalePrice
                             FROM SalesInvoiceDetails D
                             INNER JOIN Products P ON D.ProductID = P.ProductID
                             WHERE D.InvoiceID = @InvoiceID";
            SqlParameter[] sp = { new SqlParameter("@InvoiceID", SqlDbType.Int) { Value = invoiceID } };
            return clsGenericDataAccessBase_DAL.ExecuteReader(query, mapper, sp);
        }
        #endregion

        #region Mapping & Helpers
        private static Func<SqlDataReader, clsSalesInvoiceDetails_BLL> mapper = reader => new clsSalesInvoiceDetails_BLL(
            detailID: Convert.ToInt32(reader["DetailID"]),
            invoiceID: Convert.ToInt32(reader["InvoiceID"]),
            quantity: Convert.ToInt32(reader["Quantity"]),
            unitPrice: Convert.ToDecimal(reader["UnitPrice"]),
            costPriceAtSale: Convert.ToDecimal(reader["CostPriceAtSale"]),
            productID: Convert.ToInt32(reader["ProductID"]),
            barcode: reader.GetStringSafe("Barcode"),
            productName: reader.GetStringSafe("ProductName")
        );
        #endregion
    }
}