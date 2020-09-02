using Unlakki.Bns.Launcher.Shared.Models.GameConfig;

namespace Unlakki.Bns.Launcher.Shared.Services.Interfaces
{
    public interface IGamesConfigProvider
    {
        void Init();

        GamesConfig InitAndGet();

        GamesConfig Get();

        GameConfig Get(string key);
    }
}
