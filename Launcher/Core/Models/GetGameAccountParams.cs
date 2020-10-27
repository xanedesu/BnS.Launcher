using Newtonsoft.Json;

namespace Unlakki.Bns.Launcher.Core.Models
{
  class GetGameAccountParams
  {
    [JsonProperty("masterId")]
    public string MasterId { get; set; }

    [JsonProperty("toPartnerId")]
    public string ToPartnerId { get; } = "bns-ru";
  }
}
