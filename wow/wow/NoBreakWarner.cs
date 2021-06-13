﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace wow
{
    public class NoBreakWarner : IObserver
    {
        public enum BreakRequestUserResponse{ ACCEPT, SNOOZE, DISMISS};
        private Timer noBreakTimer = new Timer();

        ConfigIntParameter minutesNoBreakWarningParam = new ConfigIntParameter("MinutesNoBreakWarning", 1);
        ConfigIntParameter minutesSnoozeTimeParam = new ConfigIntParameter("MinutesSnoozeTime", 1);
        ConfigContainer configContainer = new ConfigContainer("NoBreakWarner");

        public NoBreakWarner()
        {
            List<ConfigParameter> parameters = new List<ConfigParameter>();
            parameters.Add(minutesNoBreakWarningParam);
            parameters.Add(minutesSnoozeTimeParam);
            configContainer.setParameters(parameters);

            noBreakTimer.Interval = minutesNoBreakWarningParam.getValue() * 60 * 1000;
            TopicBroker.subscribeTopic("ACTIVITY_STATE_CHANGE_EVENT", this);
            TopicBroker.subscribeTopic("SYSTEM_STATE_EVENT", this);
            noBreakTimer.Tick += NoBreakTimer_Tick;
        }

        private void NoBreakTimer_Tick(object sender, EventArgs e)
        {
            NoBreakWarningForm noBreakWarningForm = new NoBreakWarningForm(minutesNoBreakWarningParam.getValue());
            noBreakWarningForm.FormClosed += NoBreakWarningForm_FormClosed;
            noBreakWarningForm.Show();
        }

        private void NoBreakWarningForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            BreakRequestUserResponse response = ((NoBreakWarningForm)sender).Response;
            switch (response)
            {
                case BreakRequestUserResponse.ACCEPT:
                    UserBreak.Instance.startBreak();
                    break;
                case BreakRequestUserResponse.SNOOZE:
                    noBreakTimer.Stop();
                    noBreakTimer.Interval = minutesSnoozeTimeParam.getValue() * 60 * 1000;
                    noBreakTimer.Start();
                    break;
                case BreakRequestUserResponse.DISMISS:
                    noBreakTimer.Stop();
                    noBreakTimer.Interval = minutesNoBreakWarningParam.getValue() * 60 * 1000;
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
                        UserBreak.Instance.stopBreak();
                        noBreakTimer.Start();
                        break;
                    case ActivityWatcher.activityState_t.IDLE:
                        break;
                    case ActivityWatcher.activityState_t.INACTIVE:
                        noBreakTimer.Stop();
                        break;
                }

                if (subject is SystemStateHandler systemStateHandler) 
                {
                    switch (systemStateHandler.StateTranstition) 
                    {
                        case SystemStateHandler.state_transtition_t.TO_INACTIVE:
                            UserBreak.Instance.startBreak();
                            break;
                    }

                }
            }


        }


    }
}
