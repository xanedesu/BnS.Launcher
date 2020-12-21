using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Unlakki.Bns.Launcher.Shared.Infrastructure.Internet;

namespace Unlakki.Bns.Launcher.Shared.Utils
{
    public class WebHelper
    {
        public static async Task<T> TryToLoadJsonData<T>(HttpRequestMessage httpRequest)
        {
            using (HeaderedHttpClient httpClient = new HeaderedHttpClient())
            {
                HttpResponseMessage httpResponse = await httpClient.SendAsync(httpRequest);
                string content = await httpResponse.Content.ReadAsStringAsync();

                if (httpResponse.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<T>(content);
                }

                throw new HttpRequestException(httpResponse.StatusCode.ToString()) {
                    Data = { { "content", content } }
                };
            }
        }
    }
}
