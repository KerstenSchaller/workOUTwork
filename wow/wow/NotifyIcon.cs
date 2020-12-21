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
        FocusWatcher focusWatcher = new FocusWatcher();
        ActiveStateLog activeStateLog = new ActiveStateLog();
        MouseKeyHandler mouseKeyHandler = new MouseKeyHandler();
        SystemStateHandler systemStateHandler = new SystemStateHandler();

        /*Gui objects*/
        ContextMenu contextmenu = new ContextMenu();
        MenuItem menuItemDebugInformation = new MenuItem();
        MenuItem menuItemExit = new MenuItem();
        DebugInformationForm debugInformationForm;

        public NotifyIcon()
        {
            InitializeComponent();

            this.Hide();
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            
            
            //attaching activity watcher to the the subjects it needs
            mouseKeyHandler.Attach(activityWatcher);
            focusWatcher.Attach(activityWatcher);
            systemStateHandler.Attach(activityWatcher);

            //attaching the active statelog to the activity watcher
            activityWatcher.Attach(activeStateLog);
            //start watching and loggig user activity changes
            activityWatcher.start();

            menuItemDebugInformation.Text = "Debug Information";
            menuItemDebugInformation.Click += MenuItenDebugInformation_Click;
            
            menuItemExit.Text = "Exit";
            menuItemExit.Click += MenuItenExit_Click;

            contextmenu.MenuItems.Add(menuItemDebugInformation);
            contextmenu.MenuItems.Add(menuItemExit);

            notifyIconWOW.ContextMenu = contextmenu;
            notifyIconWOW.Visible = true;

            Bitmap bmp = new Bitmap( System.Reflection.Assembly.GetEntryAssembly().GetManifestResourceStream("wow.systray_icon_32.png"));
            IntPtr Hicon = bmp.GetHicon();
            Icon newIcon = Icon.FromHandle(Hicon);
            notifyIconWOW.Icon = newIcon;
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
            focusWatcher.Attach(debugInformationForm);

            debugInformationForm.Update(activityWatcher);
            debugInformationForm.Update(activeStateLog);
            debugInformationForm.Update(focusWatcher);

            debugInformationForm.FormClosed += DebugInformationForm_FormClosed;
            debugInformationForm.Show();
        }

        private void DebugInformationForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            activityWatcher.Detach(debugInformationForm);
            activeStateLog.Detach(debugInformationForm);
            focusWatcher.Detach(debugInformationForm);
        }
    }
}
