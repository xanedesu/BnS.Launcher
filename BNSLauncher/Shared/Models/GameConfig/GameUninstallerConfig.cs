using System.Xml.Serialization;

namespace Unlakki.Bns.Launcher.Shared.Models.GameConfig
{
    public class GameUninstallerConfig
    {
        [XmlAttribute("exe")]
        public string Exe { get; set; }

        [XmlAttribute("args")]
        public string Args { get; set; }
    }
}