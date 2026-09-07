using Acc_Trade_Core;
using Acc_Trede_winForms_Buisness.Global;
using Acc_Trede_winForms_Buisness.Validation;
using Acc_Trede_winForms_Buisness.Validation.Entitis;
using Acc_Trede_winForms_DataAccess.Entities;
using Acc_Trede_winForms_DataAccess.Global;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Acc_Trede_winForms_Buisness.Entities
{
    public class clsCustomers_BLL
    {
        #region Enums
        private enum _enMode { Add, Update }
        #endregion

        #region Fields
        private _enMode _Mode = _enMode.Add;
        #endregion

        #region Properties
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string Phone { get; set; }
        public string TaxNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? LastUpdate { get; set; }
        #endregion

        #region Constructors
        public clsCustomers_BLL()
        {
            this.CustomerID = -1;
            _Mode = _enMode.Add;
        }
        private clsCustomers_BLL(int customerID, string customerName, string phone, string taxNumber, DateTime createdAt, bool isActive, int createdBy, int? updatedBy, DateTime? updatedAt)
        {
            this.CustomerID = customerID;
            this.CustomerName = customerName;
            this.Phone = phone;
            this.TaxNumber = taxNumber;
            this.CreatedAt = createdAt;
            this.IsActive = isActive;
            this.CreatedBy = createdBy;
            this.UpdatedBy = updatedBy;
            this.LastUpdate = updatedAt;
        }
        #endregion

        #region Private Method
        private Result _Add()
        {
            Result<int> res = clsCustomers_DAL.InsertCustomer(this.CustomerName, this.Phone, this.TaxNumber, GlobalUser.CurrentUser.UserID);
            if (res.IsFailure)
            {
                return Result.Failure(res.Error);
            }
            this.CustomerID = res.Value;
            this._Mode = _enMode.Update;
            return Result.Success();
        }
        private Result _Update() => clsCustomers_DAL.UpdateCustomer(this.CustomerID, this.CustomerName, this.Phone, this.TaxNumber, GlobalUser.CurrentUser.UserID);
        #endregion

        #region Public Method
        public Result Save()
        {
            Result r = new clsCustomersValidator().Validate(this).ToResult();
            if (r.IsFailure) return r;

            switch (_Mode)
            {
                case _enMode.Add:
                    return _Add();
                case _enMode.Update:
                    return _Update();
            }
            return Result.Failure("خطأ: لم يتم تحديد وضع الحفظ المناسب!");
        }
        public Result Delete() => clsCustomers_DAL.DeleteCustomerSoft(this.CustomerID);
        #endregion

        #region Data Retrieval (Queries)
        public static Result<List<clsCustomers_BLL>> GetAllCustomers()
        {
            string query = @"SELECT *
                     FROM Customers 
                     ORDER BY CustomerName ASC";
            return clsGenericDataAccessBase_DAL.ExecuteReader(query,mapper);
        }
        public static Result<clsCustomers_BLL> Find(int id)
        {
            string query = @"SELECT *
                     FROM Customers 
                     WHERE CustomerID = @CustomerID";
            SqlParameter[] parms = { new SqlParameter("@CustomerID", SqlDbType.Int) { Value = id } };
            return clsGenericDataAccessBase_DAL.ExecuteSingle(query, mapper, parms);
        }
        #endregion

        #region Mapping & Helpers
        private static Func<SqlDataReader, clsCustomers_BLL> mapper = reader => new clsCustomers_BLL(
       customerID: Convert.ToInt32(reader["CustomerID"]),
       customerName: reader.GetStringSafe("CustomerName", "عميل نقدي"),
       phone: reader["Phone"]?.ToString() ?? string.Empty,
       taxNumber: reader["TaxNumber"]?.ToString() ?? string.Empty,
       createdAt: Convert.ToDateTime(reader["CreatedAt"]),
       isActive: Convert.ToBoolean(reader["IsActive"]),
       createdBy: Convert.ToInt32(reader["CreatedBy"]),
       updatedBy: reader["UpdatedBy"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["UpdatedBy"]),
       updatedAt: reader["UpdatedAt"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["UpdatedAt"])
   );

        #endregion
    }
}
