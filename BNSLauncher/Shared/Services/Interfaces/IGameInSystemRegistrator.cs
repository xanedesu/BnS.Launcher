using BNSLauncher.Shared.Models;
using System.Collections.Generic;

namespace BNSLauncher.Shared.Services.Interfaces
{
    public interface IGameInSystemRegistrator
    {
        void Register(GameRegistrationData registrationData);

        void Unregister(GameUnregistrationData unregistrationData);

        bool IsRegistered(string launcherKey, string gameRegistrationKey);

        List<InstalledGameInfo> GetInstalledGames(string launcherKey);
    }
}