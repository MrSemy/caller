using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Net.Mail;

namespace TEST_button
{
    internal static class Program
    {
        public static Form1 f1;
        public static Form2 f2;
        public static Form3 f3;
        public static Keys key = Properties.Settings.Default.hotkey;
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]

        static int Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var form1 = new Form1();
            /*using (var form1 = new Form1())
            {
                form1.HotKeyId = 1;
                form1.FormClosed += (s, e) =>
                {
                    HotKeys.Unregister(form1, form1.HotKeyId);
                };
                
                
                    HotKeys.Register(form1, form1.HotKeyId, Modifiers.ALT, key);*/
            Application.Run(form1);
            

            return 0; // OK
        }
    }
}
