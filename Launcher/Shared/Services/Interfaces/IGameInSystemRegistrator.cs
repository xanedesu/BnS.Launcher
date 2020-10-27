using System.Collections.Generic;
using Unlakki.Bns.Launcher.Shared.Models;

namespace Unlakki.Bns.Launcher.Shared.Services.Interfaces
{
  public interface IGameInSystemRegistrator
  {
    void Register(GameRegistrationData registrationData);

    void Unregister(GameUnregistrationData unregistrationData);

    bool IsRegistered(string launcherKey, string gameRegistrationKey);

    List<InstalledGameInfo> GetInstalledGames(string launcherKey);
  }
}