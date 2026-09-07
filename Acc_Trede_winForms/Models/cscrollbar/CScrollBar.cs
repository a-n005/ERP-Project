using System;
using System.Drawing;
using System.Windows.Forms;

namespace Acc_Trede_winForms.Models.CScrollB
{
    public class CScrollBar : Control
    {
        private int _value = 0;
        private int _minimum = 0;
        private int _maximum = 100;
        private int _largeChange = 10;
        private bool _isDragging = false;
        private int _dragStartY = 0;

        public event EventHandler Scroll;

        public int Value
        {
            get => _value;
            set
            {
                int newValue = Math.Max(_minimum, Math.Min(value, _maximum));
                if (_value != newValue)
                {
                    _value = newValue;
                    Invalidate();
                    Scroll?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public int Minimum { get => _minimum; set { _minimum = value; Invalidate(); } }
        public int Maximum { get => _maximum; set { _maximum = value; Invalidate(); } }
        public int LargeChange { get => _largeChange; set { _largeChange = value; Invalidate(); } }

        public CScrollBar()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer, true);
            Width = 10;
            BackColor = Color.FromArgb(24, 24, 36);
        }

        private Rectangle GetThumbRectangle()
        {
            int maxRange = Maximum - Minimum;
            if (maxRange <= 0) return Rectangle.Empty;

            int trackHeight = Height;
            int thumbHeight = Math.Max(25, (int)((float)LargeChange / (maxRange + LargeChange) * trackHeight));
            int maxThumbTop = trackHeight - thumbHeight;

            float percentage = (float)(Value - Minimum) / maxRange;
            int thumbTop = (int)(percentage * maxThumbTop);

            return new Rectangle(1, thumbTop, Width - 2, thumbHeight);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            using (SolidBrush bgBrush = new SolidBrush(BackColor))
                g.FillRectangle(bgBrush, ClientRectangle);

            Rectangle thumbRect = GetThumbRectangle();
            if (!thumbRect.IsEmpty)
            {
                Color thumbColor = _isDragging ? Color.FromArgb(140, 122, 255) : Color.FromArgb(108, 92, 231);
                using (SolidBrush thumbBrush = new SolidBrush(thumbColor))
                    g.FillRectangle(thumbBrush, thumbRect);
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button != MouseButtons.Left) return;

            Rectangle thumbRect = GetThumbRectangle();
            if (thumbRect.Contains(e.Location))
            {
                _isDragging = true;
                _dragStartY = e.Y - thumbRect.Top;
                this.Capture = true; // Keeps receiving mouse movement even if cursor leaves bar
            }
            else
            {
                // Click on track to jump directly
                int maxThumbTop = Height - thumbRect.Height;
                if (maxThumbTop > 0)
                {
                    float percentage = (float)e.Y / Height;
                    Value = Minimum + (int)(percentage * (Maximum - Minimum));
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (_isDragging)
            {
                int thumbHeight = GetThumbRectangle().Height;
                int maxThumbTop = Height - thumbHeight;

                if (maxThumbTop > 0)
                {
                    int newThumbTop = e.Y - _dragStartY;
                    newThumbTop = Math.Max(0, Math.Min(newThumbTop, maxThumbTop));

                    float percentage = (float)newThumbTop / maxThumbTop;
                    Value = Minimum + (int)(percentage * (Maximum - Minimum));
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            _isDragging = false;
            this.Capture = false; // Release global mouse capture
            Invalidate();
        }
    }
}