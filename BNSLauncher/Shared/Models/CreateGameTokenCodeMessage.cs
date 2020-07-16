using Newtonsoft.Json;

namespace BNSLauncher.Shared.Models
{
    class CreateGameTokenCodeMessage
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
        public readonly string ToPartnerId = "bns-ru";
    }
}
