using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace wow
{
    public class UserBreak : IObserver
    {
        [DllImport("user32.dll")]
        private static extern bool LockWorkStation();

        private LockScreenController individualLockScreen = LockScreenController.Instance;
        private Timer updateTimer = new Timer();
        private Stopwatch stopwatch = new Stopwatch();
        private static bool breakStarted = false;
        private static bool subscribedToSystemStateHandler = false;

        private string MinutesMinimumBreakTimeString = "MinutesMinimumBreakTime";
        private int millisecondsMinimumBreakTime;
        private TextWidget textWidget = new TextWidget();

        public UserBreak()
        {
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
                new Configuration().addConfigEntry(MinutesMinimumBreakTimeString, 1);
                millisecondsMinimumBreakTime = Int32.Parse(new Configuration().getValueString(MinutesMinimumBreakTimeString)) * 60 * 1000;
                updateTimer.Interval = 5 * 1000;
                updateTimer.Tick += UpdateTimer_Tick;
                updateTimer.Start();
                stopwatch.Start();
                individualLockScreen.setInformationtalLockscreen();
                LockWorkStation();
            }
        }

        private  void UpdateTimer_Tick(object sender, EventArgs e)
        {
            var elapsedTime = stopwatch.Elapsed;
            textWidget.Text = elapsedTime.ToString();
            if (elapsedTime <= new TimeSpan(0,0,0,0, millisecondsMinimumBreakTime)) 
            {
                textWidget.Textcolor = System.Drawing.Color.DarkRed;
                individualLockScreen.setInformationtalLockscreen();
            }
            else
            {
                textWidget.Textcolor = System.Drawing.Color.Green;
                individualLockScreen.setInformationtalLockscreen();
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
                systemStateHandler.Detach(this);
            }
        }
    }
}
