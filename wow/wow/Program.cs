using System;
using System.Windows.Forms;


namespace wow
{
    static class Program 
    {
        static NotifyIcon notifyIcon;
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            notifyIcon = new NotifyIcon();
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);
            Application.Run(notifyIcon);

 
        }

        private static void OnProcessExit(object sender, EventArgs e)
        {
            notifyIcon.Icon = null;
            notifyIcon.Visible = false;
            notifyIcon.Dispose();
        }
    }
}
