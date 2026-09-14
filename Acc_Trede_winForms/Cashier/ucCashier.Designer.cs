namespace Acc_Trede_winForms.Cashier
{
    partial class ucCashier
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucCashier));
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.dgvCart = new Acc_Trede_winForms.Models.CDGV.CDGV();
            this.pDetails = new Acc_Trede_winForms.Models.cPanel.cPanel();
            this.txtDiscount = new Acc_Trede_winForms.Models.CTextBox();
            this.lblNetTotassl = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTax = new System.Windows.Forms.Label();
            this.lblNetTotal = new System.Windows.Forms.Label();
            this.pTop = new Acc_Trede_winForms.Models.cPanel.cPanel();
            this.txtSearch = new Acc_Trede_winForms.Models.cSuggestTextBox.CTxtSuggest();
            this.cToggleSwitch1 = new Acc_Trede_winForms.Models.cToggleSwitch.cToggleSwitch();
            this.btnReturn = new Acc_Trede_winForms.Models.CButton.CBtn();
            this.btnAdd = new Acc_Trede_winForms.Models.CButton.CBtn();
            this.toolTip1 = new Acc_Trede_winForms.Models.CToolTip.CToolTip();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.pDetails.SuspendLayout();
            this.pTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            this.errorProvider1.Icon = ((System.Drawing.Icon)(resources.GetObject("errorProvider1.Icon")));
            this.errorProvider1.RightToLeft = true;
            // 
            // dgvCart
            // 
            this.dgvCart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(36)))));
            this.dgvCart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCart.Location = new System.Drawing.Point(200, 50);
            this.dgvCart.Name = "dgvCart";
            this.dgvCart.Size = new System.Drawing.Size(600, 377);
            this.dgvCart.TabIndex = 2;
            // 
            // pDetails
            // 
            this.pDetails.Controls.Add(this.txtDiscount);
            this.pDetails.Controls.Add(this.lblNetTotassl);
            this.pDetails.Controls.Add(this.label5);
            this.pDetails.Controls.Add(this.label2);
            this.pDetails.Controls.Add(this.lblTotal);
            this.pDetails.Controls.Add(this.label1);
            this.pDetails.Controls.Add(this.lblTax);
            this.pDetails.Controls.Add(this.lblNetTotal);
            this.pDetails.Dock = System.Windows.Forms.DockStyle.Left;
            this.pDetails.IstBackgroundDisabled = false;
            this.pDetails.Location = new System.Drawing.Point(0, 50);
            this.pDetails.Name = "pDetails";
            this.pDetails.Size = new System.Drawing.Size(200, 377);
            this.pDetails.TabIndex = 3;
            // 
            // txtDiscount
            // 
            this.txtDiscount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDiscount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(46)))));
            this.txtDiscount.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.txtDiscount.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.txtDiscount.BorderRadius = 10;
            this.txtDiscount.BorderSize = 2;
            this.txtDiscount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.txtDiscount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.txtDiscount.IconSize = new System.Drawing.Size(25, 25);
            this.txtDiscount.LeftIcon = null;
            this.txtDiscount.Location = new System.Drawing.Point(27, 286);
            this.txtDiscount.Margin = new System.Windows.Forms.Padding(4);
            this.txtDiscount.MaxLength = 32767;
            this.txtDiscount.Multiline = false;
            this.txtDiscount.Name = "txtDiscount";
            this.txtDiscount.Padding = new System.Windows.Forms.Padding(10, 7, 41, 7);
            this.txtDiscount.PasswordChar = false;
            this.txtDiscount.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtDiscount.PlaceholderText = "الخصم ...";
            this.txtDiscount.ReadOnly = false;
            this.txtDiscount.RightIcon = null;
            this.txtDiscount.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtDiscount.SelectedText = "";
            this.txtDiscount.SelectionLength = 0;
            this.txtDiscount.SelectionStart = 0;
            this.txtDiscount.Size = new System.Drawing.Size(110, 31);
            this.txtDiscount.TabIndex = 1;
            this.txtDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.txtDiscount.UnderlinedStyle = true;
            // 
            // lblNetTotassl
            // 
            this.lblNetTotassl.AutoSize = true;
            this.lblNetTotassl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNetTotassl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.lblNetTotassl.Location = new System.Drawing.Point(133, 343);
            this.lblNetTotassl.Name = "lblNetTotassl";
            this.lblNetTotassl.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblNetTotassl.Size = new System.Drawing.Size(55, 20);
            this.lblNetTotassl.TabIndex = 0;
            this.lblNetTotassl.Text = "الصافي :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.label5.Location = new System.Drawing.Point(133, 259);
            this.label5.Name = "label5";
            this.label5.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label5.Size = new System.Drawing.Size(51, 20);
            this.label5.TabIndex = 0;
            this.label5.Text = "الإجمالي";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.label2.Location = new System.Drawing.Point(133, 323);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label2.Size = new System.Drawing.Size(61, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "الضريبة :";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.lblTotal.Location = new System.Drawing.Point(54, 259);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(40, 20);
            this.lblTotal.TabIndex = 0;
            this.lblTotal.Text = "0.00";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.label1.Location = new System.Drawing.Point(141, 297);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label1.Size = new System.Drawing.Size(51, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "الخصم :";
            // 
            // lblTax
            // 
            this.lblTax.AutoSize = true;
            this.lblTax.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTax.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.lblTax.Location = new System.Drawing.Point(54, 323);
            this.lblTax.Name = "lblTax";
            this.lblTax.Size = new System.Drawing.Size(40, 20);
            this.lblTax.TabIndex = 0;
            this.lblTax.Text = "0.00";
            // 
            // lblNetTotal
            // 
            this.lblNetTotal.AutoSize = true;
            this.lblNetTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNetTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.lblNetTotal.Location = new System.Drawing.Point(54, 343);
            this.lblNetTotal.Name = "lblNetTotal";
            this.lblNetTotal.Size = new System.Drawing.Size(40, 20);
            this.lblNetTotal.TabIndex = 0;
            this.lblNetTotal.Text = "0.00";
            // 
            // pTop
            // 
            this.pTop.Controls.Add(this.txtSearch);
            this.pTop.Controls.Add(this.cToggleSwitch1);
            this.pTop.Controls.Add(this.btnReturn);
            this.pTop.Controls.Add(this.btnAdd);
            this.pTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pTop.IstBackgroundDisabled = false;
            this.pTop.Location = new System.Drawing.Point(0, 0);
            this.pTop.Name = "pTop";
            this.pTop.Size = new System.Drawing.Size(800, 50);
            this.pTop.TabIndex = 0;
            // 
            // txtSearch
            // 
            this.txtSearch.BackColor = System.Drawing.Color.Transparent;
            this.txtSearch.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.txtSearch.BorderFocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.txtSearch.BorderRadius = 10;
            this.txtSearch.BorderSize = 2;
            this.txtSearch.DropDownWidth = 0;
            this.txtSearch.FilterFunc = null;
            this.txtSearch.IconSize = new System.Drawing.Size(20, 20);
            this.txtSearch.ItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(46)))));
            this.txtSearch.ItemColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(46)))));
            this.txtSearch.ItemHeight = 42;
            this.txtSearch.ItemHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(129)))), ((int)(((byte)(240)))));
            this.txtSearch.ItemRadius = 0;
            this.txtSearch.ItemSubTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(101)))), ((int)(((byte)(255)))));
            this.txtSearch.ItemTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.txtSearch.LeftIcon = null;
            this.txtSearch.Location = new System.Drawing.Point(402, 12);
            this.txtSearch.MaxVisibleItems = 6;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.PanelBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(46)))));
            this.txtSearch.PanelBorderColor = System.Drawing.Color.MediumSlateBlue;
            this.txtSearch.PlaceholderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtSearch.PlaceholderText = "بحث...";
            this.txtSearch.RightIcon = null;
            this.txtSearch.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtSearch.Size = new System.Drawing.Size(250, 31);
            this.txtSearch.TabIndex = 3;
            this.txtSearch.TxtBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(46)))));
            this.txtSearch.UnderlinedStyle = true;
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
            this.cToggleSwitch1.Location = new System.Drawing.Point(242, 5);
            this.cToggleSwitch1.Name = "cToggleSwitch1";
            this.cToggleSwitch1.OffColor = System.Drawing.Color.Transparent;
            this.cToggleSwitch1.OffImage = global::Acc_Trede_winForms.Properties.Resources.eye_Show;
            this.cToggleSwitch1.OnColor = System.Drawing.Color.Transparent;
            this.cToggleSwitch1.OnImage = global::Acc_Trede_winForms.Properties.Resources.Hide;
            this.cToggleSwitch1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cToggleSwitch1.Size = new System.Drawing.Size(65, 41);
            this.cToggleSwitch1.Spacing = 0;
            this.cToggleSwitch1.TabIndex = 5;
            this.cToggleSwitch1.TitleTextOff = "";
            this.cToggleSwitch1.TitleTextOn = "";
            this.cToggleSwitch1.ToggleSwitchSize = new System.Drawing.Size(60, 26);
            // 
            // btnReturn
            // 
            this.btnReturn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReturn.BackColor = System.Drawing.Color.Transparent;
            this.btnReturn.BackgroundColor = System.Drawing.Color.Transparent;
            this.btnReturn.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.btnReturn.BorderRadius = 3;
            this.btnReturn.BorderSize = 0;
            this.btnReturn.FlatAppearance.BorderSize = 0;
            this.btnReturn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReturn.FocusBorderSize = 1;
            this.btnReturn.FocusColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.btnReturn.ForeColor = System.Drawing.Color.White;
            this.btnReturn.Icon = global::Acc_Trede_winForms.Properties.Resources.return_cart;
            this.btnReturn.IconAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnReturn.IconSize = new System.Drawing.Size(30, 30);
            this.btnReturn.Location = new System.Drawing.Point(680, 3);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.OnHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(129)))), ((int)(((byte)(240)))));
            this.btnReturn.SelectedBorderSide = Acc_Trede_winForms.Models.CButton.CBtn.BorderSide.All;
            this.btnReturn.ShowFocusBorder = true;
            this.btnReturn.Size = new System.Drawing.Size(53, 40);
            this.btnReturn.TabIndex = 4;
            this.btnReturn.TextColor = System.Drawing.Color.White;
            this.btnReturn.UseVisualStyleBackColor = false;
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
            this.btnAdd.Icon = global::Acc_Trede_winForms.Properties.Resources.add_cart;
            this.btnAdd.IconAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnAdd.IconSize = new System.Drawing.Size(30, 30);
            this.btnAdd.Location = new System.Drawing.Point(739, 4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.OnHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(129)))), ((int)(((byte)(240)))));
            this.btnAdd.SelectedBorderSide = Acc_Trede_winForms.Models.CButton.CBtn.BorderSide.All;
            this.btnAdd.ShowFocusBorder = true;
            this.btnAdd.Size = new System.Drawing.Size(53, 40);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.TextColor = System.Drawing.Color.White;
            this.btnAdd.UseVisualStyleBackColor = false;
            // 
            // toolTip1
            // 
            this.toolTip1.AutoRightToLeft = true;
            this.toolTip1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(46)))));
            this.toolTip1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.toolTip1.BorderRadius = 2;
            this.toolTip1.BorderSize = 1;
            this.toolTip1.ContentPadding = new System.Windows.Forms.Padding(10, 6, 10, 6);
            this.toolTip1.DefaultIcon = global::Acc_Trede_winForms.Properties.Resources.cercle_wrning;
            this.toolTip1.IconAlignment = Acc_Trede_winForms.Models.CToolTip.ToolTipIconAlign.Right;
            this.toolTip1.IconSize = new System.Drawing.Size(16, 16);
            this.toolTip1.IconTextSpacing = 6;
            this.toolTip1.OwnerDraw = true;
            this.toolTip1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(92)))), ((int)(((byte)(231)))));
            this.toolTip1.TooltipFont = new System.Drawing.Font("Segoe UI", 9F);
            // 
            // ucCashier
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(46)))));
            this.Controls.Add(this.dgvCart);
            this.Controls.Add(this.pDetails);
            this.Controls.Add(this.pTop);
            this.Name = "ucCashier";
            this.Size = new System.Drawing.Size(800, 427);
            this.Load += new System.EventHandler(this.ucCashier_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.pDetails.ResumeLayout(false);
            this.pDetails.PerformLayout();
            this.pTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Models.cPanel.cPanel pTop;
        private Models.CDGV.CDGV dgvCart;
        private Models.cToggleSwitch.cToggleSwitch cToggleSwitch1;
        private Models.CButton.CBtn btnReturn;
        private Models.CButton.CBtn btnAdd;
        private Models.cSuggestTextBox.CTxtSuggest txtSearch;
        private Models.cPanel.cPanel pDetails;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private Models.CToolTip.CToolTip toolTip1;
        private System.Windows.Forms.Label lblNetTotal;
        private System.Windows.Forms.Label lblTax;
        private Models.CTextBox txtDiscount;
        private System.Windows.Forms.Label lblNetTotassl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblTotal;
    }
}
