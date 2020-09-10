namespace Unlakki.Bns.Launcher
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.settingsPanel = new System.Windows.Forms.Panel();
            this.argsTextBox = new System.Windows.Forms.TextBox();
            this.saveSettingButton = new System.Windows.Forms.Button();
            this.useAllAvailableCoresCheckbox = new System.Windows.Forms.CheckBox();
            this.noTextureStreamingCheckbox = new System.Windows.Forms.CheckBox();
            this.settingsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // settingsPanel
            // 
            this.settingsPanel.Controls.Add(this.argsTextBox);
            this.settingsPanel.Controls.Add(this.noTextureStreamingCheckbox);
            this.settingsPanel.Controls.Add(this.useAllAvailableCoresCheckbox);
            this.settingsPanel.Controls.Add(this.saveSettingButton);
            this.settingsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.settingsPanel.Location = new System.Drawing.Point(0, 0);
            this.settingsPanel.Name = "settingsPanel";
            this.settingsPanel.Size = new System.Drawing.Size(240, 99);
            this.settingsPanel.TabIndex = 4;
            // 
            // argsTextBox
            // 
            this.argsTextBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.argsTextBox.Location = new System.Drawing.Point(0, 79);
            this.argsTextBox.Name = "argsTextBox";
            this.argsTextBox.Size = new System.Drawing.Size(240, 20);
            this.argsTextBox.TabIndex = 3;
            this.argsTextBox.TextChanged += new System.EventHandler(this.argsTextBox_TextChanged);
            // 
            // saveSettingButton
            // 
            this.saveSettingButton.AutoSize = true;
            this.saveSettingButton.Location = new System.Drawing.Point(189, 50);
            this.saveSettingButton.Name = "saveSettingButton";
            this.saveSettingButton.Size = new System.Drawing.Size(42, 23);
            this.saveSettingButton.TabIndex = 0;
            this.saveSettingButton.Text = "Save";
            this.saveSettingButton.UseVisualStyleBackColor = true;
            this.saveSettingButton.Click += new System.EventHandler(this.saveSettingButton_Click);
            // 
            // useAllAvailableCoresCheckbox
            // 
            this.useAllAvailableCoresCheckbox.AutoSize = true;
            this.useAllAvailableCoresCheckbox.Location = new System.Drawing.Point(12, 3);
            this.useAllAvailableCoresCheckbox.Name = "useAllAvailableCoresCheckbox";
            this.useAllAvailableCoresCheckbox.Size = new System.Drawing.Size(124, 17);
            this.useAllAvailableCoresCheckbox.TabIndex = 5;
            this.useAllAvailableCoresCheckbox.Text = "-useallavailablecores";
            this.useAllAvailableCoresCheckbox.UseVisualStyleBackColor = true;
            this.useAllAvailableCoresCheckbox.CheckedChanged += new System.EventHandler(this.useAllAvailableCoresCheckbox_CheckedChanged);
            // 
            // noTextureStreamingCheckbox
            // 
            this.noTextureStreamingCheckbox.AutoSize = true;
            this.noTextureStreamingCheckbox.Location = new System.Drawing.Point(12, 26);
            this.noTextureStreamingCheckbox.Name = "noTextureStreamingCheckbox";
            this.noTextureStreamingCheckbox.Size = new System.Drawing.Size(118, 17);
            this.noTextureStreamingCheckbox.TabIndex = 6;
            this.noTextureStreamingCheckbox.Text = "-notexturestreaming";
            this.noTextureStreamingCheckbox.UseVisualStyleBackColor = true;
            this.noTextureStreamingCheckbox.CheckedChanged += new System.EventHandler(this.noTextureStreamingCheckbox_CheckedChanged);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(240, 99);
            this.Controls.Add(this.settingsPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "bns-ru: Settings";
            this.settingsPanel.ResumeLayout(false);
            this.settingsPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel settingsPanel;
        private System.Windows.Forms.TextBox argsTextBox;
        private System.Windows.Forms.Button saveSettingButton;
        private System.Windows.Forms.CheckBox noTextureStreamingCheckbox;
        private System.Windows.Forms.CheckBox useAllAvailableCoresCheckbox;
    }
}