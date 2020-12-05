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
        public Form1()
        {
            InitializeComponent();

            MouseKeyHandler mouseKeyHandler = new MouseKeyHandler();
            mouseKeyHandler.Attach(this);
        }

        uint cnt = 0;
        public void Update(ISubject subject)
        {


            textBox1.Text = cnt.ToString();
            cnt++;
        }
    }
}
