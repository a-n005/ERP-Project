using Acc_Trede_winForms_DataAccess.UserManagement;
using Acc_Trade_Core;
using Xunit;

namespace Acc_Trede_winForms_Test.DAL
{
    public class clsUsersTest
    {
        /// <summary>
        /// Add New User
        /// </summary>
        /// <param name="username"></param>
        /// <param name="passwordHash"></param>
        /// <param name="permissions"></param>
        /// <param name="fullName"></param>
        /// <param name="phone"></param>
        [Theory]
        [InlineData("sssssssssssss", "pass123", 1, "Anas Abdullah", "0500000000")] 
        [InlineData("ssssssssss", "hash9988", 2, "Khalid Ali", null)]        
        [InlineData("sssssssss", "securesaled", 4, "Sami Sales", "0511111111")] 
        public void AddNewUser_MultipleUsers_ShouldRegisterSuccessfully(
             string username, string passwordHash, int permissions, string fullName, string? phone)
        {
            // Arrange: (المعطيات تأتي تلقائياً من الـ InlineData كبارامترات للدالة)

            // Act: استدعاء الدالة بنفس الترتيب
            var x  = clsUsers_DAL.AddNewUser(username, passwordHash, permissions, fullName, phone);

            // Assert: التحقق من النتائج
            // 1. نتأكد أن السيرفر نجح في الإدخال وأعاد معرّفاً تلقائياً أكبر من 0
            Assert.True(x.Value > 0, $"فشل إدخال المستخدم {username}. رسالة الخطأ المرتجعة: {x.Error}");

            // 2. نتأكد أن رسالة الخطأ فارغة تماماً
            Assert.True(string.IsNullOrEmpty(x.Error), $"حدث خطأ غير متوقع: {x.Error}");
        }
        [Fact]
        public void AddNewUser_isNull_ShouldNotRegister()
        { 

            // Act: استدعاء الدالة بنفس الترتيب
            var newUserID = clsUsers_DAL.AddNewUser("", null, 5, null);

            // Assert: التحقق من النتائج
            // 1. نتأكد أن السيرفر نجح في الإدخال وأعاد معرّفاً تلقائياً أكبر من 0
            Assert.False(newUserID.Value > 0, $"فشل إدخال المستخدم {""}. رسالة الخطأ المرتجعة: {newUserID.Error}");

            // 2. نتأكد أن رسالة الخطأ فارغة تماماً
            Assert.False(string.IsNullOrEmpty(newUserID.Error), $"حدث خطأ غير متوقع: {newUserID.Error}");
        }
        /// <summary>
        /// Multiple Update Users
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="username"></param>
        /// <param name="permissions"></param>
        /// <param name="fullName"></param>
        /// <param name="isActive"></param>
        /// <param name="phone"></param>
        /// <param name="updatedBy"></param>
        [Theory]
        [InlineData(1, "ana", 5, "x", false, "", 1)]
        [InlineData(2, "test6", 5, "anas abdullah", true, "0580309692", 1)]
        [InlineData(3, "test7", 5, "anas abdullah", true, "0580309692", 1)]
        public void UpdateUser_MultipleUpdated_ShouldUpdatedSuccessfully(int userID, string username, int permissions,
    string fullName, bool isActive, string phone, int updatedBy)
        {
            var x = clsUsers_DAL.UpdateUser(userID, username, permissions, fullName, isActive, updatedBy, phone);

            Assert.True(x.IsSuccess, $"Udate failed: {x.Error}");
            Assert.True(string.IsNullOrEmpty(x.Error), $"Update failed: {x.Error}");
        }
        /// <summary>
        /// Get Users
        /// </summary>
        /// <param name="userid"></param>
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void GetUserByID_ReturnDtWithRecurd_ShouldReturnSuccessfully(int userid)
        {
            var x = clsUsers_DAL.GetUserByID(userid);

            Assert.True(x.Value.Rows.Count >= 0, $"Error: {x.Error}");
            Assert.True(string.IsNullOrEmpty(x.Error), $"Error return errMsg→ {x.Error}");
        }
        [Fact]
        public void GetUserByID_ReturnDtWithRecurd_ShouldReturnFailed()
        {
            var x = clsUsers_DAL.GetUserByID(0);

            Assert.False(x.Value.Rows.Count > 0, $"Error: {x.Error}");
            Assert.True(string.IsNullOrEmpty(x.Error), $"Error return errMsg→ {x.Error}");
        }
        [Fact]
        public void GetAllUsers_ReturnDtWithRecurds_ShouldReturnSuccess()
        {
            var x = clsUsers_DAL.GetAllUsers();

            Assert.True(x.Value.Rows.Count > 0, $"Error: {x.Error}");
            Assert.True(string.IsNullOrEmpty(x.Error), $"Error return errMsg→ {x.Error}");
        }
        [Fact]
        public void GetAllUsers_Null_Failed()
        {
            var x = clsUsers_DAL.GetAllUsers();

            Assert.False(x.Value.Rows.Count <= 0, $"Error: {x.Error}");
            Assert.True(string.IsNullOrEmpty(x.Error), $"Error return errMsg→ {x.Error}");
        }
        /// <summary>
        /// Update Pass 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="newPasswordHash"></param>
        [Theory]
        [InlineData(1, "0808")]
        [InlineData(2, "0808")]
        [InlineData(3, "0808")]
        public void UpdatePassword_MultipleUpdate_ShouldSuccessfully(int userId, string newPasswordHash)
        {
            var x = clsUsers_DAL.UpdatePassword(userId, newPasswordHash);
            Assert.True(x.IsSuccess, $"Error: {x.Error}");
            Assert.True(string.IsNullOrEmpty(x.Error), $"Error, msg: {x.Error}");
        }
        [Fact]
        public void UpdatePassword_isNull_ShouldFailed()
        {
            var x = clsUsers_DAL.UpdatePassword(0, "");
            Assert.False(x.IsSuccess, $"Error: {x.Error}");
            Assert.True(string.IsNullOrEmpty(x.Error), $"Error, msg: {x.Error}");
        }
        /// <summary>
        /// Delete Soft
        /// </summary>
        /// <param name="userId"></param>
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void DeleteUserSoft_MultipleDelete_Success(int userId)
        {
            var x = clsUsers_DAL.DeleteUserSoft(userId);
            Assert.True(x.IsSuccess, $"Error, {x.Error}");
            Assert.True(string.IsNullOrEmpty(x.Error), $"Error msg: {x.Error}");
        }
        [Fact]
        public void DeleteUserSoft_Null_Success()
        {
            var x = clsUsers_DAL.DeleteUserSoft(0);
            Assert.False(x.IsFailure, $"Error, {x.Error}");
            Assert.True(string.IsNullOrEmpty(x.Error), $"Error msg: {x.Error}");
        }
        /// <summary>
        /// Login 
        /// </summary>
        /// <param name="passwordHash"></param>
        /// <param name="username"></param>
        [Theory]
        [InlineData("0808","x")]
        [InlineData("0800","ana")]
        public void LoginUser_MultipleLogin_Failed(string passwordHash, string username)
        {
            var x=clsUsers_DAL.LoginUser(username,passwordHash);
            Assert.False(x.Value.Rows.Count>0, $"Error, {x.Error}");
            Assert.True(string.IsNullOrEmpty(x.Error), $"Error msg: {x.Error}");
        }
        [Fact]
        public void LoginUser_Right_Success()
        {
            var x=clsUsers_DAL.LoginUser("ana","0808");
            Assert.True(x.Value.Rows.Count>0, $"Error, {x.Error}");
            Assert.True(string.IsNullOrEmpty(x.Error), $"Error msg: {x.Error}");
        }
    }
}
