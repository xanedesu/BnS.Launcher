using System;

namespace Unlakki.Bns.Launcher.Shared.Utils
{
  public class RegisterLauncherSoftwareInfo
  {
    public string Publisher { get; set; }

    public string LauncherKey { get; set; }

    public string InstallationPath { get; set; }

    public string Version { get; set; }

    public DateTime? InstallationDate { get; set; }

    public string LauncherId { get; set; }

    public bool? OldGamesTaken { get; set; }

    public DateTime? LastUpdateDate { get; set; }

    public string LastGamesInstallDirectory { get; set; }

    public string LauncherRegion { get; set; }
  }
}
