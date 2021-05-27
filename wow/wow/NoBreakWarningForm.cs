using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wow
{
    public partial class NoBreakWarningForm : Form
    {
        public NoBreakWarningForm()
        {
            InitializeComponent();
            Configuration configuration = new Configuration();
            if (configuration.getNoBreakWarningTimeMinutes() > 60)
            {
                label1.Text = "You didnt have a break for " + configuration.getNoBreakWarningTimeMinutes() + " minutes";
            }
            else
            {
                int hours = (int)(configuration.getNoBreakWarningTimeMinutes() / 60);
                int minutes = configuration.getNoBreakWarningTimeMinutes() - hours * 60;
                label1.Text = "You didnt have a break for " + hours + " hours and "+ minutes + " minutes";
            }
            
        }
    }
}
