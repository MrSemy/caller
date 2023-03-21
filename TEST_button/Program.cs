using System;
using System.Windows.Forms;

namespace TEST_button
{
    internal static class Program
    {
        //биндим формы для обращения между собой
        public static Form1 f1;
        public static Form2 f2;
        public static Form3 f3;
        public static Keys key = Properties.Settings.Default.hotkey; //забираем кей из настроек
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]

        static int Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var form1 = new Form1();
            Application.Run(form1);
            

            return 0;
        }
    }
}
