using Newtonsoft.Json;

namespace Unlakki.Bns.Launcher.Core.Models.Account
{
    public class Tokens
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
    }
}
