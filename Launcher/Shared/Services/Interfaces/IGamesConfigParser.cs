using Unlakki.Bns.Launcher.Shared.Models.GameConfig;

namespace Unlakki.Bns.Launcher.Shared.Services.Interfaces
{
    public interface IGamesConfigParser
    {
        GamesConfig Parse(string data);
    }
}
