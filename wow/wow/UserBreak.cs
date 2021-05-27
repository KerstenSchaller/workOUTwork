using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace wow
{
    public class UserBreak
    {
        [DllImport("user32.dll")]
        private static extern bool LockWorkStation();

        IndividualLockScreen individualLockScreen = new IndividualLockScreen();
        Timer breakTimer = new Timer();
        Timer updateTimer = new Timer();
        Stopwatch stopwatch = new Stopwatch();
        TimeSpan minimumBreakTime;

        private string MinutesMinimumBreakTimeString = "MinutesMinimumBreakTime";
        private int millisecondsMinimumBreakTime;


        public void startBreak() 
        {
            new Configuration().addConfigEntry(MinutesMinimumBreakTimeString, 1);
            millisecondsMinimumBreakTime = Int32.Parse(new Configuration().getValueString(MinutesMinimumBreakTimeString)) * 60 * 1000;
            updateTimer.Interval = 5 * 1000;
            updateTimer.Tick += UpdateTimer_Tick;
            updateTimer.Start();
            stopwatch.Start();
            individualLockScreen.setInformationtalLockscreen("", System.Drawing.Color.DarkRed);
            LockWorkStation();
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
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
    }
}
