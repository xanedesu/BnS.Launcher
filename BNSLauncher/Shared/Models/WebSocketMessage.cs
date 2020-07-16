using Newtonsoft.Json;

namespace BNSLauncher.Shared.Models
{
    class WebSocketMessage
    {
        [JsonProperty("method")]
        public string Method { get; set; }

        [JsonProperty("params")]
        public object Params { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
