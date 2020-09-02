namespace Unlakki.Bns.Launcher
{
    partial class LauncherForm
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

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LauncherForm));
            this.startGamePanel = new System.Windows.Forms.Panel();
            this.autoCloseLauncherCheckbox = new System.Windows.Forms.CheckBox();
            this.addAnotherAccountButton = new System.Windows.Forms.Button();
            this.accountsListBox = new System.Windows.Forms.ComboBox();
            this.startGameButton = new System.Windows.Forms.Button();
            this.clientVersionSelectorPanel = new System.Windows.Forms.Panel();
            this.x64ClientRadioButton = new System.Windows.Forms.RadioButton();
            this.x32ClientRadioButton = new System.Windows.Forms.RadioButton();
            this.settingsPanel = new System.Windows.Forms.Panel();
            this.saveSettingButton = new System.Windows.Forms.Button();
            this.backBotton = new System.Windows.Forms.Button();
            this.argsLabel = new System.Windows.Forms.Label();
            this.argsTextBox = new System.Windows.Forms.TextBox();
            this.startGamePanel.SuspendLayout();
            this.clientVersionSelectorPanel.SuspendLayout();
            this.settingsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // startGamePanel
            // 
            this.startGamePanel.Controls.Add(this.autoCloseLauncherCheckbox);
            this.startGamePanel.Controls.Add(this.addAnotherAccountButton);
            this.startGamePanel.Controls.Add(this.accountsListBox);
            this.startGamePanel.Controls.Add(this.startGameButton);
            this.startGamePanel.Controls.Add(this.clientVersionSelectorPanel);
            this.startGamePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.startGamePanel.Location = new System.Drawing.Point(0, 0);
            this.startGamePanel.Margin = new System.Windows.Forms.Padding(0);
            this.startGamePanel.Name = "startGamePanel";
            this.startGamePanel.Size = new System.Drawing.Size(240, 99);
            this.startGamePanel.TabIndex = 2;
            // 
            // autoCloseLauncherCheckbox
            // 
            this.autoCloseLauncherCheckbox.AutoSize = true;
            this.autoCloseLauncherCheckbox.Location = new System.Drawing.Point(9, 41);
            this.autoCloseLauncherCheckbox.Name = "autoCloseLauncherCheckbox";
            this.autoCloseLauncherCheckbox.Size = new System.Drawing.Size(186, 17);
            this.autoCloseLauncherCheckbox.TabIndex = 6;
            this.autoCloseLauncherCheckbox.Text = "Close launcher after starting game";
            this.autoCloseLauncherCheckbox.UseVisualStyleBackColor = true;
            this.autoCloseLauncherCheckbox.CheckedChanged += new System.EventHandler(this.autoCloseLauncherCheckbox_CheckedChanged);
            // 
            // addAnotherAccountButton
            // 
            this.addAnotherAccountButton.Location = new System.Drawing.Point(194, 6);
            this.addAnotherAccountButton.Name = "addAnotherAccountButton";
            this.addAnotherAccountButton.Size = new System.Drawing.Size(34, 23);
            this.addAnotherAccountButton.TabIndex = 4;
            this.addAnotherAccountButton.Text = "Add";
            this.addAnotherAccountButton.UseVisualStyleBackColor = true;
            this.addAnotherAccountButton.Click += new System.EventHandler(this.addAnotherAccountButton_Click);
            // 
            // accountsListBox
            // 
            this.accountsListBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.accountsListBox.FormattingEnabled = true;
            this.accountsListBox.Location = new System.Drawing.Point(9, 7);
            this.accountsListBox.Name = "accountsListBox";
            this.accountsListBox.Size = new System.Drawing.Size(179, 21);
            this.accountsListBox.TabIndex = 3;
            this.accountsListBox.SelectedIndexChanged += new System.EventHandler(this.accountsListBox_SelectedIndexChanged);
            // 
            // startGameButton
            // 
            this.startGameButton.Location = new System.Drawing.Point(153, 67);
            this.startGameButton.Name = "startGameButton";
            this.startGameButton.Size = new System.Drawing.Size(75, 23);
            this.startGameButton.TabIndex = 2;
            this.startGameButton.Text = "Play";
            this.startGameButton.UseVisualStyleBackColor = true;
            this.startGameButton.Click += new System.EventHandler(this.startGameButton_Click);
            // 
            // clientVersionSelectorPanel
            // 
            this.clientVersionSelectorPanel.Controls.Add(this.x64ClientRadioButton);
            this.clientVersionSelectorPanel.Controls.Add(this.x32ClientRadioButton);
            this.clientVersionSelectorPanel.Location = new System.Drawing.Point(9, 67);
            this.clientVersionSelectorPanel.Margin = new System.Windows.Forms.Padding(0);
            this.clientVersionSelectorPanel.Name = "clientVersionSelectorPanel";
            this.clientVersionSelectorPanel.Size = new System.Drawing.Size(105, 23);
            this.clientVersionSelectorPanel.TabIndex = 0;
            // 
            // x64ClientRadioButton
            // 
            this.x64ClientRadioButton.AutoSize = true;
            this.x64ClientRadioButton.Location = new System.Drawing.Point(60, 3);
            this.x64ClientRadioButton.Name = "x64ClientRadioButton";
            this.x64ClientRadioButton.Size = new System.Drawing.Size(42, 17);
            this.x64ClientRadioButton.TabIndex = 1;
            this.x64ClientRadioButton.Text = "x64";
            this.x64ClientRadioButton.UseVisualStyleBackColor = true;
            this.x64ClientRadioButton.CheckedChanged += new System.EventHandler(this.x64ClientRadioButton_CheckedChanged);
            // 
            // x32ClientRadioButton
            // 
            this.x32ClientRadioButton.AutoSize = true;
            this.x32ClientRadioButton.Checked = true;
            this.x32ClientRadioButton.Location = new System.Drawing.Point(12, 3);
            this.x32ClientRadioButton.Name = "x32ClientRadioButton";
            this.x32ClientRadioButton.Size = new System.Drawing.Size(42, 17);
            this.x32ClientRadioButton.TabIndex = 0;
            this.x32ClientRadioButton.TabStop = true;
            this.x32ClientRadioButton.Text = "x32";
            this.x32ClientRadioButton.UseVisualStyleBackColor = true;
            this.x32ClientRadioButton.CheckedChanged += new System.EventHandler(this.x32ClientRadioButton_CheckedChanged);
            // 
            // settingsPanel
            // 
            this.settingsPanel.Controls.Add(this.argsTextBox);
            this.settingsPanel.Controls.Add(this.argsLabel);
            this.settingsPanel.Controls.Add(this.backBotton);
            this.settingsPanel.Controls.Add(this.saveSettingButton);
            this.settingsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.settingsPanel.Location = new System.Drawing.Point(0, 0);
            this.settingsPanel.Name = "settingsPanel";
            this.settingsPanel.Size = new System.Drawing.Size(240, 99);
            this.settingsPanel.TabIndex = 3;
            // 
            // saveSettingButton
            // 
            this.saveSettingButton.Location = new System.Drawing.Point(153, 67);
            this.saveSettingButton.Name = "saveSettingButton";
            this.saveSettingButton.Size = new System.Drawing.Size(75, 23);
            this.saveSettingButton.TabIndex = 0;
            this.saveSettingButton.Text = "Save";
            this.saveSettingButton.UseVisualStyleBackColor = true;
            // 
            // backBotton
            // 
            this.backBotton.Location = new System.Drawing.Point(9, 67);
            this.backBotton.Name = "backBotton";
            this.backBotton.Size = new System.Drawing.Size(75, 23);
            this.backBotton.TabIndex = 1;
            this.backBotton.Text = "Back";
            this.backBotton.UseVisualStyleBackColor = true;
            // 
            // argsLabel
            // 
            this.argsLabel.AutoSize = true;
            this.argsLabel.Location = new System.Drawing.Point(6, 11);
            this.argsLabel.Name = "argsLabel";
            this.argsLabel.Size = new System.Drawing.Size(67, 13);
            this.argsLabel.TabIndex = 2;
            this.argsLabel.Text = "Launch Args";
            // 
            // argsTextBox
            // 
            this.argsTextBox.Location = new System.Drawing.Point(79, 7);
            this.argsTextBox.Name = "argsTextBox";
            this.argsTextBox.Size = new System.Drawing.Size(149, 20);
            this.argsTextBox.TabIndex = 3;
            // 
            // LauncherForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(240, 99);
            this.Controls.Add(this.startGamePanel);
            this.Controls.Add(this.settingsPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "LauncherForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "bns-ru";
            this.startGamePanel.ResumeLayout(false);
            this.startGamePanel.PerformLayout();
            this.clientVersionSelectorPanel.ResumeLayout(false);
            this.clientVersionSelectorPanel.PerformLayout();
            this.settingsPanel.ResumeLayout(false);
            this.settingsPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel startGamePanel;
        private System.Windows.Forms.RadioButton x64ClientRadioButton;
        private System.Windows.Forms.Button startGameButton;
        private System.Windows.Forms.Panel clientVersionSelectorPanel;
        private System.Windows.Forms.RadioButton x32ClientRadioButton;
        private System.Windows.Forms.ComboBox accountsListBox;
        private System.Windows.Forms.Button addAnotherAccountButton;
        private System.Windows.Forms.CheckBox autoCloseLauncherCheckbox;
        private System.Windows.Forms.Panel settingsPanel;
        private System.Windows.Forms.TextBox argsTextBox;
        private System.Windows.Forms.Label argsLabel;
        private System.Windows.Forms.Button backBotton;
        private System.Windows.Forms.Button saveSettingButton;
    }
}

