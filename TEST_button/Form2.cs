using System;
using System.Drawing;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace TEST_button
{

    public partial class Form2 : Form
    {
        //объявляем переменные для дальнейшей логики
        bool expectation;
        bool form3_opened = false;
        bool form1_opened = false;
        public Form2()
        {
            InitializeComponent();
            Program.f2 = this;
            //выставляем начальную позицию формы в зависимости от настроек
            this.StartPosition = FormStartPosition.Manual;
            Point pt = Screen.PrimaryScreen.WorkingArea.Location;
            pt.Offset(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
            if (Properties.Settings.Default.add_buttons)
            {
                pt.Offset(-this.Width, -this.Height);
            }
            else
            {
                pt.Offset(-this.Width + this.button4.Width, -this.Height);
            }
            this.Location = pt;
            this.ShowInTaskbar = false;
            //настройки стилей кнопок
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = FlatStyle.Flat;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = FlatStyle.Flat;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = FlatStyle.Flat;
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatStyle = FlatStyle.Flat;
        }

        //метод для форматирования номера (приведение в нужный вид)
        public string formate_number(string text)
        {
            Regex regex = new Regex(@"\D");
            string formatted_digits = regex.Replace(text, "");
            if ((formatted_digits.Length == 10) && (formatted_digits.StartsWith("9") || formatted_digits.StartsWith("3") || formatted_digits.StartsWith("4") || formatted_digits.StartsWith("8")))
            {
                formatted_digits = "8" + formatted_digits;
            }
            if ((formatted_digits.Length == 11) && (formatted_digits.StartsWith("7")))
            {
                formatted_digits = "8" + formatted_digits.Remove(0, 1);
            }
            return formatted_digits;
        }

        //метод для совершения звонка по http
        public void web_call(string text)
        {
            if ((text.Length == 11) || (text.Length == 5) || (text.Length == 6) || (text.Length == 13))
            {

                var client = new WebClient();
                client.Credentials = new System.Net.NetworkCredential("user", "user");
                try
                {
                    ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                    System.Net.ServicePointManager.ServerCertificateValidationCallback += (send, certificate, chain, sslPolicyErrors) => { return true; };
                    byte[] response = client.DownloadData("https://192.168.245.240/servlet?number=" + text);
                    notifyIcon1.ShowBalloonTip(2, "Статус", "Вызываю номер " + text, ToolTipIcon.Info);
                }
                catch (WebException e)
                {
                    MessageBox.Show("Аппарат недоступен", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Console.WriteLine(e.Message);
                }

            }
            return;
        }

        //кнопка "звонить"
        private void button1_Click(object sender, EventArgs e)
        {
            string text;
            text = textBox1.Text;
            string formatted_digits = formate_number(text);
            web_call(formatted_digits);
            textBox1.Text = null;
        }

        //кнопка "свернуть"
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            notifyIcon1.ShowBalloonTip(10, "Статус", "Работа в фоновом режиме", ToolTipIcon.Info);
        }

        //настройка поведения в трее
        private void развернутьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            Program.f1.Close();
        }

        //кнопка "раскрыть панель быстрого набора"
        private async void button4_Click(object sender, EventArgs e)
        {
            if (!form3_opened)
            {
                while (!expectation && Program.f3.Location.X > Screen.PrimaryScreen.WorkingArea.Width - Program.f3.panel1.Width)
                {
                    expectation = true;
                    await Task.Delay(1);
                    Program.f3.Location = new Point(Program.f3.Location.X - 8, Program.f3.Location.Y);
                    Program.f3.Invalidate();
                    expectation = false;
                }
                button4.BackgroundImage = System.Drawing.Image.FromFile(@"C:\Users\Semen\Documents\проекты\TEST_button\TEST_button\button_1.png");
                button4.BackgroundImageLayout = ImageLayout.Zoom;
                form3_opened = true;
            }
            else
            {
                while (!expectation && Program.f3.Location.X != Screen.PrimaryScreen.WorkingArea.Width + Program.f3.panel1.Width - Program.f3.Width)
                {
                    expectation = true;
                    await Task.Delay(1);
                    Program.f3.Location = new Point(Program.f3.Location.X + 8, Program.f3.Location.Y);
                    Program.f3.Invalidate();
                    expectation = false;
                }
                button4.BackgroundImage = System.Drawing.Image.FromFile(@"C:\Users\Semen\Documents\проекты\TEST_button\TEST_button\button_1_reversed.png");
                button4.BackgroundImageLayout = ImageLayout.Zoom;
                form3_opened = false;
            }
        }

        //кнопка "настройки"
        private void button3_Click(object sender, EventArgs e)
        {
            if (form1_opened)
                if (Program.f1.Visible)
                    Program.f1.Visible = false;
                else
                    Program.f1.Visible = true;

            if (Program.f1.WindowState == FormWindowState.Minimized)
            {
                Program.f1.WindowState = FormWindowState.Normal;
                Program.f1.Visible = true;
                Program.f1.Show();
                form1_opened = true;
            }

        }

        //параметры загрузки формы (без этого грузится чуть большего размера почему-то)
        private void Form2_Load(object sender, EventArgs e)
        {
            this.Height = 32;
        }

        //поведение при нажатии кнопки Enter в textbox
        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if((e.KeyCode == Keys.Enter) & (textBox1.Text != null)) 
            {
                string text;
                text = textBox1.Text;
                string formatted_digits = formate_number(text);
                web_call(formatted_digits);
                textBox1.Text = null;
            }
        }
    }
}
