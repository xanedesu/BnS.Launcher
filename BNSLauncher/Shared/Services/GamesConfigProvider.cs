using BNSLauncher.Shared.Models.GameConfig;
using BNSLauncher.Shared.Services.Interfaces;
using System.ComponentModel.Composition;

namespace BNSLauncher.Shared.Services
{
    [Export(typeof(IGamesConfigProvider))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    class GamesConfigProvider : IGamesConfigProvider
    {
        public GamesConfig Get()
        {
            return new GamesConfig();
        }

        public GameConfig Get(string key)
        {
            return new GamesConfig().GetGameConfig(key);
        }
    }
}
