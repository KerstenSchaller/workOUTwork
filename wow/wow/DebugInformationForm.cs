using System;
using System.Windows.Forms;

namespace wow
{
    public partial class DebugInformationForm : Form, IObserver
    {
        ActivityWatcher activityWatcher = new ActivityWatcher();
        FocusWatcher focusWatcher = new FocusWatcher();
        ActiveStateLog activeStateLog = new ActiveStateLog("testapp/activestatelog");
   
        public DebugInformationForm()
        {
            InitializeComponent();

            activityWatcher.Attach(activeStateLog);
            activityWatcher.Attach(this);
            activeStateLog.Attach(this);
            focusWatcher.Attach(this);

            if(activeStateLog.Count > 0) 
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
                if (activityWatcher.ActivityState == ActivityWatcher.activityState_t.ACTIVE) textBox1.Text = "Active";
                if (activityWatcher.ActivityState == ActivityWatcher.activityState_t.IDLE) textBox1.Text = "Idle";           
            }

            if(subject is FocusWatcher) 
            {
                textBox3.Text = focusWatcher.ActiveWindowTitle;
            }

            if (subject is ActiveStateLog)
            {
                listboxactiveStateLog.Items.Add(activeStateLog.getLastEntry().Key.ToString() + " | new state:  " + activeStateLog.getLastEntry().Value.newState + " | time in state " + activeStateLog.getLastEntry().Value.timeInState);
                listboxactiveStateLog.SelectedIndex = listboxactiveStateLog.Items.Count - 1;
                labelStateLogCount.Text = "ActiveStateLog number of entrys: " + activeStateLog.Count;
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
