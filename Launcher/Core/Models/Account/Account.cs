using Newtonsoft.Json;

namespace Unlakki.Bns.Launcher.Core.Models.Account
{
    public class Account
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("tokens")]
        public Tokens Tokens { get; set; }
    }
}
