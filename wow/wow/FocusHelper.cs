using System;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace wow
{
    class FocusHelper : SubjectImplementation
    {

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern Int32 GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        private System.Windows.Forms.Timer checkactiveProcessTimer = new System.Windows.Forms.Timer();

        public string ActiveWindowTitle
        {
            get 
            {
                Process currentProcess = GetActiveProcess();
                return currentProcess.MainWindowTitle; 
            }
        }

        private static Process GetActiveProcess()
        {
            IntPtr hwnd = GetForegroundWindow();
            return hwnd != null ? GetProcessByHandle(hwnd) : null;
        }

        private static Process GetProcessByHandle(IntPtr hwnd)
        {
            try
            {
                uint processID;
                GetWindowThreadProcessId(hwnd, out processID);
                return Process.GetProcessById((int)processID);
            }
            catch { return null; }
        }

        

      



    




    }
}
