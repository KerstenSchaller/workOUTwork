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
    public partial class Form1 : Form, IObserver
    {
        TimePeriod timePeriod;
        SystemStateHandler systemStateHandler;
        public Form1()
        {
            InitializeComponent();

            MouseKeyHandler mouseKeyHandler = new MouseKeyHandler();
            mouseKeyHandler.Attach(this);
            
            timePeriod = new TimePeriod();

            systemStateHandler  = new SystemStateHandler();
            systemStateHandler.Attach(this);

        }

        public void Update(ISubject subject)
        {
            textBox1.Text = (timePeriod.MouseCount + timePeriod.KeyCount).ToString();
            textBox2.Text = timePeriod.MouseCount.ToString();
            textBox3.Text = timePeriod.KeyCount.ToString();
            textBox4.Text = systemStateHandler.lock_count.ToString();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
