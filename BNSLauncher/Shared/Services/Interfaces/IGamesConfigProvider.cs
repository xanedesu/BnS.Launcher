using BNSLauncher.Shared.Models.GameConfig;

namespace BNSLauncher.Shared.Services.Interfaces
{
    interface IGamesConfigProvider
    {
        GamesConfig Get();

        GameConfig Get(string key);
    }
}
