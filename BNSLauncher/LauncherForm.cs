using BNSLauncher.Net;
using BNSLauncher.Shared.Providers.Interfaces;
using BNSLauncher.Utils;
using BNSLauncher.WebSockets;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Windows.Forms;

namespace BNSLauncher
{
    public partial class LauncherForm : Form
    {
        private Request _request;

        private IComputerNameProvider computerNameProvider;

        private ILauncherIdProvider launcherIdProvider;

        private IHardwareIdProvider hardwareIdProvider;

        public LauncherForm(IComputerNameProvider computerNameProvider, ILauncherIdProvider launcherIdProvider, IHardwareIdProvider hardwareIdProvider)
        {
            InitializeComponent();

            this._request = new Request(computerNameProvider, launcherIdProvider, hardwareIdProvider);

            this.computerNameProvider = computerNameProvider;
            this.launcherIdProvider = launcherIdProvider;
            this.hardwareIdProvider = hardwareIdProvider;
        }
        
        private async void loginButton_Click(object sender, EventArgs e)
        {
            string username = usernameTextBox.Text;
            string password = passwordTextBox.Text;

            string computerName = Base64.Base64Encode(this.computerNameProvider.Get());
            string launcherId = this.launcherIdProvider.Get();
            string hardwareId = Base64.Base64Encode(this.hardwareIdProvider.Get());

            string data = await this._request.Auth(username, password);
            JObject authData = JObject.Parse(data);

            string accessToken = (string)authData.GetValue("access_token");

            string masterId = new JwtSecurityToken(accessToken).Subject;

            var ws = new WebSocketClient($"wss://launcherbff.ru.4game.com/?token={accessToken}&hardware-id={hardwareId}&launcher-id={launcherId}&computer-name={computerName}");

            string getRameAccountRes = await ws.Send(
                "{\"method\":\"getGameAccount\",\"params\":{\"masterId\":\"MASTER_ID\",\"toPartnerId\":\"bns-ru\"},\"id\": \"UUID\"}"
                    .Replace("MASTER_ID", masterId)
                    .Replace("UUID", Guid.NewGuid().ToString())
            );

            JObject gameAuthData = JObject.Parse(getRameAccountRes);

            string login = (string)gameAuthData["result"][0]["login"];

            string gameTokenRes = await ws.Send(
                "{\"method\":\"createGameTokenCode\",\"params\":{\"accessToken\":\"TOKEN\",\"ignoreLicenseAcceptance\":false,\"login\":\"LOGIN\",\"masterId\":\"MASTERID\",\"toPartnerId\":\"bns-ru\"},\"id\":\"UUID\"}"
                    .Replace("TOKEN", accessToken)
                    .Replace("LOGIN", login)
                    .Replace("MASTERID", masterId)
                    .Replace("UUID", Guid.NewGuid().ToString())
            );

            JObject gameToken = JObject.Parse(gameTokenRes);

            Process.Start("c:\\Blade and Soul\\bin\\Client.exe", $"/username:{gameToken["result"]["login"]} /password:{gameToken["result"]["password"]}");
        }
    }
}
