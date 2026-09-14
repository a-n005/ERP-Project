using Acc_Trade_Core;
using Acc_Trede_winForms.Entities;
using Acc_Trede_winForms.Models;
using Acc_Trede_winForms.Models.CButton;
using Acc_Trede_winForms.Models.cPanel;
using Acc_Trede_winForms.Properties;
using Acc_Trede_winForms_Buisness.Global;
using Acc_Trede_winForms_Buisness.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Acc_Trede_winForms
{
    public partial class frmMain : Form
    {
        #region Constructors & Properties
        public frmMain()
        {
            InitializeComponent();
        }
        public bool isHide { get => pList.Size.Width == 193 ? true : false; }
        #endregion

        private void Login_Load(object sender, EventArgs e)
        {
            p1InLoad();
            pListInLoad();

            ShowLoginControl();


        }

        #region Work Screen Panel
        private void ShowLoginControl()
        {
            pScreen.Size = new Size(800, 427);

            foreach (Control ctrl in pScreen.Controls)
                ctrl.Dispose();

            pScreen.Controls.Clear();

            var _ucLogin = new Acc_Trede_winForms.Login.ucLogin();

            _ucLogin.Dock = DockStyle.Fill;

            _ucLogin.OnLoginSuccess += UcLogin_OnLoginSuccess;

            pScreen.Controls.Add(_ucLogin);

            this.ActiveControl = _ucLogin;

            this.BeginInvoke(new Action(() => { _ucLogin.ResetFocus(); }));

        }
        private void UcLogin_OnLoginSuccess(object sender, EventArgs e)
        {
            btnMaximized.Visible = true;
            btnMinimized.Visible = true;
            btnLogout.Visible = true;
            // 1. Remove login control from panel
            pScreen.Controls.Clear();

            // 2. Maximize the main form
            btnMaximized.PerformClick();

            // 3. Resize/Adjust container panel to fit full screen dimensions
            pScreen.Dock = DockStyle.Fill; // Automatically expands to cover screen space

            // make perform click on sales
        }
        #endregion

        #region Top Panel
        private void p1InLoad()
        {
            lblTime.Text = DateTime.Now.ToString("yyyy-MM-dd  hh:mm:ss tt");
            Timer clockTimer = new Timer();
            clockTimer.Interval = 1000; // Update every 1 second (1000 ms)
            clockTimer.Tick += (s, ev) =>
            {
                lblTime.Text = DateTime.Now.ToString("yyyy-MM-dd  hh:mm:ss tt");
            };
            clockTimer.Start();

            panel1.Paint += (s, ev) => p_Paint(panel1, ev, null, 2, false, true);
        }
        private void btnLogout_Click(object sender, EventArgs e)
        {
            btnMinimized.Visible = false;
            btnMaximized.Visible = false;
            btnLogout.Visible = false;
            GlobalUser.LogOut();
            this.WindowState = FormWindowState.Normal;
            pScreen.Dock = DockStyle.None;
            pScreen.Tag = "";
            ShowLoginControl();
        }
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void btnMaximized_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
                btnMaximized.Icon = Resources.Restore_Down;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
                btnMaximized.Icon = Resources.Maximize_Button;
            }
            pScreen.Focus();
        }
        private void btnMinimized_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            pScreen.Focus();
        }
        private void cBtn1_Click(object sender, EventArgs e)
        {
            btnLogout.PerformClick();
            Application.Exit();
        }
        #endregion

        #region List Panel
        private void pListInLoad()
        {
            pList.Paint += (s, ev) => p_Paint(pList, ev, null, 2, false, false, true);
        }
        private void btnCustomers_Click(object sender, EventArgs e)
        {
            ShowScreen(new ucCustomers(), "customers");
        }
        private void btnHide_Click(object sender, EventArgs e)
        {
            if (pList.Size.Width == 193)
            {
                pList.Size = new Size(49, this.Size.Height);

                foreach (Control item in pList.Controls)
                    if (item is CBtn btn)
                    {
                        if (btn.Tag == null && !string.IsNullOrEmpty(btn.Text))
                            btn.Tag = btn.Text;

                        btn.Text = string.Empty;
                    }

                btnHide.Icon = Resources.arrow_to_left;
            }
            else
            {
                pList.Size = new Size(193, this.Size.Height);
                btnHide.Icon = Resources.arrow_to_right;

                foreach (Control item in pList.Controls)
                    if (item is CBtn btn && btn.Tag != null)
                        btn.Text = btn.Tag.ToString();
            }
            pScreen.Focus();
        }
        #endregion

        #region Helpers

        //private bool FocusControlRecursive(Control container, string controlName)
        //{
        //    foreach (Control ctrl in container.Controls)
        //    {
        //        if (ctrl.Name == controlName)
        //        {
        //            ctrl.Focus();
        //            if (ctrl is TextBox txt) txt.SelectAll();
        //            return true;
        //        }

        //        if (ctrl.HasChildren)
        //        {
        //            if (FocusControlRecursive(ctrl, controlName))
        //                return true;
        //        }
        //    }
        //    return false;
        //}

        private void p_Paint(Panel panel, PaintEventArgs e, Color? color = null, int lineThickness = 2, bool t = false, bool b = false, bool l = false, bool r = false)
        {
            Color lineColor = color ?? Color.FromArgb(108, 92, 231);

            // Enable smooth rendering
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            using (Pen pen = new Pen(lineColor, lineThickness))
            {
                int width = panel.ClientSize.Width;
                int height = panel.ClientSize.Height;
                if (t)
                    // Line across the top edge
                    e.Graphics.DrawLine(pen, 0, 0, width, 0);
                if (r)
                    // Line down the right edge
                    e.Graphics.DrawLine(pen, width - 1, 0, width - 1, height);
                if (b)
                    // Line across the bottom edge
                    e.Graphics.DrawLine(pen, 0, height - 1, width, height - 1);
                if (l)
                    // Line down the left edge
                    e.Graphics.DrawLine(pen, 0, 0, 0, height);

            }
        }
        private void ShowScreen(UserControl newScreen, string tag)
        {
            if (pScreen.Tag?.ToString() == tag)
            {
                pScreen.Focus();
                return;
            }
            // To discharge or dispose memory, use it when you don't want to save the screen in the background
            //foreach(Control ctrl in pScreen.Controls) 
            //    ctrl.Dispose();

            pScreen.Controls.Clear();
            pScreen.Tag = tag;
            newScreen.Dock = DockStyle.Fill;

            pScreen.Controls.Add(newScreen);
            newScreen.BringToFront();

            this.ActiveControl = newScreen;

            this.BeginInvoke(new Action(() =>
            {

                Control firstFocusable = GetFirstFocusableControl(newScreen);

                if (firstFocusable != null)
                {
                    firstFocusable.Focus();
                }
                else
                {
                    newScreen.Focus();
                }
            }));
        }

        private Control GetFirstFocusableControl(Control parent)
        {
            Control ctrl = parent.GetNextControl(parent, true);

            while (ctrl != null && (!ctrl.Visible || !ctrl.CanFocus || !ctrl.TabStop))
            {
                ctrl = parent.GetNextControl(ctrl, true);
            }

            return ctrl;
        }

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;
        #endregion
    }
}
