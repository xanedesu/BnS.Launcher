using System;
using System.ComponentModel.Composition;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Unlakki.Bns.Launcher.Core.Exceptions;
using Unlakki.Bns.Launcher.Core.Models.Account;
using Unlakki.Bns.Launcher.Core.Services.Interfaces;
using Unlakki.Bns.Launcher.Shared.Services.Interfaces;
using Unlakki.Bns.Launcher.Shared.Utils;

namespace Unlakki.Bns.Launcher.Core.Services
{
    [Export(typeof(IForgameAuthProvider))]
    public class ForgameAuthProvider : IForgameAuthProvider
    {
        private string _apiAddress = "https://launcherbff.ru.4game.com";

        private readonly IComputerNameProvider _computerNameProvider;

        private readonly ILauncherIdProvider _launcherIdProvider;

        private readonly IHardwareIdProvider _hardwareIdProvider;

        public ForgameAuthProvider(
            IComputerNameProvider computerNameProvider,
            ILauncherIdProvider launcherIdProvider,
            IHardwareIdProvider hardwareIdProvider)
        {
            _computerNameProvider = computerNameProvider;
            _launcherIdProvider = launcherIdProvider;
            _hardwareIdProvider = hardwareIdProvider;
        }

        public async Task<Token> Authorize(string username, string password)
        {
            using (HttpRequestMessage httpRequest = new HttpRequestMessage(
                HttpMethod.Post,
                $"{_apiAddress}/connect/token") {
                Content = new StringContent(
                    $"grant_type=password&username={username}&password={password}&secure=true",
                    Encoding.UTF8,
                    "application/x-www-form-urlencoded"),
            })
            {
                ComposeWithHeaders(httpRequest);

                try
                {
                    return await WebHelper.TryToLoadJsonData<Token>(httpRequest);
                }
                catch (HttpRequestException ex)
                {
                    throw CreateException(ex);
                }
            }
        }

        public async Task<Token> Refresh(string refreshToken)
        {
            using (HttpRequestMessage httpRequest = new HttpRequestMessage(
                HttpMethod.Post,
                $"{_apiAddress}/connect/token") {
                Content = new StringContent(
                    $"grant_type=refresh_token&refresh_token={refreshToken}",
                    Encoding.UTF8,
                    "application/x-www-form-urlencoded")
            })
            {
                ComposeWithHeaders(httpRequest);

                try
                {
                    return await WebHelper.TryToLoadJsonData<Token>(httpRequest);
                }
                catch (HttpRequestException ex)
                {
                    throw CreateException(ex);
                }
            }
        }

        public async Task SendActivationCode(string sessionId, string code)
        {
            using (HttpRequestMessage httpRequest = new HttpRequestMessage(
                HttpMethod.Post,
                $"{_apiAddress}/api/guard/accesscodes/activate") {
                Content = new StringContent(
                    JsonConvert.SerializeObject(new { code, sessionId }),
                    Encoding.UTF8,
                    "application/json")
            })
            {
                ComposeWithHeaders(httpRequest);

                try
                {
                    await WebHelper.TryToLoadJsonData<JObject>(httpRequest);
                }
                catch (HttpRequestException ex)
                {
                    throw CreateException(ex);
                }
            }
        }

        private void ComposeWithHeaders(HttpRequestMessage httpRequest)
        {
            string computerName = _computerNameProvider.Get();
            string launcherId = _launcherIdProvider.Get();
            string hardwareId = _hardwareIdProvider.Get();

            httpRequest.Headers.TryAddWithoutValidation("Computer-Name", computerName);
            httpRequest.Headers.TryAddWithoutValidation("Hardware-Id", hardwareId);
            httpRequest.Headers.TryAddWithoutValidation("Launcher-Id", launcherId);
        }

        private Exception CreateException(HttpRequestException requestException)
        {
            var content = requestException.Data["content"]?.ToString();
            if (content == null)
            {
                return requestException;
            }

            JObject error = JObject.Parse(content).Value<JObject>("error");
            if (error.ContainsKey("data"))
            {
                return new NeedToConfirmWithCode(
                    error.Value<string>("description"),
                    error.Value<JObject>("data").Value<string>("sessionId"));
            }

            return new Exception(error.Value<string>("description"));
        }
    }
}
