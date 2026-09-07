using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace Acc_Trede_winForms_Buisness.Global
{
    public static class Helper
    {


        #region  Registry
        private const string _Path = @"SOFTWARE\Zed\LoginInfo";
        internal static void Save(string username, string password, bool remember)
        {
            using (var key = Registry.CurrentUser.CreateSubKey(_Path))
            {
                try
                {
                    if (key != null)
                    {
                        if (remember)
                        {
                            key.SetValue("Username", username);
                            key.SetValue("Password", password);
                            key.SetValue("RememberMe", "true");
                        }
                        else
                        {
                            Registry.CurrentUser.DeleteSubKeyTree(_Path, throwOnMissingSubKey: false);
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Registry Save Error: {ex.Message}");
                }
            }
        }
        public static (string username, string password, bool rememberMe) Read()
        {
            using (var key = Registry.CurrentUser.OpenSubKey(_Path))
            {
                try
                {
                    if (key != null)
                    {
                        string user = key.GetValue("Username", "")?.ToString() ?? "";
                        string pass = key.GetValue("Password", "")?.ToString() ?? "";
                        string rememberStr = key.GetValue("RememberMe", "false")?.ToString() ?? "false";
                        bool.TryParse(rememberStr, out bool remember);

                        return (user, Decrypt(pass), remember);
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Registry Read Error: {ex.Message}");
                }
            }
            return ("", "", false);
        }
        #endregion

        #region Crypto (AES Symmetric Encryption)

        private const string Key = "1234567890123456"; 

        public static string Encrypt(string plainText)
        {
            if (string.IsNullOrEmpty(plainText)) return string.Empty;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(Key);
                aesAlg.IV = new byte[aesAlg.BlockSize / 8]; 

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (var msEncrypt = new System.IO.MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    using (var swEncrypt = new System.IO.StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }

                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        public static string Decrypt(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText)) return string.Empty;

            try
            {
                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = Encoding.UTF8.GetBytes(Key);
                    aesAlg.IV = new byte[aesAlg.BlockSize / 8];

                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                    using (var msDecrypt = new System.IO.MemoryStream(Convert.FromBase64String(cipherText)))
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    using (var srDecrypt = new System.IO.StreamReader(csDecrypt))
                    {
                        return srDecrypt.ReadToEnd();
                    }
                }
            }
            catch
            {
                return string.Empty;
            }
        }

        #endregion
    }
}
