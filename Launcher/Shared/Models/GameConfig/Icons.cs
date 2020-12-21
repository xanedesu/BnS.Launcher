using System.Xml.Serialization;

namespace Unlakki.Bns.Launcher.Shared.Models.GameConfig
{
    public class Icons
    {
        [XmlAttribute("icons_url")]
        public string IconsUrl { get; set; }

        [XmlAttribute("covers_url")]
        public string CoversUrl { get; set; }

        [XmlAttribute("logos_url")]
        public string LogosUrl { get; set; }
    }
}