using Newtonsoft.Json;

namespace BNSLauncher.Core.Models
{
    class Account
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("auth")]
        public UserData Auth { get; set; }
    }
}
