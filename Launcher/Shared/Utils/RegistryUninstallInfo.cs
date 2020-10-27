using System;

namespace Unlakki.Bns.Launcher.Shared.Utils
{
  public class RegistryUninstallInfo
  {
    public string Version { get; set; }

    public string Publisher { get; set; }

    public string Name { get; set; }

    public string InstallationPath { get; set; }

    public string IconPath { get; set; }

    public string UninstallCommand { get; set; }

    public DateTime InstallationDate { get; set; }

    public long? SizeBytes { get; set; }
  }
}
