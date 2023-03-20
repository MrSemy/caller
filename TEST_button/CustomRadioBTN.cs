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

namespace TEST_button
{
    public class CustomRadioBTN : Control
    {
        public Color BackColor2 { get; set; }
        public Color radioBorderColor { get; set; }
        public int radioRoundRadius { get; set; }

        public Color radioToogleColor { get; set; }
        public Color radioHighlightColor2 { get; set; }
        public Color radioHighlightForeColor { get; set; }

        public Color radioPressedColor { get; set; }
        public Color radioPressedColor2 { get; set; }
        public Color radioPressedForeColor { get; set; }

        private bool IsHighlighted;
        public bool IsChecked;
        int TooglePosX_ON;
        int TooglePosX_OFF;
        int TooglePosX;


        public CustomRadioBTN()
        {
            Size = new Size(100, 40);
            radioRoundRadius = 40;
            BackColor = Color.Gainsboro;
            BackColor2 = Color.Silver;
            radioBorderColor = Color.Black;
            radioToogleColor = Color.Orange;
            radioHighlightColor2 = Color.OrangeRed;
            radioHighlightForeColor = Color.Black;
            IsHighlighted= false;
            radioPressedColor = Color.Red;
            radioPressedColor2 = Color.Maroon;
            radioPressedForeColor = Color.White;
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

            var foreColor = IsChecked ? radioPressedForeColor : IsHighlighted ? radioHighlightForeColor : ForeColor;
            var backColor = IsChecked ? radioPressedColor : IsHighlighted ? radioToogleColor : BackColor;
            var backColor2 = IsChecked ? radioPressedColor2 : IsHighlighted ? radioHighlightColor2 : BackColor2;


            using (var pen = new Pen(radioBorderColor, 1))
            {
                e.Graphics.DrawPath(pen, Path);
            }

            using (var brush = new LinearGradientBrush(ClientRectangle, backColor, backColor2, LinearGradientMode.Vertical))
                e.Graphics.FillPath(brush, Path);


            using (var pen = new Pen(Color.Black, 1))
            {
                var rect = ClientRectangle;
                TooglePosX_OFF = rect.X + 100;
                TooglePosX_ON = rect.X + rect.Width - rect.Height + 8;
                if (IsChecked)
                    TooglePosX = TooglePosX_ON;
                else
                    TooglePosX = TooglePosX_OFF;

                Rectangle rect1 = new Rectangle(TooglePosX, rect.Y + 8, rect.Height - 16, rect.Height - 16);
                e.Graphics.DrawEllipse(pen, rect1);
                e.Graphics.FillEllipse(new SolidBrush(radioToogleColor), rect1);
            }




            base.OnPaint(e);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            radioBorderColor = Color.DarkGreen;
            //IsHighlighted = true;
            Parent.Invalidate(Bounds, false);
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            //IsHighlighted = false;
            //IsChecked = false;
            radioBorderColor = Color.Black;
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
                return GetRoundedRectangle(rect, radioRoundRadius);
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
