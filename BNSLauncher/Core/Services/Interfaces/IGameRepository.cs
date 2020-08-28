using BNSLauncher.Shared.Models;

namespace BNSLauncher.Core.Services.Interfaces
{
    public interface IGameRepository
    {
        InstalledGameInfo GetOrDefault(string gameKey);
    }
}