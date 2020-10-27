using System.Xml.Serialization;

namespace Unlakki.Bns.Launcher.Shared.Models.GameConfig
{
  public class GameComponentConfig
  {
    [XmlAttribute("name")]
    public string Name { get; set; }

    [XmlAttribute("enableForOS")]
    public string EnableForOS { get; set; }

    [XmlAttribute("path")]
    public string Path { get; set; }

    [XmlAttribute("key")]
    public string Key { get; set; }

    [XmlAttribute("value")]
    public string Value { get; set; }

    [XmlAttribute("exe")]
    public string Exe { get; set; }

    [XmlAttribute("source")]
    public string Source { get; set; }

    [XmlAttribute("compare_type")]
    public string CompareType { get; set; }

    [XmlAttribute("install")]
    public string Install { get; set; }

    [XmlAttribute("args")]
    public string Args { get; set; }
  }
}