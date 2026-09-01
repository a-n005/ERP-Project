using Acc_Trade_Core;
using Acc_Trede_winForms.Models.CMessageBox;
using Acc_Trede_winForms_Buisness.Global;
using Acc_Trede_winForms_Buisness.Validation;
using Global;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace Acc_Trede_winForms.Login
{
    public partial class ucLogin : UserControl
    {
        public event EventHandler OnLoginSuccess;
        public ucLogin()
        {
            InitializeComponent();
        }

        private void cBtn1_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren()) return;

            Result r = GlobalUser.Login(txtUsername.Texts.Trim(), txtPassword.Texts.Trim(), cbRememberMe.Checked);
            if (r.IsFailure)
                CMsgB.Show("خطا", r.Error, false);
            else
            {
                OnLoginSuccess?.Invoke(this, new EventArgs());
            }
        }

        private void ucLogin_Load(object sender, EventArgs e)
        {
            if (this.ParentForm != null)
            {
                this.ParentForm.AcceptButton = cBtn1;
            }

            txtUsername.Validating += (s, ev) =>
            {
                if (string.IsNullOrWhiteSpace(txtUsername.Texts))
                {
                    errorProvider1.SetError(txtUsername, $"خطا: يجب ادخال اسم المستخدم.");
                }
                else
                    errorProvider1.SetError(txtUsername, "");
            };
            txtPassword.Validating += (s, ev) =>
            {
                if (string.IsNullOrWhiteSpace(txtPassword.Texts))
                {
                    errorProvider1.SetError(txtPassword, $"خطا: يجب ادخال كلمة المرور.");
                }
                else
                    errorProvider1.SetError(txtPassword, "");
            };
            var (username, pass, remember) = HelperRegistre.Read();
            txtUsername.Texts = username.Trim();
            txtPassword.Texts = pass.Trim();
            cbRememberMe.Checked = remember;
        }
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                cBtn1.PerformClick();
                return true; // Marks the key as handled and suppresses the Windows system "ding" sound
            }
            return base.ProcessDialogKey(keyData);
        }

    }
}
