using System;

namespace Unlakki.Bns.Launcher.Shared.Services.Interfaces
{
    public class LauncherRegistrationDataUpdate
    {
        public string Key { get; set; }

        public string Name { get; set; }

        public string Version { get; set; }

        public DateTime UpdateDate { get; set; }

        public string UninstallCommand { get; set; }

        public long SizeBytes { get; set; }
    }
}
