using Newtonsoft.Json;

namespace BNSLauncher.Shared.Models
{
    class GameLoginCredentials
    {
        [JsonProperty("login")]
        public string Login { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("expiration")]
        public string Expiration { get; set; }
    }
}
