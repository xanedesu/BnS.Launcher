using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using Unlakki.Bns.Launcher.Core.Enums;
using Unlakki.Bns.Launcher.Core.Models;
using Unlakki.Bns.Launcher.Core.Models.Account;
using Unlakki.Bns.Launcher.Core.Services.Interfaces;

namespace Unlakki.Bns.Launcher.Core.Services
{
    [Export(typeof(ILauncherConfigProvider))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class LauncherConfigProvider : ILauncherConfigProvider
    {
        private readonly ILauncherConfigDataProvider _configDataProvider;

        private readonly ILauncherConfigParser _configParser;

        private LauncherConfig _launcherConfig;

        [ImportingConstructor]
        public LauncherConfigProvider(
            ILauncherConfigDataProvider configDataProvider,
            ILauncherConfigParser configParser)
        {
            _configDataProvider = configDataProvider;
            _configParser = configParser;
        }

        public void Init()
        {
            ReadConfig();
        }

        public LauncherConfigProvider InitAndGet()
        {
            ReadConfig();
            return this;
        }

        public void AddOrUpdateAccount(Account account)
        {
            _launcherConfig.Accounts.RemoveAll(acc => acc.Username == account.Username);
            _launcherConfig.Accounts.Add(account);
            WriteConfig();
        }

        public void UpdateLastUsedAccount(string lastUsedAccountUsername)
        {
            _launcherConfig.LastUsedAccount = lastUsedAccountUsername;
            WriteConfig();
        }

        public void UpdateGameVersion(GameVersion gameVersion)
        {
            _launcherConfig.gameVersion = gameVersion;
            WriteConfig();
        }

        public void UpdateStartGameArguments(string arguments)
        {
            _launcherConfig.Arguments = arguments;
            WriteConfig();
        }

        public void UpdateAutoCloseLauncher(bool autoClose)
        {
            _launcherConfig.AutoCloseLauncher = autoClose;
            WriteConfig();
        }

        public List<Account> GetAccounts()
        {
            return _launcherConfig.Accounts;
        }

        public string GetLastUsedAccount()
        {
            return _launcherConfig.LastUsedAccount;
        }

        public GameVersion GetGameVersion()
        {
            return _launcherConfig.gameVersion;
        }

        public string GetGameArguments()
        {
            return _launcherConfig.Arguments;
        }

        public bool GetAutoCloseLauncher()
        {
            return _launcherConfig.AutoCloseLauncher;
        }

        private void ReadConfig()
        {
            try
            {
                string configText = _configDataProvider.Read();
                _launcherConfig = _configParser.Parse(configText);
            }
            catch (FileNotFoundException)
            {
                _launcherConfig = new LauncherConfig {
                    Accounts = new List<Account>(),
                    LastUsedAccount = string.Empty,
                    gameVersion = GameVersion.x32,
                    Arguments = string.Empty
                };
            }
        }

        private void WriteConfig()
        {
            string configText = _configParser.Stringify(_launcherConfig);
            _configDataProvider.Write(configText);
        }
    }
}
