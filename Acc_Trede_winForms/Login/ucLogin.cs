using Acc_Trade_Core;
using Acc_Trede_winForms.Models.CMessageBox;
using Acc_Trede_winForms_Buisness.Global;
using Acc_Trede_winForms_Buisness.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
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

            Result r = GlobalUser.Login(txtUsername.Text.Trim(), txtPassword.Text.Trim(), cbRememberMe.Checked);
            if (r.IsFailure)
                CMsgB.Show("خطا", r.Error, false);
            else
            {
                OnLoginSuccess?.Invoke(this, new EventArgs());
            }
        }

        private void ucLogin_Load(object sender, EventArgs e)
        {
            ResetFocus();
            cbRememberMe.KeyPress += (s, ev) =>
            {
                if (ev.KeyChar == (char)Keys.Enter)
                    cbRememberMe.Checked = !cbRememberMe.Checked;
            };

            txtUsername.Validating += (s, ev) =>
            {
                if (string.IsNullOrWhiteSpace(txtUsername.Text))
                {
                    errorProvider1.SetError(txtUsername, $"خطا: يجب ادخال اسم المستخدم.");
                }
                else
                    errorProvider1.SetError(txtUsername, "");
            };

            txtPassword.Validating += (s, ev) =>
            {
                if (string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    errorProvider1.SetError(txtPassword, $"خطا: يجب ادخال كلمة المرور.");
                }
                else
                    errorProvider1.SetError(txtPassword, "");
            };

            var (username, pass, remember) = Helper.Read();
            txtUsername.Text = username.Trim();
            txtPassword.Text = pass.Trim();
            cbRememberMe.Checked = remember;
            if( cbRememberMe.Checked ) cBtn1.Focus();
        }

        public void ResetFocus()
        {
            txtUsername.Focus();
            txtUsername.SelectAll();
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                // التأكد من أن التركيز الحالي هو إما زر الدخول أو أحد صناديق الإدخال داخل هذه الواجهة
                if (cBtn1.Focused || txtUsername.Focused || txtPassword.Focused || cbRememberMe.Focused)
                {
                    cBtn1.PerformClick();
                    return true; // منع انتقال الحدث للنموذج الرئيسي أو الشاشات الأخرى
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}