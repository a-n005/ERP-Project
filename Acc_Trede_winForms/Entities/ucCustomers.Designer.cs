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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucCustomers));
            this.pBottom = new System.Windows.Forms.Panel();
            this.lblRecords = new System.Windows.Forms.Label();
            this.pTop = new System.Windows.Forms.Panel();
            this.pAdd = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvCustomers = new Acc_Trede_winForms.Models.CDGV.CDGV();
            this.btnAddCustomers = new Acc_Trede_winForms.Models.CButton.CBtn();
            this.txtUsername = new Acc_Trede_winForms.Models.CTextBox();
            this.txtTaxNumber = new Acc_Trede_winForms.Models.CTextBox();
            this.txtPhone = new Acc_Trede_winForms.Models.CTextBox();
            this.btnDelete = new Acc_Trede_winForms.Models.CButton.CBtn();
            this.btnUpdate = new Acc_Trede_winForms.Models.CButton.CBtn();
            this.btnAdd = new Acc_Trede_winForms.Models.CButton.CBtn();
            this.txtSearch = new Acc_Trede_winForms.Models.CTextBox();
            this.btnRefresh = new Acc_Trede_winForms.Models.CButton.CBtn();
            this.pBottom.SuspendLayout();
            this.pTop.SuspendLayout();
            this.pAdd.SuspendLayout();
            this.SuspendLayout();
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
            this.pTop.Controls.Add(this.btnDelete);
            this.pTop.Controls.Add(this.btnRefresh);
            this.pTop.Controls.Add(this.btnUpdate);
            this.pTop.Controls.Add(this.btnAdd);
            this.pTop.Controls.Add(this.txtSearch);
            this.pTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pTop.Location = new System.Drawing.Point(0, 0);
            this.pTop.Name = "pTop";
            this.pTop.Size = new System.Drawing.Size(789, 67);
            this.pTop.TabIndex = 2;
            // 
            // pAdd
            // 
            this.pAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(46)))));
            this.pAdd.Controls.Add(this.btnAddCustomers);
            this.pAdd.Controls.Add(this.txtUsername);
            this.pAdd.Controls.Add(this.txtTaxNumber);
            this.pAdd.Controls.Add(this.txtPhone);
            this.pAdd.Controls.Add(this.label1);
            this.pAdd.Location = new System.Drawing.Point(224, 73);
            this.pAdd.Name = "pAdd";
            this.pAdd.Size = new System.Drawing.Size(322, 384);
            this.pAdd.TabIndex = 3;
            this.pAdd.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Simplified Arabic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.label1.Location = new System.Drawing.Point(112, 23);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label1.Size = new System.Drawing.Size(98, 35);
            this.label1.TabIndex = 0;
            this.label1.Text = "أضافة عميل";
            // 
            // dgvCustomers
            // 
            this.dgvCustomers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(36)))));
            this.dgvCustomers.Location = new System.Drawing.Point(139, 220);
            this.dgvCustomers.Name = "dgvCustomers";
            this.dgvCustomers.Size = new System.Drawing.Size(150, 150);
            this.dgvCustomers.TabIndex = 4;
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
            this.btnAddCustomers.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnAddCustomers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.btnAddCustomers.Icon = global::Acc_Trede_winForms.Properties.Resources.Add;
            this.btnAddCustomers.IconAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddCustomers.IconSize = new System.Drawing.Size(20, 20);
            this.btnAddCustomers.Location = new System.Drawing.Point(86, 290);
            this.btnAddCustomers.Name = "btnAddCustomers";
            this.btnAddCustomers.OnHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(129)))), ((int)(((byte)(240)))));
            this.btnAddCustomers.SelectedBorderSide = Acc_Trede_winForms.Models.CButton.CBtn.BorderSide.All;
            this.btnAddCustomers.Size = new System.Drawing.Size(150, 40);
            this.btnAddCustomers.TabIndex = 3;
            this.btnAddCustomers.Text = "     أضافة العميل";
            this.btnAddCustomers.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.btnAddCustomers.UseVisualStyleBackColor = false;
            this.btnAddCustomers.Click += new System.EventHandler(this.btnAddCustomersClick);
            // 
            // txtUsername
            // 
            this.txtUsername.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(46)))));
            this.txtUsername.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.txtUsername.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.txtUsername.BorderRadius = 10;
            this.txtUsername.BorderSize = 2;
            this.txtUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.txtUsername.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.txtUsername.IconSize = new System.Drawing.Size(20, 20);
            this.txtUsername.LeftIcon = null;
            this.txtUsername.Location = new System.Drawing.Point(36, 109);
            this.txtUsername.Margin = new System.Windows.Forms.Padding(4);
            this.txtUsername.Multiline = false;
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Padding = new System.Windows.Forms.Padding(10, 7, 36, 7);
            this.txtUsername.PasswordChar = false;
            this.txtUsername.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtUsername.PlaceholderText = "   اسم العميل";
            this.txtUsername.RightIcon = ((System.Drawing.Image)(resources.GetObject("txtUsername.RightIcon")));
            this.txtUsername.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtUsername.SelectionLength = 0;
            this.txtUsername.SelectionStart = 0;
            this.txtUsername.Size = new System.Drawing.Size(250, 31);
            this.txtUsername.TabIndex = 0;
            this.txtUsername.Texts = "";
            this.txtUsername.UnderlinedStyle = true;
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
            this.txtTaxNumber.Location = new System.Drawing.Point(36, 224);
            this.txtTaxNumber.Margin = new System.Windows.Forms.Padding(4);
            this.txtTaxNumber.Multiline = false;
            this.txtTaxNumber.Name = "txtTaxNumber";
            this.txtTaxNumber.Padding = new System.Windows.Forms.Padding(10, 7, 38, 7);
            this.txtTaxNumber.PasswordChar = false;
            this.txtTaxNumber.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtTaxNumber.PlaceholderText = "   الرقم الضريبي";
            this.txtTaxNumber.RightIcon = ((System.Drawing.Image)(resources.GetObject("txtTaxNumber.RightIcon")));
            this.txtTaxNumber.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtTaxNumber.SelectionLength = 0;
            this.txtTaxNumber.SelectionStart = 0;
            this.txtTaxNumber.Size = new System.Drawing.Size(250, 31);
            this.txtTaxNumber.TabIndex = 2;
            this.txtTaxNumber.Texts = "";
            this.txtTaxNumber.UnderlinedStyle = true;
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
            this.txtPhone.Location = new System.Drawing.Point(36, 158);
            this.txtPhone.Margin = new System.Windows.Forms.Padding(4);
            this.txtPhone.Multiline = false;
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Padding = new System.Windows.Forms.Padding(10, 7, 36, 7);
            this.txtPhone.PasswordChar = false;
            this.txtPhone.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtPhone.PlaceholderText = "   الهاتف";
            this.txtPhone.RightIcon = ((System.Drawing.Image)(resources.GetObject("txtPhone.RightIcon")));
            this.txtPhone.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtPhone.SelectionLength = 0;
            this.txtPhone.SelectionStart = 0;
            this.txtPhone.Size = new System.Drawing.Size(250, 31);
            this.txtPhone.TabIndex = 1;
            this.txtPhone.Texts = "";
            this.txtPhone.UnderlinedStyle = true;
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.BackColor = System.Drawing.Color.Transparent;
            this.btnDelete.BackgroundColor = System.Drawing.Color.Transparent;
            this.btnDelete.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.btnDelete.BorderRadius = 3;
            this.btnDelete.BorderSize = 0;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Icon = global::Acc_Trede_winForms.Properties.Resources.Delete;
            this.btnDelete.IconAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnDelete.IconSize = new System.Drawing.Size(30, 30);
            this.btnDelete.Location = new System.Drawing.Point(605, 12);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.OnHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(129)))), ((int)(((byte)(240)))));
            this.btnDelete.SelectedBorderSide = Acc_Trede_winForms.Models.CButton.CBtn.BorderSide.All;
            this.btnDelete.Size = new System.Drawing.Size(53, 40);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.TextColor = System.Drawing.Color.White;
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnAddClick);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.BackColor = System.Drawing.Color.Transparent;
            this.btnUpdate.BackgroundColor = System.Drawing.Color.Transparent;
            this.btnUpdate.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.btnUpdate.BorderRadius = 3;
            this.btnUpdate.BorderSize = 0;
            this.btnUpdate.FlatAppearance.BorderSize = 0;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.ForeColor = System.Drawing.Color.White;
            this.btnUpdate.Icon = global::Acc_Trede_winForms.Properties.Resources.Update;
            this.btnUpdate.IconAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnUpdate.IconSize = new System.Drawing.Size(30, 30);
            this.btnUpdate.Location = new System.Drawing.Point(664, 13);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.OnHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(129)))), ((int)(((byte)(240)))));
            this.btnUpdate.SelectedBorderSide = Acc_Trede_winForms.Models.CButton.CBtn.BorderSide.All;
            this.btnUpdate.Size = new System.Drawing.Size(53, 40);
            this.btnUpdate.TabIndex = 1;
            this.btnUpdate.TextColor = System.Drawing.Color.White;
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnAddClick);
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
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Icon = global::Acc_Trede_winForms.Properties.Resources.Add;
            this.btnAdd.IconAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnAdd.IconSize = new System.Drawing.Size(30, 30);
            this.btnAdd.Location = new System.Drawing.Point(721, 12);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.OnHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(129)))), ((int)(((byte)(240)))));
            this.btnAdd.SelectedBorderSide = Acc_Trede_winForms.Models.CButton.CBtn.BorderSide.All;
            this.btnAdd.Size = new System.Drawing.Size(53, 40);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.TextColor = System.Drawing.Color.White;
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAddClick);
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
            this.txtSearch.Multiline = false;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Padding = new System.Windows.Forms.Padding(10, 7, 41, 7);
            this.txtSearch.PasswordChar = false;
            this.txtSearch.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtSearch.PlaceholderText = "   بحث";
            this.txtSearch.RightIcon = ((System.Drawing.Image)(resources.GetObject("txtSearch.RightIcon")));
            this.txtSearch.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtSearch.SelectionLength = 0;
            this.txtSearch.SelectionStart = 0;
            this.txtSearch.Size = new System.Drawing.Size(250, 31);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.Texts = "";
            this.txtSearch.UnderlinedStyle = true;
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
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Icon = global::Acc_Trede_winForms.Properties.Resources.Update;
            this.btnRefresh.IconAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnRefresh.IconSize = new System.Drawing.Size(30, 30);
            this.btnRefresh.Location = new System.Drawing.Point(12, 13);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.OnHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(129)))), ((int)(((byte)(240)))));
            this.btnRefresh.SelectedBorderSide = Acc_Trede_winForms.Models.CButton.CBtn.BorderSide.All;
            this.btnRefresh.Size = new System.Drawing.Size(53, 40);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.TextColor = System.Drawing.Color.White;
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnAddClick);
            // 
            // ucCustomers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.dgvCustomers);
            this.Controls.Add(this.pAdd);
            this.Controls.Add(this.pTop);
            this.Controls.Add(this.pBottom);
            this.Name = "ucCustomers";
            this.Size = new System.Drawing.Size(789, 511);
            this.Load += new System.EventHandler(this.ucCustomers_Load);
            this.pBottom.ResumeLayout(false);
            this.pBottom.PerformLayout();
            this.pTop.ResumeLayout(false);
            this.pAdd.ResumeLayout(false);
            this.pAdd.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pBottom;
        private System.Windows.Forms.Panel pTop;
        private System.Windows.Forms.Panel pAdd;
        private System.Windows.Forms.Label label1;
        private Models.CTextBox txtUsername;
        private Models.CTextBox txtPhone;
        private Models.CTextBox txtTaxNumber;
        private Models.CButton.CBtn btnAddCustomers;
        private Models.CDGV.CDGV dgvCustomers;
        private System.Windows.Forms.Label lblRecords;
        private Models.CButton.CBtn btnAdd;
        private Models.CButton.CBtn btnDelete;
        private Models.CButton.CBtn btnUpdate;
        private Models.CTextBox txtSearch;
        private Models.CButton.CBtn btnRefresh;
    }
}
