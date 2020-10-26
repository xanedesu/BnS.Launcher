using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unlakki.Bns.Launcher.Components.Router;
using Unlakki.Bns.Launcher.Core.Enums;
using Unlakki.Bns.Launcher.Core.Infrastructure.WebSocket;
using Unlakki.Bns.Launcher.Core.Models;
using Unlakki.Bns.Launcher.Core.Models.Account;
using Unlakki.Bns.Launcher.Core.Services;
using Unlakki.Bns.Launcher.Core.Services.Interfaces;
using Unlakki.Bns.Launcher.Shared.Services;
using Unlakki.Bns.Launcher.Shared.Services.Interfaces;

namespace Unlakki.Bns.Launcher.Components
{
    public partial class LauncherPage : RoutableComponent
    {
        IForgameAuthProvider _forgameAuthProvider;

        IGameAuthProvider _gameAuthProvider;

        ILauncherConfigProvider _launcherConfigProvider;

        GameManager _gameManager;

        public LauncherPage(
            IComputerNameProvider computerNameProvider,
            ILauncherIdProvider launcherIdProvider,
            IHardwareIdProvider hardwareIdProvider,
            ILauncherConfigProvider launcherConfigProvider,
            IForgameAuthProvider forgameAuthProvider)
        {
            InitializeComponent();

            _launcherConfigProvider = launcherConfigProvider;
            _forgameAuthProvider = forgameAuthProvider;

            var gamesConfigDataProvider = new GamesConfigCdnDataProvider();
            var gamesConfigParser = new GamesConfigXmlParser();

            var gamesConfigProvider = new GamesConfigProvider(gamesConfigDataProvider, gamesConfigParser);
            Task.Run(gamesConfigProvider.Init);

            var gameInWindowsRegistrator = new GameInWindowsRegistrator();
            _gameManager = new GameManager(gameInWindowsRegistrator, gamesConfigProvider);

            var ws = new WebSocket(computerNameProvider, launcherIdProvider, hardwareIdProvider);
            _gameAuthProvider = new GameAuthProvider(ws);

            Load += LauncherPage_Load;
        }

        private void LauncherPage_Load(object sender, EventArgs e)
        {
            UpdateUiFromConfig();
        }

        private void addAnotherAccountButton_Click(object sender, EventArgs e)
        {
            Router.SetLocation("/auth");
        }

        private void openSettingsFormButton_Click(object sender, EventArgs e)
        {
            Router.SetLocation("/settings");
        }

        private void accountsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _launcherConfigProvider.UpdateLastUsedAccount((string)accountsListBox.SelectedItem);
        }

        private void x32ClientRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (x32ClientRadioButton.Checked)
            {
                _launcherConfigProvider.UpdateGameVersion(GameVersion.x32);
            }
        }

        private void x64ClientRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (x64ClientRadioButton.Checked)
            {
                _launcherConfigProvider.UpdateGameVersion(GameVersion.x64);
            }
        }

        private void autoCloseLauncherCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            _launcherConfigProvider.UpdateAutoCloseLauncher(autoCloseLauncherCheckbox.Checked);
        }

        private async void startGameButton_Click(object sender, EventArgs e)
        {
            string username = (string)accountsListBox.SelectedItem;

            Account account = _launcherConfigProvider.GetAccounts().Find(acc => acc.Username == username);
            if (account == null)
            {
                MessageBox.Show("Select account.");
                return;
            }

            var jwt = new JwtSecurityToken(account.Token.AccessToken);

            try
            {
                long validTo = jwt.ValidTo.Ticks;
                if (DateTime.Now.Ticks > validTo)
                {
                    Token token = await _forgameAuthProvider.Refresh(account.Token.RefreshToken);
                    _launcherConfigProvider.AddOrUpdateAccount(new Account
                    {
                        Username = username,
                        Token = token
                    });

                    account.Token = token;
                }

                var gameTokenCode = await _gameAuthProvider.GetGameTokenCode(account.Token.AccessToken);

                _gameManager.Launch("bns-ru", new GameLaunchData
                {
                    Login = gameTokenCode.Login,
                    Password = gameTokenCode.Password,
                    Version = _launcherConfigProvider.GetGameVersion(),
                    Arguments = _launcherConfigProvider.GetGameArguments()
                });

                if (_launcherConfigProvider.GetAutoCloseLauncher())
                {
                    var mainForm = (MainForm)Parent;
                    mainForm.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateUiFromConfig()
        {
            UpdateAccountList();
            accountsListBox.SelectedItem = _launcherConfigProvider.GetLastUsedAccount();

            GameVersion version = _launcherConfigProvider.GetGameVersion();

            x32ClientRadioButton.Checked = version == GameVersion.x32;
            x64ClientRadioButton.Checked = version == GameVersion.x64;

            autoCloseLauncherCheckbox.Checked = _launcherConfigProvider.GetAutoCloseLauncher();
        }

        private void UpdateAccountList(bool mustUpdateItemIndex = false)
        {
            accountsListBox.Items.Clear();

            foreach (var account in _launcherConfigProvider.GetAccounts())
            {
                accountsListBox.Items.Add(account.Username);
            }

            if (mustUpdateItemIndex)
            {
                accountsListBox.SelectedIndex = accountsListBox.Items.Count - 1;
            }
        }
    }
}
