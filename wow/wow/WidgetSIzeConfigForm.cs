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
    public partial class WidgetSizeConfigForm : Form
    {
        ConfigContainer config;
        public WidgetSizeConfigForm(ConfigContainer _config)
        {
            config = _config;
            InitializeComponent();
            label1.Text = config.ID + " :Resize and move this window to the \n place where the widget should appear on the lockscreen.";
            updateLabels();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            var parameters = config.getParameters();
            foreach(var parameter in parameters)
            {
                if (parameter.getID() == "Widget_X_Position") 
                {
                    parameter.setValue(this.Location.X.ToString());
                    continue;
                }
                if (parameter.getID() == "Widget_Y_Position") 
                {
                    parameter.setValue(this.Location.Y.ToString());
                    continue;
                }
                if (parameter.getID() == "Widget_X_Size") 
                {
                    parameter.setValue(this.Width.ToString());
                    continue;
                }
                if (parameter.getID() == "Widget_Y_Size")
                {
                    parameter.setValue(this.Height.ToString());
                    continue;
                }
            }
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void WidgetSizeConfigForm_Resize(object sender, EventArgs e)
        {
            updateLabels();
        }

        private void WidgetSizeConfigForm_Move(object sender, EventArgs e)
        {
            updateLabels();
        }

        private void updateLabels() 
        {
            label2.Text = "width: " + this.Width + " height: " + this.Height;
            label3.Text = "x: "+ this.Location.X + " y: " + this.Location.Y;
        }
    }
}
