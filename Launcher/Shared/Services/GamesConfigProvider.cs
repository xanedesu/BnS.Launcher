using System.ComponentModel.Composition;
using Unlakki.Bns.Launcher.Shared.Models.GameConfig;
using Unlakki.Bns.Launcher.Shared.Services.Interfaces;
using System.Threading.Tasks;

namespace Unlakki.Bns.Launcher.Shared.Services
{
    [Export(typeof(IGamesConfigProvider))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class GamesConfigProvider : IGamesConfigProvider
    {
        private IGamesConfigDataProvider _dataProvider;

        private IGamesConfigParser _configParser;

        private GamesConfig _sharedConfig;

        public GamesConfigProvider(IGamesConfigDataProvider dataProvider, IGamesConfigParser configParser)
        {
            _dataProvider = dataProvider;
            _configParser = configParser;
        }

        public async void Init()
        {
            if (_sharedConfig != null)
                return;
            await LoadConfig();
        }

        public GamesConfig InitAndGet()
        {
            Init();
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
