﻿using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace _pcboxManager
{
    static class Program
    {
        public static string[] args = Environment.GetCommandLineArgs(); //Get command line arguments

        private enum ShowWindowEnum
        {
            Hide = 0,
            ShowNormal = 1, ShowMinimized = 2, ShowMaximized = 3,
            Maximize = 3, ShowNormalNoActivate = 4, Show = 5,
            Minimize = 6, ShowMinNoActivate = 7, ShowNoActivate = 8,
            Restore = 9, ShowDefault = 10, ForceMinimized = 11
        };

        [StructLayout(LayoutKind.Sequential)]
        public struct COPYDATASTRUCT
        {
            public IntPtr dwData;
            public int cbData;
            public IntPtr lpData;
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool ShowWindow(IntPtr hWnd, ShowWindowEnum flags);

        [DllImport("user32.dll")]
        private static extern int SetForegroundWindow(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string className, string windowTitle);

        public const int WM_COPYDATA = 0x004A;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, ref COPYDATASTRUCT lParam);

        private static Mutex mutex = null;

        [STAThread]
        static void Main()
        {
            const string name = "PCBox Manager";
            bool firstInstance;

            //Use a mutex to check if this is the first instance of Manager
            mutex = new Mutex(true, name, out firstInstance);

            //If it's not, we need to restore and focus the existing window, as well as pass on any potential command line arguments
            if (!firstInstance)
            {
                //Finds the existing window, unhides it, restores it and sets focus to it
                IntPtr hWnd = FindWindow(null, "PCBox Manager");
                ShowWindow(hWnd, ShowWindowEnum.Show);
                ShowWindow(hWnd, ShowWindowEnum.Restore);
                SetForegroundWindow(hWnd);

                //If this second instance comes from a VM shortcut, we need to pass on the command line arguments so the VM will start
                //in the existing instance.
                //NOTE: This code will have to be modified in case more command line arguments are added in the future.
                if (args.Length == 3 && args[1] == "-S" && args[2] != null)
                {
                    string message = args[2];
                    COPYDATASTRUCT cds;
                    cds.dwData = IntPtr.Zero;
                    cds.lpData = Marshal.StringToHGlobalAnsi(message);
                    cds.cbData = message.Length;
                    SendMessage(hWnd, WM_COPYDATA, IntPtr.Zero, ref cds);
                }

                return;
            }
            else
            {
                //Then check if any instances of PCBox are already running and warn the user
                Process[] pname = Process.GetProcessesByName("pcbox");
                if (pname.Length > 0)
                {
                    DialogResult result = MessageBox.Show("At least one instance of pcbox is already running. It's not recommended that you run PCBox.exe directly outside of Manager. Do you want to continue at your own risk?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.No)
                    {
                        return;
                    }
                }
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new frmMain());
            }
        }
    }
}
