namespace Acc_Trede_winForms
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTime = new System.Windows.Forms.Label();
            this.pList = new System.Windows.Forms.Panel();
            this.cBtn7 = new Acc_Trede_winForms.Models.CButton.CBtn();
            this.cBtn6 = new Acc_Trede_winForms.Models.CButton.CBtn();
            this.cBtn5 = new Acc_Trede_winForms.Models.CButton.CBtn();
            this.cBtn4 = new Acc_Trede_winForms.Models.CButton.CBtn();
            this.pScreen = new System.Windows.Forms.Panel();
            this.btnCustomers = new Acc_Trede_winForms.Models.CButton.CBtn();
            this.cBtn2 = new Acc_Trede_winForms.Models.CButton.CBtn();
            this.btnHide = new Acc_Trede_winForms.Models.CButton.CBtn();
            this.btnLogout = new Acc_Trede_winForms.Models.CButton.CBtn();
            this.btnMinimized = new Acc_Trede_winForms.Models.CButton.CBtn();
            this.btnMaximized = new Acc_Trede_winForms.Models.CButton.CBtn();
            this.cBtn1 = new Acc_Trede_winForms.Models.CButton.CBtn();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.panel1.SuspendLayout();
            this.pList.SuspendLayout();
            this.SuspendLayout();
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            this.errorProvider1.RightToLeft = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(33)))));
            this.panel1.Controls.Add(this.btnLogout);
            this.panel1.Controls.Add(this.btnMinimized);
            this.panel1.Controls.Add(this.btnMaximized);
            this.panel1.Controls.Add(this.cBtn1);
            this.panel1.Controls.Add(this.lblTime);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 23);
            this.panel1.TabIndex = 8;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.lblTime.Location = new System.Drawing.Point(13, 4);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(101, 16);
            this.lblTime.TabIndex = 0;
            this.lblTime.Text = "date and time";
            // 
            // pList
            // 
            this.pList.BackColor = System.Drawing.Color.Transparent;
            this.pList.Controls.Add(this.cBtn7);
            this.pList.Controls.Add(this.cBtn6);
            this.pList.Controls.Add(this.cBtn5);
            this.pList.Controls.Add(this.cBtn4);
            this.pList.Controls.Add(this.btnCustomers);
            this.pList.Controls.Add(this.cBtn2);
            this.pList.Controls.Add(this.btnHide);
            this.pList.Dock = System.Windows.Forms.DockStyle.Right;
            this.pList.Location = new System.Drawing.Point(607, 23);
            this.pList.Name = "pList";
            this.pList.Size = new System.Drawing.Size(193, 427);
            this.pList.TabIndex = 10;
            // 
            // cBtn7
            // 
            this.cBtn7.BackColor = System.Drawing.Color.Transparent;
            this.cBtn7.BackgroundColor = System.Drawing.Color.Transparent;
            this.cBtn7.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.cBtn7.BorderRadius = 3;
            this.cBtn7.BorderSize = 2;
            this.cBtn7.Dock = System.Windows.Forms.DockStyle.Top;
            this.cBtn7.FlatAppearance.BorderSize = 0;
            this.cBtn7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cBtn7.FocusBorderSize = 1;
            this.cBtn7.FocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.cBtn7.Font = new System.Drawing.Font("Simplified Arabic", 15.75F, System.Drawing.FontStyle.Bold);
            this.cBtn7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.cBtn7.Icon = null;
            this.cBtn7.IconAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cBtn7.IconSize = new System.Drawing.Size(30, 30);
            this.cBtn7.Location = new System.Drawing.Point(0, 252);
            this.cBtn7.Name = "cBtn7";
            this.cBtn7.OnHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(129)))), ((int)(((byte)(240)))));
            this.cBtn7.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cBtn7.SelectedBorderSide = Acc_Trede_winForms.Models.CButton.CBtn.BorderSide.Bottom;
            this.cBtn7.ShowFocusBorder = true;
            this.cBtn7.Size = new System.Drawing.Size(193, 42);
            this.cBtn7.TabIndex = 6;
            this.cBtn7.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.cBtn7.UseVisualStyleBackColor = false;
            // 
            // cBtn6
            // 
            this.cBtn6.BackColor = System.Drawing.Color.Transparent;
            this.cBtn6.BackgroundColor = System.Drawing.Color.Transparent;
            this.cBtn6.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.cBtn6.BorderRadius = 3;
            this.cBtn6.BorderSize = 2;
            this.cBtn6.Dock = System.Windows.Forms.DockStyle.Top;
            this.cBtn6.FlatAppearance.BorderSize = 0;
            this.cBtn6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cBtn6.FocusBorderSize = 1;
            this.cBtn6.FocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.cBtn6.Font = new System.Drawing.Font("Simplified Arabic", 15.75F, System.Drawing.FontStyle.Bold);
            this.cBtn6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.cBtn6.Icon = null;
            this.cBtn6.IconAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cBtn6.IconSize = new System.Drawing.Size(30, 30);
            this.cBtn6.Location = new System.Drawing.Point(0, 210);
            this.cBtn6.Name = "cBtn6";
            this.cBtn6.OnHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(129)))), ((int)(((byte)(240)))));
            this.cBtn6.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cBtn6.SelectedBorderSide = Acc_Trede_winForms.Models.CButton.CBtn.BorderSide.Bottom;
            this.cBtn6.ShowFocusBorder = true;
            this.cBtn6.Size = new System.Drawing.Size(193, 42);
            this.cBtn6.TabIndex = 5;
            this.cBtn6.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.cBtn6.UseVisualStyleBackColor = false;
            // 
            // cBtn5
            // 
            this.cBtn5.BackColor = System.Drawing.Color.Transparent;
            this.cBtn5.BackgroundColor = System.Drawing.Color.Transparent;
            this.cBtn5.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.cBtn5.BorderRadius = 3;
            this.cBtn5.BorderSize = 2;
            this.cBtn5.Dock = System.Windows.Forms.DockStyle.Top;
            this.cBtn5.FlatAppearance.BorderSize = 0;
            this.cBtn5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cBtn5.FocusBorderSize = 1;
            this.cBtn5.FocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.cBtn5.Font = new System.Drawing.Font("Simplified Arabic", 15.75F, System.Drawing.FontStyle.Bold);
            this.cBtn5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.cBtn5.Icon = null;
            this.cBtn5.IconAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cBtn5.IconSize = new System.Drawing.Size(30, 30);
            this.cBtn5.Location = new System.Drawing.Point(0, 168);
            this.cBtn5.Name = "cBtn5";
            this.cBtn5.OnHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(129)))), ((int)(((byte)(240)))));
            this.cBtn5.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cBtn5.SelectedBorderSide = Acc_Trede_winForms.Models.CButton.CBtn.BorderSide.Bottom;
            this.cBtn5.ShowFocusBorder = true;
            this.cBtn5.Size = new System.Drawing.Size(193, 42);
            this.cBtn5.TabIndex = 4;
            this.cBtn5.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.cBtn5.UseVisualStyleBackColor = false;
            // 
            // cBtn4
            // 
            this.cBtn4.BackColor = System.Drawing.Color.Transparent;
            this.cBtn4.BackgroundColor = System.Drawing.Color.Transparent;
            this.cBtn4.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.cBtn4.BorderRadius = 3;
            this.cBtn4.BorderSize = 2;
            this.cBtn4.Dock = System.Windows.Forms.DockStyle.Top;
            this.cBtn4.FlatAppearance.BorderSize = 0;
            this.cBtn4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cBtn4.FocusBorderSize = 1;
            this.cBtn4.FocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.cBtn4.Font = new System.Drawing.Font("Simplified Arabic", 15.75F, System.Drawing.FontStyle.Bold);
            this.cBtn4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.cBtn4.Icon = null;
            this.cBtn4.IconAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cBtn4.IconSize = new System.Drawing.Size(30, 30);
            this.cBtn4.Location = new System.Drawing.Point(0, 126);
            this.cBtn4.Name = "cBtn4";
            this.cBtn4.OnHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(129)))), ((int)(((byte)(240)))));
            this.cBtn4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cBtn4.SelectedBorderSide = Acc_Trede_winForms.Models.CButton.CBtn.BorderSide.Bottom;
            this.cBtn4.ShowFocusBorder = true;
            this.cBtn4.Size = new System.Drawing.Size(193, 42);
            this.cBtn4.TabIndex = 3;
            this.cBtn4.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.cBtn4.UseVisualStyleBackColor = false;
            // 
            // pScreen
            // 
            this.pScreen.BackColor = System.Drawing.Color.Transparent;
            this.pScreen.Location = new System.Drawing.Point(0, 23);
            this.pScreen.Name = "pScreen";
            this.pScreen.Size = new System.Drawing.Size(557, 427);
            this.pScreen.TabIndex = 7;
            // 
            // btnCustomers
            // 
            this.btnCustomers.BackColor = System.Drawing.Color.Transparent;
            this.btnCustomers.BackgroundColor = System.Drawing.Color.Transparent;
            this.btnCustomers.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.btnCustomers.BorderRadius = 3;
            this.btnCustomers.BorderSize = 2;
            this.btnCustomers.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCustomers.FlatAppearance.BorderSize = 0;
            this.btnCustomers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCustomers.FocusBorderSize = 1;
            this.btnCustomers.FocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.btnCustomers.Font = new System.Drawing.Font("Simplified Arabic", 15.75F, System.Drawing.FontStyle.Bold);
            this.btnCustomers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.btnCustomers.Icon = global::Acc_Trede_winForms.Properties.Resources.People;
            this.btnCustomers.IconAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCustomers.IconSize = new System.Drawing.Size(30, 30);
            this.btnCustomers.Location = new System.Drawing.Point(0, 84);
            this.btnCustomers.Name = "btnCustomers";
            this.btnCustomers.OnHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(129)))), ((int)(((byte)(240)))));
            this.btnCustomers.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnCustomers.SelectedBorderSide = Acc_Trede_winForms.Models.CButton.CBtn.BorderSide.Bottom;
            this.btnCustomers.ShowFocusBorder = true;
            this.btnCustomers.Size = new System.Drawing.Size(193, 42);
            this.btnCustomers.TabIndex = 2;
            this.btnCustomers.Text = "العملاء";
            this.btnCustomers.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.btnCustomers.UseVisualStyleBackColor = false;
            this.btnCustomers.Click += new System.EventHandler(this.btnCustomers_Click);
            // 
            // cBtn2
            // 
            this.cBtn2.BackColor = System.Drawing.Color.Transparent;
            this.cBtn2.BackgroundColor = System.Drawing.Color.Transparent;
            this.cBtn2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.cBtn2.BorderRadius = 3;
            this.cBtn2.BorderSize = 2;
            this.cBtn2.Dock = System.Windows.Forms.DockStyle.Top;
            this.cBtn2.FlatAppearance.BorderSize = 0;
            this.cBtn2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cBtn2.FocusBorderSize = 1;
            this.cBtn2.FocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.cBtn2.Font = new System.Drawing.Font("Simplified Arabic", 15.75F, System.Drawing.FontStyle.Bold);
            this.cBtn2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.cBtn2.Icon = global::Acc_Trede_winForms.Properties.Resources.Cashier_Machine;
            this.cBtn2.IconAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cBtn2.IconSize = new System.Drawing.Size(30, 30);
            this.cBtn2.Location = new System.Drawing.Point(0, 42);
            this.cBtn2.Name = "cBtn2";
            this.cBtn2.OnHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(129)))), ((int)(((byte)(240)))));
            this.cBtn2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cBtn2.SelectedBorderSide = Acc_Trede_winForms.Models.CButton.CBtn.BorderSide.Bottom;
            this.cBtn2.ShowFocusBorder = true;
            this.cBtn2.Size = new System.Drawing.Size(193, 42);
            this.cBtn2.TabIndex = 1;
            this.cBtn2.Text = "الكاشير";
            this.cBtn2.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.cBtn2.UseVisualStyleBackColor = false;
            // 
            // btnHide
            // 
            this.btnHide.BackColor = System.Drawing.Color.Transparent;
            this.btnHide.BackgroundColor = System.Drawing.Color.Transparent;
            this.btnHide.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.btnHide.BorderRadius = 3;
            this.btnHide.BorderSize = 2;
            this.btnHide.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnHide.FlatAppearance.BorderSize = 0;
            this.btnHide.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHide.FocusBorderSize = 1;
            this.btnHide.FocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.btnHide.Font = new System.Drawing.Font("Simplified Arabic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHide.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.btnHide.Icon = global::Acc_Trede_winForms.Properties.Resources.arrow_to_right;
            this.btnHide.IconAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHide.IconSize = new System.Drawing.Size(30, 30);
            this.btnHide.Location = new System.Drawing.Point(0, 0);
            this.btnHide.Name = "btnHide";
            this.btnHide.OnHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(129)))), ((int)(((byte)(240)))));
            this.btnHide.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnHide.SelectedBorderSide = Acc_Trede_winForms.Models.CButton.CBtn.BorderSide.Bottom;
            this.btnHide.ShowFocusBorder = true;
            this.btnHide.Size = new System.Drawing.Size(193, 42);
            this.btnHide.TabIndex = 0;
            this.btnHide.Text = "اخفاء";
            this.btnHide.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.btnHide.UseVisualStyleBackColor = false;
            this.btnHide.Click += new System.EventHandler(this.btnHide_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLogout.BackColor = System.Drawing.Color.Transparent;
            this.btnLogout.BackgroundColor = System.Drawing.Color.Transparent;
            this.btnLogout.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.btnLogout.BorderRadius = 3;
            this.btnLogout.BorderSize = 0;
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.FocusBorderSize = 1;
            this.btnLogout.FocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.Icon = global::Acc_Trede_winForms.Properties.Resources.Logout;
            this.btnLogout.IconAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnLogout.IconSize = new System.Drawing.Size(15, 15);
            this.btnLogout.Location = new System.Drawing.Point(654, 1);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.OnHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(129)))), ((int)(((byte)(240)))));
            this.btnLogout.SelectedBorderSide = Acc_Trede_winForms.Models.CButton.CBtn.BorderSide.All;
            this.btnLogout.ShowFocusBorder = true;
            this.btnLogout.Size = new System.Drawing.Size(24, 19);
            this.btnLogout.TabIndex = 1;
            this.btnLogout.TabStop = false;
            this.btnLogout.TextColor = System.Drawing.Color.White;
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Visible = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnMinimized
            // 
            this.btnMinimized.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMinimized.BackColor = System.Drawing.Color.Transparent;
            this.btnMinimized.BackgroundColor = System.Drawing.Color.Transparent;
            this.btnMinimized.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.btnMinimized.BorderRadius = 3;
            this.btnMinimized.BorderSize = 0;
            this.btnMinimized.FlatAppearance.BorderSize = 0;
            this.btnMinimized.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimized.FocusBorderSize = 1;
            this.btnMinimized.FocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.btnMinimized.ForeColor = System.Drawing.Color.White;
            this.btnMinimized.Icon = global::Acc_Trede_winForms.Properties.Resources.Minimized;
            this.btnMinimized.IconAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnMinimized.IconSize = new System.Drawing.Size(15, 15);
            this.btnMinimized.Location = new System.Drawing.Point(713, 2);
            this.btnMinimized.Name = "btnMinimized";
            this.btnMinimized.OnHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(129)))), ((int)(((byte)(240)))));
            this.btnMinimized.SelectedBorderSide = Acc_Trede_winForms.Models.CButton.CBtn.BorderSide.All;
            this.btnMinimized.ShowFocusBorder = true;
            this.btnMinimized.Size = new System.Drawing.Size(24, 19);
            this.btnMinimized.TabIndex = 1;
            this.btnMinimized.TabStop = false;
            this.btnMinimized.TextColor = System.Drawing.Color.White;
            this.btnMinimized.UseVisualStyleBackColor = false;
            this.btnMinimized.Visible = false;
            this.btnMinimized.Click += new System.EventHandler(this.btnMinimized_Click);
            // 
            // btnMaximized
            // 
            this.btnMaximized.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMaximized.BackColor = System.Drawing.Color.Transparent;
            this.btnMaximized.BackgroundColor = System.Drawing.Color.Transparent;
            this.btnMaximized.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.btnMaximized.BorderRadius = 3;
            this.btnMaximized.BorderSize = 0;
            this.btnMaximized.FlatAppearance.BorderSize = 0;
            this.btnMaximized.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMaximized.FocusBorderSize = 1;
            this.btnMaximized.FocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.btnMaximized.ForeColor = System.Drawing.Color.White;
            this.btnMaximized.Icon = global::Acc_Trede_winForms.Properties.Resources.Restore_Down;
            this.btnMaximized.IconAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnMaximized.IconSize = new System.Drawing.Size(15, 15);
            this.btnMaximized.Location = new System.Drawing.Point(743, 2);
            this.btnMaximized.Name = "btnMaximized";
            this.btnMaximized.OnHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(129)))), ((int)(((byte)(240)))));
            this.btnMaximized.SelectedBorderSide = Acc_Trede_winForms.Models.CButton.CBtn.BorderSide.All;
            this.btnMaximized.ShowFocusBorder = true;
            this.btnMaximized.Size = new System.Drawing.Size(24, 19);
            this.btnMaximized.TabIndex = 1;
            this.btnMaximized.TabStop = false;
            this.btnMaximized.TextColor = System.Drawing.Color.White;
            this.btnMaximized.UseVisualStyleBackColor = false;
            this.btnMaximized.Visible = false;
            this.btnMaximized.Click += new System.EventHandler(this.btnMaximized_Click);
            // 
            // cBtn1
            // 
            this.cBtn1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cBtn1.BackColor = System.Drawing.Color.Transparent;
            this.cBtn1.BackgroundColor = System.Drawing.Color.Transparent;
            this.cBtn1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.cBtn1.BorderRadius = 3;
            this.cBtn1.BorderSize = 0;
            this.cBtn1.FlatAppearance.BorderSize = 0;
            this.cBtn1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cBtn1.FocusBorderSize = 1;
            this.cBtn1.FocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.cBtn1.ForeColor = System.Drawing.Color.White;
            this.cBtn1.Icon = global::Acc_Trede_winForms.Properties.Resources.Close;
            this.cBtn1.IconAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.cBtn1.IconSize = new System.Drawing.Size(15, 15);
            this.cBtn1.Location = new System.Drawing.Point(773, 2);
            this.cBtn1.Name = "cBtn1";
            this.cBtn1.OnHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(129)))), ((int)(((byte)(240)))));
            this.cBtn1.SelectedBorderSide = Acc_Trede_winForms.Models.CButton.CBtn.BorderSide.All;
            this.cBtn1.ShowFocusBorder = true;
            this.cBtn1.Size = new System.Drawing.Size(24, 19);
            this.cBtn1.TabIndex = 1;
            this.cBtn1.TabStop = false;
            this.cBtn1.TextColor = System.Drawing.Color.White;
            this.cBtn1.UseVisualStyleBackColor = false;
            this.cBtn1.Click += new System.EventHandler(this.cBtn1_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(46)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pScreen);
            this.Controls.Add(this.pList);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(800, 450);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.Load += new System.EventHandler(this.Login_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pList.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Panel pList;
        private Models.CButton.CBtn btnHide;
        private Models.CButton.CBtn cBtn1;
        private System.Windows.Forms.Panel pScreen;
        private Models.CButton.CBtn btnLogout;
        private Models.CButton.CBtn btnMinimized;
        private Models.CButton.CBtn btnMaximized;
        private Models.CButton.CBtn cBtn7;
        private Models.CButton.CBtn cBtn6;
        private Models.CButton.CBtn cBtn5;
        private Models.CButton.CBtn cBtn4;
        private Models.CButton.CBtn btnCustomers;
        private Models.CButton.CBtn cBtn2;
    }
}