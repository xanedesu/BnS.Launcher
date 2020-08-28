using BNSLauncher.Core.Enums;
using BNSLauncher.Core.Models;
using BNSLauncher.Core.Services;
using BNSLauncher.Core.Utils;
using BNSLauncher.Shared.Infrastructure.Internet;
using BNSLauncher.Shared.Infrastructure.Internet.Exceptions;
using BNSLauncher.Shared.Infrastructure.Internet.Interfaces;
using BNSLauncher.Shared.Models;
using BNSLauncher.Shared.Services;
using BNSLauncher.Shared.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Windows.Forms;

namespace BNSLauncher
{
    public partial class LauncherForm : Form
    {
        private IWebHelper _webHelper;

        private IWebSocketHelper _socketHelper;

        private GameManager _gameManager;

        private string _sessionId;

        private Dictionary<string, UserData> _users;

        public LauncherForm(
            IComputerNameProvider computerNameProvider,
            ILauncherIdProvider launcherIdProvider,
            IHardwareIdProvider hardwareIdProvider)
        {
            InitializeComponent();

            IGameInSystemRegistrator gameInSystemRegistrator = new GameInWindowsRegistrator();
            IGamesConfigProvider gamesConfigProvider = new GamesConfigProvider();

            _webHelper = new WebHelper(computerNameProvider, launcherIdProvider, hardwareIdProvider);
            _socketHelper = new WebSocketHelper(computerNameProvider, launcherIdProvider, hardwareIdProvider);
            _gameManager = new GameManager(gameInSystemRegistrator, gamesConfigProvider);

            ConfigLoader config = ConfigLoader.LoadConfig();

            _users = config.GetUsers();

            if (_users.Count > 0)
            {
                foreach (var user in _users)
                {
                    accountsListBox.Items.Add(user.Key);
                }

                ShowGameLauncherPanel();
            }

            accountsListBox.SelectedItem = config.GetPrefferedAccount();
        }

        private void ShowLoginPanel()
        {
            loginPanel.Show();
            comfirmationCodePanel.Hide();
            startGamePanel.Hide();

            usernameTextBox.Select();

            usernameTextBox.Text = "";
            passwordTextBox.Text = "";
        }

        private void ShowConfirmationCodePanel(string message)
        {
            textLabel.Text = message;

            loginPanel.Hide();
            comfirmationCodePanel.Show();
            confirmationCodeTextBox.Select();
        }

        private void ShowGameLauncherPanel()
        {
            loginPanel.Hide();
            comfirmationCodePanel.Hide();
            startGamePanel.Show();

            startGameButton.Select();
        }

        private async void loginButton_Click(object sender, EventArgs e)
        {
            try
            {
                ForgameAccountTokens authData = await _webHelper.Authorize(usernameTextBox.Text, passwordTextBox.Text);
                AddAccount(usernameTextBox.Text, authData.AccessToken, authData.RefreshToken);

                ShowGameLauncherPanel();
            }
            catch (NeedToConfirmWithCode ex)
            {
                _sessionId = ex.SessionId;
                ShowConfirmationCodePanel(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void confirmationCodeTextBox_TextChanged(object sender, EventArgs e)
        {
            if (confirmationCodeTextBox.TextLength == 6)
            {
                await _webHelper.SendVerificationCode(_sessionId, confirmationCodeTextBox.Text);

                ForgameAccountTokens authData = await _webHelper.Authorize(usernameTextBox.Text, passwordTextBox.Text);
                AddAccount(usernameTextBox.Text, authData.AccessToken, authData.RefreshToken);

                ShowGameLauncherPanel();
            }
        }

        private void addAnotherAccountButton_Click(object sender, EventArgs e)
        {
            ShowLoginPanel();
        }

        private async void startGameButton_Click(object sender, EventArgs e)
        {
            string selectedUser = (string)accountsListBox.SelectedItem;
            if (string.IsNullOrEmpty(selectedUser))
            {
                MessageBox.Show("Select account.");
                return;
            }

            UserData userData;
            _users.TryGetValue(selectedUser, out userData);

            JwtSecurityToken token = new JwtSecurityToken(userData.AccessToken);

            long validTo = token.ValidTo.Ticks;
            if (DateTime.Now.Ticks > validTo)
            {
                _users.Remove(selectedUser);

                ForgameAccountTokens authData = await _webHelper.RefreshTokens(userData.RefreshToken);
                AddAccount(selectedUser, authData.AccessToken, authData.RefreshToken);
            }

            string masterId = token.Subject;

            _socketHelper.Connect(userData.AccessToken);

            GameAccount account = await _socketHelper.GetGameAccount(masterId);
            GameLoginCredentials credentials = await _socketHelper.GetGameLoginCredentials(userData.AccessToken, masterId, account.Login);

            _socketHelper.Disconnect();

            GameLaunchData launchData = new GameLaunchData()
            {
                Login = credentials.Login,
                Password = credentials.Password,
                Arguments = ""
            };

            if (x32ClientRadioButton.Checked)
            {
                launchData.Version = GameVersion.x32;

                _gameManager.Launch("bns-ru_live", launchData);
                return;
            }

            if (x64ClientRadioButton.Checked)
            {
                launchData.Version = GameVersion.x64;

                _gameManager.Launch("bns-ru_live", launchData);
            }
        }

        private void accountsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConfigLoader.SavePrefferedAccount((string)accountsListBox.SelectedItem);
        }

        private void AddAccount(string username, string accessToken, string refreshToken)
        {
            accountsListBox.Items.Add(username);
            accountsListBox.SelectedItem = username;

            UserData user = new UserData()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };

            ConfigLoader.SaveAccount(username, user);
            _users.Add(username, user);
        }
    }
}
