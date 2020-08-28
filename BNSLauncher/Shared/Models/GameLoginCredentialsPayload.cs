using Newtonsoft.Json;

namespace BNSLauncher.Shared.Models
{
    class GameLoginCredentialsPayload
    {
        [JsonProperty("accessToken")]
        public string AccessToken { get; set; }

        [JsonProperty("ignoreLicenseAcceptance")]
        public bool IgnoreLicenseAcceptance { get; set; }

        [JsonProperty("login")]
        public string Login { get; set; }

        [JsonProperty("masterId")]
        public string MasterId { get; set; }

        [JsonProperty("toPartnerId")]
        public string ToPartnerId { get => "bns-ru"; }
    }
}
