using System;
using System.Windows.Forms;

namespace wow
{
    public partial class DebugInformationForm : Form, IObserver
    {
        public DebugInformationForm()
        {
            InitializeComponent();

        }

        public void Update(ISubject subject)
        {
            // update gui depending on sender
            if (subject is ActivityWatcher)
            {
                if (((ActivityWatcher)subject).ActivityState == ActivityWatcher.activityState_t.ACTIVE) textBoxState.Text = "Active";
                if (((ActivityWatcher)subject).ActivityState == ActivityWatcher.activityState_t.IDLE) textBoxState.Text = "Idle";
            }

            if (subject is FocusWatcher)
            {
                textBoxWindowTitle.Text = ((FocusWatcher)subject).ActiveWindowTitle;
            }

            if (subject is ActiveStateLog)
            {
                if (((ActiveStateLog)subject).Count > 0)
                {
                    listboxactiveStateLog.Items.Add(((ActiveStateLog)subject).getLastEntry().Key.ToString() + " | new state:  " + ((ActiveStateLog)subject).getLastEntry().Value.newState + " | time in last state " + ((ActiveStateLog)subject).getLastEntry().Value.timeInState);
                }
                listboxactiveStateLog.SelectedIndex = listboxactiveStateLog.Items.Count - 1;
                labelStateLogCount.Text = "ActiveStateLog number of entrys: " + ((ActiveStateLog)subject).Count;
            }

        }



    }
}


