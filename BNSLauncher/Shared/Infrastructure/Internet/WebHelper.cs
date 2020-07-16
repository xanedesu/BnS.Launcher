using BNSLauncher.Shared.Infrastructure.Internet.Exceptions;
using BNSLauncher.Shared.Infrastructure.Internet.Interfaces;
using BNSLauncher.Shared.Models;
using BNSLauncher.Shared.Providers.Interfaces;
using BNSLauncher.Shared.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BNSLauncher.Shared.Infrastructure.Internet
{
    class WebHelper
    {
        private IComputerNameProvider computerNameProvider;

        private ILauncherIdProvider launcherIdProvider;

        private IHardwareIdProvider hardwareIdProvider;

        public WebHelper(IComputerNameProvider computerNameProvider, ILauncherIdProvider launcherIdProvider, IHardwareIdProvider hardwareIdProvider)
        {
            this.computerNameProvider = computerNameProvider;
            this.launcherIdProvider = launcherIdProvider;
            this.hardwareIdProvider = hardwareIdProvider;
        }

        private async Task<T> Fetch<T>(string uri, FetchInit init)
        {
            string computerName = Base64.Encode(this.computerNameProvider.Get());
            string launcherId = this.launcherIdProvider.Get();
            string hardwareId = Base64.Encode(this.hardwareIdProvider.Get());

            HttpClientHandler handler = new HttpClientHandler()
            {
                AutomaticDecompression = ~DecompressionMethods.None
            };

            using (HttpClient httpClient = new HttpClient(handler))
            {
                using (HttpRequestMessage request = new HttpRequestMessage(new HttpMethod(init.Method), uri))
                {
                    request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/72.0.3626.119 Safari/537.36");
                    request.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8");
                    request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate");
                    request.Headers.TryAddWithoutValidation("Accept-Language", "ru-RU,ru;q=0.9,en-US;q=0.8,en;q=0.7");

                    request.Headers.TryAddWithoutValidation("Computer-Name", computerName);
                    request.Headers.TryAddWithoutValidation("Hardware-Id", hardwareId);
                    request.Headers.TryAddWithoutValidation("Launcher-Id", launcherId);

                    request.Content = new StringContent(init.Data);
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse(init.ContentType); // "application/json;charset=UTF-8"

                    HttpResponseMessage response = await httpClient.SendAsync(request);

                    string responseText = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        return JsonConvert.DeserializeObject<T>(responseText);
                    }

                    throw new Exception(responseText);
                }
            }
        }

        public async Task<AuthData> Authorize(string username, string password)
        {
            FetchInit init = new FetchInit()
            {
                Method = "POST",
                ContentType = "application/x-www-form-urlencoded",
                Data = $"username={username}&password={password}&secure=true&grant_type=password"
            };

            try
            {
                return await this.Fetch<AuthData>("https://launcherbff.ru.4game.com/connect/token", init);
            }
            catch (Exception ex)
            {
                JObject json = JObject.Parse(ex.Message);

                if (json.ContainsKey("error"))
                {
                    string errorDescription = (string)json["error"]["description"];

                    if (json.Value<JObject>("error").ContainsKey("data"))
                    {
                        throw new NeedConfirmWithCode(errorDescription, (string)json["error"]["data"]["sessionId"]);
                    }

                    throw new Exception(errorDescription);
                }

                throw ex;
            }
        }

        public async Task<AuthData> RefreshTokens(string refreshToken)
        {
            FetchInit init = new FetchInit()
            {
                Method = "POST",
                ContentType = "application/x-www-form-urlencoded",
                Data = $"grant_type=refresh_token&refresh_token={refreshToken}"
            };

            return await this.Fetch<AuthData>("https://launcherbff.ru.4game.com/connect/token", init);
        }

        public async Task<bool> SendVerificationCode(string sessionId, string code)
        {
            ConfirmCodePayload payload = new ConfirmCodePayload()
            {
                Code = code,
                SessionId = sessionId
            };

            FetchInit init = new FetchInit()
            {
                Method = "POST",
                ContentType = "application/json;charset=UTF-8",
                Data = JsonConvert.SerializeObject(payload)
            };

            await this.Fetch<JToken>("https://launcherbff.ru.4game.com/api/guard/accesscodes/activate", init);
            return true;
        }
    }
}
