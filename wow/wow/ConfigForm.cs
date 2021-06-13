using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace wow
{
    public partial class ConfigForm : Form
    {
        public ConfigForm()
        {
            InitializeComponent();
            this.Icon = Configuration.getApplicationIcon();
            listViewParameters.View = View.Details;
            listViewParameters.LabelEdit = true;
            listViewParameters.MultiSelect = false;

            listViewParameters.SelectedIndexChanged += new EventHandler(listViewSelectionChangedEventHandler);
            populateListBox();
            populateListView();


        }

        private void listViewSelectionChangedEventHandler(object sender, EventArgs e)
        {
      
        }

        private void populateListView()
        {
            var configObjects = Configuration.getConfigObjects();


            int index = (listBoxConfigObjects.SelectedIndex == -1) ? 0 : listBoxConfigObjects.SelectedIndex;
            ConfigContainer configurable = Configuration.getConfigObjectbyID((string)listBoxConfigObjects.Items[index]);
            listViewParameters.Clear();
            listViewParameters.Columns.Add("ConfigurationName", 200);
            listViewParameters.Columns.Add("Value", 100);
            foreach (var parameter in configurable.getParameters())
            {
                addRow(parameter.getID(), parameter.getValue());
            }


        }

        private void populateListBox() 
        {
            listBoxConfigObjects.Items.Clear();
            var configObjects = Configuration.getConfigObjects();

            foreach (var confO in configObjects)
            {
                listBoxConfigObjects.Items.Add(confO.ID);
            }

            
        }

        private void addRow(string name, string value) 
        {
            listViewParameters.Items.Add(new ListViewItem(new string[] { name, value }));
        }

  

        private void ConfigChangeDialogFormClosedHandler(object sender, EventArgs e)
        {
            populateListView();
        }

        private void listBoxConfigObjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            populateListView();
        }

        private void button_editParameter_Click(object sender, EventArgs e)
        {
            var dict = Configuration.getAllEntrys();
            try
            {
                int selectedIndex = listViewParameters.SelectedIndices[0];
                string key = listViewParameters.Items[selectedIndex].SubItems[0].Text;

                ConfigChangeDialogForm configChanger = new ConfigChangeDialogForm(key, dict[key]);
                configChanger.FormClosed += ConfigChangeDialogFormClosedHandler;
                configChanger.Show();
            }
            catch { }
        }

        private void buttonEditWidget_Click(object sender, EventArgs e)
        {
           var confobject = Configuration.getConfigObjectbyID(listBoxConfigObjects.SelectedItem.ToString());
            if (confobject.ID.Contains("Widget")) 
            {
                WidgetSizeConfigForm widgetConfigForm = new WidgetSizeConfigForm(confobject);
                widgetConfigForm.FormClosed += ConfigChangeDialogFormClosedHandler;
                widgetConfigForm.Show();
            }
        }
    }
}
