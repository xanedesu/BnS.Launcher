using System.Collections.Generic;

namespace BNSLauncher.Shared.Models.GameConfig
{
    class GamesConfig
    {
        private List<GameConfig> _games = new List<GameConfig>();

        public GamesConfig()
        {
            _games.Add(new BnsConfig());
        }

        public GameConfig GetGameConfig(string key)
        {
            return _games.Find((GameConfig config) => config.GameKey == key);
        }
    }
}
