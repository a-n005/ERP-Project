using Acc_Trade_Core;
using Acc_Trede_winForms_DataAccess.UserManagement;
using Acc_Trede_winForms_Buisness.Validation;
using Acc_Trede_winForms_Buisness.Validation.UserManagement;
using System;
using System.Data;
using Acc_Trede_winForms_Buisness.Global;
using System.Data.SqlClient;
using Acc_Trede_winForms_DataAccess.Global;
using System.Collections.Generic;

namespace Acc_Trede_winForms_Buisness.UserManagement
{
    public class clsUser_BLL
    {
        #region Enums
        private enum _enMode { Add = 1, Update = 2 };
        #endregion

        #region Fields
        private _enMode _mode = _enMode.Add;
        #endregion

        #region Proprties
        public int UserID { get; set; } = -1;
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
        #endregion

        #region Constructors
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

        #endregion

        #region Private Methods
        private Result _AddNewUser()
        {
            Result r = new clsUserValiator(clsUserValiator.enMode.ForAdd).Validate(this).ToResult();
            if (r.IsFailure) return r;

            Result<int> res = clsUsers_DAL.AddNewUser(this.UserName, Helper.Encrypt(this.PassWordHash), (int)this.Permissions, this.FullName, phone: this.Phone, GlobalUser.CurrentUser?.UserID ?? -1);
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

            return clsUsers_DAL.UpdateUser(this.UserID, this.UserName, (int)this.Permissions, this.FullName, this.IsActive, GlobalUser.CurrentUser?.UserID ?? -1, this.Phone);
        }
        #endregion

        #region Public Methods
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
        public Result UpdatePassword() => clsUsers_DAL.UpdatePassword(this.UserID, Helper.Encrypt(this.PassWordHash));
        public Result Delete() => clsUsers_DAL.DeleteUserSoft(this.UserID);
        public bool HasPermission(enPermissions permissionToCheck)
        {
            if (this.Permissions == enPermissions.Admin)
                return true;

            return (this.Permissions & permissionToCheck) == permissionToCheck;
        }

        #endregion

        #region Data Retrival (Queries)
        public static Result<List<clsUser_BLL>> GetAllUsers()
        {
            string query = @"SELECT * FROM Users";
            return clsGenericDataAccessBase_DAL.ExecuteReader(query, mapper);
        }
        public static Result<clsUser_BLL> Find(int userID)
        {
            string query = @"SELECT * FROM Users where userid= @userid";
            SqlParameter[] sp = { new SqlParameter("@userid", SqlDbType.Int) { Value = userID } };
            return clsGenericDataAccessBase_DAL.ExecuteSingle(query, mapper, sp);
        }
        public static Result<clsUser_BLL> Login(string username, string password)
        {
            string query = @"SELECT * 
                     FROM Users 
                     WHERE Username = @Username AND PasswordHash = @PasswordHash";
            SqlParameter[] sp = {new SqlParameter("@Username", SqlDbType.NVarChar) { Value=username},
            new SqlParameter("@PasswordHash",SqlDbType.NVarChar){Value=password}
            };
            var r = clsGenericDataAccessBase_DAL.ExecuteSingle(query, mapper, sp);
            if (r.IsFailure)
                return Result<clsUser_BLL>.Failure("اسم المسخدم او كلمة المرور غير صحيحة.");
            return r;
        }
        #endregion

        #region Mapping & Helpers
        public static Func<SqlDataReader, clsUser_BLL> mapper = reader => new clsUser_BLL(
                 userID: Convert.ToInt32(reader["UserID"]),
                 userName: reader.GetStringSafe("UserName"),
                 passWordHash: reader.GetStringSafe("PasswordHash"),
                 permissions: (enPermissions)Convert.ToInt32(reader["Permissions"]),
                 fullName: reader.GetStringSafe("FullName"),
                 phone: reader.GetStringSafe("Phone"),
                 isActive: Convert.ToBoolean(reader["IsActive"]),
                 updatedBy: reader.GetNullable<int>("UpdatedBy"),
                 createdBy: Convert.ToInt32(reader["CreatedBy"]),
                 updatedAt: reader.GetNullable<DateTime>("LastUpdate"),
                 createdAt: Convert.ToDateTime(reader["CreatedAt"])
             );
        #endregion
    }
}
