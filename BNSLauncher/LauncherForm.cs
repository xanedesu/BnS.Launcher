using BNSLauncher.Shared.Infrastructure.Internet;
using BNSLauncher.Shared.Infrastructure.Internet.Exceptions;
using BNSLauncher.Shared.Models;
using BNSLauncher.Shared.Providers.Interfaces;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace BNSLauncher
{
    public partial class LauncherForm : Form
    {
        private WebHelper webHelper;

        private GameLauncher launcher;

        private string sessionId;

        private string accessToken;

        public LauncherForm(IComputerNameProvider computerNameProvider, ILauncherIdProvider launcherIdProvider, IHardwareIdProvider hardwareIdProvider)
        {
            InitializeComponent();

            this.webHelper = new WebHelper(computerNameProvider, launcherIdProvider, hardwareIdProvider);
            this.launcher = new GameLauncher(new WebSocketHelper(computerNameProvider, launcherIdProvider, hardwareIdProvider));
        }

        private void ShowConfirmationCodePanel(string message)
        {
            this.textLabel.Text = message;

            this.loginPanel.Hide();
            this.comfirmationCodePanel.Show();
            this.confirmationCodeTextBox.Select();
        }

        private void ShowGameLauncherPanel()
        {
            this.loginPanel.Hide();
            this.comfirmationCodePanel.Hide();
            this.startGamePanel.Show();
            this.startGameButton.Select();
        }

        private async void loginButton_Click(object sender, EventArgs e)
        {
            try
            {
                AuthData authData = await this.webHelper.Authorize(usernameTextBox.Text, passwordTextBox.Text);
                this.accessToken = authData.AccessToken;

                this.ShowGameLauncherPanel();
            }
            catch (Exception ex)
            {
                if (ex is NeedConfirmWithCode)
                {
                    NeedConfirmWithCode needConfirmWithCodeEx = (NeedConfirmWithCode)ex;

                    this.sessionId = needConfirmWithCodeEx.SessionId;
                    this.ShowConfirmationCodePanel(needConfirmWithCodeEx.Message);

                    return;
                }

                MessageBox.Show(ex.Message);
                return;
            }
        }

        private async void confirmationCodeTextBox_TextChanged(object sender, EventArgs e)
        {
            if (confirmationCodeTextBox.TextLength == 6)
            {
                await this.webHelper.SendVerificationCode(this.sessionId, confirmationCodeTextBox.Text);

                AuthData authData = await this.webHelper.Authorize(usernameTextBox.Text, passwordTextBox.Text);
                this.accessToken = authData.AccessToken;

                this.ShowGameLauncherPanel();
            }
        }

        private async void startGameButton_Click(object sender, EventArgs e)
        {
            string gameAuthString = await launcher.GetGameAuthString(this.accessToken);

            if (x32ClientRadioButton.Checked)
            {
                Process.Start("C:\\Blade and Soul\\bin\\Client.exe", gameAuthString);
                return;
            }

            if (x64ClientRadioButton.Checked)
            {
                Process.Start("C:\\Blade and Soul\\bin64\\Client.exe", gameAuthString);
            }
        }
    }
}
