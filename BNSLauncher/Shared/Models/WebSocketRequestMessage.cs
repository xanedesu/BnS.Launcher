using Newtonsoft.Json;

namespace BNSLauncher.Shared.Models
{
    class WebSocketRequestMessage
    {
        [JsonProperty("jsonrpc")]
        public string JsonRpc { get => "2.0"; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("method")]
        public string Method { get; set; }

        [JsonProperty("params")]
        public object Params { get; set; }
    }
}
