using Newtonsoft.Json;

namespace BNSLauncher.Shared.Models
{
    class ConfirmCodePayload
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("sessionId")]
        public string SessionId { get; set; }
    }
}
