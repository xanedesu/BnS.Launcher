using Unlakki.Bns.Launcher.Core.Enums;

namespace Unlakki.Bns.Launcher.Core.Models
{
  public class GameLaunchData
  {
    public string Login { get; set; }

    public string Password { get; set; }

    public GameVersion Version { get; set; }

    public string Arguments { get; set; }
  }
}
