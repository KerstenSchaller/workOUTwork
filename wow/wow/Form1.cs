using System;
using System.Windows.Forms;

namespace wow
{
    public partial class Form1 : Form, IObserver
    {
        ActivityWatcher activityWatcher = new ActivityWatcher();
        FocusWatcher focusWatcher = new FocusWatcher();
        ActiveStateLog activeStateLog = new ActiveStateLog("testapp/activestatelog");
   
        public Form1()
        {
            InitializeComponent();

            activityWatcher.Attach(activeStateLog);
            activityWatcher.Attach(this);
            activeStateLog.Attach(this);
            focusWatcher.Attach(this);

            foreach (var item in activeStateLog.getAllEntrys())
            {
                listBox1.Items.Add(item.Key.ToString() + " | new state:  " + item.Value.newState + " | time in state " + item.Value.timeInState);
            }
        }

        public void Update(ISubject subject)
        {
            if (activityWatcher.ActivityState == ActivityWatcher.activityState_t.ACTIVE) textBox1.Text = "Active";
            if (activityWatcher.ActivityState == ActivityWatcher.activityState_t.IDLE) textBox1.Text = "Idle";
            textBox2.Text = activityWatcher.TimeInState.ToString();

            textBox3.Text = focusWatcher.ActiveWindowTitle;
            if(subject is ActiveStateLog) 
            {
                listBox1.Items.Add(activeStateLog.getLastEntry().Key.ToString() + " | new state:  " + activeStateLog.getLastEntry().Value.newState + " | time in state " + activeStateLog.getLastEntry().Value.timeInState);
            }
           
            
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        
    }
}
