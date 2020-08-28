using Newtonsoft.Json;

namespace BNSLauncher.Core.Models
{
    class UserData
    {
        [JsonProperty("accessToken")]
        public string AccessToken { get; set; }

        [JsonProperty("refreshToken")]
        public string RefreshToken { get; set; }
    }
}
