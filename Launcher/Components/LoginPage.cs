using System;
using System.Windows.Forms;
using Unlakki.Bns.Launcher.Components.Router;
using Unlakki.Bns.Launcher.Core.Exceptions;
using Unlakki.Bns.Launcher.Core.Models.Account;
using Unlakki.Bns.Launcher.Core.Services.Interfaces;

namespace Unlakki.Bns.Launcher.Components
{
    public partial class LoginPage : RoutedComponent
    {
        ILauncherConfigProvider _launcherConfigProvider;

        IForgameAuthProvider _forgameAuthProvider;

        public LoginPage(
            ILauncherConfigProvider launcherConfigProvider,
            IForgameAuthProvider forgameAuthProvider)
        {
            InitializeComponent();

            _launcherConfigProvider = launcherConfigProvider;
            _forgameAuthProvider = forgameAuthProvider;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Router.SetLocation("/");
        }

        private async void loginButton_Click(object sender, EventArgs e)
        {
            string username = usernameTextBox.Text;
            string password = passwordTextBox.Text;

            try
            {
                Token token = await _forgameAuthProvider.Authorize(username, password);
                _launcherConfigProvider.AddOrUpdateAccount(new Account()
                {
                    Username = username,
                    Token = token
                });
                _launcherConfigProvider.UpdateLastUsedAccount(username);

                Router.SetLocation("/");
            }
            catch (NeedToConfirmWithCode ex)
            {
                Router.SetLocation(string.Format(
                    "/auth/activation?message={0}&sessionId={1}&username={2}&password={3}",
                    ex.Message,
                    ex.SessionId,
                    Uri.EscapeDataString(username),
                    Uri.EscapeDataString(password)));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void hidePasswordCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (hidePasswordCheckbox.Checked)
            {
                passwordTextBox.PasswordChar = '*';
                return;
            }

            passwordTextBox.PasswordChar = char.MinValue;
        }
    }
}
