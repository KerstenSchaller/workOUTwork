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
    public partial class ConfigForm : Form
    {
        Configuration config = new Configuration();
        public ConfigForm()
        {
            InitializeComponent();
            listView1.View = View.Details;
            listView1.LabelEdit = true;
            listView1.MultiSelect = false;

            listView1.SelectedIndexChanged += new EventHandler(listViewSelectionChangedEventHandler);
            populate(); 
      
        }

        private void listViewSelectionChangedEventHandler(object sender, EventArgs e)
        {
      
        }

        private void populate() 
        {
            listView1.Clear();
            listView1.Columns.Add("ConfigurationName", 200);
            listView1.Columns.Add("Value", 100);
            var dict = config.getAllEntrys();
            foreach (KeyValuePair<string, string> kv in dict) 
            {
                addRow(kv.Key, kv.Value);
            }
        }

        private void addRow(string name, string value) 
        {
            listView1.Items.Add(new ListViewItem(new string[] { name, value }));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Configuration config = new Configuration();
            var dict = config.getAllEntrys();
            int selectedIndex = listView1.SelectedIndices[0];
            string key = listView1.Items[selectedIndex].SubItems[0].Text;

            ConfigChangeDialogForm configChanger = new ConfigChangeDialogForm(key, dict[key]);
            configChanger.FormClosed += ConfigChangeDialogFormClosedHandler;
            configChanger.Show();
        }

        private void ConfigChangeDialogFormClosedHandler(object sender, EventArgs e)
        {
            populate();
        }
    }
}
