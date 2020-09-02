using System.Collections.Generic;

namespace Unlakki.Bns.Launcher.Shared.Models.GameConfig
{
    public class Games : List<GameConfig>
    {
        public Games()
        {
        }

        public Games(IEnumerable<GameConfig> collection)
          : base(collection)
        {
        }
    }
}