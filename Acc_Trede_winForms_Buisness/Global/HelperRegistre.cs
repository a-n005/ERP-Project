using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Acc_Trede_winForms_Buisness.Global
{
    public sealed class HelperRegistre
    {
        private const string _Path = @"SOFTWARE\Zed\LoginInfo";

        public static void Save(string username, string password, bool remember)
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
                            key.SetValue("Password", CryptoHelper.Encrypt(password));
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

                        return (user, CryptoHelper.Decrypt(pass), remember);
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Registry Read Error: {ex.Message}");
                }
            }
            return ("", "", false);
        }
    }
}
