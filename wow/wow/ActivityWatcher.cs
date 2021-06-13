using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;


namespace wow
{
    public class ActivityWatcher : SubjectImplementation, IObserver
    {
        public enum activityState_t { ACTIVE, IDLE, INACTIVE };
        private activityState_t activityState = activityState_t.INACTIVE;
        private Timer activeIdleTimeout = new Timer();
        private Stopwatch timeInState = new Stopwatch();
        private TimeSpan timeInLastState;

        private bool sensitiveToMouseKeyHandler = true;

        ConfigIntParameter secondsToIdleParam = new ConfigIntParameter("secondsToIdle", 3 * 60 * 1000);
        ConfigIntParameter secondsToInactiveParam = new ConfigIntParameter("secondsToInactive", 5 * 60 * 1000);
        ConfigContainer configContainer = new ConfigContainer("ActivityWatcher");

        public ActivityWatcher() 
        {
            List<ConfigParameter> parameters = new List<ConfigParameter>();
            parameters.Add(secondsToIdleParam);
            parameters.Add(secondsToInactiveParam);
            configContainer.setParameters(parameters);

            TopicBroker.publishTopic("ACTIVITY_STATE_CHANGE_EVENT", this);
        }

        public void start() 
        {
            timeInState.Start();
            TopicBroker.subscribeTopic("MOUSE_KEY_EVENT", this);
            TopicBroker.subscribeTopic("SYSTEM_STATE_EVENT", this);
            activeIdleTimeout.Interval = secondsToIdleParam.getValue()*1000;
            activeIdleTimeout.Tick += new EventHandler(idleTimeoutCallback);
            activeIdleTimeout.Start();
            
        }

        private void idleTimeoutCallback(object sender, EventArgs e) 
        {
            if (this.activityState == activityState_t.ACTIVE)
            {
                this.ToIdle();
            }
            else
            {
                if (this.activityState == activityState_t.IDLE)
                {
                    this.ToInactive();
                }
            }
        }

        public activityState_t ActivityState 
        {
            get { return activityState; }
        }

        public TimeSpan TimeInLastState 
        {
            get { return timeInLastState; }
        }

        public TimeSpan TimeInState
        {
            get { return timeInState.Elapsed; }
        }

        private void ToInactive() 
        {
            sensitiveToMouseKeyHandler = false;
            activeIdleTimeout.Stop();
            timeInLastState = timeInState.Elapsed;
            timeInState.Restart();
            activityState = activityState_t.INACTIVE;
            this.Notify();
        }

        private void ToIdle() 
        {
            activeIdleTimeout.Stop();
            activeIdleTimeout.Interval = secondsToInactiveParam.getValue()*1000;
            activeIdleTimeout.Start();
            timeInLastState = timeInState.Elapsed;
            timeInState.Restart();
            activityState = activityState_t.IDLE;
            this.Notify();
        }

        private void ToActive()
        {
            sensitiveToMouseKeyHandler = true;
            activeIdleTimeout.Stop();
            activeIdleTimeout.Interval = secondsToIdleParam.getValue() * 1000; ;
            activeIdleTimeout.Start();
            timeInLastState = timeInState.Elapsed;
            timeInState.Restart();
            activityState = activityState_t.ACTIVE;
            this.Notify();
        }

        public void Update(ISubject subject)
        {
            if ((subject is MouseKeyHandler) && (sensitiveToMouseKeyHandler == true)) 
            {
                if (activityState != activityState_t.ACTIVE)
                {
                    this.ToActive();
                }
                else
                {
                    activeIdleTimeout.Stop();
                    activeIdleTimeout.Interval = secondsToIdleParam.getValue() * 1000; ;
                    activeIdleTimeout.Start();
                }
            }

            if (subject is SystemStateHandler systemStateHandler) 
            {
                switch (systemStateHandler.StateTranstition)
                {
                    case SystemStateHandler.state_transtition_t.TO_INACTIVE:
                        this.ToInactive();
                        break;
                    case SystemStateHandler.state_transtition_t.TO_ACTIVE:
                        this.ToActive();
                        break;
                    default:
                        break;
                }
            }

        }



    }
}
