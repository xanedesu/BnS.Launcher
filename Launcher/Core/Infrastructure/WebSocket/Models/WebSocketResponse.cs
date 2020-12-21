using Newtonsoft.Json;

namespace Unlakki.Bns.Launcher.Core.Infrastructure.WebSocket.Models
{
    class WebSocketResponse<TResult>
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("result")]
        public TResult Result { get; set; }

        [JsonProperty("error")]
        public Error Error { get; set; }
    }
}
