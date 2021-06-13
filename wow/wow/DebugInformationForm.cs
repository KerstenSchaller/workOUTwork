using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace wow
{
    public partial class DebugInformationForm : Form, IObserver
    {
        private bool initialized = false;
        public DebugInformationForm()
        {
            InitializeComponent();
            this.Icon = Configuration.getApplicationIcon();
            this.Text = Configuration.ApplicationName + " - " + "Debug Information"; //set window title
        }

        public void Update(ISubject subject)
        {
            // update gui depending on sender
            if (subject is ActivityWatcher activityWatcher)
            {
                if (activityWatcher.ActivityState == ActivityWatcher.activityState_t.ACTIVE) textBoxState.Text = "Active";
                if (activityWatcher.ActivityState == ActivityWatcher.activityState_t.IDLE) textBoxState.Text = "Idle";
            }

            if (subject is ActiveStateLog activeStateLog)
            {
                // poplate listbox on first opening
                if (!initialized)
                {

                    var allEntrys = activeStateLog.getAllEntrys();
                    foreach (var entry in allEntrys)
                    {
                        listboxactiveStateLog.Items.Add(entry.Key.ToString() + " | new state:  " + entry.Value.newState + " | time in last state " + entry.Value.timeInState);
                    }
                    initialized = true;
                }
                else 
                { 
                    // append new entry to listbox
                    if (((ActiveStateLog)subject).Count > 0)
                    {
                        var entry = ((ActiveStateLog)subject).getLastEntry();
                        listboxactiveStateLog.Items.Add(entry.Key.ToString() + " | new state:  " + entry.Value.newState + " | time in last state " + entry.Value.timeInState);
                    }
                    listboxactiveStateLog.SelectedIndex = listboxactiveStateLog.Items.Count - 1;
                    labelStateLogCount.Text = "ActiveStateLog number of entrys: " + ((ActiveStateLog)subject).Count;
                }
            }

        }

        private void buttonOpenDataFolder_Click(object sender, EventArgs e)
        {
            string path = Configuration.getDataBasePath();
            Process.Start(path);
        }

        private void buttonManualBreak_Click(object sender, EventArgs e)
        {
            UserBreak ubreak = UserBreak.Instance;
            ubreak.startBreak();
        }
    }
}


