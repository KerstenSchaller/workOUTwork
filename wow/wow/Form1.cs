using System;
using System.Windows.Forms;

namespace wow
{
    public partial class Form1 : Form, IObserver
    {
        ActivityWatcher activityWatcher = new ActivityWatcher();
        FocusWatcher focusWatcher = new FocusWatcher();
        ActiveStateLog activeStateLog;


        public Form1()
        {
            InitializeComponent();

            activeStateLog = new ActiveStateLog(activityWatcher);

            activityWatcher.Attach(this);
            focusWatcher.Attach(this);



        }

        public void Update(ISubject subject)
        {
            if (activityWatcher.ActivityState == ActivityWatcher.activityState_t.ACTIVE) textBox1.Text = "Active";
            if (activityWatcher.ActivityState == ActivityWatcher.activityState_t.IDLE) textBox1.Text = "Idle";
            textBox2.Text = activityWatcher.TimeInState.ToString();

            textBox3.Text = focusWatcher.ActiveWindowTitle;




        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        
    }
}
