using Acc_Trade_Core;
using Acc_Trede_winForms_DataAccess.Entities;
using Global;
using System;
using System.Data;

namespace Acc_Trede_winForms_Buisness.Entities
{
    public class clsCustomers_BLL
    {
        private enum _enMode { Add, Update }
        private _enMode _Mode = _enMode.Add;
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string Phone { get; set; }
        public string TaxNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? LastUpdate { get; set; }
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
        public Result Save()
        {
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
        public static Result<DataTable> GetAllCustomers() => clsCustomers_DAL.GetAllCustomers();
        public static Result<clsCustomers_BLL> FindByID(int id)
        {
            Result<DataTable> res = clsCustomers_DAL.GetCustomerByID(id);
            if (res.IsFailure)
                return Result<clsCustomers_BLL>.Failure(res.Error);
            if (res.Value == null && res.Value.Rows.Count == 0)
                Result<clsCustomers_BLL>.Failure("لم يتم العثور على العميل المطلوب.");
            DataRow r = res.Value.Rows[0];
            clsCustomers_BLL customer = MapFromDataRow(r);
            return Result<clsCustomers_BLL>.Success(customer);
        }
        private static clsCustomers_BLL MapFromDataRow(DataRow dr)
        {
            return new clsCustomers_BLL(
                customerID: Convert.ToInt32(dr["CustomerID"]),
                customerName: dr["CustomerName"].ToString(),
                phone: dr["Phone"].ToString(),
                taxNumber: dr["TaxNumber"].ToString(),
                createdAt: Convert.ToDateTime(dr["CreatedAt"]),
                isActive: Convert.ToBoolean(dr["IsActive"]),
                createdBy: Convert.ToInt32(dr["CreatedBy"]),
                updatedBy: dr["UpdatedBy"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["UpdatedBy"]),
                updatedAt: dr["UpdatedAt"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["UpdatedAt"])
                );
        }
    }
}
