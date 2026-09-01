using System.Drawing;
using System.Windows.Forms;

namespace Acc_Trede_winForms.Models.CMessageBox
{
    public static class CMsgB
    {
        public static DialogResult Show(string title, string message, bool showCancelButton = true)
        {
            var msgBox=new CMsgBox(title, message, showCancelButton);
            msgBox.SetFore(Color.FromArgb(64, 64, 64), Color.FromArgb(108, 92, 231), Color.FromArgb(108, 92, 231));
            msgBox.SetPanelColor(null,null,null,true);
            msgBox.SetButton(Color.FromArgb(141, 129, 240), null, 10, 2);
            return msgBox.ShowDialog();
        }
    }
}