using Newtonsoft.Json;
using System.Collections.Generic;
using Unlakki.Bns.Launcher.Core.Enums;

namespace Unlakki.Bns.Launcher.Core.Models
{
    public class LauncherConfig
    {
        [JsonProperty("accounts")]
        public List<Unlakki.Bns.Launcher.Core.Models.Account.Account> Accounts { get; set; }

        [JsonProperty("lastUsedAccount")]
        public string LastUsedAccount { get; set; }

        [JsonProperty("gameVersion")]
        public GameVersion gameVersion { get; set; }

        [JsonProperty("gameArguments")]
        public string Arguments { get; set; }

        [JsonProperty("autoCloseLauncher")]
        public bool AutoCloseLauncher { get; set; }
    }
}
