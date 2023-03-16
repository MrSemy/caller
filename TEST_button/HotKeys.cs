using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace TEST_button
{
    [Flags]
    enum Modifiers : uint
    {
        ALT = 0x0001,
        CONTROL = 0x0002,
        SHIFT = 0x0004,
        WIN = 0x0008
    }
    
    static class HotKeys
    {
        const string USER32 = "user32.dll";

        [DllImport(USER32, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool RegisterHotKey(
            IntPtr hWnd,
            int Id,
            Modifiers fsModifiers,
            Keys vk
            );

        [DllImport(USER32, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool UnregisterHotKey(
            IntPtr hWnd,
            int Id
            );

        public static bool Register(Form form1, int id,
                                      Modifiers mod,
                                      Keys key)
        {

            if (form1 == null || form1.IsDisposed)
                return false;
            if ((uint)mod == 0U)
                return false;
            if (key == Keys.None)
                return false;

            return RegisterHotKey(form1.Handle, id, mod, key);
        }

        public static bool Unregister(Form form1, int id)
        {
            if (form1 == null || form1.IsDisposed)
                return false;

            return UnregisterHotKey(form1.Handle, id);
        }
    }
}