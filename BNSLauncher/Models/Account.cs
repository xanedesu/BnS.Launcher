using Newtonsoft.Json;

namespace BNSLauncher.Models
{
    class Account
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("auth")]
        public Auth Auth { get; set; }
    }
}
