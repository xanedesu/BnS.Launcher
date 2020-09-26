namespace Unlakki.Bns.Launcher.Components
{
    partial class LauncherPage
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
            this.launcherPanel = new System.Windows.Forms.Panel();
            this.openSettingsFormButton = new System.Windows.Forms.Button();
            this.autoCloseLauncherCheckbox = new System.Windows.Forms.CheckBox();
            this.addAnotherAccountButton = new System.Windows.Forms.Button();
            this.accountsListBox = new System.Windows.Forms.ComboBox();
            this.startGameButton = new System.Windows.Forms.Button();
            this.clientVersionSelectorPanel = new System.Windows.Forms.Panel();
            this.x64ClientRadioButton = new System.Windows.Forms.RadioButton();
            this.x32ClientRadioButton = new System.Windows.Forms.RadioButton();
            this.launcherPanel.SuspendLayout();
            this.clientVersionSelectorPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // launcherPanel
            // 
            this.launcherPanel.Controls.Add(this.openSettingsFormButton);
            this.launcherPanel.Controls.Add(this.autoCloseLauncherCheckbox);
            this.launcherPanel.Controls.Add(this.addAnotherAccountButton);
            this.launcherPanel.Controls.Add(this.accountsListBox);
            this.launcherPanel.Controls.Add(this.startGameButton);
            this.launcherPanel.Controls.Add(this.clientVersionSelectorPanel);
            this.launcherPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.launcherPanel.Location = new System.Drawing.Point(0, 0);
            this.launcherPanel.Margin = new System.Windows.Forms.Padding(0);
            this.launcherPanel.Name = "launcherPanel";
            this.launcherPanel.Size = new System.Drawing.Size(240, 99);
            this.launcherPanel.TabIndex = 3;
            // 
            // openSettingsFormButton
            // 
            this.openSettingsFormButton.AutoSize = true;
            this.openSettingsFormButton.Location = new System.Drawing.Point(176, 67);
            this.openSettingsFormButton.Name = "openSettingsFormButton";
            this.openSettingsFormButton.Size = new System.Drawing.Size(55, 23);
            this.openSettingsFormButton.TabIndex = 7;
            this.openSettingsFormButton.Text = "Settings";
            this.openSettingsFormButton.UseVisualStyleBackColor = true;
            this.openSettingsFormButton.Click += new System.EventHandler(this.openSettingsFormButton_Click);
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
            this.autoCloseLauncherCheckbox.Visible = false;
            this.autoCloseLauncherCheckbox.CheckedChanged += new System.EventHandler(this.autoCloseLauncherCheckbox_CheckedChanged);
            // 
            // addAnotherAccountButton
            // 
            this.addAnotherAccountButton.AutoSize = true;
            this.addAnotherAccountButton.Location = new System.Drawing.Point(192, 6);
            this.addAnotherAccountButton.Name = "addAnotherAccountButton";
            this.addAnotherAccountButton.Size = new System.Drawing.Size(36, 23);
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
            this.accountsListBox.Size = new System.Drawing.Size(177, 21);
            this.accountsListBox.TabIndex = 3;
            this.accountsListBox.SelectedIndexChanged += new System.EventHandler(this.accountsListBox_SelectedIndexChanged);
            // 
            // startGameButton
            // 
            this.startGameButton.AutoSize = true;
            this.startGameButton.Location = new System.Drawing.Point(133, 67);
            this.startGameButton.Name = "startGameButton";
            this.startGameButton.Size = new System.Drawing.Size(37, 23);
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
            // LauncherPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.launcherPanel);
            this.Name = "LauncherPage";
            this.Size = new System.Drawing.Size(240, 99);
            this.launcherPanel.ResumeLayout(false);
            this.launcherPanel.PerformLayout();
            this.clientVersionSelectorPanel.ResumeLayout(false);
            this.clientVersionSelectorPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel launcherPanel;
        private System.Windows.Forms.Button openSettingsFormButton;
        private System.Windows.Forms.CheckBox autoCloseLauncherCheckbox;
        private System.Windows.Forms.Button addAnotherAccountButton;
        private System.Windows.Forms.ComboBox accountsListBox;
        private System.Windows.Forms.Button startGameButton;
        private System.Windows.Forms.Panel clientVersionSelectorPanel;
        private System.Windows.Forms.RadioButton x64ClientRadioButton;
        private System.Windows.Forms.RadioButton x32ClientRadioButton;
    }
}
