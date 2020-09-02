using Newtonsoft.Json;

namespace Unlakki.Bns.Launcher.Core.Models.GameAccount
{
    class GameTokenCode
    {
        [JsonProperty("login")]
        public string Login { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("expiration")]
        public string Expiration { get; set; }
    }
}
