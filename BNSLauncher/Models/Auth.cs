using Newtonsoft.Json;

namespace BNSLauncher.Models
{
    class Auth
    {
        [JsonProperty("accessToken")]
        public string AccessToken { get; set; }

        [JsonProperty("refreshToken")]
        public string RefreshToken { get; set; }
    }
}
