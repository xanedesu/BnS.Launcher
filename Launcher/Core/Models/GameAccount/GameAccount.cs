using Newtonsoft.Json;

namespace Unlakki.Bns.Launcher.Core.Models.GameAccount
{
    class GameAccount
    {
        [JsonProperty("masterId")]
        public string MasterId { get; set; }

        [JsonProperty("partnerId")]
        public string PartnerId { get; set; }

        [JsonProperty("login")]
        public string Login { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("ban")]
        public object Ban { get; set; }

        [JsonProperty("subscription")]
        public object Subscription { get; set; }

        [JsonProperty("roles")]
        public object[] Roles { get; set; }

        [JsonProperty("context")]
        public string Context { get; set; }

        [JsonProperty("lastLoginTime")]
        public string LastLoginTime { get; set; }

        [JsonProperty("whenCreated")]
        public string WhenCreated { get; set; }
    }
}
