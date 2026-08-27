using Acc_Trade_Core;
using Acc_Trede_winForms_DataAccess.Entities;
using Acc_Trede_winForms_Buisness.Validation;
using Acc_Trede_winForms_Buisness.Validation.Entitis;
using Global;
using System;
using System.Data;

namespace Acc_Trede_winForms.Entities
{
    public class clsSuppliers_BLL
    {
        private enum _enMode { Add, Update };
        private _enMode _Mode = _enMode.Add;
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
        public Result Save()
        {
            Result r=new clsSuppliersValidator().Validate(this).ToResult();
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
        public static Result<DataTable> GetAllSupplier() => clsSuppliers_DAL.GetAllSuppliers();
        public static Result<clsSuppliers_BLL> FindSupplierByID(int SupplierID)
        {
            Result<DataTable> res = clsSuppliers_DAL.GetSupplierByID(SupplierID);
            if (res.IsFailure)
                return Result<clsSuppliers_BLL>.Failure(res.Error);
            if (res.Value == null && res.Value.Rows.Count == 0)
                return Result<clsSuppliers_BLL>.Failure("لم يتم العثور على المورد المطلوب.");
            DataRow dr = res.Value.Rows[0];
            clsSuppliers_BLL supplier = MapFromDataRow(dr);
            return Result<clsSuppliers_BLL>.Success(supplier);

        }
        private static clsSuppliers_BLL MapFromDataRow(DataRow dr)
        {
            return new clsSuppliers_BLL(
                 supplierID: Convert.ToInt32(dr["SupplierID"]),
                 supplierName: dr["SupplierName"].ToString(),
                 companyName: dr["CompanyName"].ToString(),
                 taxNumber: dr["TaxNumber"].ToString(),
                 phone: dr["Phone"] == DBNull.Value ? null : dr["Phone"].ToString(),
                 createdAt: Convert.ToDateTime(dr["CreatedAt"]),
                 isActive: Convert.ToBoolean(dr["IsActive"]),
                 createdBy: Convert.ToInt32(dr["CreatedBy"]),
                 updatedBy: dr["UpdatedBy"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["UpdatedBy"]),
                 lastUpdated: dr["UpdatedAt"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["UpdatedAt"])
             );
        }

    }
}
