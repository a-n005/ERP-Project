using Acc_Trede_winForms.Models.CButton;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Acc_Trede_winForms.Models.CMessageBox
{
    public class CMsgBox : Form
    {
        #region Fields
        private Label lblTitle;
        private Label lblMessage;
        private CBtn btnOk;
        private CBtn btnCancel;
        private Panel bottomPanel;
        private Panel topBorder;
        private Panel bottomBorder;

        //private Size _formSize= new Size(380, 200);
        // Form Corner Radius
        private int _formBorderRadius = 15;

        // Color Fields
        private Color _topBorderColor = Color.FromArgb(108, 92, 231);
        private Color _bottomBorderColor = Color.FromArgb(108, 92, 231);
        private Color _titleColor = Color.FromArgb(108, 92, 231);
        private Color _messageColor = Color.White;
        private Color _bottomPanelColor = Color.FromArgb(30, 30, 46);
        private float _MFontSize = 12;
        private float _TFontSize = 12;

        // OK Button Properties Backing Fields
        private Color _btnOkColor = Color.FromArgb(30, 30, 46);
        private Color _btnOkBorderColor = Color.FromArgb(30, 30, 46);
        private int _btnOkBorderRadius = 15;
        private int _btnOkBorderSize = 2;
        private Color _btnOkOnHoverColor = Color.FromArgb(108, 110, 231);
        private Color _btnOkTextColor = Color.White;
        private Image _btnOkIcon = null;
        private ContentAlignment _btnOkIconAlignment = ContentAlignment.MiddleLeft;
        private Size _btnOkIconSize = new Size(20, 20);
        private float _btnOFontSize = 12;

        // Cancel Button Properties Backing Fields
        private Color _btnCancelColor = Color.FromArgb(30, 30, 46);
        private Color _btnCancelBorderColor = Color.FromArgb(30, 30, 46);
        private int _btnCancelBorderRadius = 15;
        private int _btnCancelBorderSize = 2;
        private Color _btnCancelOnHoverColor = Color.FromArgb(108, 110, 231);
        private Color _btnCancelTextColor = Color.White;
        private Image _btnCancelIcon = null;
        private ContentAlignment _btnCancelIconAlignment = ContentAlignment.MiddleLeft;
        private Size _btnCancelIconSize = new Size(20, 20);
        private float _btnCFontSize = 12;
        #endregion
        #region Native Imports for Smooth Rounding

        [DllImport("gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );

        [DllImport("gdi32.dll", EntryPoint = "DeleteObject")]
        private static extern bool DeleteObject(IntPtr hObject);

        #endregion

        #region Custom Properties

        [Category("Custom Appearance")]
        public int FormBorderRadius
        {
            get => _formBorderRadius;
            set
            {
                _formBorderRadius = value;
                ApplyRegion();
                this.Invalidate();
            }
        }

        [Category("Custom Appearance")]
        public Color TopBorderColor
        {
            get => _topBorderColor;
            set { _topBorderColor = value; if (_topBorderColor != null) topBorder.BackColor = value; this.Invalidate(); }
        }
        [Category("Custom Appearance")]
        public Color BottomBorderColor
        {
            get => _bottomBorderColor;
            set { _bottomBorderColor = value; if (_bottomBorderColor != null) bottomBorder.BackColor = value; this.Invalidate(); }
        }

        [Category("Custom Appearance")]
        public Color TitleColor
        {
            get => _titleColor;
            set { _titleColor = value; if (lblTitle != null) lblTitle.ForeColor = value; }
        }


        [Category("Custom Appearance")]
        public Color MessageColor
        {
            get => _messageColor;
            set { _messageColor = value; if (lblMessage != null) lblMessage.ForeColor = value; }
        }

        [Category("Custom Appearance")]
        public Color BottomPanelColor
        {
            get => _bottomPanelColor;
            set { _bottomPanelColor = value; if (bottomPanel != null) bottomPanel.BackColor = value; }
        }

        [Category("Custom Appearance")]
        public Color CardBackColor
        {
            get => this.BackColor;
            set => this.BackColor = value;
        }

        // OK Button Exposed Properties
        [Category("OK Button")]
        public Color BtnOkColor
        {
            get => _btnOkColor;
            set { _btnOkColor = value; if (btnOk != null) btnOk.BackgroundColor = value; }
        }

        [Category("OK Button")]
        public Color BtnOkBorderColor
        {
            get => _btnOkBorderColor;
            set { _btnOkBorderColor = value; if (btnOk != null) btnOk.BorderColor = value; }
        }

        [Category("OK Button")]
        public int BtnOkBorderRadius
        {
            get => _btnOkBorderRadius;
            set { _btnOkBorderRadius = value; if (btnOk != null) btnOk.BorderRadius = value; }
        }

        [Category("OK Button")]
        public int BtnOkBorderSize
        {
            get => _btnOkBorderSize;
            set { _btnOkBorderSize = value; if (btnOk != null) btnOk.BorderSize = value; }
        }

        [Category("OK Button")]
        public Color BtnOkOnHoverColor
        {
            get => _btnOkOnHoverColor;
            set { _btnOkOnHoverColor = value; if (btnOk != null) btnOk.OnHoverColor = value; }
        }

        [Category("OK Button")]
        public Color BtnOkTextColor
        {
            get => _btnOkTextColor;
            set { _btnOkTextColor = value; if (btnOk != null) btnOk.TextColor = value; }
        }

        [Category("OK Button")]
        public Image BtnOkIcon
        {
            get => _btnOkIcon;
            set { _btnOkIcon = value; if (btnOk != null) btnOk.Icon = value; }
        }

        [Category("OK Button")]
        public ContentAlignment BtnOkIconAlignment
        {
            get => _btnOkIconAlignment;
            set { _btnOkIconAlignment = value; if (btnOk != null) btnOk.IconAlignment = value; }
        }

        [Category("OK Button")]
        public Size BtnOkIconSize
        {
            get => _btnOkIconSize;
            set { _btnOkIconSize = value; if (btnOk != null) btnOk.IconSize = value; }
        }

        // Cancel Button Exposed Properties
        [Category("Cancel Button")]
        public Color BtnCancelColor
        {
            get => _btnCancelColor;
            set { _btnCancelColor = value; if (btnCancel != null) btnCancel.BackgroundColor = value; }
        }

        [Category("Cancel Button")]
        public Color BtnCancelBorderColor
        {
            get => _btnCancelBorderColor;
            set { _btnCancelBorderColor = value; if (btnCancel != null) btnCancel.BorderColor = value; }
        }

        [Category("Cancel Button")]
        public int BtnCancelBorderRadius
        {
            get => _btnCancelBorderRadius;
            set { _btnCancelBorderRadius = value; if (btnCancel != null) btnCancel.BorderRadius = value; }
        }

        [Category("Cancel Button")]
        public int BtnCancelBorderSize
        {
            get => _btnCancelBorderSize;
            set { _btnCancelBorderSize = value; if (btnCancel != null) btnCancel.BorderSize = value; }
        }

        [Category("Cancel Button")]
        public Color BtnCancelOnHoverColor
        {
            get => _btnCancelOnHoverColor;
            set { _btnCancelOnHoverColor = value; if (btnCancel != null) btnCancel.OnHoverColor = value; }
        }

        [Category("Cancel Button")]
        public Color BtnCancelTextColor
        {
            get => _btnCancelTextColor;
            set { _btnCancelTextColor = value; if (btnCancel != null) btnCancel.TextColor = value; }
        }

        [Category("Cancel Button")]
        public Image BtnCancelIcon
        {
            get => _btnCancelIcon;
            set { _btnCancelIcon = value; if (btnCancel != null) btnCancel.Icon = value; }
        }

        [Category("Cancel Button")]
        public ContentAlignment BtnCancelIconAlignment
        {
            get => _btnCancelIconAlignment;
            set { _btnCancelIconAlignment = value; if (btnCancel != null) btnCancel.IconAlignment = value; }
        }

        [Category("Cancel Button")]
        public Size BtnCancelIconSize
        {
            get => _btnCancelIconSize;
            set { _btnCancelIconSize = value; if (btnCancel != null) btnCancel.IconSize = value; }
        }

        #endregion

        public CMsgBox(string title, string message, bool showCancel = true, bool showOk = true)
        {
            InitializeComponent();

            lblTitle.Text = title;
            lblMessage.Text = message;
            btnCancel.Visible = showCancel;
            btnOk.Visible = showOk;
            if (!showCancel)
            {
                btnOk.Location = new Point((bottomPanel.Width - btnOk.Width) / 2, (bottomPanel.Height - btnOk.Height) / 2);
            }
        }
        public void PerformClik()
        {
            btnOk.PerformClick();
        }
        public void SetPanelColor(Color? pColor, Color? bottomColor, Color? card, bool inTop = false, bool inBottom = false)
        {
            this.TitleColor = pColor ?? Color.FromArgb(108, 92, 231);
            this.BtnCancelBorderColor = pColor ?? Color.FromArgb(108, 92, 231);
            this.BtnOkBorderColor = pColor ?? Color.FromArgb(108, 92, 231);

            this.BottomPanelColor = bottomColor ?? Color.FromArgb(30, 30, 46);
            this.BtnOkColor = bottomColor ?? Color.FromArgb(30, 30, 46);
            this.BtnCancelColor = bottomColor ?? Color.FromArgb(30, 30, 46);

            this.CardBackColor = card ?? Color.FromArgb(30, 30, 46);

            if (inTop)
                this.TopBorderColor = pColor ?? Color.FromArgb(108, 92, 231);
            else
                this.BottomBorderColor = Color.Transparent;
            if (inBottom)
                this.BottomBorderColor = pColor ?? Color.FromArgb(108, 92, 231);
            else
                this.TopBorderColor = Color.Transparent;

        }
        public void SetButton(Image iconOk, Image iconCancel, ContentAlignment? alignment = null, Size? sizeOk = null, Size? sizeCancel = null)
        {
            BtnCancelIconSize = sizeCancel ?? Size.Empty;
            BtnOkIconSize = sizeOk ?? Size.Empty;
            BtnCancelIconAlignment = alignment ?? ContentAlignment.MiddleCenter;
            BtnOkIconAlignment = alignment ?? ContentAlignment.MiddleCenter;
            BtnOkIcon = iconOk;
            BtnCancelIcon = iconCancel;
        }
        public void SetButton(Color? onHover, Color? btnColor, int? radius, int? borderSize)
        {
            this.BtnCancelOnHoverColor = onHover ?? Color.FromArgb(108, 110, 231);
            this.BtnOkOnHoverColor = onHover ?? Color.FromArgb(108, 110, 231);

            BtnOkBorderRadius = radius ?? 0;
            BtnCancelBorderRadius = radius ?? 0;
            BtnCancelBorderSize = borderSize ?? 0;
            BtnOkBorderSize = borderSize ?? 0;
            BtnOkColor = btnColor ?? Color.Transparent;
            BtnCancelColor = btnColor ?? Color.Transparent;
        }
        public void SetFore(Color? msgForeColor, Color? btnForeColor, Color? titleForeColor, float? msgSize = 12, float? btnSize = 12, float? titleSize = 12)
        {
            this.MessageColor = msgForeColor ?? Color.White;

            this.BtnOkTextColor = btnForeColor ?? Color.White;
            this.BtnCancelTextColor = btnForeColor ?? Color.White;
            this.TitleColor = titleForeColor ?? Color.White;

            this.lblTitle.Font = new Font("Segoe UI", titleSize ?? 12f, FontStyle.Bold);
            this.lblMessage.Font = new Font("Segoe UI", msgSize ?? 12f, FontStyle.Bold);
            this.btnCancel.Font = new Font("Segoe UI", btnSize ?? 12f, FontStyle.Bold);
            this.btnOk.Font = new Font("Segoe UI", btnSize ?? 12f, FontStyle.Bold);
        }
        public void SetSize(short? y, short? h)
        {
            this.Size = new Size(h ?? 380, y ?? 200);
        }
        private void InitializeComponent()
        {
            this.Size = new Size(380, 200);
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(30, 30, 46);

            topBorder = new Panel
            {
                Dock = DockStyle.Top,
                Height = 4,
                BackColor = _topBorderColor
            };
            bottomBorder = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 4,
                BackColor = _bottomBorderColor
            };
            lblTitle = new Label
            {
                Dock = DockStyle.Top,
                Height = 44,
                ForeColor = _topBorderColor,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Padding = new Padding(0, 4, 0, 0)
            };

            bottomPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 55,
                BackColor = _bottomPanelColor
            };

            // Setup OK CBtn
            btnOk = new CBtn
            {
                Text = "OK",
                DialogResult = DialogResult.OK,
                Size = new Size(90, 32),
                Location = new Point(85, 10),
                BackgroundColor = _bottomPanelColor,
                BorderColor = _topBorderColor,
                BorderRadius = _btnOkBorderRadius,
                BorderSize = _btnOkBorderSize,
                OnHoverColor = _btnOkOnHoverColor,
                TextColor = _btnOkTextColor,
                Icon = _btnOkIcon,
                IconAlignment = _btnOkIconAlignment,
                IconSize = _btnOkIconSize,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };

            // Setup Cancel CBtn
            btnCancel = new CBtn
            {
                Text = "Cancel",
                DialogResult = DialogResult.Cancel,
                Size = new Size(90, 32),
                Location = new Point(195, 10),
                BackgroundColor = _bottomPanelColor,
                BorderColor = _topBorderColor,
                BorderRadius = _btnCancelBorderRadius,
                BorderSize = _btnCancelBorderSize,
                OnHoverColor = _btnCancelOnHoverColor,
                TextColor = _btnCancelTextColor,
                Icon = _btnCancelIcon,
                IconAlignment = _btnCancelIconAlignment,
                IconSize = _btnCancelIconSize,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Regular),
                Cursor = Cursors.Hand
            };

            bottomPanel.Controls.Add(btnOk);
            bottomPanel.Controls.Add(btnCancel);

            lblMessage = new Label
            {
                Dock = DockStyle.Fill,
                ForeColor = _messageColor,
                RightToLeft = RightToLeft.Yes,
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                TextAlign = ContentAlignment.MiddleCenter,
                Padding = new Padding(15, 0, 15, 0)
            };

            this.Controls.Add(lblMessage);
            this.Controls.Add(bottomPanel);
            this.Controls.Add(lblTitle);
            this.Controls.Add(topBorder);
            this.Controls.Add(bottomBorder);
            this.Resize += (s, e) => ApplyRegion();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            ApplyRegion();
        }

        private void ApplyRegion()
        {
            if (_formBorderRadius > 2)
            {
                IntPtr ptr = CreateRoundRectRgn(0, 0, Width + 1, Height + 1, _formBorderRadius, _formBorderRadius);
                this.Region = System.Drawing.Region.FromHrgn(ptr);
                DeleteObject(ptr);
            }
            else
            {
                this.Region = new Region(this.ClientRectangle);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Smoothly render top border line conforming to rounded edges
            int strokeWidth = 4;
            using (Pen pen = new Pen(_topBorderColor, strokeWidth))
            {
                if (_formBorderRadius > 2)
                {
                    int diameter = _formBorderRadius * 2;
                    using (GraphicsPath path = new GraphicsPath())
                    {
                        path.AddArc(0, 0, diameter, diameter, 180, 90);
                        path.AddLine(_formBorderRadius, 0, Width - _formBorderRadius, 0);
                        path.AddArc(Width - diameter, 0, diameter, diameter, 270, 90);

                        e.Graphics.DrawPath(pen, path);
                    }
                }
                else
                {
                    e.Graphics.DrawLine(pen, 0, 2, Width, 2);
                }
            }
        }

        public static DialogResult Show(string title, string message, bool showCancel = true,
            Color? okColor = null, Color? topAccentColor = null, Image okIcon = null, Image cancelIcon = null)
        {
            using (var msg = new CMsgBox(title, message, showCancel))
            {
                if (okColor.HasValue) msg.BtnOkColor = okColor.Value;
                if (topAccentColor.HasValue) msg.TopBorderColor = topAccentColor.Value;
                if (okIcon != null) msg.BtnOkIcon = okIcon;
                if (cancelIcon != null) msg.BtnCancelIcon = cancelIcon;

                return msg.ShowDialog();
            }
        }
    }
}