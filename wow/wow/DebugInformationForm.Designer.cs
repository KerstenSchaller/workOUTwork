﻿namespace wow
{
    partial class DebugInformationForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxState = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxWindowTitle = new System.Windows.Forms.TextBox();
            this.listboxactiveStateLog = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.labelStateLogCount = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBoxState
            // 
            this.textBoxState.Location = new System.Drawing.Point(140, 29);
            this.textBoxState.Name = "textBoxState";
            this.textBoxState.Size = new System.Drawing.Size(100, 20);
            this.textBoxState.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "State";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(39, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Active window title";
            // 
            // textBoxWindowTitle
            // 
            this.textBoxWindowTitle.Location = new System.Drawing.Point(140, 51);
            this.textBoxWindowTitle.Name = "textBoxWindowTitle";
            this.textBoxWindowTitle.Size = new System.Drawing.Size(792, 20);
            this.textBoxWindowTitle.TabIndex = 8;
            // 
            // listboxactiveStateLog
            // 
            this.listboxactiveStateLog.FormattingEnabled = true;
            this.listboxactiveStateLog.Location = new System.Drawing.Point(140, 96);
            this.listboxactiveStateLog.Name = "listboxactiveStateLog";
            this.listboxactiveStateLog.Size = new System.Drawing.Size(792, 199);
            this.listboxactiveStateLog.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "ActiveStateLog";
            // 
            // labelStateLogCount
            // 
            this.labelStateLogCount.AutoSize = true;
            this.labelStateLogCount.Location = new System.Drawing.Point(137, 80);
            this.labelStateLogCount.Name = "labelStateLogCount";
            this.labelStateLogCount.Size = new System.Drawing.Size(167, 13);
            this.labelStateLogCount.TabIndex = 11;
            this.labelStateLogCount.Text = "ActiveStateLog number of entrys: ";
            // 
            // DebugInformationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 347);
            this.Controls.Add(this.labelStateLogCount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listboxactiveStateLog);
            this.Controls.Add(this.textBoxWindowTitle);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxState);
            this.Name = "DebugInformationForm";
            this.Text = "DebugInformation";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxState;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxWindowTitle;
        private System.Windows.Forms.ListBox listboxactiveStateLog;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelStateLogCount;
    }
}
