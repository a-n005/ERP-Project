using Acc_Trede_winForms.Login;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Acc_Trede_winForms.Models.CMessageBox
{
    public static class CMsgB
    {
        public static DialogResult Show(string title, string message, bool showCancelButton = true,bool isYN=false)
        {
            var msgBox = new CMsgBox(title, message, showCancelButton,isYN:isYN);
            msgBox.SetFore(Color.FromArgb(64, 64, 64), Color.FromArgb(108, 92, 231), Color.FromArgb(108, 92, 231));
            msgBox.SetPanelColor(null, null, null, true);
            msgBox.SetButton(Color.FromArgb(141, 129, 240), null, 10, 2);
            return msgBox.ShowDialog();
        }
        public static void Show(string title, string message, short ms)
        {
            var msgBox = new CMsgBox(title, message, false, false);
            msgBox.SetFore(Color.FromArgb(64, 64, 64), Color.FromArgb(108, 92, 231), Color.FromArgb(108, 92, 231));
            msgBox.SetPanelColor(null, null, null, true, true
                );
            msgBox.SetButton(Color.FromArgb(141, 129, 240), null, 10, 2);
            //msgBox.SetSize(null, 190);
            // 1. Configure Manual Positioning
            msgBox.StartPosition = FormStartPosition.Manual;
            int x = 0;
            // 2. Position form at bottom-right inside active window/screen area
            Form mainForm = Application.OpenForms[0]; // Reference main active form
            if (mainForm != null)
            {
                if (mainForm is frmMain f)
                    if (f.isHide)
                        x = mainForm.Right - msgBox.Width - 197;  // 20px offset from right border
                    else
                        x = mainForm.Right - msgBox.Width - 55;  // 20px offset from right border
                int y = mainForm.Bottom - msgBox.Height - 50; // 20px offset from bottom border
                msgBox.Location = new Point(x, y);
            }

            // 3. Auto-Dismiss Timer
            System.Windows.Forms.Timer clockTimer = new System.Windows.Forms.Timer();
            clockTimer.Interval = ms > 0 ? (int)ms : 3000;

            clockTimer.Tick += (s, ev) =>
            {
                clockTimer.Stop();
                clockTimer.Dispose();

                if (!msgBox.IsDisposed)
                {
                    msgBox.Close();
                }
            };

            clockTimer.Start();
            msgBox.Show();
        }
    }
}