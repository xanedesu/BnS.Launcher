using System;
using System.IdentityModel.Tokens.Jwt;
using System.Windows.Forms;
using Unlakki.Bns.Launcher.Core.Enums;
using Unlakki.Bns.Launcher.Core.Infrastructure.Crypto;
using Unlakki.Bns.Launcher.Core.Infrastructure.Crypto.Interfaces;
using Unlakki.Bns.Launcher.Core.Infrastructure.WebSocket;
using Unlakki.Bns.Launcher.Core.Models;
using Unlakki.Bns.Launcher.Core.Models.Account;
using Unlakki.Bns.Launcher.Core.Models.GameAccount;
using Unlakki.Bns.Launcher.Core.Services;
using Unlakki.Bns.Launcher.Core.Services.Interfaces;
using Unlakki.Bns.Launcher.Shared.Services;
using Unlakki.Bns.Launcher.Shared.Services.Interfaces;

namespace Unlakki.Bns.Launcher
{
    public partial class LauncherForm : Form
    {
        private ILauncherConfigProvider _launcherConfigProvider;

        private IGamesConfigProvider _gamesConfigProvider;

        private IForgameAuthProvider _forgameAuthProvider;

        private GameManager _gameManager;

        private IGameAuthProvider _gameAuthProvider;

        public LauncherForm()
        {
            InitializeComponent();

            LauncherInWindowsRegistrator launcherInWindowsRegistrator = new LauncherInWindowsRegistrator();
            LauncherIdGenerator launcherIdGenerator = new LauncherIdGenerator();

            ComputerNameProvider computerNameProvider = new ComputerNameProvider();
            LauncherIdProvider launcherIdProvider = new LauncherIdProvider(launcherInWindowsRegistrator, launcherIdGenerator);
            HardwareIdProvider hardwareIdProvider = new HardwareIdProvider(launcherIdProvider);

            IGamesConfigDataProvider gamesConfigDataProvider = new GamesConfigCdnDataProvider();
            IGamesConfigParser gamesConfigParser = new GamesConfigXmlParser();

            _gamesConfigProvider = new GamesConfigProvider(gamesConfigDataProvider, gamesConfigParser);
            _gamesConfigProvider.Init();

            IAesStorage aesStorage = new AesStorage(computerNameProvider);
            ICryptoManager cryptoManager = new CryptoManager(aesStorage);
            ILauncherConfigDataProvider launcherConfigProvider = new LauncherConfigDataProvider(cryptoManager);
            ILauncherConfigParser launcherConfigParser = new LauncherConfigJsonParser();

            _launcherConfigProvider = new LauncherConfigProvider(launcherConfigProvider, launcherConfigParser);
            _launcherConfigProvider.Init();

            _forgameAuthProvider = new ForgameAuthProvider(computerNameProvider, launcherIdProvider, hardwareIdProvider);

            IGameInSystemRegistrator gameInWindowsRegistrator = new GameInWindowsRegistrator();
            _gameManager = new GameManager(gameInWindowsRegistrator, _gamesConfigProvider);

            WebSocket ws = new WebSocket(computerNameProvider, launcherIdProvider, hardwareIdProvider);
            _gameAuthProvider = new GameAuthProvider(ws);

            UpdateUiFromConfig();  
        }

        private void addAnotherAccountButton_Click(object sender, EventArgs e)
        {
            Hide();
            
            using (LoginForm loginForm = new LoginForm(_forgameAuthProvider, _launcherConfigProvider))
            {
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    UpdateAccountList(true);
                }
            }

            Show();
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

            JwtSecurityToken jwt = new JwtSecurityToken(account.Tokens.AccessToken);

            long validTo = jwt.ValidTo.Ticks;
            if (DateTime.Now.Ticks > validTo)
            {
                Tokens tokens = await _forgameAuthProvider.RefreshTokens(account.Tokens.RefreshToken);
                _launcherConfigProvider.AddOrUpdateAccount(new Account
                {
                    Username = username,
                    Tokens = tokens
                });

                account.Tokens = tokens;
            }

            GameTokenCode gameTokenCode = await _gameAuthProvider.GetGameTokenCode(account.Tokens.AccessToken);

            _gameManager.Launch("bns-ru_live", new GameLaunchData
            {
                Login = gameTokenCode.Login,
                Password = gameTokenCode.Password,
                Version = _launcherConfigProvider.GetGameVersion(),
                Arguments = _launcherConfigProvider.GetGameArguments()
            });

            if (_launcherConfigProvider.GetAutoCloseLauncher())
            {
                Close();
            }
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
                accountsListBox.SelectedIndex = accountsListBox.Items.Count - 1;
        }
    }
}
