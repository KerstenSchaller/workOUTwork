using System;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace wow
{
    class FocusWatcher : SubjectImplementation
    {

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern Int32 GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        private System.Windows.Forms.Timer checkactiveProcessTimer = new System.Windows.Forms.Timer();

        private string activeWindowTitle = "";

        public string ActiveWindowTitle
        {
            get { return activeWindowTitle; }
        }

        public FocusWatcher()
        {
            checkactiveProcessTimer.Interval = 1000;
            checkactiveProcessTimer.Tick += new EventHandler(checkactiveProcessTimerCallback);
            checkactiveProcessTimer.Start();
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

        

        private void checkactiveProcessTimerCallback(object sender, EventArgs e)
        {
            Process currentProcess = GetActiveProcess();
            if (activeWindowTitle != currentProcess.MainWindowTitle)
            {
                activeWindowTitle = currentProcess.MainWindowTitle;
                this.Notify();
            }
        }



    




    }
}
