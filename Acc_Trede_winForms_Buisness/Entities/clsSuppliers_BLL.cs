using Acc_Trade_Core;
using Acc_Trede_winForms_DataAccess.Entities;
using Acc_Trede_winForms_Buisness.Validation;
using Acc_Trede_winForms_Buisness.Validation.Entitis;
using System;
using System.Data;
using System.Collections.Generic;
using Acc_Trede_winForms_DataAccess.Global;
using System.Data.SqlClient;
using Acc_Trede_winForms_Buisness.Global;

namespace Acc_Trede_winForms.Entities
{
    public class clsSuppliers_BLL
    {
        #region Enums
        private enum _enMode { Add, Update };
        #endregion

        #region Fields
        private _enMode _Mode = _enMode.Add;
        #endregion

        #region Properties
        public int SupplierID { get; set; }
        public string SupplierName { get; set; }
        public string CompanyName { get; set; }
        public string TaxNumber { get; set; }
        // current balance add later
        public string Phone { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? LastUpdated { get; set; }
        #endregion

        #region Constructors
        public clsSuppliers_BLL()
        {
            SupplierID = -1;
            _Mode = _enMode.Add;
        }
        private clsSuppliers_BLL(int supplierID, string supplierName, string companyName, string taxNumber, string phone, DateTime createdAt, bool isActive, int createdBy, int? updatedBy, DateTime? lastUpdated)
        {
            this.SupplierID = supplierID;
            this.SupplierName = supplierName;
            this.CompanyName = companyName;
            this.TaxNumber = taxNumber;
            this.Phone = phone;
            this.CreatedAt = createdAt;
            this.IsActive = isActive;
            this.CreatedBy = createdBy;
            this.UpdatedBy = updatedBy;
            this.LastUpdated = lastUpdated;
            _Mode = _enMode.Update;
        }
        #endregion

        #region Private Method
        private Result _AddNewSupplier()
        {
            Result<int> res = clsSuppliers_DAL.InsertSupplier(this.SupplierName, this.CompanyName, this.Phone, this.TaxNumber, GlobalUser.CurrentUser.UserID);
            if (res.IsFailure)
                return Result.Failure(res.Error);
            this.SupplierID = res.Value;
            this._Mode = _enMode.Update;
            return Result.Success();
        }
        private Result _UpdateSupplier() => clsSuppliers_DAL.UpdateSupplier(this.SupplierID, this.SupplierName, this.CompanyName, this.Phone, this.TaxNumber, GlobalUser.CurrentUser.UserID);
        #endregion

        #region Public Method
        public Result Save()
        {
            Result r = new clsSuppliersValidator().Validate(this).ToResult();
            if (r.IsFailure) return r;

            switch (_Mode)
            {
                case _enMode.Add:
                    return _AddNewSupplier();
                case _enMode.Update:
                    return _UpdateSupplier();
            }
            return Result.Failure("خطأ: لم يتم تحديد وضع الحفظ المناسب!");
        }
        public Result DeleteSupplier(int SupplierID) => clsSuppliers_DAL.DeleteSupplierSoft(SupplierID);
        #endregion

        #region Data Retrieval (Queries)
        public static Result<List<clsSuppliers_BLL>> GetAllSupplier()
        {
            string query = @"SELECT * 
                     FROM Suppliers 
                     ORDER BY CompanyName ASC, SupplierName ASC";
            return clsGenericDataAccessBase_DAL.ExecuteReader(query,mapper);
        }
        public static Result<clsSuppliers_BLL> Find(int SupplierID)
        {
            string query = @"SELECT *
                     FROM Suppliers 
                     WHERE SupplierID = @SupplierID";
            SqlParameter[] parms = { new SqlParameter("@SupplierID", SqlDbType.Int) { Value = SupplierID } };
           return clsGenericDataAccessBase_DAL.ExecuteSingle(query,mapper,parms);
        }
        #endregion

        #region Mapping & Helpers
        private static Func<SqlDataReader, clsSuppliers_BLL> mapper = reader => new clsSuppliers_BLL(
            supplierID: Convert.ToInt32(reader["SupplierID"]),
            supplierName: reader["SupplierName"] != DBNull.Value ? reader["SupplierName"].ToString() : "مورد غير معروف",
            companyName: reader["CompanyName"] != DBNull.Value ? reader["CompanyName"].ToString() : string.Empty,
            taxNumber: reader["TaxNumber"] != DBNull.Value ? reader["TaxNumber"].ToString() : string.Empty,
            phone: reader["Phone"] != DBNull.Value ? reader["Phone"].ToString() : null,
            createdAt: Convert.ToDateTime(reader["CreatedAt"]),
            isActive: Convert.ToBoolean(reader["IsActive"]),
            createdBy: Convert.ToInt32(reader["CreatedBy"]),
            updatedBy: reader["UpdatedBy"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["UpdatedBy"]),
            lastUpdated: reader["UpdatedAt"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["UpdatedAt"])
        );
        #endregion
    }
}
