using System;

namespace BNSLauncher.Shared.Services.Interfaces
{
    public class GameRegistrationData
    {
        public string LauncherKey { get; set; }

        public string IconPath { get; set; }

        public string Name { get; set; }

        public string UninstallCommand { get; set; }

        public string Key { get; set; }

        public string Version { get; set; }

        public string RunnerPath { get; set; }

        public byte[] IconData { get; set; }

        public string InstallationPath { get; set; }

        public DateTime InstallationDate { get; set; }

        public string Size { get; set; }

        public string RunnerArgs { get; set; }
    }
}
