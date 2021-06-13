
namespace wow
{
    partial class ConfigForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listViewParameters = new System.Windows.Forms.ListView();
            this.button_editParameter = new System.Windows.Forms.Button();
            this.listBoxConfigObjects = new System.Windows.Forms.ListBox();
            this.buttonEditWidget = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listViewParameters
            // 
            this.listViewParameters.HideSelection = false;
            this.listViewParameters.Location = new System.Drawing.Point(133, 12);
            this.listViewParameters.Name = "listViewParameters";
            this.listViewParameters.Size = new System.Drawing.Size(312, 251);
            this.listViewParameters.TabIndex = 0;
            this.listViewParameters.UseCompatibleStateImageBehavior = false;
            // 
            // button_editParameter
            // 
            this.button_editParameter.Location = new System.Drawing.Point(133, 274);
            this.button_editParameter.Name = "button_editParameter";
            this.button_editParameter.Size = new System.Drawing.Size(308, 23);
            this.button_editParameter.TabIndex = 1;
            this.button_editParameter.Text = "Edit Parameter";
            this.button_editParameter.UseVisualStyleBackColor = true;
            this.button_editParameter.Click += new System.EventHandler(this.button_editParameter_Click);
            // 
            // listBoxConfigObjects
            // 
            this.listBoxConfigObjects.FormattingEnabled = true;
            this.listBoxConfigObjects.Location = new System.Drawing.Point(12, 12);
            this.listBoxConfigObjects.Name = "listBoxConfigObjects";
            this.listBoxConfigObjects.Size = new System.Drawing.Size(115, 251);
            this.listBoxConfigObjects.TabIndex = 2;
            this.listBoxConfigObjects.SelectedIndexChanged += new System.EventHandler(this.listBoxConfigObjects_SelectedIndexChanged);
            // 
            // buttonEditWidget
            // 
            this.buttonEditWidget.Location = new System.Drawing.Point(12, 274);
            this.buttonEditWidget.Name = "buttonEditWidget";
            this.buttonEditWidget.Size = new System.Drawing.Size(115, 23);
            this.buttonEditWidget.TabIndex = 3;
            this.buttonEditWidget.Text = "Edit Widget";
            this.buttonEditWidget.UseVisualStyleBackColor = true;
            this.buttonEditWidget.Click += new System.EventHandler(this.buttonEditWidget_Click);
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 308);
            this.Controls.Add(this.buttonEditWidget);
            this.Controls.Add(this.listBoxConfigObjects);
            this.Controls.Add(this.button_editParameter);
            this.Controls.Add(this.listViewParameters);
            this.Name = "ConfigForm";
            this.Text = "ConfigForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewParameters;
        private System.Windows.Forms.Button button_editParameter;
        private System.Windows.Forms.ListBox listBoxConfigObjects;
        private System.Windows.Forms.Button buttonEditWidget;
    }
}