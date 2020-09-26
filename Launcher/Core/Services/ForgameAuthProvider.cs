using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel.Composition;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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

        private IComputerNameProvider _computerNameProvider;

        private ILauncherIdProvider _launcherIdProvider;

        private IHardwareIdProvider _hardwareIdProvider;

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
                $"{_apiAddress}/connect/token")
            {
                Content = new StringContent(
                    $"username={username}&password={password}&secure=true&grant_type=password",
                    Encoding.UTF8,
                    "application/x-www-form-urlencoded"),
            })
            {
                AddRequestHeaders(httpRequest);

                try
                {
                    return await WebHelper.TryToLoadJsonData<Token>(httpRequest);
                }
                catch (HttpRequestException ex)
                {
                    JObject error = (JObject)JObject.Parse((string)ex.Data["content"])["error"];
                    if (error.ContainsKey("data"))
                    {
                        throw new NeedToConfirmWithCode(
                            (string)error["description"],
                            (string)error["data"]["sessionId"]);
                    }
                    
                    throw new Exception((string)error["description"]);
                }
            }
        }

        public async Task<Token> RefreshTokens(string refreshToken)
        {
            using (HttpRequestMessage httpRequest = new HttpRequestMessage(
                HttpMethod.Post,
                $"{_apiAddress}/connect/token")
            {
                Content = new StringContent(
                    $"refresh_token={refreshToken}&grand_Type=refresh_token",
                    Encoding.UTF8,
                    "application/x-www-form-urlencoded")
            })
            {
                AddRequestHeaders(httpRequest);

                return await WebHelper.TryToLoadJsonData<Token>(httpRequest);
            }
        }

        public async Task SendActivationCode(string sessionId, string code)
        {
            using (HttpRequestMessage httpRequest = new HttpRequestMessage(
                HttpMethod.Post,
                $"{_apiAddress}/api/guard/accesscodes/activate")
            {
                Content = new StringContent(
                    JsonConvert.SerializeObject(new { code, sessionId }),
                    Encoding.UTF8,
                    "application/json")
            })
            {
                AddRequestHeaders(httpRequest);

                try
                {
                    await WebHelper.TryToLoadJsonData<object>(httpRequest);
                }
                catch (HttpRequestException ex)
                {
                    throw new Exception((string)ex.Data["content"]);
                }
            }
        }
    
        private void AddRequestHeaders(HttpRequestMessage httpRequest)
        {
            string computerName = _computerNameProvider.Get();
            string launcherId = _launcherIdProvider.Get();
            string hardwareId = _hardwareIdProvider.Get();

            httpRequest.Headers.TryAddWithoutValidation("Computer-Name", computerName);
            httpRequest.Headers.TryAddWithoutValidation("Hardware-Id", hardwareId);
            httpRequest.Headers.TryAddWithoutValidation("Launcher-Id", launcherId);
        }
    }
}
