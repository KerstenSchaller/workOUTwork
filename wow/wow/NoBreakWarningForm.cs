using System;
using System.Windows.Forms;

namespace wow
{
    public partial class NoBreakWarningForm : Form
    {
        private NoBreakWarner.BreakRequestUserResponse response;

        public NoBreakWarner.BreakRequestUserResponse Response 
        {
            get { return response; }
        }

        public NoBreakWarningForm()
        {
            InitializeComponent();
            if (Configuration.getNoBreakWarningTimeMinutes() > 60)
            {
                label1.Text = "You didnt have a break for " + Configuration.getNoBreakWarningTimeMinutes() + " minutes";
            }
            else
            {
                int hours = (int)(Configuration.getNoBreakWarningTimeMinutes() / 60);
                int minutes = Configuration.getNoBreakWarningTimeMinutes() - hours * 60;
                label1.Text = "You didnt have a break for " + hours + " hours and "+ minutes + " minutes";
            }
            
        }

        private void buttonDoBreak_Click(object sender, EventArgs e)
        {
            response = NoBreakWarner.BreakRequestUserResponse.ACCEPT;
            this.Close();
        }

        private void buttonZnoze_Click(object sender, EventArgs e)
        {
            response = NoBreakWarner.BreakRequestUserResponse.SNOOZE;
            this.Close();
        }

        private void buttonDismiss_Click(object sender, EventArgs e)
        {
            response = NoBreakWarner.BreakRequestUserResponse.DISMISS;
            this.Close();
        }
    }
}
