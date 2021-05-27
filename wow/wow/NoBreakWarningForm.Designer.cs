
namespace wow
{
    partial class NoBreakWarningForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.buttonDoBreak = new System.Windows.Forms.Button();
            this.buttonDismiss = new System.Windows.Forms.Button();
            this.buttonZnoze = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "You didnt have a break for: ";
            // 
            // buttonDoBreak
            // 
            this.buttonDoBreak.Location = new System.Drawing.Point(12, 94);
            this.buttonDoBreak.Name = "buttonDoBreak";
            this.buttonDoBreak.Size = new System.Drawing.Size(75, 23);
            this.buttonDoBreak.TabIndex = 1;
            this.buttonDoBreak.Text = "Do break";
            this.buttonDoBreak.UseVisualStyleBackColor = true;
            this.buttonDoBreak.Click += new System.EventHandler(this.buttonDoBreak_Click);
            // 
            // buttonDismiss
            // 
            this.buttonDismiss.Location = new System.Drawing.Point(274, 94);
            this.buttonDismiss.Name = "buttonDismiss";
            this.buttonDismiss.Size = new System.Drawing.Size(75, 23);
            this.buttonDismiss.TabIndex = 2;
            this.buttonDismiss.Text = "Dismiss";
            this.buttonDismiss.UseVisualStyleBackColor = true;
            this.buttonDismiss.Click += new System.EventHandler(this.buttonDismiss_Click);
            // 
            // buttonZnoze
            // 
            this.buttonZnoze.Location = new System.Drawing.Point(143, 94);
            this.buttonZnoze.Name = "buttonZnoze";
            this.buttonZnoze.Size = new System.Drawing.Size(75, 23);
            this.buttonZnoze.TabIndex = 3;
            this.buttonZnoze.Text = "Snooze";
            this.buttonZnoze.UseVisualStyleBackColor = true;
            this.buttonZnoze.Click += new System.EventHandler(this.buttonZnoze_Click);
            // 
            // NoBreakWarningForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(377, 133);
            this.Controls.Add(this.buttonZnoze);
            this.Controls.Add(this.buttonDismiss);
            this.Controls.Add(this.buttonDoBreak);
            this.Controls.Add(this.label1);
            this.Name = "NoBreakWarningForm";
            this.Text = "NoBreakWarningForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonDoBreak;
        private System.Windows.Forms.Button buttonDismiss;
        private System.Windows.Forms.Button buttonZnoze;
    }
}