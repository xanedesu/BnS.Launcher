using BNSLauncher.Net.Exceptions;
using BNSLauncher.Shared.Providers.Interfaces;
using BNSLauncher.Utils;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BNSLauncher.Net
{
    class Request
    {
        private static readonly string DEFAULT_USER_AGENT = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.89 Safari/537.36";

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
                    request.Headers.TryAddWithoutValidation("User-Agent", DEFAULT_USER_AGENT);
                    request.Headers.TryAddWithoutValidation("computer-name", computerName);
                    request.Headers.TryAddWithoutValidation("hardware-id", hardwareId);
                    request.Headers.TryAddWithoutValidation("launcher-id", launcherId);
                    request.Headers.TryAddWithoutValidation("Referer", "https://launcher.ru.4game.com/");

                    request.Content = new StringContent($"username={username}&password={password}&secure=true&grant_type=password");
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");

                    HttpResponseMessage response = await httpClient.SendAsync(request);

                    string text = await response.Content.ReadAsStringAsync();
                    JObject json = JObject.Parse(text);

                    if (json.ContainsKey("error"))
                    {
                        string errorDescription = (string)json["error"]["description"];

                        if (json.Value<JObject>("error").ContainsKey("data"))
                        {
                            throw new NeedConfirmWithCode(errorDescription, (string)json["error"]["data"]["sessionId"]);
                        }

                        throw new RequestException(errorDescription);
                    }

                    return (string)json["access_token"];
                }
            }
        }

        public async Task SendConfirmationCode(string code, string sessionId)
        {
            string computerName = Base64.Base64Encode(this.computerNameProvider.Get());
            string launcherId = this.launcherIdProvider.Get();
            string hardwareId = Base64.Base64Encode(this.hardwareIdProvider.Get());

            HttpClientHandler handler = new HttpClientHandler();

            handler.AutomaticDecompression = ~DecompressionMethods.None;

            using (HttpClient httpClient = new HttpClient(handler))
            {
                using (HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("POST"), "https://launcherbff.ru.4game.com/api/guard/accesscodes/activate"))
                {
                    request.Headers.TryAddWithoutValidation("User-Agent", DEFAULT_USER_AGENT);
                    request.Headers.TryAddWithoutValidation("Computer-Name", computerName);
                    request.Headers.TryAddWithoutValidation("Hardware-Id", hardwareId);
                    request.Headers.TryAddWithoutValidation("Launcher-Id", launcherId);        
                    request.Headers.TryAddWithoutValidation("Referer", "https://launcher.ru.4game.com/auth");

                    request.Content = new StringContent("{\"sessionId\":\"SID\",\"code\":\"CODE\"}".Replace("SID", sessionId).Replace("CODE", code));
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json;charset=UTF-8");

                    await httpClient.SendAsync(request);
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
                    request.Headers.TryAddWithoutValidation("User-Agent", DEFAULT_USER_AGENT);
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
