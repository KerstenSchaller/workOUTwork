using System;
using System.Diagnostics;
using System.Windows.Forms;


namespace wow
{
    public class ActivityWatcher : SubjectImplementation, IObserver
    {
        public enum activityState_t { ACTIVE, IDLE, INACTIVE };
        private activityState_t activityState = activityState_t.ACTIVE;
        private Timer activeIdleTimeout = new Timer();
        private Stopwatch timeInState = new Stopwatch();
        private TimeSpan timeInLastState;
        private int timeToIdleSeconds = 15 * 1000;
        private int timeToInactiveSeconds = 30 * 1000;

        public void start() 
        {
            timeInState.Start();
            TopicBroker.subscribeTopic("mousekeyhandler.event", this);
            activeIdleTimeout.Interval = timeToIdleSeconds;
            activeIdleTimeout.Tick += new EventHandler(idleTimeoutCallback);
            activeIdleTimeout.Start();
            this.Notify();
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
            activeIdleTimeout.Interval = timeToInactiveSeconds;
            activeIdleTimeout.Start();
            timeInLastState = timeInState.Elapsed;
            timeInState.Restart();
            activityState = activityState_t.IDLE;
            this.Notify();
        }

        private void ToActive()
        {
            activeIdleTimeout.Stop();
            activeIdleTimeout.Interval = timeToIdleSeconds;
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
                    activeIdleTimeout.Interval = timeToIdleSeconds;
                    activeIdleTimeout.Start();
                }
            }

            if (subject is SystemStateHandler) 
            {
                switch (((SystemStateHandler)subject).StateTranstition) 
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

            /*
            if (subject is FocusWatcher)
            {
                //stop timer if vmware gets active because no keyboard and mouse can be detected then
                if (((FocusWatcher)subject).ActiveWindowTitle.Contains("VMware"))
                {
                    if (activeIdleTimeout.Enabled)
                    {
                        activeIdleTimeout.Stop();
                        Console.WriteLine("Stopping idle timeout");
                    }
                }
                else
                {
                    //Start timer again if needed
                    if (!activeIdleTimeout.Enabled)
                    {
                        //activeIdleTimeout.Start();
                        Console.WriteLine("Starting idle timeout");
                    }
                }
            }
            */




        }


    }
}
