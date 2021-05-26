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
    public partial class ConfigChangeDialogForm : Form
    {
        string key;
        public ConfigChangeDialogForm(string _key, string _value)
        {
            InitializeComponent();
            this.Icon = new Configuration().getApplicationIcon();
            key = _key;
            label1.Text = key;
            textBox_value.Text = _value;
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            Configuration config = new Configuration();
            config.changeConfig(key, textBox_value.Text);
            this.Close();
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
