using System.Net;
using System.Net.Http;

namespace Unlakki.Bns.Launcher.Shared.Infrastructure.Internet
{
    public class HeaderedHttpClient : HttpClient
    {
        public HeaderedHttpClient(): base(new HttpClientHandler()
        {
            AutomaticDecompression = ~DecompressionMethods.None
        })
        {
            DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/72.0.3626.119 Safari/537.36");
            DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8");
            DefaultRequestHeaders.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate");
            DefaultRequestHeaders.TryAddWithoutValidation("Accept-Language", "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7");
        }
    }
}
