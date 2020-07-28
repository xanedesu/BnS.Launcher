using Newtonsoft.Json;

namespace BNSLauncher.Core.Models
{
    class Auth
    {
        [JsonProperty("accessToken")]
        public string AccessToken { get; set; }

        [JsonProperty("refreshToken")]
        public string RefreshToken { get; set; }
    }
}
