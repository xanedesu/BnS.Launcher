using System.Threading.Tasks;
using Unlakki.Bns.Launcher.Shared.Models.GameConfig;

namespace Unlakki.Bns.Launcher.Shared.Services.Interfaces
{
  public interface IGamesConfigProvider
  {
    Task Init();

    Task<GamesConfig> InitAndGet();

    GamesConfig Get();

    GameConfig Get(string key);
  }
}
