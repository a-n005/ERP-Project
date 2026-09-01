using System;
using System.Security.Cryptography;
using System.Text;

namespace Acc_Trede_winForms_Buisness.Global
{
    internal class CryptoHelper
    {
        // more secure
        private static readonly byte[] OptionalEntropy = Encoding.UTF8.GetBytes("Zed_App_Entropy_2026");

        /// <summary>
        /// تشفير النص باستخدام حساب مستخدم ويندوز الحالي (DPAPI)
        /// </summary>
        internal static string Encrypt(string plainText)
        {
            if (string.IsNullOrEmpty(plainText)) return "";

            try
            {
                byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);

                // التشفير لنطاق المستخدم الحالي فقط (DataProtectionScope.CurrentUser)
                byte[] encryptedBytes = ProtectedData.Protect(
                    plainBytes,
                    OptionalEntropy,
                    DataProtectionScope.CurrentUser
                );

                return Convert.ToBase64String(encryptedBytes);
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// فك تشفير النص المأخوذ من الـ Registry
        /// </summary>
        internal static string Decrypt(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText)) return "";

            try
            {
                byte[] cipherBytes = Convert.FromBase64String(cipherText);

                byte[] plainBytes = ProtectedData.Unprotect(
                    cipherBytes,
                    OptionalEntropy,
                    DataProtectionScope.CurrentUser
                );

                return Encoding.UTF8.GetString(plainBytes);
            }
            catch
            {
                return "";
            }
        }
    }
}
