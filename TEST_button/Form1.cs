using System;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace TEST_button
{
    public partial class Form1 : Form //форма настроек
    {
        Form2 form2 = new Form2();
        Form3 form3 = new Form3();
        //так как хоткеи глобальные, используем WinAPI для работы с хоткеями
        const int WM_HOTKEY = 0x0312;
        const int WM_COPY = 0x0301;

        [DllImport("user32.dll")]
        private static extern int
            GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern int
    GetWindowThreadProcessId(Int32 hWnd, out Int32 lpdwProcessId);

        [DllImport("User32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll")]
        private static extern int GetCurrentThreadId();


        [DllImport("user32.dll")]
        private static extern IntPtr GetFocus();

        [DllImport("user32.dll")]
        private static extern int
    AttachThreadInput(int idAttach, int idAttachTo, bool fAttach);


        public Form1()
        {
            Program.f1 = this;
            InitializeComponent();
            this.KeyPreview = true;
            //забираем настройки из базы настроек
            this.roundedSwitch3.IsChecked = Properties.Settings.Default.use_hotkeys;
            this.roundedSwitch2.IsChecked = Properties.Settings.Default.add_buttons;
            this.roundedSwitch1.IsChecked = Properties.Settings.Default.always_hide;
            if (Properties.Settings.Default.hotkey == Keys.T) 
            {
                this.customRadioBTN1.IsChecked = true;
                this.customRadioBTN2.IsChecked = false;
            }
            else
            {
                this.customRadioBTN2.IsChecked = true;
                this.customRadioBTN1.IsChecked = false;
            }
            if (Properties.Settings.Default.use_hotkeys)
                HotKeys.Register(this, this.HotKeyId, Modifiers.ALT, Program.key);

            this.textBox1.Text = Properties.Settings.Default.button1_name;
            this.textBox3.Text = Properties.Settings.Default.button2_name;
            this.textBox5.Text = Properties.Settings.Default.button3_name;
            this.textBox7.Text = Properties.Settings.Default.button4_name;
            this.textBox9.Text = Properties.Settings.Default.button5_name;
            this.textBox11.Text = Properties.Settings.Default.button6_name;
            this.textBox13.Text = Properties.Settings.Default.button7_name;
            this.textBox15.Text = Properties.Settings.Default.button8_name;
            this.textBox17.Text = Properties.Settings.Default.button9_name;
            this.textBox19.Text = Properties.Settings.Default.button10_name;
            this.textBox2.Text = Properties.Settings.Default.button1_number;
            this.textBox4.Text = Properties.Settings.Default.button2_number;
            this.textBox6.Text = Properties.Settings.Default.button3_number;
            this.textBox8.Text = Properties.Settings.Default.button4_number;
            this.textBox10.Text = Properties.Settings.Default.button5_number;
            this.textBox12.Text = Properties.Settings.Default.button6_number;
            this.textBox14.Text = Properties.Settings.Default.button7_number;
            this.textBox16.Text = Properties.Settings.Default.button8_number;
            this.textBox18.Text = Properties.Settings.Default.button9_number;
            this.textBox20.Text = Properties.Settings.Default.button10_number;
        }

        //читаем хоткей
        private void read_hotkey()
        {
            int handle = GetForegroundWindow();
            int ProcessID;
            int SelectedThreadId = GetWindowThreadProcessId(handle, out ProcessID);
            int CurrentThreadId = GetCurrentThreadId();
            Process SelectedProcess = Process.GetProcessById(ProcessID);
            AttachThreadInput(SelectedThreadId, CurrentThreadId, true);
            IntPtr FocusedWindowEx = GetFocus();
            SendMessage(FocusedWindowEx, WM_COPY, IntPtr.Zero, IntPtr.Zero);
            string number_from_buffer = Clipboard.GetText();
            AttachThreadInput(SelectedThreadId, CurrentThreadId, false);
            string formatted_number_from_buf = Program.f2.formate_number(number_from_buffer);
            Program.f2.web_call(formatted_number_from_buf);
            return;
        }


        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_HOTKEY && m.WParam.ToInt32() == HotKeyId)
            {
                read_hotkey();
            }
            base.WndProc(ref m);
        }

        public int HotKeyId { get; set; }

        //кнопка отмена
        private void roundButton2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        //переключатель использовать хоткеи
        private void roundedSwitch3_MouseUp(object sender, MouseEventArgs e)
        {
            Properties.Settings.Default.use_hotkeys = roundedSwitch3.IsChecked;
            if (!roundedSwitch3.IsChecked)
                {
                HotKeys.Unregister(this, this.HotKeyId);
                this.customRadioBTN1.IsChecked = false;
                this.customRadioBTN2.IsChecked = false;
                this.customRadioBTN1.Invalidate();
                this.customRadioBTN2.Invalidate();
                }
            else
                HotKeys.Register(this, this.HotKeyId, Modifiers.ALT, Program.key);

        }
        
        //кнопка сохранить настройки
        private void roundButton1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.use_hotkeys = roundedSwitch1.IsChecked;
            Properties.Settings.Default.add_buttons = roundedSwitch2.IsChecked;
            Properties.Settings.Default.use_hotkeys = roundedSwitch3.IsChecked;
            Properties.Settings.Default.button1_name = this.textBox1.Text;
            Properties.Settings.Default.button2_name = this.textBox3.Text;
            Properties.Settings.Default.button3_name = this.textBox5.Text;
            Properties.Settings.Default.button4_name = this.textBox7.Text;
            Properties.Settings.Default.button5_name = this.textBox9.Text;
            Properties.Settings.Default.button6_name = this.textBox11.Text;
            Properties.Settings.Default.button7_name = this.textBox13.Text;
            Properties.Settings.Default.button8_name = this.textBox15.Text;
            Properties.Settings.Default.button9_name = this.textBox17.Text;
            Properties.Settings.Default.button10_name = this.textBox19.Text;
            Properties.Settings.Default.button1_number = this.textBox2.Text;
            Properties.Settings.Default.button2_number = this.textBox4.Text;
            Properties.Settings.Default.button3_number = this.textBox6.Text;
            Properties.Settings.Default.button4_number = this.textBox8.Text;
            Properties.Settings.Default.button5_number = this.textBox10.Text;
            Properties.Settings.Default.button6_number = this.textBox12.Text;
            Properties.Settings.Default.button7_number = this.textBox14.Text;
            Properties.Settings.Default.button8_number = this.textBox16.Text;
            Properties.Settings.Default.button9_number = this.textBox18.Text;
            Properties.Settings.Default.button10_number = this.textBox20.Text;
            Properties.Settings.Default.Save();
            MessageBox.Show("Настройки сохранены", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
        }

        //переключатель панели быстрого набора
        private void roundedSwitch2_MouseUp(object sender, MouseEventArgs e)
        {
            Properties.Settings.Default.add_buttons = this.roundedSwitch2.IsChecked;
            if (this.roundedSwitch2.IsChecked)
            {
                form2.button4.Visible = true;
                form2.button4.Enabled = true;
                Point pt = Screen.PrimaryScreen.WorkingArea.Location;
                pt.Offset(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
                pt.Offset(-form2.Width, -form2.Height);
                form2.Location = pt;
                form2.Invalidate();
                Program.f3.Visible = true;
            }
            else
            {
                form2.button4.Visible = false;
                form2.button4.Enabled = false;
                Point pt = Screen.PrimaryScreen.WorkingArea.Location;
                pt.Offset(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
                pt.Offset(-form2.Width+form2.button4.Width, -form2.Height);
                form2.Location = pt;
                form2.Invalidate();
                Program.f3.Visible = false;
            }

        }

        //переключаетель работы в фоновом режиме
        private void roundedSwitch1_MouseUp(object sender, MouseEventArgs e)
        {
            Properties.Settings.Default.use_hotkeys = this.roundedSwitch1.IsChecked;
            if (roundedSwitch1.IsChecked)
            {
                form2.Hide();
                Program.f2.notifyIcon1.ShowBalloonTip(10, "Статус", "Работа в фоновом режиме", ToolTipIcon.Info);
            }
            else
                form2.Show();
        }

        //параметры загрузки основной формы
        private void Form1_Load(object sender, EventArgs e)
        {
            
            if (!Properties.Settings.Default.always_hide)
                form2.Show();
            else
                form2.Hide();
            if (roundedSwitch3.IsChecked | Properties.Settings.Default.use_hotkeys)
            {
                HotKeys.Register(this, this.HotKeyId, Modifiers.ALT, Program.key);
            }
        }

        //поведение при закрытии основной формы
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            form2.notifyIcon1.Visible = false;
            HotKeys.Unregister(this, this.HotKeyId);
        }

        //переключатель хоткеев
        private void customRadioBTN1_MouseUp(object sender, MouseEventArgs e)
        {
            if (customRadioBTN1.IsChecked)
            {
                HotKeys.Unregister(this, this.HotKeyId);
                Properties.Settings.Default.hotkey = Keys.T;
                customRadioBTN2.IsChecked = false;
                customRadioBTN2.Invalidate();
                Program.key = Keys.T;
                HotKeys.Register(this, this.HotKeyId, Modifiers.ALT, Program.key);
            }
        }

        private void customRadioBTN2_MouseUp(object sender, MouseEventArgs e)
        {
            if (customRadioBTN2.IsChecked)
            {
                HotKeys.Unregister(this, this.HotKeyId);
                Properties.Settings.Default.hotkey = Keys.Z;
                customRadioBTN1.IsChecked = false;
                customRadioBTN1.Invalidate();
                Program.key = Keys.Z;
                HotKeys.Register(this, this.HotKeyId, Modifiers.ALT, Program.key);

            }
        }

        //привязка к настройкам панели быстрого набора
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Program.f3.roundButton1.Text= textBox1.Text;
            Program.f3.roundButton1.Invalidate();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            Program.f3.roundButton2.Text = textBox3.Text;
            Program.f3.roundButton2.Invalidate();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            Program.f3.roundButton3.Text = textBox5.Text;
            Program.f3.roundButton3.Invalidate();
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            Program.f3.roundButton4.Text = textBox7.Text;
            Program.f3.roundButton4.Invalidate();
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            Program.f3.roundButton5.Text = textBox9.Text;
            Program.f3.roundButton5.Invalidate();
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            Program.f3.roundButton6.Text = textBox11.Text;
            Program.f3.roundButton6.Invalidate();
        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            Program.f3.roundButton7.Text = textBox13.Text;
            Program.f3.roundButton7.Invalidate();
        }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {
            Program.f3.roundButton8.Text = textBox15.Text;
            Program.f3.roundButton8.Invalidate();
        }

        private void textBox17_TextChanged(object sender, EventArgs e)
        {
            Program.f3.roundButton9.Text = textBox17.Text;
            Program.f3.roundButton9.Invalidate();
        }

        private void textBox19_TextChanged(object sender, EventArgs e)
        {
            Program.f3.roundButton10.Text = textBox19.Text;
            Program.f3.roundButton10.Invalidate();
        }
    }
}
