using System;
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
        private int timeToIdleMilliSeconds = 5 * 60 * 1000;
        private int timeToInactiveMilliSeconds = 20 * 60 * 1000;

        public ActivityWatcher() 
        {
            Configuration config = new Configuration();
            timeToIdleMilliSeconds = config.secondsToIdle * 1000;
            timeToInactiveMilliSeconds = config.secondsToInactive * 1000;
            TopicBroker.publishTopic("ACTIVITY_STATE_CHANGE_EVENT", this);
        }

        public void start() 
        {
            timeInState.Start();
            TopicBroker.subscribeTopic("MOUSE_KEY_EVENT", this);
            TopicBroker.subscribeTopic("SYSTEM_STATE_EVENT", this);
            activeIdleTimeout.Interval = timeToIdleMilliSeconds;
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
            
            activeIdleTimeout.Stop();
            timeInLastState = timeInState.Elapsed;
            timeInState.Restart();
            activityState = activityState_t.INACTIVE;
            this.Notify();
        }

        private void ToIdle() 
        {
            activeIdleTimeout.Stop();
            activeIdleTimeout.Interval = timeToInactiveMilliSeconds;
            activeIdleTimeout.Start();
            timeInLastState = timeInState.Elapsed;
            timeInState.Restart();
            activityState = activityState_t.IDLE;
            this.Notify();
        }

        private void ToActive()
        {
            activeIdleTimeout.Stop();
            activeIdleTimeout.Interval = timeToIdleMilliSeconds;
            activeIdleTimeout.Start();
            timeInLastState = timeInState.Elapsed;
            timeInState.Restart();
            activityState = activityState_t.ACTIVE;
            this.Notify();
        }

        public void Update(ISubject subject)
        {
            if (subject is MouseKeyHandler) 
            {
                if (activityState != activityState_t.ACTIVE)
                {
                    this.ToActive();
                }
                else
                {
                    activeIdleTimeout.Stop();
                    activeIdleTimeout.Interval = timeToIdleMilliSeconds;
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
