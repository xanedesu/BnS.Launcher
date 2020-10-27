using System;

namespace Unlakki.Bns.Launcher.Shared.Models
{
  public class InstalledGameInfo
  {
    public string GameKey { get; set; }

    public string GameName { get; set; }

    public string Path { get; set; }

    public string Version { get; set; }

    public DateTime? InstallationDate { get; set; }

    public string IconPath { get; set; }
  }
}