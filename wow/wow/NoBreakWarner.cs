using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wow
{
    class NoBreakWarner : IObserver
    {
        private Timer noBreakTimer = new Timer();

        public NoBreakWarner()
        {
            Configuration config = new Configuration();
            config.addConfigEntry("MinutesNoBreakWarning", 1);
            noBreakTimer.Interval = new Configuration().getNoBreakWarningTimeMinutes() * 60 * 1000;
            TopicBroker.subscribeTopic("ACTIVITY_STATE_CHANGE_EVENT", this);
            noBreakTimer.Tick += NoBreakTimer_Tick;
        }

        private void NoBreakTimer_Tick(object sender, EventArgs e)
        {
            NoBreakWarningForm noBreakWarningForm = new NoBreakWarningForm();
            noBreakWarningForm.Show();
        }

        public void Update(ISubject subject)
        {
            if (subject is ActivityWatcher activityWatcher) 
            {
                switch(activityWatcher.ActivityState) 
                {
                    case ActivityWatcher.activityState_t.ACTIVE:
                        noBreakTimer.Start();
                        break;
                    case ActivityWatcher.activityState_t.IDLE:
                        break;
                    case ActivityWatcher.activityState_t.INACTIVE:
                        noBreakTimer.Stop();
                        break;
                }
            }
        }


    }
}
