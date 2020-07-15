using BNSLauncher.Net;
using BNSLauncher.Net.Exceptions;
using BNSLauncher.Shared.Providers.Interfaces;
using BNSLauncher.WebSockets;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BNSLauncher
{
    public partial class LauncherForm : Form
    {
        private Request _request;

        private WebSocketClient _ws;

        private string sessionId;

        private string accessToken;

        public LauncherForm(IComputerNameProvider computerNameProvider, ILauncherIdProvider launcherIdProvider, IHardwareIdProvider hardwareIdProvider)
        {
            InitializeComponent();

            this._request = new Request(computerNameProvider, launcherIdProvider, hardwareIdProvider);
            this._ws = new WebSocketClient(computerNameProvider, launcherIdProvider, hardwareIdProvider);
        }

        private void ShowConfirmationCodePanel(string message)
        {
            this.textLabel.Text = message;

            this.loginPanel.Hide();
            this.comfirmationCodePanel.Show();
        }

        private void ShowGameLauncherPanel()
        {
            this.loginPanel.Hide();
            this.comfirmationCodePanel.Hide();
            this.startGamePanel.Show();
        }

        private async void loginButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.accessToken = await this._request.Auth(usernameTextBox.Text, passwordTextBox.Text);
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
                await this._request.SendConfirmationCode(confirmationCodeTextBox.Text, this.sessionId);

                this.accessToken = await this._request.Auth(usernameTextBox.Text, passwordTextBox.Text);
                this.ShowGameLauncherPanel();
            }
        }

        private async void startGameButton_Click(object sender, EventArgs e)
        {
            string gameAuthData = await this.GetGameAuthString(this.accessToken);

            if (x32ClientRadioButton.Checked)
            {
                Process.Start("C:\\Blade and Soul\\bin\\Client.exe", gameAuthData);
                return;
            }

            if (x64ClientRadioButton.Checked)
            {
                Process.Start("C:\\Blade and Soul\\bin64\\Client.exe", gameAuthData);
            }
        }

        private async Task<string> GetGameAuthString(string accessToken)
        {
            string masterId = new JwtSecurityToken(accessToken).Subject;

            this._ws.Connect(accessToken);

            string gameAccountString = await this._ws.Send(
                "{\"method\":\"getGameAccount\",\"params\":{\"masterId\":\"MASTER_ID\",\"toPartnerId\":\"bns-ru\"},\"id\": \"UUID\"}"
                    .Replace("MASTER_ID", masterId)
                    .Replace("UUID", Guid.NewGuid().ToString())
            );

            JObject gameAccountObject = JObject.Parse(gameAccountString);

            string login = (string)gameAccountObject["result"][0]["login"];

            string gameTokenString = await this._ws.Send(
                "{\"method\":\"createGameTokenCode\",\"params\":{\"accessToken\":\"TOKEN\",\"ignoreLicenseAcceptance\":false,\"login\":\"LOGIN\",\"masterId\":\"MASTERID\",\"toPartnerId\":\"bns-ru\"},\"id\":\"UUID\"}"
                    .Replace("TOKEN", accessToken)
                    .Replace("LOGIN", login)
                    .Replace("MASTERID", masterId)
                    .Replace("UUID", Guid.NewGuid().ToString())
            );

            JObject gameTokenObject = JObject.Parse(gameTokenString);

            string username = (string)gameTokenObject["result"]["login"];
            string password = (string)gameTokenObject["result"]["password"];

            return $"/username:{username} /password:{password}";
        }
    }
}
