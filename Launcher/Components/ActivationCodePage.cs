using System;
using System.Windows.Forms;
using Unlakki.Bns.Launcher.Components.Router;
using Unlakki.Bns.Launcher.Core.Models.Account;
using Unlakki.Bns.Launcher.Core.Services.Interfaces;

namespace Unlakki.Bns.Launcher.Components
{
    public partial class ActivationCodePage : RoutedComponent
    {
        private ILauncherConfigProvider _launcherConfigProvider;

        private IForgameAuthProvider _forgameAuthProvider;

        public ActivationCodePage(
            ILauncherConfigProvider launcherConfigProvider,
            IForgameAuthProvider forgameAuthProvider)
        {
            InitializeComponent();

            _launcherConfigProvider = launcherConfigProvider;
            _forgameAuthProvider = forgameAuthProvider;

            Load += ActivationCodePage_Load;
        }

        private void ActivationCodePage_Load(object sender, EventArgs e)
        {
            string message;
            if (Router.QueryParams.TryGetValue("message", out message))
            {
                messageContainer.Text = message;
            }
        }

        private async void activationCodeTextBox_TextChanged(object sender, EventArgs e)
        {
            if (activationCodeTextBox.TextLength != 6)
            {
                return;
            }

            string sessionId;
            if (!Router.QueryParams.TryGetValue("sessionId", out sessionId))
            {
                return;
            }

            try
            {
                string username = Uri.UnescapeDataString(Router.QueryParams["username"]);
                string password = Uri.UnescapeDataString(Router.QueryParams["password"]);

                await _forgameAuthProvider.SendActivationCode(sessionId, activationCodeTextBox.Text);
                Token token = await _forgameAuthProvider.Authorize(username, password);
                _launcherConfigProvider.AddOrUpdateAccount(new Account
                {
                    Username = username,
                    Token = token
                });
                _launcherConfigProvider.UpdateLastUsedAccount(username);

                Router.SetLocation("/");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
