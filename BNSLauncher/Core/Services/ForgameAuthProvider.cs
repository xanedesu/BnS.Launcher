using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel.Composition;
using System.Net.Http;
using System.Net.Http.Headers;
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
        private IComputerNameProvider _computerNameProvider;

        private ILauncherIdProvider _launcherIdProvider;

        private IHardwareIdProvider _hardwareIdProvider;

        public ForgameAuthProvider(IComputerNameProvider computerNameProvider,
            ILauncherIdProvider launcherIdProvider,
            IHardwareIdProvider hardwareIdProvider)
        {
            _computerNameProvider = computerNameProvider;
            _launcherIdProvider = launcherIdProvider;
            _hardwareIdProvider = hardwareIdProvider;
        }

        public async Task<Tokens> Authorize(string username, string password)
        {
            string computerName = _computerNameProvider.Get();
            string launcherId = _launcherIdProvider.Get();
            string hardwareId = _hardwareIdProvider.Get();

            using (HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, "https://launcherbff.ru.4game.com/connect/token"))
            {
                httpRequest.Headers.TryAddWithoutValidation("Computer-Name", computerName);
                httpRequest.Headers.TryAddWithoutValidation("Hardware-Id", hardwareId);
                httpRequest.Headers.TryAddWithoutValidation("Launcher-Id", launcherId);

                httpRequest.Content = new StringContent($"username={username}&password={password}&secure=true&grant_type=password");
                httpRequest.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");

                try
                {
                    return await WebHelper.TryToLoadJsonData<Tokens>(httpRequest);
                }
                catch (HttpRequestException ex)
                {
                    JObject error = (JObject)JObject.Parse((string)ex.Data["body"])["error"];
                    if (error.ContainsKey("data"))
                        throw new NeedToConfirmWithCode((string)error["description"], (string)error["data"]["sessionId"]);
                    
                    throw new BadRequest((string)error["description"]);
                }
            }
        }

        public async Task<Tokens> RefreshTokens(string refreshToken)
        {
            string computerName = _computerNameProvider.Get();
            string launcherId = _launcherIdProvider.Get();
            string hardwareId = _hardwareIdProvider.Get();

            using (HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, "https://launcherbff.ru.4game.com/connect/token"))
            {
                httpRequest.Headers.TryAddWithoutValidation("Computer-Name", computerName);
                httpRequest.Headers.TryAddWithoutValidation("Hardware-Id", hardwareId);
                httpRequest.Headers.TryAddWithoutValidation("Launcher-Id", launcherId);

                httpRequest.Content = new StringContent($"grant_type=refresh_token&refresh_token={refreshToken}");
                httpRequest.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");

                return await WebHelper.TryToLoadJsonData<Tokens>(httpRequest);
            }
        }

        public async Task SendActivationCode(string sessionId, string code)
        {
            string computerName = _computerNameProvider.Get();
            string launcherId = _launcherIdProvider.Get();
            string hardwareId = _hardwareIdProvider.Get();

            using (HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, "https://launcherbff.ru.4game.com/api/guard/accesscodes/activate"))
            {
                httpRequest.Headers.TryAddWithoutValidation("Computer-Name", computerName);
                httpRequest.Headers.TryAddWithoutValidation("Hardware-Id", hardwareId);
                httpRequest.Headers.TryAddWithoutValidation("Launcher-Id", launcherId);

                httpRequest.Content = new StringContent(JsonConvert.SerializeObject(new
                {
                    code,
                    sessionId
                }));
                httpRequest.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json;charset=UTF-8");

                try
                {
                    await WebHelper.TryToLoadJsonData<object>(httpRequest);
                }
                catch (HttpRequestException ex)
                {
                    throw new BadRequest((string)ex.Data["body"]);
                }
            }
        }
    }
}
