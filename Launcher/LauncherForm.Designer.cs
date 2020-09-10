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
            this.openSettingsFormButton = new System.Windows.Forms.Button();
            this.startGamePanel.SuspendLayout();
            this.clientVersionSelectorPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // startGamePanel
            // 
            this.startGamePanel.Controls.Add(this.openSettingsFormButton);
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
            // LauncherForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(240, 99);
            this.Controls.Add(this.startGamePanel);
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
        private System.Windows.Forms.Button openSettingsFormButton;
    }
}

