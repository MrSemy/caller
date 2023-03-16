﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace TEST_button
{
    public partial class Form1 : Form
    {
        Form2 form2 = new Form2();
        Form3 form3 = new Form3();
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
            this.roundedSwitch3.IsChecked = Properties.Settings.Default.use_hotkeys;
            this.roundedSwitch2.IsChecked = Properties.Settings.Default.add_buttons;
            this.roundedSwitch1.IsChecked = Properties.Settings.Default.use_hotkeys;
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
            Program.f2.textBox1.Text = number_from_buffer;
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

        private void roundButton2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }


        private void roundedSwitch3_MouseUp(object sender, MouseEventArgs e)
        {
            Properties.Settings.Default.use_hotkeys = this.roundedSwitch3.IsChecked;
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

        private void roundButton1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.use_hotkeys = this.roundedSwitch3.IsChecked;
            Properties.Settings.Default.add_buttons = this.roundedSwitch2.IsChecked;
            Properties.Settings.Default.use_hotkeys = this.roundedSwitch1.IsChecked;
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
            MessageBox.Show("Настройки сохранены", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Properties.Settings.Default.Save();
        }

        private void roundedSwitch2_MouseUp(object sender, MouseEventArgs e)
        {
            Properties.Settings.Default.add_buttons = this.roundedSwitch2.IsChecked;
            if (this.roundedSwitch2.IsChecked)
            {
                form2.Width = 233;
                form2.Height = 32;
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
                form2.Width = 199;
                form2.Height = 32;
                form2.button4.Visible = false;
                form2.button4.Enabled = false;
                Point pt = Screen.PrimaryScreen.WorkingArea.Location;
                pt.Offset(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
                pt.Offset(-form2.Width, -form2.Height);
                form2.Location = pt;
                form2.Invalidate();
                Program.f3.Visible = false;
            }

        }

        private void roundedSwitch1_MouseUp(object sender, MouseEventArgs e)
        {
            Properties.Settings.Default.use_hotkeys = this.roundedSwitch1.IsChecked;
            if (roundedSwitch1.IsChecked)
                form2.Hide();
            else
                form2.Show();
        }

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
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            form2.notifyIcon1.Visible = false;
            HotKeys.Unregister(this, this.HotKeyId);
        }

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

    }
}