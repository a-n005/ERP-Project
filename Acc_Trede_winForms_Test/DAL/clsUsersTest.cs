using Acc_Trede_winForms_DataAccess;
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
            string errMsg;

            // Act: استدعاء الدالة بنفس الترتيب
            int newUserID = clsUsers_DAL.AddNewUser(username, passwordHash, permissions, fullName, out errMsg, phone);

            // Assert: التحقق من النتائج
            // 1. نتأكد أن السيرفر نجح في الإدخال وأعاد معرّفاً تلقائياً أكبر من 0
            Assert.True(newUserID > 0, $"فشل إدخال المستخدم {username}. رسالة الخطأ المرتجعة: {errMsg}");

            // 2. نتأكد أن رسالة الخطأ فارغة تماماً
            Assert.True(string.IsNullOrEmpty(errMsg), $"حدث خطأ غير متوقع: {errMsg}");
        }
        [Fact]
        public void AddNewUser_isNull_ShouldNotRegister()
        {
            // Arrange: (المعطيات تأتي تلقائياً من الـ InlineData كبارامترات للدالة)
            string errMsg;

            // Act: استدعاء الدالة بنفس الترتيب
            int newUserID = clsUsers_DAL.AddNewUser("", null, 5, null, out errMsg);

            // Assert: التحقق من النتائج
            // 1. نتأكد أن السيرفر نجح في الإدخال وأعاد معرّفاً تلقائياً أكبر من 0
            Assert.False(newUserID > 0, $"فشل إدخال المستخدم {""}. رسالة الخطأ المرتجعة: {errMsg}");

            // 2. نتأكد أن رسالة الخطأ فارغة تماماً
            Assert.False(string.IsNullOrEmpty(errMsg), $"حدث خطأ غير متوقع: {errMsg}");
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
            var x = clsUsers_DAL.UpdateUser(userID, username, permissions, fullName, isActive, updatedBy, out string errmsg, phone);

            Assert.True(x, $"Udate failed: {errmsg}");
            Assert.True(string.IsNullOrEmpty(errmsg), $"Update failed: {errmsg}");
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
            var x = clsUsers_DAL.GetUserByID(userid, out string err);

            Assert.True(x.Rows.Count >= 0, $"Error: {err}");
            Assert.True(string.IsNullOrEmpty(err), $"Error return errMsg→ {err}");
        }
        [Fact]
        public void GetUserByID_ReturnDtWithRecurd_ShouldReturnFailed()
        {
            var x = clsUsers_DAL.GetUserByID(0, out string err);

            Assert.False(x.Rows.Count > 0, $"Error: {err}");
            Assert.True(string.IsNullOrEmpty(err), $"Error return errMsg→ {err}");
        }
        [Theory]
        [InlineData("ana")]
        [InlineData("test6")]
        [InlineData("test7")]
        public void GetUserByUserName_ReturnDtWithRecurd_ShouldReturnSuccessfully(string userid)
        {
            var x = clsUsers_DAL.GetUserByUserName(userid, out string err);

            Assert.True(x.Rows.Count >= 0, $"Error: {err}");
            Assert.True(string.IsNullOrEmpty(err), $"Error return errMsg→ {err}");
        }
        [Fact]
        public void GetUserByUserName_ReturnDtWithRecurd_ShouldReturnFailed()
        {
            var x = clsUsers_DAL.GetUserByUserName("", out string err);

            Assert.False(x.Rows.Count > 0, $"Error: {err}");
            Assert.True(string.IsNullOrEmpty(err), $"Error return errMsg→ {err}");
        }
        [Fact]
        public void GetAllUsers_ReturnDtWithRecurds_ShouldReturnSuccess()
        {
            var x = clsUsers_DAL.GetAllUsers( out string err);

            Assert.True(x.Rows.Count > 0, $"Error: {err}");
            Assert.True(string.IsNullOrEmpty(err), $"Error return errMsg→ {err}");
        }
        [Fact]
        public void GetAllUsers_Null_Failed()
        {
            var x = clsUsers_DAL.GetAllUsers( out string err);

            Assert.False(x.Rows.Count <= 0, $"Error: {err}");
            Assert.True(string.IsNullOrEmpty(err), $"Error return errMsg→ {err}");
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
            var x = clsUsers_DAL.UpdatePassword(userId, newPasswordHash, out string errorMessage);
            Assert.True(x, $"Error: {errorMessage}");
            Assert.True(string.IsNullOrEmpty(errorMessage), $"Error, msg: {errorMessage}");
        }
        [Fact]
        public void UpdatePassword_isNull_ShouldFailed()
        {
            var x = clsUsers_DAL.UpdatePassword(0, "", out string errorMessage);
            Assert.False(x, $"Error: {errorMessage}");
            Assert.True(string.IsNullOrEmpty(errorMessage), $"Error, msg: {errorMessage}");
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
            var x = clsUsers_DAL.DeleteUserSoft(userId, out string errMsg);
            Assert.True(x, $"Error, {errMsg}");
            Assert.True(string.IsNullOrEmpty(errMsg),$"Error msg: {errMsg}");
        }
        [Fact]
        public void DeleteUserSoft_Null_Success()
        {
            var x = clsUsers_DAL.DeleteUserSoft(0, out string errMsg);
            Assert.False(x, $"Error, {errMsg}");
            Assert.True(string.IsNullOrEmpty(errMsg), $"Error msg: {errMsg}");
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
            var x=clsUsers_DAL.LoginUser(username,passwordHash,out string errorMessage);
            Assert.False(x.Rows.Count>0, $"Error, {errorMessage}");
            Assert.True(string.IsNullOrEmpty(errorMessage), $"Error msg: {errorMessage}");
        }
        [Fact]
        public void LoginUser_Right_Success()
        {
            var x=clsUsers_DAL.LoginUser("ana","0808",out string errorMessage);
            Assert.True(x.Rows.Count>0, $"Error, {errorMessage}");
            Assert.True(string.IsNullOrEmpty(errorMessage), $"Error msg: {errorMessage}");
        }
    }
}
