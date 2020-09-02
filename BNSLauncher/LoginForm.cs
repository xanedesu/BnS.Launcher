using System;
using System.Windows.Forms;
using Unlakki.Bns.Launcher.Core.Exceptions;
using Unlakki.Bns.Launcher.Core.Models.Account;
using Unlakki.Bns.Launcher.Core.Services.Interfaces;
using System.Threading.Tasks;

namespace Unlakki.Bns.Launcher
{
    public partial class LoginForm : Form
    {
        private readonly IForgameAuthProvider _forgameAuthProvider;

        private readonly ILauncherConfigProvider _launcherConfigProvider;

        public LoginForm(
            IForgameAuthProvider forgameAuthProvider,
            ILauncherConfigProvider launcherConfigProvider)
        {
            InitializeComponent();

            _forgameAuthProvider = forgameAuthProvider;
            _launcherConfigProvider = launcherConfigProvider;
        }

        private async void loginButton_Click(object sender, EventArgs e)
        {
            string username = usernameTextBox.Text;
            string password = passwordTextBox.Text;

            try
            {
                await TryAddAccount(username, password);
                DialogResult = DialogResult.OK;
            }
            catch (NeedToConfirmWithCode ex)
            {
                Hide();

                using (ActivationCodeForm activationCodeForm = new ActivationCodeForm(_forgameAuthProvider, ex.Message, ex.SessionId))
                {
                    if (activationCodeForm.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            await TryAddAccount(username, password);
                            DialogResult = DialogResult.OK;
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Unexpected error while adding account");
                        }
                    }
                }
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

            passwordTextBox.PasswordChar = '\0';
        }

        private async Task TryAddAccount(string username, string password)
        {
            Tokens tokens = await _forgameAuthProvider.Authorize(username, password);
            _launcherConfigProvider.AddOrUpdateAccount(new Account()
            {
                Username = username,
                Tokens = tokens
            });
        }
    }
}
