using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wow
{
    public partial class NotifyIcon : Form
    {
        /* Bussines logic objects*/
        ActivityWatcher activityWatcher = new ActivityWatcher();
        ActiveStateLog activeStateLog = new ActiveStateLog();
        MouseKeyHandler mouseKeyHandler = new MouseKeyHandler();
        SystemStateHandler systemStateHandler = new SystemStateHandler();

        /*Gui objects*/
        ContextMenu contextmenu = new ContextMenu();
        MenuItem menuItemDebugInformation = new MenuItem();
        MenuItem menuItemConfigurationGui = new MenuItem();
        MenuItem menuItemExit = new MenuItem();
        DebugInformationForm debugInformationForm;
        ConfigForm configurationForm;

        public NotifyIcon()
        {
            InitializeComponent();

            this.Hide();
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;

            menuItemDebugInformation.Text = "Debug Information";
            menuItemDebugInformation.Click += MenuItenDebugInformation_Click;

            menuItemConfigurationGui.Text = "Configuration";
            menuItemConfigurationGui.Click += MenuItenConfig_Click;

            menuItemExit.Text = "Exit";
            menuItemExit.Click += MenuItenExit_Click;

            contextmenu.MenuItems.Add(menuItemDebugInformation);
            contextmenu.MenuItems.Add(menuItemConfigurationGui);
            contextmenu.MenuItems.Add(menuItemExit);

            notifyIconWOW.ContextMenu = contextmenu;
            notifyIconWOW.Visible = true;

            Bitmap bmp = new Bitmap( System.Reflection.Assembly.GetEntryAssembly().GetManifestResourceStream("wow.systray_icon_32.png"));
            IntPtr Hicon = bmp.GetHicon();
            Icon newIcon = Icon.FromHandle(Hicon);
            notifyIconWOW.Icon = newIcon;

            //start watching and loggig user activity changes
            activityWatcher.start();
        }

        private void MenuItenExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MenuItenDebugInformation_Click(object sender, EventArgs e)
        {
            debugInformationForm = new DebugInformationForm();
            //attaching the gui to its data providers
            activityWatcher.Attach(debugInformationForm);
            activeStateLog.Attach(debugInformationForm);

            //updating the gui initially
            debugInformationForm.Update(activityWatcher);
            debugInformationForm.Update(activeStateLog);

            debugInformationForm.FormClosed += DebugInformationForm_FormClosed;
            debugInformationForm.Show();
        }

        private void MenuItenConfig_Click(object sender, EventArgs e)
        {
            configurationForm = new ConfigForm();
            configurationForm.Show();
        }

        private void DebugInformationForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            activityWatcher.Detach(debugInformationForm);
            activeStateLog.Detach(debugInformationForm);
        }
    }
}
