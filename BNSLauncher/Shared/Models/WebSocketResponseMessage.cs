using Newtonsoft.Json;

namespace BNSLauncher.Shared.Models
{
    class WebSocketResponseMessage<TResult>
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("result")]
        public TResult Result { get; set; }
    }
}
