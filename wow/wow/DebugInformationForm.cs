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
            Configuration config = new Configuration();
            this.Text = config.ApplicationName + " - " + "Debug Information"; //set window title
        }

        public void Update(ISubject subject)
        {
            // update gui depending on sender
            if (subject is ActivityWatcher)
            {
                if (((ActivityWatcher)subject).ActivityState == ActivityWatcher.activityState_t.ACTIVE) textBoxState.Text = "Active";
                if (((ActivityWatcher)subject).ActivityState == ActivityWatcher.activityState_t.IDLE) textBoxState.Text = "Idle";
            }

            if (subject is ActiveStateLog)
            {
                // poplate listbox on first opening
                if (!initialized)
                {

                    var allEntrys = ((ActiveStateLog)subject).getAllEntrys();
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
            Configuration config = new Configuration();
            string path = config.getDataBasePath();
            Process.Start(path);
        }
    }
}


