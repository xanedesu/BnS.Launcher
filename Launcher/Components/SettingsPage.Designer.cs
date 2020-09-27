namespace Unlakki.Bns.Launcher.Components
{
    partial class SettingsPage
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.settingsPanel = new System.Windows.Forms.Panel();
            this.cancelButton = new System.Windows.Forms.Button();
            this.argsTextBox = new System.Windows.Forms.TextBox();
            this.noTextureStreamingCheckbox = new System.Windows.Forms.CheckBox();
            this.useAllAvailableCoresCheckbox = new System.Windows.Forms.CheckBox();
            this.saveSettingButton = new System.Windows.Forms.Button();
            this.optsPanel = new System.Windows.Forms.Panel();
            this.settingsPanel.SuspendLayout();
            this.optsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // settingsPanel
            // 
            this.settingsPanel.Controls.Add(this.optsPanel);
            this.settingsPanel.Controls.Add(this.argsTextBox);
            this.settingsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.settingsPanel.Location = new System.Drawing.Point(0, 0);
            this.settingsPanel.Margin = new System.Windows.Forms.Padding(0);
            this.settingsPanel.Name = "settingsPanel";
            this.settingsPanel.Size = new System.Drawing.Size(240, 99);
            this.settingsPanel.TabIndex = 5;
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(134, 50);
            this.cancelButton.Margin = new System.Windows.Forms.Padding(0);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(48, 23);
            this.cancelButton.TabIndex = 7;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // argsTextBox
            // 
            this.argsTextBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.argsTextBox.Location = new System.Drawing.Point(0, 79);
            this.argsTextBox.Margin = new System.Windows.Forms.Padding(0);
            this.argsTextBox.Name = "argsTextBox";
            this.argsTextBox.Size = new System.Drawing.Size(240, 20);
            this.argsTextBox.TabIndex = 3;
            this.argsTextBox.TextChanged += new System.EventHandler(this.argsTextBox_TextChanged);
            // 
            // noTextureStreamingCheckbox
            // 
            this.noTextureStreamingCheckbox.AutoSize = true;
            this.noTextureStreamingCheckbox.Location = new System.Drawing.Point(12, 6);
            this.noTextureStreamingCheckbox.Margin = new System.Windows.Forms.Padding(0);
            this.noTextureStreamingCheckbox.Name = "noTextureStreamingCheckbox";
            this.noTextureStreamingCheckbox.Size = new System.Drawing.Size(118, 17);
            this.noTextureStreamingCheckbox.TabIndex = 6;
            this.noTextureStreamingCheckbox.Text = "-notexturestreaming";
            this.noTextureStreamingCheckbox.UseVisualStyleBackColor = true;
            this.noTextureStreamingCheckbox.CheckedChanged += new System.EventHandler(this.noTextureStreamingCheckbox_CheckedChanged);
            // 
            // useAllAvailableCoresCheckbox
            // 
            this.useAllAvailableCoresCheckbox.AutoSize = true;
            this.useAllAvailableCoresCheckbox.Location = new System.Drawing.Point(12, 26);
            this.useAllAvailableCoresCheckbox.Margin = new System.Windows.Forms.Padding(0);
            this.useAllAvailableCoresCheckbox.Name = "useAllAvailableCoresCheckbox";
            this.useAllAvailableCoresCheckbox.Size = new System.Drawing.Size(124, 17);
            this.useAllAvailableCoresCheckbox.TabIndex = 5;
            this.useAllAvailableCoresCheckbox.Text = "-useallavailablecores";
            this.useAllAvailableCoresCheckbox.UseVisualStyleBackColor = true;
            this.useAllAvailableCoresCheckbox.CheckedChanged += new System.EventHandler(this.useAllAvailableCoresCheckbox_CheckedChanged);
            // 
            // saveSettingButton
            // 
            this.saveSettingButton.AutoSize = true;
            this.saveSettingButton.Location = new System.Drawing.Point(186, 50);
            this.saveSettingButton.Margin = new System.Windows.Forms.Padding(0);
            this.saveSettingButton.Name = "saveSettingButton";
            this.saveSettingButton.Size = new System.Drawing.Size(42, 23);
            this.saveSettingButton.TabIndex = 0;
            this.saveSettingButton.Text = "Save";
            this.saveSettingButton.UseVisualStyleBackColor = true;
            this.saveSettingButton.Click += new System.EventHandler(this.saveSettingButton_Click);
            // 
            // optsPanel
            // 
            this.optsPanel.Controls.Add(this.saveSettingButton);
            this.optsPanel.Controls.Add(this.useAllAvailableCoresCheckbox);
            this.optsPanel.Controls.Add(this.noTextureStreamingCheckbox);
            this.optsPanel.Controls.Add(this.cancelButton);
            this.optsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.optsPanel.Location = new System.Drawing.Point(0, 0);
            this.optsPanel.Margin = new System.Windows.Forms.Padding(0);
            this.optsPanel.Name = "optsPanel";
            this.optsPanel.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.optsPanel.Size = new System.Drawing.Size(240, 79);
            this.optsPanel.TabIndex = 8;
            // 
            // SettingsPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.settingsPanel);
            this.Name = "SettingsPage";
            this.Size = new System.Drawing.Size(240, 99);
            this.settingsPanel.ResumeLayout(false);
            this.settingsPanel.PerformLayout();
            this.optsPanel.ResumeLayout(false);
            this.optsPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel settingsPanel;
        private System.Windows.Forms.TextBox argsTextBox;
        private System.Windows.Forms.CheckBox noTextureStreamingCheckbox;
        private System.Windows.Forms.CheckBox useAllAvailableCoresCheckbox;
        private System.Windows.Forms.Button saveSettingButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Panel optsPanel;
    }
}
