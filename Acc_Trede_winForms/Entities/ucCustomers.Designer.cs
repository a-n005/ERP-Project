namespace Acc_Trede_winForms.Entities
{
    partial class ucCustomers
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucCustomers));
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.cPanel1 = new Acc_Trede_winForms.Models.cPanel.cPanel();
            this.pAdd = new Acc_Trede_winForms.Models.cPanel.cPanel();
            this.lblTitel = new System.Windows.Forms.Label();
            this.cbActive = new Acc_Trede_winForms.Models.CCB();
            this.txtPhone = new Acc_Trede_winForms.Models.CTextBox();
            this.txtTaxNumber = new Acc_Trede_winForms.Models.CTextBox();
            this.txtCustomerName = new Acc_Trede_winForms.Models.CTextBox();
            this.dgvCustomers = new Acc_Trede_winForms.Models.CDGV.CDGV();
            this.pBottom = new System.Windows.Forms.Panel();
            this.lblRecords = new System.Windows.Forms.Label();
            this.pTop = new System.Windows.Forms.Panel();
            this.txtSearch = new Acc_Trede_winForms.Models.CTextBox();
            this.btnX = new Acc_Trede_winForms.Models.CButton.CBtn();
            this.btnAddCustomers = new Acc_Trede_winForms.Models.CButton.CBtn();
            this.cToggleSwitch1 = new Acc_Trede_winForms.Models.cToggleSwitch.cToggleSwitch();
            this.btnRefresh = new Acc_Trede_winForms.Models.CButton.CBtn();
            this.btnAdd = new Acc_Trede_winForms.Models.CButton.CBtn();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.cPanel1.SuspendLayout();
            this.pAdd.SuspendLayout();
            this.pBottom.SuspendLayout();
            this.pTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            this.errorProvider1.Icon = ((System.Drawing.Icon)(resources.GetObject("errorProvider1.Icon")));
            this.errorProvider1.RightToLeft = true;
            // 
            // cPanel1
            // 
            this.cPanel1.Controls.Add(this.pAdd);
            this.cPanel1.Controls.Add(this.dgvCustomers);
            this.cPanel1.Controls.Add(this.pBottom);
            this.cPanel1.Controls.Add(this.pTop);
            this.cPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cPanel1.Location = new System.Drawing.Point(0, 0);
            this.cPanel1.Name = "cPanel1";
            this.cPanel1.Size = new System.Drawing.Size(789, 511);
            this.cPanel1.TabIndex = 6;
            // 
            // pAdd
            // 
            this.pAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(46)))));
            this.pAdd.Controls.Add(this.btnX);
            this.pAdd.Controls.Add(this.lblTitel);
            this.pAdd.Controls.Add(this.cbActive);
            this.pAdd.Controls.Add(this.txtPhone);
            this.pAdd.Controls.Add(this.btnAddCustomers);
            this.pAdd.Controls.Add(this.txtTaxNumber);
            this.pAdd.Controls.Add(this.txtCustomerName);
            this.pAdd.Location = new System.Drawing.Point(224, 73);
            this.pAdd.Name = "pAdd";
            this.pAdd.Size = new System.Drawing.Size(322, 384);
            this.pAdd.TabIndex = 5;
            this.pAdd.Visible = false;
            // 
            // lblTitel
            // 
            this.lblTitel.AutoSize = true;
            this.lblTitel.BackColor = System.Drawing.Color.Transparent;
            this.lblTitel.Font = new System.Drawing.Font("Simplified Arabic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.lblTitel.Location = new System.Drawing.Point(112, 39);
            this.lblTitel.Name = "lblTitel";
            this.lblTitel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblTitel.Size = new System.Drawing.Size(98, 35);
            this.lblTitel.TabIndex = 0;
            this.lblTitel.Text = "أضافة عميل";
            // 
            // cbActive
            // 
            this.cbActive.BoxBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(46)))));
            this.cbActive.BoxSize = 18;
            this.cbActive.CheckedColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.cbActive.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbActive.FocusBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.cbActive.FocusBorderRadius = 6;
            this.cbActive.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.cbActive.ForeColor = System.Drawing.Color.White;
            this.cbActive.Location = new System.Drawing.Point(175, 276);
            this.cbActive.MinimumSize = new System.Drawing.Size(28, 24);
            this.cbActive.Name = "cbActive";
            this.cbActive.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cbActive.Size = new System.Drawing.Size(111, 24);
            this.cbActive.TabIndex = 3;
            this.cbActive.Text = "تنشيط العميل ";
            this.cbActive.UncheckedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.cbActive.UseVisualStyleBackColor = true;
            this.cbActive.Visible = false;
            // 
            // txtPhone
            // 
            this.txtPhone.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(46)))));
            this.txtPhone.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.txtPhone.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.txtPhone.BorderRadius = 10;
            this.txtPhone.BorderSize = 2;
            this.txtPhone.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.txtPhone.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.txtPhone.IconSize = new System.Drawing.Size(20, 20);
            this.txtPhone.LeftIcon = null;
            this.txtPhone.Location = new System.Drawing.Point(36, 178);
            this.txtPhone.Margin = new System.Windows.Forms.Padding(4);
            this.txtPhone.MaxLength = 32767;
            this.txtPhone.Multiline = false;
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Padding = new System.Windows.Forms.Padding(10, 7, 36, 7);
            this.txtPhone.PasswordChar = false;
            this.txtPhone.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtPhone.PlaceholderText = "   الهاتف";
            this.txtPhone.ReadOnly = false;
            this.txtPhone.RightIcon = null;
            this.txtPhone.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtPhone.SelectedText = "";
            this.txtPhone.SelectionLength = 0;
            this.txtPhone.SelectionStart = 0;
            this.txtPhone.Size = new System.Drawing.Size(250, 31);
            this.txtPhone.TabIndex = 1;
            this.txtPhone.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtPhone.UnderlinedStyle = true;
            // 
            // txtTaxNumber
            // 
            this.txtTaxNumber.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(46)))));
            this.txtTaxNumber.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.txtTaxNumber.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.txtTaxNumber.BorderRadius = 10;
            this.txtTaxNumber.BorderSize = 2;
            this.txtTaxNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.txtTaxNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.txtTaxNumber.IconSize = new System.Drawing.Size(22, 22);
            this.txtTaxNumber.LeftIcon = null;
            this.txtTaxNumber.Location = new System.Drawing.Point(36, 231);
            this.txtTaxNumber.Margin = new System.Windows.Forms.Padding(4);
            this.txtTaxNumber.MaxLength = 32767;
            this.txtTaxNumber.Multiline = false;
            this.txtTaxNumber.Name = "txtTaxNumber";
            this.txtTaxNumber.Padding = new System.Windows.Forms.Padding(10, 7, 38, 7);
            this.txtTaxNumber.PasswordChar = false;
            this.txtTaxNumber.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtTaxNumber.PlaceholderText = "   الرقم الضريبي";
            this.txtTaxNumber.ReadOnly = false;
            this.txtTaxNumber.RightIcon = null;
            this.txtTaxNumber.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtTaxNumber.SelectedText = "";
            this.txtTaxNumber.SelectionLength = 0;
            this.txtTaxNumber.SelectionStart = 0;
            this.txtTaxNumber.Size = new System.Drawing.Size(250, 31);
            this.txtTaxNumber.TabIndex = 2;
            this.txtTaxNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtTaxNumber.UnderlinedStyle = true;
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(46)))));
            this.txtCustomerName.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.txtCustomerName.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.txtCustomerName.BorderRadius = 10;
            this.txtCustomerName.BorderSize = 2;
            this.txtCustomerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.txtCustomerName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.txtCustomerName.IconSize = new System.Drawing.Size(20, 20);
            this.txtCustomerName.LeftIcon = null;
            this.txtCustomerName.Location = new System.Drawing.Point(36, 125);
            this.txtCustomerName.Margin = new System.Windows.Forms.Padding(4);
            this.txtCustomerName.MaxLength = 32767;
            this.txtCustomerName.Multiline = false;
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.Padding = new System.Windows.Forms.Padding(10, 7, 36, 7);
            this.txtCustomerName.PasswordChar = false;
            this.txtCustomerName.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtCustomerName.PlaceholderText = "   اسم العميل";
            this.txtCustomerName.ReadOnly = false;
            this.txtCustomerName.RightIcon = null;
            this.txtCustomerName.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtCustomerName.SelectedText = "";
            this.txtCustomerName.SelectionLength = 0;
            this.txtCustomerName.SelectionStart = 0;
            this.txtCustomerName.Size = new System.Drawing.Size(250, 31);
            this.txtCustomerName.TabIndex = 0;
            this.txtCustomerName.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtCustomerName.UnderlinedStyle = true;
            // 
            // dgvCustomers
            // 
            this.dgvCustomers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(36)))));
            this.dgvCustomers.Location = new System.Drawing.Point(139, 128);
            this.dgvCustomers.Name = "dgvCustomers";
            this.dgvCustomers.Size = new System.Drawing.Size(150, 274);
            this.dgvCustomers.TabIndex = 4;
            this.dgvCustomers.TabStop = false;
            // 
            // pBottom
            // 
            this.pBottom.BackColor = System.Drawing.Color.Transparent;
            this.pBottom.Controls.Add(this.lblRecords);
            this.pBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pBottom.Location = new System.Drawing.Point(0, 483);
            this.pBottom.Name = "pBottom";
            this.pBottom.Size = new System.Drawing.Size(789, 28);
            this.pBottom.TabIndex = 1;
            // 
            // lblRecords
            // 
            this.lblRecords.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRecords.AutoSize = true;
            this.lblRecords.BackColor = System.Drawing.Color.Transparent;
            this.lblRecords.Font = new System.Drawing.Font("Simplified Arabic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecords.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.lblRecords.Location = new System.Drawing.Point(719, 0);
            this.lblRecords.Name = "lblRecords";
            this.lblRecords.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblRecords.Size = new System.Drawing.Size(66, 28);
            this.lblRecords.TabIndex = 0;
            this.lblRecords.Text = "records";
            // 
            // pTop
            // 
            this.pTop.BackColor = System.Drawing.Color.Transparent;
            this.pTop.Controls.Add(this.cToggleSwitch1);
            this.pTop.Controls.Add(this.btnRefresh);
            this.pTop.Controls.Add(this.btnAdd);
            this.pTop.Controls.Add(this.txtSearch);
            this.pTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pTop.Location = new System.Drawing.Point(0, 0);
            this.pTop.Name = "pTop";
            this.pTop.Size = new System.Drawing.Size(789, 67);
            this.pTop.TabIndex = 2;
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(46)))));
            this.txtSearch.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.txtSearch.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.txtSearch.BorderRadius = 10;
            this.txtSearch.BorderSize = 2;
            this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.txtSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.txtSearch.IconSize = new System.Drawing.Size(25, 25);
            this.txtSearch.LeftIcon = null;
            this.txtSearch.Location = new System.Drawing.Point(296, 21);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(4);
            this.txtSearch.MaxLength = 32767;
            this.txtSearch.Multiline = false;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Padding = new System.Windows.Forms.Padding(10, 7, 41, 7);
            this.txtSearch.PasswordChar = false;
            this.txtSearch.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtSearch.PlaceholderText = "   بحث";
            this.txtSearch.ReadOnly = false;
            this.txtSearch.RightIcon = null;
            this.txtSearch.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtSearch.SelectedText = "";
            this.txtSearch.SelectionLength = 0;
            this.txtSearch.SelectionStart = 0;
            this.txtSearch.Size = new System.Drawing.Size(250, 31);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtSearch.UnderlinedStyle = true;
            this.txtSearch._TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // btnX
            // 
            this.btnX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnX.BackColor = System.Drawing.Color.Transparent;
            this.btnX.BackgroundColor = System.Drawing.Color.Transparent;
            this.btnX.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.btnX.BorderRadius = 3;
            this.btnX.BorderSize = 0;
            this.btnX.FlatAppearance.BorderSize = 0;
            this.btnX.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnX.FocusBorderSize = 1;
            this.btnX.FocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.btnX.ForeColor = System.Drawing.Color.White;
            this.btnX.Icon = global::Acc_Trede_winForms.Properties.Resources.Close;
            this.btnX.IconAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnX.IconSize = new System.Drawing.Size(15, 15);
            this.btnX.Location = new System.Drawing.Point(292, 6);
            this.btnX.Name = "btnX";
            this.btnX.OnHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(129)))), ((int)(((byte)(240)))));
            this.btnX.SelectedBorderSide = Acc_Trede_winForms.Models.CButton.CBtn.BorderSide.All;
            this.btnX.ShowFocusBorder = true;
            this.btnX.Size = new System.Drawing.Size(24, 19);
            this.btnX.TabIndex = 5;
            this.btnX.TextColor = System.Drawing.Color.White;
            this.btnX.UseVisualStyleBackColor = false;
            this.btnX.Click += new System.EventHandler(this.btnX_Click);
            // 
            // btnAddCustomers
            // 
            this.btnAddCustomers.BackColor = System.Drawing.Color.Transparent;
            this.btnAddCustomers.BackgroundColor = System.Drawing.Color.Transparent;
            this.btnAddCustomers.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.btnAddCustomers.BorderRadius = 10;
            this.btnAddCustomers.BorderSize = 2;
            this.btnAddCustomers.FlatAppearance.BorderSize = 0;
            this.btnAddCustomers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddCustomers.FocusBorderSize = 1;
            this.btnAddCustomers.FocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.btnAddCustomers.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnAddCustomers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.btnAddCustomers.Icon = global::Acc_Trede_winForms.Properties.Resources.Add;
            this.btnAddCustomers.IconAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddCustomers.IconSize = new System.Drawing.Size(20, 20);
            this.btnAddCustomers.Location = new System.Drawing.Point(86, 306);
            this.btnAddCustomers.Name = "btnAddCustomers";
            this.btnAddCustomers.OnHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(129)))), ((int)(((byte)(240)))));
            this.btnAddCustomers.SelectedBorderSide = Acc_Trede_winForms.Models.CButton.CBtn.BorderSide.All;
            this.btnAddCustomers.ShowFocusBorder = true;
            this.btnAddCustomers.Size = new System.Drawing.Size(150, 40);
            this.btnAddCustomers.TabIndex = 4;
            this.btnAddCustomers.Text = "     أضافة العميل";
            this.btnAddCustomers.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.btnAddCustomers.UseVisualStyleBackColor = false;
            this.btnAddCustomers.Click += new System.EventHandler(this.btnAddCustomersClick);
            // 
            // cToggleSwitch1
            // 
            this.cToggleSwitch1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cToggleSwitch1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.cToggleSwitch1.BorderSize = 1;
            this.cToggleSwitch1.Checked = true;
            this.cToggleSwitch1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cToggleSwitch1.IconSize = new System.Drawing.Size(20, 20);
            this.cToggleSwitch1.InsideTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.cToggleSwitch1.InsideTextFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.cToggleSwitch1.InsideTextOff = "إظهار";
            this.cToggleSwitch1.InsideTextOn = "إخفاء";
            this.cToggleSwitch1.KnobColor = System.Drawing.Color.Transparent;
            this.cToggleSwitch1.LabelFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cToggleSwitch1.LabelForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.cToggleSwitch1.LabelPosition = Acc_Trede_winForms.Models.cToggleSwitch.TextPosition.Left;
            this.cToggleSwitch1.Location = new System.Drawing.Point(224, 13);
            this.cToggleSwitch1.Name = "cToggleSwitch1";
            this.cToggleSwitch1.OffColor = System.Drawing.Color.Transparent;
            this.cToggleSwitch1.OffImage = global::Acc_Trede_winForms.Properties.Resources.eye_Show;
            this.cToggleSwitch1.OnColor = System.Drawing.Color.Transparent;
            this.cToggleSwitch1.OnImage = global::Acc_Trede_winForms.Properties.Resources.Hide;
            this.cToggleSwitch1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cToggleSwitch1.Size = new System.Drawing.Size(65, 41);
            this.cToggleSwitch1.Spacing = 0;
            this.cToggleSwitch1.TabIndex = 2;
            this.cToggleSwitch1.TitleTextOff = "";
            this.cToggleSwitch1.TitleTextOn = "";
            this.cToggleSwitch1.ToggleSwitchSize = new System.Drawing.Size(60, 26);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.Transparent;
            this.btnRefresh.BackgroundColor = System.Drawing.Color.Transparent;
            this.btnRefresh.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.btnRefresh.BorderRadius = 3;
            this.btnRefresh.BorderSize = 0;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.FocusBorderSize = 1;
            this.btnRefresh.FocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Icon = global::Acc_Trede_winForms.Properties.Resources.Refresh;
            this.btnRefresh.IconAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnRefresh.IconSize = new System.Drawing.Size(30, 30);
            this.btnRefresh.Location = new System.Drawing.Point(12, 13);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.OnHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(129)))), ((int)(((byte)(240)))));
            this.btnRefresh.SelectedBorderSide = Acc_Trede_winForms.Models.CButton.CBtn.BorderSide.All;
            this.btnRefresh.ShowFocusBorder = true;
            this.btnRefresh.Size = new System.Drawing.Size(53, 40);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.TextColor = System.Drawing.Color.White;
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.BackColor = System.Drawing.Color.Transparent;
            this.btnAdd.BackgroundColor = System.Drawing.Color.Transparent;
            this.btnAdd.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.btnAdd.BorderRadius = 3;
            this.btnAdd.BorderSize = 0;
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.FocusBorderSize = 1;
            this.btnAdd.FocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Icon = global::Acc_Trede_winForms.Properties.Resources.Add;
            this.btnAdd.IconAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnAdd.IconSize = new System.Drawing.Size(30, 30);
            this.btnAdd.Location = new System.Drawing.Point(721, 12);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.OnHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(129)))), ((int)(((byte)(240)))));
            this.btnAdd.SelectedBorderSide = Acc_Trede_winForms.Models.CButton.CBtn.BorderSide.All;
            this.btnAdd.ShowFocusBorder = true;
            this.btnAdd.Size = new System.Drawing.Size(53, 40);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.TextColor = System.Drawing.Color.White;
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAddClick);
            // 
            // ucCustomers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.cPanel1);
            this.Name = "ucCustomers";
            this.Size = new System.Drawing.Size(789, 511);
            this.Load += new System.EventHandler(this.ucCustomers_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.cPanel1.ResumeLayout(false);
            this.pAdd.ResumeLayout(false);
            this.pAdd.PerformLayout();
            this.pBottom.ResumeLayout(false);
            this.pBottom.PerformLayout();
            this.pTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pBottom;
        private System.Windows.Forms.Panel pTop;
        private System.Windows.Forms.Label lblTitel;
        private Models.CTextBox txtCustomerName;
        private Models.CTextBox txtPhone;
        private Models.CTextBox txtTaxNumber;
        private Models.CButton.CBtn btnAddCustomers;
        private Models.CDGV.CDGV dgvCustomers;
        private System.Windows.Forms.Label lblRecords;
        private Models.CButton.CBtn btnAdd;
        private Models.CTextBox txtSearch;
        private Models.CButton.CBtn btnRefresh;
        private Models.CCB cbActive;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private Models.CButton.CBtn btnX;
        private Models.cPanel.cPanel pAdd;
        private Models.cPanel.cPanel cPanel1;
        private Models.cToggleSwitch.cToggleSwitch cToggleSwitch1;
    }
}
