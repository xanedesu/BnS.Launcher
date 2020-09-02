using Newtonsoft.Json;
using System.Collections.Generic;
using Unlakki.Bns.Launcher.Core.Enums;

namespace Unlakki.Bns.Launcher.Core.Models
{
    public class LauncherConfig
    {
        [JsonProperty("accounts")]
        public List<Unlakki.Bns.Launcher.Core.Models.Account.Account> Accounts { get; set; }

        [JsonProperty("last_used_account")]
        public string LastUsedAccount { get; set; }

        [JsonProperty("version")]
        public GameVersion gameVersion { get; set; }

        [JsonProperty("arguments")]
        public string Arguments { get; set; }

        [JsonProperty("auto_close_launcher")]
        public bool AutoCloseLauncher { get; set; }
    }
}
