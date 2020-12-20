using System;
using System.Windows.Forms;

namespace wow
{
    public partial class DebugInformationForm : Form, IObserver
    {
        ActivityWatcher activityWatcher = new ActivityWatcher();
        FocusWatcher focusWatcher = new FocusWatcher();
        ActiveStateLog activeStateLog = new ActiveStateLog();

        public DebugInformationForm()
        {
            InitializeComponent();

            activityWatcher.Attach(activeStateLog);
            activityWatcher.Attach(this);
            activeStateLog.Attach(this);
            focusWatcher.Attach(this);

            activityWatcher.start();

            textBoxState.Text = (activityWatcher.ActivityState == ActivityWatcher.activityState_t.ACTIVE) ? "Active" : "Idle";
            if (activeStateLog.Count > 0)
            {
                foreach (var item in activeStateLog.getAllEntrys())
                {
                    listboxactiveStateLog.Items.Add(item.Key.ToString() + " | new state:  " + item.Value.newState + " | time in state " + item.Value.timeInState);
                    listboxactiveStateLog.SelectedIndex = listboxactiveStateLog.Items.Count - 1;
                    labelStateLogCount.Text = "ActiveStateLog number of entrys: " + activeStateLog.Count;
                }
            }

        }

        public void Update(ISubject subject)
        {
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
                    listboxactiveStateLog.Items.Add(((ActiveStateLog)subject).getLastEntry().Key.ToString() + " | new state:  " + ((ActiveStateLog)subject).getLastEntry().Value.newState + " | time in state " + activeStateLog.getLastEntry().Value.timeInState);
                }
                listboxactiveStateLog.SelectedIndex = listboxactiveStateLog.Items.Count - 1;
                labelStateLogCount.Text = "ActiveStateLog number of entrys: " + ((ActiveStateLog)subject).Count;
            }

        }



    
    private void label1_Click(object sender, EventArgs e)
    {

    }

    private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}


