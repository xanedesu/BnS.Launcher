using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Unlakki.Bns.Launcher.Shared.Models.GameConfig
{
    [XmlRoot("config")]
    public class GamesConfig
    {
        [XmlElement("icons", IsNullable = false)]
        public Icons Icons { get; set; }

        [XmlArray("games", IsNullable = false)]
        [XmlArrayItem("game")]
        public Games Games { get; set; }

        public string GetGameIconUrl(string gameKey)
        {
            return Path.Combine(Icons.IconsUrl, gameKey + ".ico");
        }

        public string GetGameLogoUrl(string gameKey)
        {
            return Path.Combine(Icons.LogosUrl, gameKey + ".png");
        }

        public string GetGameCoverUrl(string gameKey)
        {
            return Path.Combine(Icons.CoversUrl, gameKey + ".png");
        }

        public GameConfig GetGameConfig(string gameKey)
        {
            return Games.FirstOrDefault(game => game.Key == gameKey);
        }
    }
}
