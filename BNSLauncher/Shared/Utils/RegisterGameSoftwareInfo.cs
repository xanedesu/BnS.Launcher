using System;

namespace Unlakki.Bns.Launcher.Shared.Utils
{
    public class RegisterGameSoftwareInfo
    {
        public string Publisher { get; set; }

        public string LauncherKey { get; set; }

        public string GameName { get; set; }

        public string InstallationPath { get; set; }

        public string Version { get; set; }

        public DateTime? InstallationDate { get; set; }

        public string LastGamesInstallDirectory { get; set; }
    }
}
