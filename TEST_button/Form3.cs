using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using System.Net;

namespace TEST_button
{
    
    public partial class Form3 : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        

        private static extern IntPtr CreateRoundRectRgn
    (
     int nLeftRect,
     int nTopRect,
     int nRightRect,
     int nBottomRect,
     int nWidthEllipse,
     int nHeightEllipse
     );
        public Form3()
        {
            Program.f3 = this;
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 4, 4));
            //Начальное положение формы задаётся вручную
            this.StartPosition = FormStartPosition.Manual;
            //Верхний левый угол экрана
            Point pt = Screen.PrimaryScreen.WorkingArea.Location;
            //Перенос в нижний правый угол экрана без панели задач
            pt.Offset(Screen.PrimaryScreen.WorkingArea.Width + panel1.Width, Screen.PrimaryScreen.WorkingArea.Height + this.Height);
            //Перенос в местоположение верхнего левого угла формы, чтобы её правый нижний угол попал в правый нижний угол экрана
            pt.Offset(-this.Width, -this.Height);
            this.Location = pt;
            this.BackColor = Color.FromArgb(59, 64, 69);
            this.roundButton1.Text = Properties.Settings.Default.button1_name;
            this.roundButton2.Text = Properties.Settings.Default.button2_name;
            this.roundButton3.Text = Properties.Settings.Default.button3_name;
            this.roundButton4.Text = Properties.Settings.Default.button4_name;
            this.roundButton5.Text = Properties.Settings.Default.button5_name;
            this.roundButton6.Text = Properties.Settings.Default.button6_name;
            this.roundButton7.Text = Properties.Settings.Default.button7_name;
            this.roundButton8.Text = Properties.Settings.Default.button8_name;
            this.roundButton9.Text = Properties.Settings.Default.button9_name;
            this.roundButton10.Text = Properties.Settings.Default.button10_name;
            if (!Properties.Settings.Default.add_buttons)
                this.Hide();
            else
                this.Show();
        }
        string number;
        string formatted_number;

        private void roundButton1_Click(object sender, EventArgs e)
        {
            string number = Properties.Settings.Default.button1_number;
            formatted_number = Program.f2.formate_number(number);
            Program.f2.web_call(formatted_number);
        }



        private void roundButton2_Click(object sender, EventArgs e)
        {
            number = Properties.Settings.Default.button2_number;
            formatted_number = Program.f2.formate_number(number);
            Program.f2.web_call(formatted_number);
        }

        private void roundButton3_Click(object sender, EventArgs e)
        {
            number = Properties.Settings.Default.button3_number;
            formatted_number = Program.f2.formate_number(number);
            Program.f2.web_call(formatted_number);
        }

        private void roundButton4_Click(object sender, EventArgs e)
        {
            number = Properties.Settings.Default.button4_number;
            formatted_number = Program.f2.formate_number(number);
            Program.f2.web_call(formatted_number);
        }

        private void roundButton5_Click(object sender, EventArgs e)
        {
            number = Properties.Settings.Default.button5_number;
            formatted_number = Program.f2.formate_number(number);
            Program.f2.web_call(formatted_number);
        }

        private void roundButton6_Click(object sender, EventArgs e)
        {
            number = Properties.Settings.Default.button6_number;
            formatted_number = Program.f2.formate_number(number);
            Program.f2.web_call(formatted_number);
        }

        private void roundButton10_Click(object sender, EventArgs e)
        {
            number = Properties.Settings.Default.button7_number;
            formatted_number = Program.f2.formate_number(number);
            Program.f2.web_call(formatted_number);
        }

        private void roundButton9_Click(object sender, EventArgs e)
        {
            number = Properties.Settings.Default.button8_number;
            formatted_number = Program.f2.formate_number(number);
            Program.f2.web_call(formatted_number);
        }

        private void roundButton8_Click(object sender, EventArgs e)
        {
            number = Properties.Settings.Default.button9_number;
            formatted_number = Program.f2.formate_number(number);
            Program.f2.web_call(formatted_number);
        }

        private void roundButton7_Click(object sender, EventArgs e)
        {
            number = Properties.Settings.Default.button10_number;
            formatted_number = Program.f2.formate_number(number);
            Program.f2.web_call(formatted_number);
        }
    }
}
