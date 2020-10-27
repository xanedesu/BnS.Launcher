using System.Xml.Serialization;
using Unlakki.Bns.Launcher.Core.Enums;
using Unlakki.Bns.Launcher.Shared.Extensions;

namespace Unlakki.Bns.Launcher.Shared.Models.GameConfig
{
  public class GameEventConfig
  {
    private string _launchTypeAsInt;

    [XmlAttribute("key")]
    public string EventKey { get; set; }

    [XmlAttribute("name")]
    public string EventName { get; set; }

    [XmlAttribute("version")]
    public string Version { get; set; }

    [XmlAttribute("base_url")]
    public string BaseUrl { get; set; }

    [XmlAttribute("url")]
    public string Url { get; set; }

    [XmlAttribute("ex_name")]
    public string FrostGame { get; set; }

    [XmlAttribute("ln_chk_name")]
    public string FrostPath { get; set; }

    [XmlAttribute("ln_params")]
    public string LaunchParams { get; set; }

    [XmlAttribute("ln_name")]
    public string FrostLauncher { get; set; }

    [XmlAttribute("size")]
    public string Size { get; set; }

    [XmlIgnore]
    public LaunchType LaunchType { get; private set; }

    [XmlAttribute("ln_type")]
    public string LaunchTypeAsInt {
      get {
        return _launchTypeAsInt;
      }
      set {
        _launchTypeAsInt = value;
        LaunchType = _launchTypeAsInt.ToLaunchType();
      }
    }
  }
}