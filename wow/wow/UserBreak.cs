using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace wow
{
    public class UserBreak : IObserver
    {
        [DllImport("user32.dll")]
        private static extern bool LockWorkStation();

        private IndividualLockScreen individualLockScreen = IndividualLockScreen.Instance;
        private Timer updateTimer = new Timer();
        private Stopwatch stopwatch = new Stopwatch();
        private static bool breakStarted = false;
        private static bool subscribedToSystemStateHandler = false;

        private string MinutesMinimumBreakTimeString = "MinutesMinimumBreakTime";
        private int millisecondsMinimumBreakTime;

        public UserBreak()
        {
            if (subscribedToSystemStateHandler == false)
            {
                TopicBroker.subscribeTopic("SYSTEM_STATE_EVENT", this);
                subscribedToSystemStateHandler = true;
            }
        }

        public async void startBreak()
        {
            if (!breakStarted)
            {
                new Configuration().addConfigEntry(MinutesMinimumBreakTimeString, 1);
                millisecondsMinimumBreakTime = Int32.Parse(new Configuration().getValueString(MinutesMinimumBreakTimeString)) * 60 * 1000;
                updateTimer.Interval = 5 * 1000;
                updateTimer.Tick += UpdateTimer_Tick;
                updateTimer.Start();
                stopwatch.Start();
                await individualLockScreen.setInformationtalLockscreen("", System.Drawing.Color.DarkRed);
                LockWorkStation();
            }
        }

        private  void UpdateTimer_Tick(object sender, EventArgs e)
        {
            var elapsedTime = stopwatch.Elapsed;
            if (elapsedTime <= new TimeSpan(0,0,0,0, millisecondsMinimumBreakTime)) 
            {
                individualLockScreen.setInformationtalLockscreen(elapsedTime.ToString(), System.Drawing.Color.DarkRed);
            }
            else
            {
                individualLockScreen.setInformationtalLockscreen(elapsedTime.ToString(), System.Drawing.Color.Green);
            }
            updateTimer.Start();
        }

        public void Update(ISubject subject)
        {
            if (subject is SystemStateHandler systemStateHandler) 
            {
                if (breakStarted == true && systemStateHandler.StateTranstition == SystemStateHandler.state_transtition_t.TO_ACTIVE) 
                {
                    breakStarted = false;
                }
            }
        }
    }
}
