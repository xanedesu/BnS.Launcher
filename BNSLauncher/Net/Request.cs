using BNSLauncher.Shared.Providers.Interfaces;
using BNSLauncher.Utils;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BNSLauncher.Net
{
    class Request
    {
        private IComputerNameProvider computerNameProvider;

        private ILauncherIdProvider launcherIdProvider;

        private IHardwareIdProvider hardwareIdProvider;

        public Request(IComputerNameProvider computerNameProvider, ILauncherIdProvider launcherIdProvider, IHardwareIdProvider hardwareIdProvider)
        {
            this.computerNameProvider = computerNameProvider;
            this.launcherIdProvider = launcherIdProvider;
            this.hardwareIdProvider = hardwareIdProvider;
        }

        public async Task<string> Auth(string username, string password)
        {
            string computerName = Base64.Base64Encode(this.computerNameProvider.Get());
            string launcherId = this.launcherIdProvider.Get();
            string hardwareId = Base64.Base64Encode(this.hardwareIdProvider.Get());

            HttpClientHandler handler = new HttpClientHandler();

            handler.AutomaticDecompression = ~DecompressionMethods.None;

            using (HttpClient httpClient = new HttpClient(handler))
            {
                using (HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("POST"), "https://launcherbff.ru.4game.com/connect/token"))
                {
                    request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.116 Safari/537.36");
                    request.Headers.TryAddWithoutValidation("computer-name", computerName);
                    request.Headers.TryAddWithoutValidation("hardware-id", hardwareId);
                    request.Headers.TryAddWithoutValidation("launcher-id", launcherId);
                    request.Headers.TryAddWithoutValidation("Referer", "https://launcher.ru.4game.com/");

                    request.Content = new StringContent($"username={username}&password={password}&secure=true&grant_type=password");
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");

                    HttpResponseMessage response = await httpClient.SendAsync(request);

                    return await response.Content.ReadAsStringAsync();
                }
            }
        }

        public async Task<string> RefreshTokens(string accessToken, string refreshToken)
        {
            string computerName = Base64.Base64Encode(this.computerNameProvider.Get());
            string launcherId = this.launcherIdProvider.Get();
            string hardwareId = Base64.Base64Encode(this.hardwareIdProvider.Get());

            HttpClientHandler handler = new HttpClientHandler();

            handler.AutomaticDecompression = ~DecompressionMethods.None;

            using (HttpClient httpClient = new HttpClient(handler))
            {
                using (HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("POST"), "https://launcherbff.ru.4game.com/connect/token"))
                {
                    request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.116 Safari/537.36");
                    request.Headers.TryAddWithoutValidation("computer-name", computerName);
                    request.Headers.TryAddWithoutValidation("hardware-id", hardwareId);
                    request.Headers.TryAddWithoutValidation("launcher-id", launcherId);
                    request.Headers.TryAddWithoutValidation("Referer", "https://launcher.ru.4game.com/");
                    request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {accessToken}");

                    request.Content = new StringContent($"refresh_token={refreshToken}&grant_type=refresh_token");
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");

                    HttpResponseMessage response = await httpClient.SendAsync(request);

                    return await response.Content.ReadAsStringAsync();
                }
            }
        }
    }
}
