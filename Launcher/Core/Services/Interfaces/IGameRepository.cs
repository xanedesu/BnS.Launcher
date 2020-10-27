using Unlakki.Bns.Launcher.Shared.Models;

namespace Unlakki.Bns.Launcher.Core.Services.Interfaces
{
  public interface IGameRepository
  {
    InstalledGameInfo GetOrDefault(string key);
  }
}