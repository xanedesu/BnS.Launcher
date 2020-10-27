using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Unlakki.Bns.Launcher.Core.Enums;
using Unlakki.Bns.Launcher.Shared.Extensions;

namespace Unlakki.Bns.Launcher.Shared.Models.GameConfig
{
  public class GameConfig
  {
    private string _stageSizes;

    [XmlAttribute("id")]
    public int Id { get; set; }

    [XmlAttribute("key")]
    public string OldKey { get; set; }

    [XmlAttribute("key_new")]
    public string Key { get; set; }

    [XmlIgnore]
    public string EnvKey {
      get {
        return Key + "_" + Environment;
      }
    }

    [XmlIgnore]
    public string FrostKey {
      get {
        return OldKey + "_" + Environment;
      }
    }

    [XmlAttribute("install_key")]
    public string InstallKey { get; set; }

    [XmlIgnore]
    public string InstallEnvKey {
      get {
        return InstallKey + "_" + Environment;
      }
    }

    [XmlAttribute("type")]
    public string Environment { get; set; }

    [XmlAttribute("name")]
    public string Name { get; set; }

    [XmlAttribute("display_name")]
    public string DisplayName { get; set; }

    [XmlAttribute("version")]
    public string Version { get; set; }

    [XmlAttribute("version_check_url")]
    public string VersionCheckUrl { get; set; }

    [XmlAttribute("updater_exe_name")]
    public string UpdaterExeName { get; set; }

    [XmlAttribute("url")]
    public string Url { get; set; }

    [XmlIgnore]
    public bool CheckTorrent { get; private set; }

    [XmlAttribute("check_torrent")]
    public string CheckTorrentStr {
      get {
        return (CheckTorrent ? 1 : 0).ToString("D");
      }
      set {
        CheckTorrent = value == "1";
      }
    }

    [XmlIgnore]
    public bool HasLightClient { get; private set; }

    [XmlAttribute("has_light_client")]
    public string HasLightClientStr {
      get {
        return (HasLightClient ? 1 : 0).ToString("D");
      }
      set {
        HasLightClient = value == "1";
      }
    }

    [XmlAttribute("game_url_2")]
    public string GameUrl { get; set; }

    [XmlAttribute("base_url")]
    public string BaseUrl { get; set; }

    [XmlAttribute("ex_name")]
    public string FrostGame { get; set; }

    [XmlAttribute("ln_chk_name")]
    public string FrostPath { get; set; }

    [XmlAttribute("ln_params")]
    public string LaunchParams { get; set; }

    [XmlAttribute("ln_name")]
    public string FrostLauncher { get; set; }

    [XmlAttribute("opt_name")]
    public string OptionsExe { get; set; }

    [XmlAttribute("size")]
    public string Size { get; set; }

    [XmlAttribute("description")]
    public string Description { get; set; }

    [XmlIgnore]
    public long[] StageSizesArray { get; private set; } = new long[0];

    [XmlAttribute("stage_sizes")]
    public string StageSizes {
      get {
        return _stageSizes;
      }
      set {
        _stageSizes = value;
        if (string.IsNullOrWhiteSpace(value))
        {
          StageSizesArray = new long[0];
        }
        else
        {
          long result;
          StageSizesArray = ((IEnumerable<string>)value.Split(',')).Select(e => long.TryParse(e, out result) ? result : 0L).ToArray();
        }
      }
    }

    [XmlIgnore]
    public LaunchType LaunchType { get; private set; }

    [XmlAttribute("ln_type")]
    public string LaunchTypeAsInt {
      get {
        return ((int)LaunchType).ToString();
      }
      set {
        LaunchType = value.ToLaunchType();
      }
    }

    [XmlElement("check")]
    public GameComponents Components { get; set; } = new GameComponents();

    [XmlElement("uninstall")]
    public GameUninstallerConfig Uninstall { get; set; }

    [XmlElement("env")]
    public GameEventConfigs Events { get; set; } = new GameEventConfigs();

    public GameConfig WithEvent(string eventKey)
    {
      GameEventConfig gameEventConfig = Events.FirstOrDefault(e => e.EventKey == eventKey);
      if (gameEventConfig == null)
        return this;
      GameConfig gameConfig = Clone();
      if (gameEventConfig.Version != null)
        gameConfig.Version = gameEventConfig.Version;
      if (gameEventConfig.BaseUrl != null)
        gameConfig.BaseUrl = gameEventConfig.BaseUrl;
      if (gameEventConfig.FrostGame != null)
        gameConfig.FrostGame = gameEventConfig.FrostGame;
      if (gameEventConfig.FrostPath != null)
        gameConfig.FrostPath = gameEventConfig.FrostPath;
      if (gameEventConfig.LaunchParams != null)
        gameConfig.LaunchParams = gameEventConfig.LaunchParams;
      if (gameEventConfig.FrostLauncher != null)
        gameConfig.FrostLauncher = gameEventConfig.FrostLauncher;
      if (gameEventConfig.Size != null)
        gameConfig.Size = gameEventConfig.Size;
      if (gameEventConfig.LaunchTypeAsInt != null)
        gameConfig.LaunchTypeAsInt = gameEventConfig.LaunchTypeAsInt;
      if (gameEventConfig.Url != null)
        gameConfig.Url = gameEventConfig.Url;
      gameConfig.OldKey = gameEventConfig.EventKey;
      return gameConfig;
    }

    public GameConfig WithVersion(string version)
    {
      GameConfig gameConfig = Clone();
      gameConfig.Version = version;
      return gameConfig;
    }

    private GameConfig Clone()
    {
      return (GameConfig)MemberwiseClone();
    }
  }
}