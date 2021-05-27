using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wow
{
    public class NoBreakWarner : IObserver
    {
        public enum BreakRequestUserResponse{ ACCEPT, SNOOZE, DISMISS};
        private Timer noBreakTimer = new Timer();

        private string MinutesNoBreakWarningString = "MinutesNoBreakWarning";
        private string MinutesSnoozeTimeString = "MinutesSnoozeTime";

        private int millisecondsNoBreakWarning;
        private int millisecondsSnoozeTime;

        public NoBreakWarner()
        {
            Configuration config = new Configuration();
            config.addConfigEntry(MinutesNoBreakWarningString, 1);
            config.addConfigEntry(MinutesSnoozeTimeString, 1);
            millisecondsNoBreakWarning = Int32.Parse(new Configuration().getValueString(MinutesNoBreakWarningString)) * 60 * 1000;
            millisecondsSnoozeTime = Int32.Parse(new Configuration().getValueString(MinutesSnoozeTimeString)) * 60 * 1000;
            noBreakTimer.Interval = millisecondsNoBreakWarning;
            TopicBroker.subscribeTopic("ACTIVITY_STATE_CHANGE_EVENT", this);
            noBreakTimer.Tick += NoBreakTimer_Tick;
        }

        private void NoBreakTimer_Tick(object sender, EventArgs e)
        {
            NoBreakWarningForm noBreakWarningForm = new NoBreakWarningForm();
            noBreakWarningForm.FormClosed += NoBreakWarningForm_FormClosed;
            noBreakWarningForm.Show();
        }

        private void NoBreakWarningForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            BreakRequestUserResponse response = ((NoBreakWarningForm)sender).Response;
            switch (response)
            {
                case BreakRequestUserResponse.ACCEPT:
                    UserBreak ubreak = new UserBreak();
                    ubreak.startBreak();
                    break;
                case BreakRequestUserResponse.SNOOZE:
                    noBreakTimer.Stop();
                    noBreakTimer.Interval = millisecondsSnoozeTime;
                    noBreakTimer.Start();
                    break;
                case BreakRequestUserResponse.DISMISS:
                    noBreakTimer.Stop();
                    noBreakTimer.Interval = millisecondsNoBreakWarning;
                    noBreakTimer.Start();
                    break;
            }
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
