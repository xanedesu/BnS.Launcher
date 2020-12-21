using System;
using System.Web;
using System.Windows.Forms;
using Unlakki.Bns.Launcher.Components.Router;
using Unlakki.Bns.Launcher.Core.Exceptions;
using Unlakki.Bns.Launcher.Core.Models.Account;
using Unlakki.Bns.Launcher.Core.Services.Interfaces;

namespace Unlakki.Bns.Launcher.Components
{
    public partial class LoginPage : RoutableComponent
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
                _launcherConfigProvider.AddOrUpdateAccount(new Account() {
                    Username = username,
                    Token = token
                });
                _launcherConfigProvider.UpdateLastUsedAccount(username);

                Router.SetLocation("/");
            }
            catch (NeedToConfirmWithCode ex)
            {
                var collection = HttpUtility.ParseQueryString(string.Empty);
                collection.Add("message", ex.Message);
                collection.Add("username", username);
                collection.Add("password", password);

                string path = $"/auth/activate/{ex.SessionId}?{collection}";

                Router.SetLocation(path);
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
