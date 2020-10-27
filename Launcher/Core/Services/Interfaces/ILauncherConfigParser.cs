using Unlakki.Bns.Launcher.Core.Models;

namespace Unlakki.Bns.Launcher.Core.Services.Interfaces
{
  public interface ILauncherConfigParser
  {
    LauncherConfig Parse(string text);

    string Stringify(LauncherConfig config);
  }
}
