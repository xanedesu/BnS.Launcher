using Newtonsoft.Json;

namespace BNSLauncher.Shared.Models
{
    class GetGameAccountMessage
    {
        [JsonProperty("masterId")]
        public string MasterId { get; set; }

        [JsonProperty("toPartnerId")]
        public readonly string ToPartnerId = "bns-ru";
    }
}
