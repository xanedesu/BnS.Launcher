using Newtonsoft.Json;

namespace BNSLauncher.Core.Models
{
    class Config
    {
        [JsonProperty("accounts")]
        public Account[] Accounts { get; set; }

        [JsonProperty("prefferedAccount")]
        public string PrefferedAccount { get; set; }
    }
}
