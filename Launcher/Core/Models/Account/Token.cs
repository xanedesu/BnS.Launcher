using Newtonsoft.Json;

namespace Unlakki.Bns.Launcher.Core.Models.Account
{
  public class Token
  {
    [JsonProperty("access_token")]
    public string AccessToken { get; set; }

    [JsonProperty("refresh_token")]
    public string RefreshToken { get; set; }
  }
}
