using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using static OwOMaker.Helpers.Mem;

namespace OwO_Maker
{
    class BackgroundHelper
    {
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        public static extern bool PostMessage(IntPtr hWnd, nint Msg, nint wParam, int lParam);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out Point lpPoint);

        [DllImport("user32.dll")]
        static extern bool SetCursorPos(int X, int Y);
        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hwnd, ref RECT rectangle);

        [DllImport("user32.dll")]
        private static extern bool BlockInput(bool block);
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        public enum KeyEvents
        {
            WM_KEYDOWN = 0x0100,
            WM_KEYUP = 0x0101,
            WM_SYSKEYDOWN = 0x0104,
            WM_SYSKEYUP = 0x0105,
        };

        public enum MouseEvents
        {
            WM_MOVE = 0x0200,
            WM_KEYDOWN = 0x0201,
            WM_KEYUP = 0x0202,
            WM_KEYCLK = 0x203
        };


        public enum KeyCodes
        {
            #region KeyCodes
            VK_PRIOR = 0x21,
            VK_NEXT = 0x22,
            VK_END = 0x23,
            VK_HOME = 0x24,

            VK_LEFT = 0x25,
            VK_UP = 0x26,
            VK_RIGHT = 0x27,
            VK_DOWN = 0x28,

            VK_CANCEL = 0x03,
            VK_BACK = 0x08,
            VK_TAB = 0x09,
            VK_RETURN = 0x0D,
            VK_SHIFT = 0x10,
            VK_CONTROL = 0x11,
            VK_MENU = 0x12,
            VK_PAUSE = 0x13,
            VK_CAPITAL = 0x14,
            VK_ESCAPE = 0x1B,
            VK_SPACE = 0x20,
            VK_SNAPSHOT = 0x2C,
            VK_INSERT = 0x2D,
            VK_DELETE = 0x2E,
            VK_LWIN = 0x5B,
            VK_RWIN = 0x5C,
            VK_NUMPAD0 = 0x60,
            VK_NUMPAD1 = 0x61,
            VK_NUMPAD2 = 0x62,
            VK_NUMPAD3 = 0x63,
            VK_NUMPAD4 = 0x64,
            VK_NUMPAD5 = 0x65,
            VK_NUMPAD6 = 0x66,
            VK_NUMPAD7 = 0x67,
            VK_NUMPAD8 = 0x68,
            VK_NUMPAD9 = 0x69,
            VK_MULTIPLY = 0x6A,
            VK_ADD = 0x6B,
            VK_SUBTRACT = 0x6D,
            VK_DECIMAL = 0x6E,
            VK_DIVIDE = 0x6F,
            VK_F1 = 0x70,
            VK_F2 = 0x71,
            VK_F3 = 0x72,
            VK_F4 = 0x73,
            VK_F5 = 0x74,
            VK_F6 = 0x75,
            VK_F7 = 0x76,
            VK_F8 = 0x77,
            VK_F9 = 0x78,
            VK_F10 = 0x79,
            VK_F11 = 0x7A,
            VK_F12 = 0x7B,
            VK_NUMLOCK = 0x90,
            VK_SCROLL = 0x91,
            VK_ALT = 0x12,

            // Regular

            VK_0 = 0x30,
            VK_1 = 0x31,
            VK_2 = 0x32,
            VK_3 = 0x33,
            VK_4 = 0x34,
            VK_5 = 0x35,
            VK_6 = 0x36,
            VK_7 = 0x37,
            VK_8 = 0x38,
            VK_9 = 0x39,

            VK_A = 0x41,
            VK_B = 0x42,
            VK_C = 0x43,
            VK_D = 0x44,
            VK_E = 0x45,
            VK_F = 0x46,
            VK_G = 0x47,
            VK_H = 0x48,
            VK_I = 0x49,
            VK_J = 0x4A,
            VK_K = 0x4B,
            VK_L = 0x4C,
            VK_M = 0x4D,
            VK_N = 0x4E,
            VK_O = 0x4F,
            VK_P = 0x50,
            VK_Q = 0x51,
            VK_R = 0x52,
            VK_S = 0x53,
            VK_T = 0x54,
            VK_U = 0x55,
            VK_V = 0x56,
            VK_W = 0x57,
            VK_X = 0x58,
            VK_Y = 0x59,
            VK_Z = 0x5A,

            VK_APPS = 0x5D,
            VK_SLEEP = 0x5F,
            VK_SEPERATOR = 0x6C,
            VK_LSHIFT = 0xA0,
            VK_RSHIFT = 0xA1,
            VK_LCONTROL = 0xA2,
            VK_RCOONTROL = 0xA3,
            VK_LMENU = 0xA4,
            VK_RMENU = 0xA5,
            #endregion
        }


        public async static Task<bool> SendKey(IntPtr hwnd, KeyCodes key, int Delay)
        {
            bool result;
            result = PostMessage(hwnd, (nint)KeyEvents.WM_KEYDOWN, (char)key, 1);
            await Task.Delay(Delay);
            result = PostMessage(hwnd, (nint)KeyEvents.WM_KEYUP, (char)key, 0);

            return result;
        }

        public async static Task<bool> SendClick(IntPtr hwnd, int x, int y, int Delay)
        {
            CheckIfCursorIsMovingInClient(hwnd);
            await Task.Delay(200);
            bool result;
            result = PostMessage(hwnd, (nint)MouseEvents.WM_MOVE, 0, MakeLParam(x, y));
            await Task.Delay(Delay / 4);
            result = PostMessage(hwnd, (nint)MouseEvents.WM_KEYDOWN, 1, MakeLParam(x, y));
            await Task.Delay(Delay / 4);
            result = PostMessage(hwnd, (nint)MouseEvents.WM_KEYCLK, 1, MakeLParam(x, y));
            await Task.Delay(Delay / 4);
            result = PostMessage(hwnd, (nint)MouseEvents.WM_KEYUP, 0, MakeLParam(x, y));
            await Task.Delay(5);
            result = PostMessage(hwnd, (nint)MouseEvents.WM_MOVE, 0, MakeLParam(0, 0));
            await Task.Delay((Delay / 4) - 50);
            return result;
        }

        public static void CheckIfCursorIsMovingInClient(IntPtr hwnd)
        {
            RECT clientPos = new RECT();
            GetWindowRect(hwnd, ref clientPos);
            Point mousePos;
            GetCursorPos(out mousePos);
            if (mousePos.X > clientPos.Left && mousePos.Y > clientPos.Top && mousePos.X < clientPos.Right && mousePos.Y < clientPos.Bottom && GetForegroundWindow() == hwnd)
            {
                //Console.WriteLine("Mouse is in Window, move outside!");
                SetCursorPos(clientPos.Left - 100, clientPos.Top - 100);
            }
        }

        public static int MakeLParam(int LoWord, int HiWord)
        {
            return (int)((HiWord << 16) | (LoWord & 0xFFFF));
        }
    }
}
