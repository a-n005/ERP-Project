using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Acc_Trede_winForms.Models.CButton
{
    public class CBtn : Button
    {
        [Flags]
        public enum BorderSide
        {
            All = 0,
            Top = 1,
            Right = 2,
            Bottom = 4,
            Left = 8
        }
        #region Fields
        private int borderSize = 0;
        private int borderRadius = 0;
        private Color borderColor = Color.PaleVioletRed;

        // Hover & Background Backing Fields
        private Color onHoverColor = Color.FromArgb(141, 129, 240);
        private Color defaultBackColor = Color.MediumSlateBlue;
        private bool isHovered = false;

        // Icon Fields
        private Image icon = null;
        private Size iconSize = new Size(20, 20);
        private ContentAlignment iconAlignment = ContentAlignment.MiddleLeft;

        private BorderSide selectedBorderSide = BorderSide.All;
        #endregion
        #region Properties

        [Category("Custom Properties")]
        [Description("Select which sides to draw borders on.")]
        [Editor("System.Windows.Forms.Design.BorderSidesEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public BorderSide SelectedBorderSide
        {
            get => selectedBorderSide;
            set
            {
                selectedBorderSide = value;
                this.Invalidate(); // Redraws control when changed in Properties window
            }
        }
        [Category("Custom Properties")]
        public Image Icon
        {
            get { return icon; }
            set
            {
                icon = value;
                this.Invalidate();
            }
        }

        [Category("Custom Properties")]
        public Size IconSize
        {
            get { return iconSize; }
            set
            {
                iconSize = value;
                this.Invalidate();
            }
        }

        [Category("Custom Properties")]
        public ContentAlignment IconAlignment
        {
            get { return iconAlignment; }
            set
            {
                iconAlignment = value;
                this.Invalidate();
            }
        }

        [Category("Custom Properties")]
        public int BorderSize
        {
            get { return borderSize; }
            set
            {
                borderSize = value;
                this.Invalidate();
            }
        }

        [Category("Custom Properties")]
        public int BorderRadius
        {
            get { return borderRadius; }
            set
            {
                borderRadius = value;
                this.Invalidate();
            }
        }

        [Category("Custom Properties")]
        public Color BorderColor
        {
            get { return borderColor; }
            set
            {
                borderColor = value;
                this.Invalidate();
            }
        }

        [Category("Custom Properties")]
        public Color OnHoverColor
        {
            get { return onHoverColor; }
            set
            {
                onHoverColor = value;
                this.Invalidate();
            }
        }

        [Category("Custom Properties")]
        public Color BackgroundColor
        {
            get { return this.BackColor; }
            set
            {
                this.BackColor = value;
                defaultBackColor = value;
            }
        }

        [Category("Custom Properties")]
        public Color TextColor
        {
            get { return this.ForeColor; }
            set { this.ForeColor = value; }
        }
        #endregion
        // Constructor
        public CBtn()
        {
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.Size = new Size(150, 40);
            this.BackColor = defaultBackColor;
            this.ForeColor = Color.White;
            this.Resize += new EventHandler(Button_Resize);

            this.SetStyle(ControlStyles.Selectable, true);
        }
        #region override Methods
        protected override bool ShowFocusCues => false;
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            isHovered = true;
            this.BackColor = onHoverColor;
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            isHovered = false;
            this.BackColor = defaultBackColor;
        }
        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);

            Rectangle rectSurface = this.ClientRectangle;
            Rectangle rectBorder = Rectangle.Inflate(rectSurface, -borderSize, -borderSize);
            int smoothSize = borderSize > 0 ? borderSize : 2;

            if (borderRadius > 2 && SelectedBorderSide == BorderSide.All) // Rounded button (Full border only)
            {
                using (GraphicsPath pathSurface = GetFigurePath(rectSurface, borderRadius))
                using (GraphicsPath pathBorder = GetFigurePath(rectBorder, borderRadius - borderSize))
                using (Pen penSurface = new Pen(this.Parent != null ? this.Parent.BackColor : Color.Transparent, smoothSize))
                using (Pen penBorder = new Pen(borderColor, borderSize))
                {
                    pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                    // Button surface
                    this.Region = new Region(pathSurface);

                    // Draw surface border for HD result
                    pevent.Graphics.DrawPath(penSurface, pathSurface);

                    // Button border                    
                    if (borderSize >= 1)
                        pevent.Graphics.DrawPath(penBorder, pathBorder);
                }
            }
            else // Square button OR single-side border
            {
                pevent.Graphics.SmoothingMode = SmoothingMode.None;

                // Button surface
                this.Region = new Region(rectSurface);

                if (borderSize >= 1)
                {
                    using (Pen penBorder = new Pen(borderColor, borderSize))
                    {
                        int w = this.Width - 1;
                        int h = this.Height - 1;

                        if (SelectedBorderSide == BorderSide.All)
                        {
                            penBorder.Alignment = PenAlignment.Inset;
                            pevent.Graphics.DrawRectangle(penBorder, 0, 0, w, h);
                        }
                        else
                        {
                            // Draw specific border sides
                            if (SelectedBorderSide.HasFlag(BorderSide.Top))
                                pevent.Graphics.DrawLine(penBorder, 0, 0, w, 0);

                            if (SelectedBorderSide.HasFlag(BorderSide.Right))
                                pevent.Graphics.DrawLine(penBorder, w, 0, w, h);

                            if (SelectedBorderSide.HasFlag(BorderSide.Bottom))
                                pevent.Graphics.DrawLine(penBorder, 0, h, w, h);

                            if (SelectedBorderSide.HasFlag(BorderSide.Left))
                                pevent.Graphics.DrawLine(penBorder, 0, 0, 0, h);
                        }
                    }
                }
            }

            // Draw Icon
            if (icon != null)
            {
                pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                pevent.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

                Point iconLocation = GetIconLocation();
                pevent.Graphics.DrawImage(icon, new Rectangle(iconLocation, iconSize));
            }
        }
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            if (this.Parent != null)
            {
                this.Parent.BackColorChanged += new EventHandler(Container_BackColorChanged);
            }
        }
        #endregion
        #region private Methods
        private void Container_BackColorChanged(object sender, EventArgs e)
        {
            this.Invalidate();
        }
        private Point GetIconLocation()
        {
            int x = 10;
            int y = (this.Height - iconSize.Height) / 2;

            switch (iconAlignment)
            {
                case ContentAlignment.MiddleLeft:
                    x = 10;
                    break;
                case ContentAlignment.MiddleCenter:
                    x = (this.Width - iconSize.Width) / 2;
                    break;
                case ContentAlignment.MiddleRight:
                    x = this.Width - iconSize.Width - 10;
                    break;
            }

            return new Point(x, y);
        }
        private void Button_Resize(object sender, EventArgs e)
        {
            if (borderRadius > this.Height)
                borderRadius = this.Height;
        }
        private GraphicsPath GetFigurePath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            float curveSize = radius * 2F;

            path.StartFigure();
            path.AddArc(rect.X, rect.Y, curveSize, curveSize, 180, 90);
            path.AddArc(rect.Right - curveSize, rect.Y, curveSize, curveSize, 270, 90);
            path.AddArc(rect.Right - curveSize, rect.Bottom - curveSize, curveSize, curveSize, 0, 90);
            path.AddArc(rect.X, rect.Bottom - curveSize, curveSize, curveSize, 90, 90);
            path.CloseFigure();
            return path;
        }
        #endregion
    }
}