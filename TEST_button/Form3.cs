using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace TEST_button
{
    
    public partial class Form3 : Form
    {
        //делаем закруглённые края формы
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
            //задаём начальное положение формы
            this.StartPosition = FormStartPosition.Manual;
            Point pt = Screen.PrimaryScreen.WorkingArea.Location;
            pt.Offset(Screen.PrimaryScreen.WorkingArea.Width + panel1.Width, Screen.PrimaryScreen.WorkingArea.Height/2 + this.Height/2);
            pt.Offset(-this.Width, -this.Height);
            this.Location = pt;

            this.BackColor = Color.FromArgb(59, 64, 69);
            //забираем из настроек имена кнопок быстрого набора
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
            //из настроек узнаём, форма должна быть скрыта или активна
            if (!Properties.Settings.Default.add_buttons)
                this.Hide();
            else
                this.Show();
        }
        //объявляем переменные для последующей логики
        string number;
        string formatted_number;

        //программируем поведение при нажатии кнопок
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
            number = Properties.Settings.Default.button10_number;
            formatted_number = Program.f2.formate_number(number);
            Program.f2.web_call(formatted_number);
        }

        private void roundButton9_Click(object sender, EventArgs e)
        {
            number = Properties.Settings.Default.button9_number;
            formatted_number = Program.f2.formate_number(number);
            Program.f2.web_call(formatted_number);
        }

        private void roundButton8_Click(object sender, EventArgs e)
        {
            number = Properties.Settings.Default.button8_number;
            formatted_number = Program.f2.formate_number(number);
            Program.f2.web_call(formatted_number);
        }

        private void roundButton7_Click(object sender, EventArgs e)
        {
            number = Properties.Settings.Default.button7_number;
            formatted_number = Program.f2.formate_number(number);
            Program.f2.web_call(formatted_number);
        }
    }
}
