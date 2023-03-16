using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
//using System.Net.Http;
using System.Net;
using System.Configuration;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace TEST_button
{
    public partial class Form2 : Form
    {
        bool expectation;
        bool form3_opened = false;
        bool form1_opened = false;
        public Form2()
        {
            InitializeComponent();
            Program.f2 = this;
            this.StartPosition = FormStartPosition.Manual;
            Point pt = Screen.PrimaryScreen.WorkingArea.Location;
            pt.Offset(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
            pt.Offset(-this.Width, -this.Height);

            this.Location = pt;
            this.ShowInTaskbar = false;
            this.BackColor = Color.FromArgb(59, 64, 69);
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = FlatStyle.Flat;
            this.button1.BackColor = Color.FromArgb(59, 64, 69);
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = FlatStyle.Flat;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = FlatStyle.Flat;
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatStyle = FlatStyle.Flat;
        }

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

        //static readonly HttpClient client = new HttpClient();
   
        /*private CredentialCache GetCredential()
        {
            
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
            CredentialCache credentialCache = new CredentialCache();
            credentialCache.Add(new System.Uri("http://192.168.245.240/servlet?number="), "Basic", new NetworkCredential("user", "user"));
            return credentialCache;
        }
        */

        public async void web_call(string text)
        {
            if ((text.Length == 11) || (text.Length == 5) || (text.Length == 6) || (text.Length == 13))
            {
                //string url = "http://192.168.245.240/servlet?number=" + text;
                //string K = "user:user";
                /*
                try
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("user", "user");
                    HttpResponseMessage response = await client.GetAsync("http://192.168.245.240/servlet?number=" + text);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseBody);
                }
                catch(HttpRequestException e)
                {
                    Console.WriteLine(e.Message);
                }
                */
                /*var client = new WebClient();
                client.Credentials = new System.Net.NetworkCredential("user", "user");
                WebException exception = null;
                byte[] response = client.DownloadData("http://192.168.245.240/servlet?number=" + text);*/
                
                //var content = client.DownloadString(@"http://user:user@192.168.245.240/servlet?number=" + text);
                //WebRequest wrGETURL = WebRequest.Create("http://192.168.245.240/servlet?p=login&q=login&username=user&pwd=user&jumpto=URI&number=" + text);

                HttpWebRequest.CreateHttp("http://192.168.245.240/servlet?p=login&q=login&username=user&pwd=user&jumpto=URI&number=" + text);
                //System.Net.WebResponse resp = wrGETURL.GetResponse();
                //System.IO.Stream stream = resp.GetResponseStream();
                //System.IO.StreamReader sr = new System.IO.StreamReader(stream);
                //string s = sr.ReadToEnd();
                //Console.WriteLine(s);
                //HttpWebRequest.Create("http://localhost/call_to=/" + text);
                /*WebRequest request = WebRequest.Create(url);
                request.Credentials = GetCredential();
                request.PreAuthenticate = true;
                notifyIcon1.ShowBalloonTip(10, "Статус", "Вызываю номер " + text, ToolTipIcon.Info);*/
            }
            return;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string text;
            text = textBox1.Text;
            string formatted_digits = formate_number(text);
            web_call(formatted_digits);
            textBox1.Text = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            notifyIcon1.ShowBalloonTip(10, "Статус", "Работа в фоновом режиме", ToolTipIcon.Info);
        }

        private void развернутьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            Application.Exit();
        }

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
                button4.BackgroundImage = Image.FromFile(@"C:\Users\User\source\repos\TEST_button\TEST_button\add_but2.png");
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
                button4.BackgroundImage = Image.FromFile(@"C:\Users\User\source\repos\TEST_button\TEST_button\add_but.png");
                button4.BackgroundImageLayout = ImageLayout.Zoom;
                form3_opened = false;
            }
        }

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

        private void Form2_Load(object sender, EventArgs e)
        {
            this.Width = 232;
            this.Height = 32;

        }
    }
}
