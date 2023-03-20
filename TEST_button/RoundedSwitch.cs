using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using System.Windows.Forms;

namespace TEST_Switch
{
    public class RoundedSwitch : Control
    {
        public Color BackColor2 { get; set; }
        public Color SwitchBorderColor { get; set; }
        public int SwitchRoundRadius { get; set; }

        public Color SwitchToogleColor { get; set; }
        public Color SwitchHighlightColor2 { get; set; }
        public Color SwitchHighlightForeColor { get; set; }

        public Color SwitchPressedColor { get; set; }
        public Color SwitchPressedColor2 { get; set; }
        public Color SwitchPressedForeColor { get; set; }

        private bool IsHighlighted;
        public bool IsChecked;
        int TooglePosX_ON;
        int TooglePosX_OFF;
        int TooglePosX;


        public RoundedSwitch()
        {
            Size = new Size(100, 40);
            SwitchRoundRadius = 40;
            BackColor = Color.Gainsboro;
            BackColor2 = Color.Silver;
            SwitchBorderColor = Color.Black;
            SwitchToogleColor = Color.Orange;
            SwitchHighlightColor2 = Color.OrangeRed;
            SwitchHighlightForeColor = Color.Black;
            SwitchPressedColor = Color.Red;
            SwitchPressedColor2 = Color.Maroon;
            SwitchPressedForeColor = Color.White;
            IsHighlighted = false;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams createParams = base.CreateParams;
                createParams.ExStyle |= 0x00000020; // WS_EX_TRANSPARENT
                return createParams;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;

            var foreColor = IsChecked ? SwitchPressedForeColor : IsHighlighted ? SwitchHighlightForeColor : ForeColor;
            var backColor = IsChecked ? SwitchPressedColor : IsHighlighted ? SwitchToogleColor : BackColor;
            var backColor2 = IsChecked ? SwitchPressedColor2 : IsHighlighted ? SwitchHighlightColor2 : BackColor2;


            using (var pen = new Pen(SwitchBorderColor, 1))
            {
                e.Graphics.DrawPath(pen, Path);
            }

            using (var brush = new LinearGradientBrush(ClientRectangle, backColor, backColor2, LinearGradientMode.Vertical))
                e.Graphics.FillPath(brush, Path);

            
            using (var pen = new Pen(Color.Black, 1))
            {
                var rect = ClientRectangle;
                TooglePosX_OFF = rect.X + 4;
                TooglePosX_ON = rect.X + rect.Width - rect.Height +4;
                if (IsChecked)
                {
                    while (TooglePosX != TooglePosX_ON)
                    {
                        TooglePosX = TooglePosX + 1;
                        
                       
                    }
                }
                else
                {
                    while (TooglePosX != TooglePosX_OFF)
                    {
                        TooglePosX = TooglePosX - 1;
                        
                        
                    }
                }
                Rectangle rect1 = new Rectangle(TooglePosX, rect.Y + 4, rect.Height - 8, rect.Height - 8);
                e.Graphics.DrawEllipse(pen, rect1);
                e.Graphics.FillEllipse(new SolidBrush(SwitchToogleColor), rect1);
            }
                        
            
            
            /*using (var brush = new SolidBrush(foreColor))
            {
                var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                var rect = new Rectangle(ClientRectangle.X + ClientRectangle.Width + 10, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height);
                rect.Inflate(-4, -4);
                e.Graphics.DrawString(Text, Font, brush, rect, sf);
            }
           */
            base.OnPaint(e);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            SwitchBorderColor = Color.DarkGreen;
            //IsHighlighted = true;
            Parent.Invalidate(Bounds, false);
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            //IsHighlighted = false;
            //IsChecked = false;
            SwitchBorderColor = Color.Black;
            Parent.Invalidate(Bounds, false);
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            Parent.Invalidate(Bounds, true);
            Invalidate();
            if (IsChecked)
                IsChecked = false;
            else
                IsChecked = true;
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            Parent.Invalidate(Bounds, false);
            Invalidate();
            //IsChecked = false;
        }

        protected GraphicsPath Path
        {
            get
            {
                var rect = ClientRectangle;
                rect.Inflate(-1, -1);
                return GetRoundedRectangle(rect, SwitchRoundRadius);
            }
        }

        public static GraphicsPath GetRoundedRectangle(Rectangle rect, int d)
        {
            var gp = new GraphicsPath();

            gp.AddArc(rect.X, rect.Y, d, d, 180, 90);
            gp.AddArc(rect.X + rect.Width - d, rect.Y, d, d, 270, 90);
            gp.AddArc(rect.X + rect.Width - d, rect.Y + rect.Height - d, d, d, 0, 90);
            gp.AddArc(rect.X, rect.Y + rect.Height - d, d, d, 90, 90);
            gp.CloseFigure();

            return gp;
        }
    }
}
