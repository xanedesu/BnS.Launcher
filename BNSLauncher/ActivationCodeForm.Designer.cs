namespace Unlakki.Bns.Launcher
{
    partial class ActivationCodeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ActivationCodeForm));
            this.comfirmationCodePanel = new System.Windows.Forms.Panel();
            this.message = new System.Windows.Forms.Label();
            this.activationCodeTextBox = new System.Windows.Forms.TextBox();
            this.comfirmationCodePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // comfirmationCodePanel
            // 
            this.comfirmationCodePanel.Controls.Add(this.message);
            this.comfirmationCodePanel.Controls.Add(this.activationCodeTextBox);
            this.comfirmationCodePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comfirmationCodePanel.Location = new System.Drawing.Point(0, 0);
            this.comfirmationCodePanel.Name = "comfirmationCodePanel";
            this.comfirmationCodePanel.Size = new System.Drawing.Size(240, 99);
            this.comfirmationCodePanel.TabIndex = 6;
            // 
            // message
            // 
            this.message.Dock = System.Windows.Forms.DockStyle.Top;
            this.message.Location = new System.Drawing.Point(0, 0);
            this.message.Name = "message";
            this.message.Size = new System.Drawing.Size(240, 32);
            this.message.TabIndex = 1;
            this.message.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // activationCodeTextBox
            // 
            this.activationCodeTextBox.Location = new System.Drawing.Point(12, 41);
            this.activationCodeTextBox.Name = "activationCodeTextBox";
            this.activationCodeTextBox.Size = new System.Drawing.Size(216, 20);
            this.activationCodeTextBox.TabIndex = 0;
            this.activationCodeTextBox.TextChanged += new System.EventHandler(this.activationCodeTextBox_TextChanged);
            // 
            // ActivationCodeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(240, 99);
            this.Controls.Add(this.comfirmationCodePanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ActivationCodeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "bns-ru: Enter activation code";
            this.comfirmationCodePanel.ResumeLayout(false);
            this.comfirmationCodePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel comfirmationCodePanel;
        private System.Windows.Forms.Label message;
        private System.Windows.Forms.TextBox activationCodeTextBox;
    }
}