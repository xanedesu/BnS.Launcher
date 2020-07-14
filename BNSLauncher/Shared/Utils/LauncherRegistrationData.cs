using System;

namespace BNSLauncher.Shared.Utils
{
    public class LauncherRegistrationData
    {
        public string IconTarget { get; set; }

        public string IconPath { get; set; }

        public string Name { get; set; }

        public string UninstallCommand { get; set; }

        public string Key { get; set; }

        public string Version { get; set; }

        public string InstallationPath { get; set; }

        public DateTime InstallationDate { get; set; }

        public long SizeBytes { get; set; }

        public string RunnerPath { get; set; }

        public string UrlSchemeName { get; set; }

        public string LastGamesInstallPath { get; set; }

        public string LauncherRegion { get; set; }
    }
}
