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
    public partial class Form1 : Form, IObserver
    {
        ActivityWatcher activityWatcher = new ActivityWatcher();

        public Form1()
        {
            InitializeComponent();

            activityWatcher.Attach(this);
        }

        public void Update(ISubject subject)
        {
            if (activityWatcher.ActivityState == ActivityWatcher.activityState_t.ACTIVE) textBox1.Text = "Active";
            if (activityWatcher.ActivityState == ActivityWatcher.activityState_t.IDLE) textBox1.Text = "Idle";
            textBox2.Text = activityWatcher.TimeInState.ToString();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
