using System;
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
        NoBreakWarner noBreakWarner = new NoBreakWarner();

        /*Gui objects*/
        ContextMenu contextmenu = new ContextMenu();
        MenuItem menuItemDebugInformation = new MenuItem();
        MenuItem menuItemConfigurationGui = new MenuItem();
        MenuItem menuItemExit = new MenuItem();
        DebugInformationForm debugInformationForm;
        ConfigForm configurationForm;

        /*Independent Screen widgets*/
        DilbertWidget dilbertWidget = new DilbertWidget();

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

            notifyIconWOW.Icon = new Configuration().getApplicationIcon();

            ScreenImageComposer.Instance.attachWidget(dilbertWidget);

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
