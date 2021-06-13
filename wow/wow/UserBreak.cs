using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace wow
{
    public class UserBreak : IObserver
    {
        [DllImport("user32.dll")]
        private static extern bool LockWorkStation();

        private static UserBreak instance = null;
        private static readonly object padlock = new object();
        public static UserBreak Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new UserBreak();
                    }
                    return instance;
                }
            }
        }

        private LockScreenController individualLockScreen = LockScreenController.Instance;
        private Timer updateTimer = new Timer();
        private Stopwatch stopwatch = new Stopwatch();
        private static bool breakStarted = false;
        private static bool subscribedToSystemStateHandler = false;

        ConfigIntParameter minutesMinimumBreakTimeParam = new ConfigIntParameter("MinutesMinimumBreakTime", 5);
        ConfigIntParameter secondsDisplayUpdateParam = new ConfigIntParameter("secondsDisplayUpdate", 5);
        ConfigContainer configContainer = new ConfigContainer("UserBreak");

        private TextWidget textWidget = new TextWidget();

        private UserBreak()
        {
            List<ConfigParameter> parameters = new List<ConfigParameter>();
            parameters.Add(minutesMinimumBreakTimeParam);
            parameters.Add(secondsDisplayUpdateParam);
            configContainer.setParameters(parameters);

            updateTimer.Tick += UpdateTimer_Tick;
            ScreenImageComposer.Instance.attachWidget(textWidget);
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
                updateTimer.Interval = secondsDisplayUpdateParam.getValue() * 1000;
                updateTimer.Start();
                stopwatch.Start();
                await individualLockScreen.setInformationtalLockscreen();
                LockWorkStation();
            }
        }

        internal void stopBreak()
        {
            updateTimer.Stop();
            stopwatch.Stop();
            stopwatch.Reset();
            individualLockScreen.stop();
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            var elapsedTime = stopwatch.Elapsed;
            textWidget.Text = elapsedTime.ToString();
            if (elapsedTime <= new TimeSpan(0, minutesMinimumBreakTimeParam.getValue(), 0)) 
            {
                textWidget.Textcolor = System.Drawing.Color.DarkRed;
            }
            else
            {
                textWidget.Textcolor = System.Drawing.Color.Green;
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
                    updateTimer.Enabled = false;
                    systemStateHandler.Detach(this);
                }
                
            }
        }
    }
}
