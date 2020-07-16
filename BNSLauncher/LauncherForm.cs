using BNSLauncher.Models;
using BNSLauncher.Shared.Infrastructure.Internet;
using BNSLauncher.Shared.Infrastructure.Internet.Exceptions;
using BNSLauncher.Shared.Models;
using BNSLauncher.Shared.Providers.Interfaces;
using BNSLauncher.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Windows.Forms;

namespace BNSLauncher
{
    public partial class LauncherForm : Form
    {
        private WebHelper webHelper;

        private GameLauncher launcher;

        private string sessionId;

        private Dictionary<string, Auth> users;

        private string pathToGameFolder;

        public LauncherForm(IComputerNameProvider computerNameProvider, ILauncherIdProvider launcherIdProvider, IHardwareIdProvider hardwareIdProvider)
        {
            InitializeComponent();

            this.webHelper = new WebHelper(computerNameProvider, launcherIdProvider, hardwareIdProvider);
            this.launcher = new GameLauncher(new WebSocketHelper(computerNameProvider, launcherIdProvider, hardwareIdProvider));

            ConfigLoader config = ConfigLoader.LoadConfig();

            this.pathToGameFolder = config.GetPathToGame();
            this.users = config.GetUsers();

            if (this.users.Count > 0)
            {
                foreach (var user in this.users)
                {
                    this.accountsListBox.Items.Add(user.Key);
                }

                this.ShowGameLauncherPanel();
            }

            this.accountsListBox.SelectedItem = config.GetPrefferedAccount();
        }

        private void ShowLoginPanel()
        {
            this.loginPanel.Show();
            this.comfirmationCodePanel.Hide();
            this.startGamePanel.Hide();

            this.usernameTextBox.Select();

            this.usernameTextBox.Text = "";
            this.passwordTextBox.Text = "";
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
                this.AddAccount(usernameTextBox.Text, authData.AccessToken, authData.RefreshToken);

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
                this.AddAccount(usernameTextBox.Text, authData.AccessToken, authData.RefreshToken);

                this.ShowGameLauncherPanel();
            }
        }

        private void addAnotherAccountButton_Click(object sender, EventArgs e)
        {
            this.ShowLoginPanel();
        }

        private void updatePathToGameButton_Click(object sender, EventArgs e)
        {
            this.pathToGameFolder = this.SelectGameFolder();
            ConfigLoader.SavePathToGame(this.pathToGameFolder);
        }

        private async void startGameButton_Click(object sender, EventArgs e)
        {
            string selectedUser = (string)this.accountsListBox.SelectedItem;
            if (string.IsNullOrEmpty(selectedUser))
            {
                MessageBox.Show("Select account.");
                return;
            }

            this.users.TryGetValue(selectedUser, out Auth user);

            long validTo = new JwtSecurityToken(user.AccessToken).ValidTo.Ticks;
            if (DateTime.Now.Ticks > validTo)
            {
                this.users.Remove(selectedUser);

                AuthData authData = await this.webHelper.RefreshTokens(user.RefreshToken);
                this.AddAccount(selectedUser, authData.AccessToken, authData.RefreshToken);
            }

            string gameAuthString = await launcher.GetGameAuthString(user.AccessToken);

            if (x32ClientRadioButton.Checked)
            {
                this.RunGame("bin", gameAuthString);
                return;
            }

            if (x64ClientRadioButton.Checked)
            {
                this.RunGame("bin64", gameAuthString);
            }
        }

        private void accountsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConfigLoader.SavePrefferedAccount((string)this.accountsListBox.SelectedItem);
        }

        private void RunGame(string clientVersion, string gameAuthString)
        {
            if (string.IsNullOrEmpty(this.pathToGameFolder))
            {
                this.pathToGameFolder = this.SelectGameFolder();
                ConfigLoader.SavePathToGame(pathToGameFolder);
            }

            string pathToClientExe = Path.Combine(this.pathToGameFolder, clientVersion, "Client.exe");
            if (File.Exists(pathToClientExe))
            {
                Process.Start(pathToClientExe, gameAuthString);

                if (this.autoCloseLauncherCheckbox.Checked)
                {
                    Application.Exit();
                }

                return;
            }

            MessageBox.Show("Invalid path to game.");
        }
        
        private string SelectGameFolder()
        {
            using (FolderBrowserDialog folderBrowser = new FolderBrowserDialog())
            {
                DialogResult dialogResult = folderBrowser.ShowDialog();

                if (dialogResult == DialogResult.OK && !string.IsNullOrEmpty(folderBrowser.SelectedPath))
                {
                    return folderBrowser.SelectedPath;
                }

                throw new Exception("Invalid path to game game.");
            }
        }
    
        private void AddAccount(string username, string accessToken, string refreshToken)
        {
            this.accountsListBox.Items.Add(username);
            this.accountsListBox.SelectedItem = username;

            Auth user = new Auth()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };

            ConfigLoader.SaveAccount(username, user);
            this.users.Add(username, user);
        }
    }
}
