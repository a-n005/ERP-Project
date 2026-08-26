using Acc_Trade_Core;
using Acc_Trede_winForms_DataAccess.UserManagement;
using Acc_Trede_winForms_Buisness.Validation;
using Acc_Trede_winForms_Buisness.Validation.UserManagement;
using Global;
using System;
using System.Data;

namespace Acc_Trede_winForms_Buisness.UserManagement
{
    public class clsUser_BLL
    {
        private enum _enMode { Add = 1, Update = 2 };
        private _enMode _mode = _enMode.Add;
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string PassWordHash { get; set; }
        public enPermissions Permissions { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public bool IsActive { get; set; }
        public int? UpdatedBy { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public clsUser_BLL()
        {
            this.UserID = -1;
            this.UserName = string.Empty;
            this.PassWordHash = string.Empty;
            this.Permissions = enPermissions.None;
            this.FullName = string.Empty;
            this.Phone = null;
            this.IsActive = true;

            this._mode = _enMode.Add;
        }
        private clsUser_BLL(int userID, string userName, string passWordHash, enPermissions permissions,
            string fullName, string phone, bool isActive, int? updatedBy, int createdBy, DateTime? updatedAt, DateTime createdAt)
        {
            this.UserID = userID;
            this.UserName = userName;
            this.PassWordHash = passWordHash;
            this.Permissions = permissions;
            this.FullName = fullName;
            this.Phone = phone;
            this.IsActive = isActive;
            _mode = _enMode.Update;
            this.UpdatedBy = updatedBy;
            this.CreatedBy = createdBy;
            this.UpdatedAt = updatedAt;
            this.CreatedAt = createdAt;
        }

        private Result _AddNewUser()
        {
            Result r = new clsUserValiator(clsUserValiator.enMode.ForAdd).Validate(this).ToResult();
            if (r.IsFailure) return r;

            Result<int> res = clsUsers_DAL.AddNewUser(this.UserName, this.PassWordHash, (int)this.Permissions, this.FullName, phone: this.Phone);
            if (res.IsFailure)
                return Result.Failure(res.Error);
            this.UserID = res.Value;
            this._mode = _enMode.Update;
            return Result.Success();
        }
        private Result _UpdateUser()
        {
            Result r = new clsUserValiator(clsUserValiator.enMode.ForUpdate).Validate(this).ToResult();
            if (r.IsFailure) return r;

            return clsUsers_DAL.UpdateUser(this.UserID, this.UserName, (int)this.Permissions, this.FullName, this.IsActive, GlobalUser.CurrentUser.UserID, this.Phone);
        }

        public Result Save()
        {
            switch (this._mode)
            {
                case _enMode.Add:
                    return _AddNewUser();
                case _enMode.Update:
                    return _UpdateUser();
            }
            return Result.Failure("خطأ: لم يتم تحديد وضع الحفظ المناسب!");
        }

        public Result UpdatePassword() => clsUsers_DAL.UpdatePassword(this.UserID, this.PassWordHash);
        public Result Delete() => clsUsers_DAL.DeleteUserSoft(this.UserID);
        public static Result<DataTable> GetAllUsers() => clsUsers_DAL.GetAllUsers();
        public static Result<clsUser_BLL> FindByID(int userID)
        {
            Result<DataTable> res = clsUsers_DAL.GetUserByID(userID);

            if (res.IsFailure)
            {
                return Result<clsUser_BLL>.Failure(res.Error);
            }

            if (res.Value == null || res.Value.Rows.Count == 0)
            {
                return Result<clsUser_BLL>.Failure("لم يتم العثور على المستخدم المطلوب.");
            }

            DataRow dr = res.Value.Rows[0];

            clsUser_BLL user = MapFromDataRow(dr);

            return Result<clsUser_BLL>.Success(user);
        }
        public bool HasPermission(enPermissions permissionToCheck)
        {
            if (this.Permissions == enPermissions.All)
                return true;

            return (this.Permissions & permissionToCheck) == permissionToCheck;
        }
        public static Result<DataTable> LoginUser(string username, string password)=> clsUsers_DAL.LoginUser(username, password);
        public static clsUser_BLL MapFromDataRow(DataRow dr)
        {
            return new clsUser_BLL(
                 userID: Convert.ToInt32(dr["UserID"]),
                 userName: dr["UserName"].ToString(),
                 passWordHash: dr["PasswordHash"].ToString(),
                 permissions: (enPermissions)Convert.ToInt32(dr["Permissions"]),
                 fullName: dr["FullName"].ToString(),
                 phone: dr["Phone"] == DBNull.Value ? null : dr["Phone"].ToString(),
                 isActive: Convert.ToBoolean(dr["IsActive"]),
                 updatedBy: dr["UpdatedBy"] == DBNull.Value ? (Int32?)null : Convert.ToInt32(dr["UpdatedBy"]),
                 createdBy: Convert.ToInt32(dr["CreatedBy"]),
                 updatedAt: dr["LastUpdate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["LastUpdate"]),
                 createdAt: Convert.ToDateTime(dr["CreatedAt"])
             );
        }
    }
}
