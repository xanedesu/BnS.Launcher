using Newtonsoft.Json;

namespace Unlakki.Bns.Launcher.Core.Infrastructure.WebSocket.Models
{
    public class Error
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}
