using Newtonsoft.Json;

namespace Unlakki.Bns.Launcher.Core.Infrastructure.WebSocket.Models
{
  class WebSocketRequest
  {
    [JsonProperty("jsonrpc")]
    public string JsonRpc { get; } = "2.0";

    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("method")]
    public string Method { get; set; }

    [JsonProperty("params")]
    public object Params { get; set; }
  }
}
