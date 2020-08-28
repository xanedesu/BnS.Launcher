using Newtonsoft.Json;

namespace BNSLauncher.Shared.Models
{
    class ActivationCodePayload
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("sessionId")]
        public string SessionId { get; set; }
    }
}
