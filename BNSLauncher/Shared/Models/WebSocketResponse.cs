using Newtonsoft.Json;

namespace BNSLauncher.Shared.Models
{
    class WebSocketResponse<T>
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("result")]
        public T Result { get; set; }
    }
}
