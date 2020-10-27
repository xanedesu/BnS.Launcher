using System.ComponentModel.Composition;
using System.Threading.Tasks;
using Unlakki.Bns.Launcher.Shared.Models.GameConfig;
using Unlakki.Bns.Launcher.Shared.Services.Interfaces;

namespace Unlakki.Bns.Launcher.Shared.Services
{
  [Export(typeof(IGamesConfigProvider))]
  [PartCreationPolicy(CreationPolicy.Shared)]
  public class GamesConfigProvider : IGamesConfigProvider
  {
    private IGamesConfigDataProvider _dataProvider;

    private IGamesConfigParser _configParser;

    private GamesConfig _sharedConfig;

    public GamesConfigProvider(
      IGamesConfigDataProvider dataProvider,
      IGamesConfigParser configParser)
    {
      _dataProvider = dataProvider;
      _configParser = configParser;
    }

    public async Task Init()
    {
      if (_sharedConfig != null)
        return;
      await LoadConfig();
    }

    public async Task<GamesConfig> InitAndGet()
    {
      await Init();
      return Get();
    }

    public GamesConfig Get()
    {
      return _sharedConfig;
    }

    public GameConfig Get(string key)
    {
      return Get().GetGameConfig(key);
    }

    private async Task LoadConfig()
    {
      _sharedConfig = _configParser.Parse(await _dataProvider.GetData());
    }
  }
}
