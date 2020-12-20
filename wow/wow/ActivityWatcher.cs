using System;
using System.Diagnostics;
using System.Windows.Forms;


namespace wow
{
    public class ActivityWatcher : SubjectImplementation, IObserver
    {
        public enum activityState_t { ACTIVE, IDLE };
        private activityState_t activityState = activityState_t.IDLE;
        private MouseKeyHandler mouseKeyHandler = new MouseKeyHandler();
        private SystemStateHandler systemStateHandler = new SystemStateHandler();
        FocusWatcher focuswatcher = new FocusWatcher();
        private Timer activeIdleTimeout = new Timer();
        private Timer ObserUpdateTimer = new Timer();
        private Stopwatch timeInState = new Stopwatch();
        private TimeSpan timeInLastState;

        public ActivityWatcher() 
        {
            mouseKeyHandler.Attach(this);
            systemStateHandler.Attach(this);
            focuswatcher.Attach(this);
            timeInState.Start();

            activeIdleTimeout.Interval = 10*1000;
            activeIdleTimeout.Tick += new EventHandler(idleTimeoutCallback);
            activeIdleTimeout.Start();

            this.idleToActive();
        }

        private void idleTimeoutCallback(object sender, EventArgs e) 
        {
            this.activeToIdle();
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

        private void activeToIdle() 
        {
            activeIdleTimeout.Stop();
            timeInLastState = timeInState.Elapsed;
            timeInState.Restart();
            activityState = activityState_t.IDLE;
            this.Notify();
        }

        private void idleToActive()
        {
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
                if (activityState == activityState_t.IDLE)
                {
                    this.idleToActive();
                }
                else 
                { 
                    activeIdleTimeout.Stop();
                    activeIdleTimeout.Start();
                }
            }

            if (subject is SystemStateHandler) 
            {
                switch (systemStateHandler.StateTranstition) 
                {
                    case SystemStateHandler.state_transtition_t.ACTIVE_TO_IDLE:
                        this.activeToIdle();
                        break;
                    case SystemStateHandler.state_transtition_t.IDLE_TO_ACTIVE:
                        this.idleToActive();
                        break;
                    default:
                        break;
                }
            }
            
            if (subject is FocusWatcher)
            {
                //stop timer if vmware gets active because no keyboard and mouse can be detected then
                if (focuswatcher.ActiveWindowTitle.Contains("VMware"))
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


        }


    }
}
