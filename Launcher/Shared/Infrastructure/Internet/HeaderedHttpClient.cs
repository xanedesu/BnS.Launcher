using System.Net;
using System.Net.Http;

namespace Unlakki.Bns.Launcher.Shared.Infrastructure.Internet
{
    public class HeaderedHttpClient : HttpClient
    {
        private readonly string _userAgent = "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/72.0.3626.119 Safari/537.36";

        private readonly string _accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8";

        private readonly string _acceptEncoding = "gzip, deflate";

        private readonly string _acceptLanguage = "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7";

        public HeaderedHttpClient() : base(new HttpClientHandler() {
            AutomaticDecompression = ~DecompressionMethods.None
        })
        {
            DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", _userAgent);
            DefaultRequestHeaders.TryAddWithoutValidation("Accept", _accept);
            DefaultRequestHeaders.TryAddWithoutValidation("Accept-Encoding", _acceptEncoding);
            DefaultRequestHeaders.TryAddWithoutValidation("Accept-Language", _acceptLanguage);
        }
    }
}
