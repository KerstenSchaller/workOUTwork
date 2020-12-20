using System;
using System.Windows.Forms;


namespace wow
{
    static class Program 
    {

        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            ActivityWatcher activityWatcher = new ActivityWatcher();
            FocusWatcher focusWatcher = new FocusWatcher();
            ActiveStateLog activeStateLog = new ActiveStateLog();
            MouseKeyHandler mouseKeyHandler = new MouseKeyHandler();
            SystemStateHandler systemStateHandler = new SystemStateHandler();



            //attaching activity watcher to the the subjects it needs
            mouseKeyHandler.Attach(activityWatcher);
            focusWatcher.Attach(activityWatcher);
            systemStateHandler.Attach(activityWatcher);

            //attaching the active statelog to the activity watcher
            activityWatcher.Attach(activeStateLog);



            DebugInformationForm debugInformationForm = new DebugInformationForm();

            //attaching the gui to its data providers
            activityWatcher.Attach(debugInformationForm);
            activeStateLog.Attach(debugInformationForm);
            focusWatcher.Attach(debugInformationForm);

            //start watching and loggig user activity changes
            activityWatcher.start();

            Application.Run(debugInformationForm);

            


        }

    }
}
