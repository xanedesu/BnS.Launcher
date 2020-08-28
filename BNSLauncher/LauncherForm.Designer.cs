namespace BNSLauncher
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
            this.usernameTextBox = new System.Windows.Forms.TextBox();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.loginButton = new System.Windows.Forms.Button();
            this.loginPanel = new System.Windows.Forms.Panel();
            this.comfirmationCodePanel = new System.Windows.Forms.Panel();
            this.textLabel = new System.Windows.Forms.Label();
            this.confirmationCodeTextBox = new System.Windows.Forms.TextBox();
            this.startGamePanel = new System.Windows.Forms.Panel();
            this.autoCloseLauncherCheckbox = new System.Windows.Forms.CheckBox();
            this.addAnotherAccountButton = new System.Windows.Forms.Button();
            this.accountsListBox = new System.Windows.Forms.ComboBox();
            this.startGameButton = new System.Windows.Forms.Button();
            this.clientVersionSelectorPanel = new System.Windows.Forms.Panel();
            this.x64ClientRadioButton = new System.Windows.Forms.RadioButton();
            this.x32ClientRadioButton = new System.Windows.Forms.RadioButton();
            this.loginPanel.SuspendLayout();
            this.comfirmationCodePanel.SuspendLayout();
            this.startGamePanel.SuspendLayout();
            this.clientVersionSelectorPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // usernameTextBox
            // 
            this.usernameTextBox.Location = new System.Drawing.Point(12, 12);
            this.usernameTextBox.Name = "usernameTextBox";
            this.usernameTextBox.Size = new System.Drawing.Size(216, 20);
            this.usernameTextBox.TabIndex = 0;
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Location = new System.Drawing.Point(12, 38);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.Size = new System.Drawing.Size(216, 20);
            this.passwordTextBox.TabIndex = 1;
            // 
            // loginButton
            // 
            this.loginButton.Location = new System.Drawing.Point(153, 64);
            this.loginButton.Name = "loginButton";
            this.loginButton.Size = new System.Drawing.Size(75, 23);
            this.loginButton.TabIndex = 2;
            this.loginButton.Text = "Login";
            this.loginButton.UseVisualStyleBackColor = true;
            this.loginButton.Click += new System.EventHandler(this.loginButton_Click);
            // 
            // loginPanel
            // 
            this.loginPanel.AutoSize = true;
            this.loginPanel.Controls.Add(this.usernameTextBox);
            this.loginPanel.Controls.Add(this.passwordTextBox);
            this.loginPanel.Controls.Add(this.loginButton);
            this.loginPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loginPanel.Location = new System.Drawing.Point(0, 0);
            this.loginPanel.Name = "loginPanel";
            this.loginPanel.Size = new System.Drawing.Size(240, 99);
            this.loginPanel.TabIndex = 5;
            // 
            // comfirmationCodePanel
            // 
            this.comfirmationCodePanel.Controls.Add(this.textLabel);
            this.comfirmationCodePanel.Controls.Add(this.confirmationCodeTextBox);
            this.comfirmationCodePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comfirmationCodePanel.Location = new System.Drawing.Point(0, 0);
            this.comfirmationCodePanel.Name = "comfirmationCodePanel";
            this.comfirmationCodePanel.Size = new System.Drawing.Size(240, 99);
            this.comfirmationCodePanel.TabIndex = 5;
            this.comfirmationCodePanel.Visible = false;
            // 
            // textLabel
            // 
            this.textLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.textLabel.Location = new System.Drawing.Point(0, 0);
            this.textLabel.Name = "textLabel";
            this.textLabel.Size = new System.Drawing.Size(240, 32);
            this.textLabel.TabIndex = 1;
            this.textLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // confirmationCodeTextBox
            // 
            this.confirmationCodeTextBox.Location = new System.Drawing.Point(12, 41);
            this.confirmationCodeTextBox.Name = "confirmationCodeTextBox";
            this.confirmationCodeTextBox.Size = new System.Drawing.Size(216, 20);
            this.confirmationCodeTextBox.TabIndex = 0;
            this.confirmationCodeTextBox.TextChanged += new System.EventHandler(this.confirmationCodeTextBox_TextChanged);
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
            this.startGamePanel.Visible = false;
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
            // 
            // LauncherForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(240, 99);
            this.Controls.Add(this.startGamePanel);
            this.Controls.Add(this.comfirmationCodePanel);
            this.Controls.Add(this.loginPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "LauncherForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "bns-ru";
            this.loginPanel.ResumeLayout(false);
            this.loginPanel.PerformLayout();
            this.comfirmationCodePanel.ResumeLayout(false);
            this.comfirmationCodePanel.PerformLayout();
            this.startGamePanel.ResumeLayout(false);
            this.startGamePanel.PerformLayout();
            this.clientVersionSelectorPanel.ResumeLayout(false);
            this.clientVersionSelectorPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox usernameTextBox;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.Button loginButton;
        private System.Windows.Forms.Panel loginPanel;
        private System.Windows.Forms.Panel comfirmationCodePanel;
        private System.Windows.Forms.TextBox confirmationCodeTextBox;
        private System.Windows.Forms.Label textLabel;
        private System.Windows.Forms.Panel startGamePanel;
        private System.Windows.Forms.RadioButton x64ClientRadioButton;
        private System.Windows.Forms.Button startGameButton;
        private System.Windows.Forms.Panel clientVersionSelectorPanel;
        private System.Windows.Forms.RadioButton x32ClientRadioButton;
        private System.Windows.Forms.ComboBox accountsListBox;
        private System.Windows.Forms.Button addAnotherAccountButton;
        private System.Windows.Forms.CheckBox autoCloseLauncherCheckbox;
    }
}

