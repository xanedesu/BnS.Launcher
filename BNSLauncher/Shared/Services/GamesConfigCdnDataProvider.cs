using System.ComponentModel.Composition;
using System.Threading.Tasks;
using Unlakki.Bns.Launcher.Shared.Infrastructure.Internet;
using Unlakki.Bns.Launcher.Shared.Services.Interfaces;

namespace Unlakki.Bns.Launcher.Shared.Services
{
    [Export(typeof(IGamesConfigDataProvider))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class GamesConfigCdnDataProvider : IGamesConfigDataProvider
    {
        private string _configAddress = "https://cdn.inn.ru/4game/config-live.xml";

        public async Task<string> GetData()
        {
            using (HeaderedHttpClient httpClient = new HeaderedHttpClient())
            {
                string responseText = await httpClient.GetStringAsync(_configAddress);
                return responseText;
            }
        }
    }
}
