using Newtonsoft.Json;

namespace BNSLauncher.Shared.Models
{
    class GameAccountPayload
    {
        [JsonProperty("masterId")]
        public string MasterId { get; set; }

        [JsonProperty("toPartnerId")]
        public string ToPartnerId { get => "bns-ru"; }
    }
}
