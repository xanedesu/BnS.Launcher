using BNSLauncher.Core.Utils;
using BNSLauncher.Shared.Infrastructure.Internet.Exceptions;
using BNSLauncher.Shared.Infrastructure.Internet.Interfaces;
using BNSLauncher.Shared.Models;
using BNSLauncher.Shared.Services.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel.Composition;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BNSLauncher.Shared.Infrastructure.Internet
{
    [Export(typeof(IWebHelper))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    class WebHelper : IWebHelper
    {
        private IComputerNameProvider _computerNameProvider;

        private ILauncherIdProvider _launcherIdProvider;

        private IHardwareIdProvider _hardwareIdProvider;

        public WebHelper(
            IComputerNameProvider computerNameProvider,
            ILauncherIdProvider launcherIdProvider,
            IHardwareIdProvider hardwareIdProvider)
        {
            _computerNameProvider = computerNameProvider;
            _launcherIdProvider = launcherIdProvider;
            _hardwareIdProvider = hardwareIdProvider;
        }

        private async Task<T> Request<T>(string uri, RequestInit init)
        {
            string computerName = Base64.Encode(_computerNameProvider.Get());
            string launcherId = _launcherIdProvider.Get();
            string hardwareId = Base64.Encode(_hardwareIdProvider.Get());

            using (HttpClient httpClient = new HttpClient(new HttpClientHandler()
            {
                AutomaticDecompression = ~DecompressionMethods.None
            }))
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
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse(init.ContentType);

                    HttpResponseMessage responseMessage = await httpClient.SendAsync(request);
                    string responseString = await responseMessage.Content.ReadAsStringAsync();

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        return JsonConvert.DeserializeObject<T>(responseString);
                    }

                    try
                    {
                        JObject error = JObject.Parse(responseString).Value<JObject>("error");
                        throw new RequestError(error.Value<string>("description"), error);
                    }
                    catch (JsonReaderException)
                    {
                        throw new Exception("Can't parse json input.");
                    }
                }
            }
        }

        public async Task<ForgameAccountTokens> Authorize(string username, string password)
        {
            RequestInit init = new RequestInit()
            {
                Method = "POST",
                ContentType = "application/x-www-form-urlencoded",
                Data = $"username={username}&password={password}&secure=true&grant_type=password"
            };

            try
            {
                return await Request<ForgameAccountTokens>("https://launcherbff.ru.4game.com/connect/token", init);
            }
            catch (RequestError ex)
            {
                if (ex.Json.ContainsKey("data"))
                {
                    throw new NeedToConfirmWithCode(ex.Message, (string)ex.Json["data"]["sessionId"]);
                }

                throw ex;
            }
        }

        public async Task<ForgameAccountTokens> RefreshTokens(string refreshToken)
        {
            RequestInit init = new RequestInit()
            {
                Method = "POST",
                ContentType = "application/x-www-form-urlencoded",
                Data = $"grant_type=refresh_token&refresh_token={refreshToken}"
            };

            return await Request<ForgameAccountTokens>("https://launcherbff.ru.4game.com/connect/token", init);
        }

        public async Task<bool> SendVerificationCode(string sessionId, string code)
        {
            ActivationCodePayload payload = new ActivationCodePayload()
            {
                Code = code,
                SessionId = sessionId
            };

            RequestInit init = new RequestInit()
            {
                Method = "POST",
                ContentType = "application/json;charset=UTF-8",
                Data = JsonConvert.SerializeObject(payload)
            };

            await Request<object>("https://launcherbff.ru.4game.com/api/guard/accesscodes/activate", init);
            return true;
        }
    }
}
