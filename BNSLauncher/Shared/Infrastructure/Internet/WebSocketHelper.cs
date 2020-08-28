using BNSLauncher.Core.Utils;
using BNSLauncher.Shared.Extensions;
using BNSLauncher.Shared.Infrastructure.Internet.Interfaces;
using BNSLauncher.Shared.Infrastructure.Internet.Utils;
using BNSLauncher.Shared.Models;
using BNSLauncher.Shared.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using WebSocketSharp;

namespace BNSLauncher.Shared.Infrastructure.Internet
{
    [Export(typeof(IWebSocketHelper))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    class WebSocketHelper : IWebSocketHelper, IDisposable
    {
        private static readonly string WS_URL = "wss://launcherbff.ru.4game.com/";

        private IComputerNameProvider _computerNameProvider;

        private ILauncherIdProvider _launcherIdProvider;

        private IHardwareIdProvider _hardwareIdProvider;

        private WebSocket _ws;

        public WebSocketHelper(
            IComputerNameProvider computerNameProvider,
            ILauncherIdProvider launcherIdProvider,
            IHardwareIdProvider hardwareIdProvider)
        {
            _computerNameProvider = computerNameProvider;
            _launcherIdProvider = launcherIdProvider;
            _hardwareIdProvider = hardwareIdProvider;
        }

        public void Connect(string accessToken)
        {
            string computerName = Base64.Encode(_computerNameProvider.Get());
            string launcherId = _launcherIdProvider.Get();
            string hardwareId = Base64.Encode(_hardwareIdProvider.Get());

            string url = WS_URL
                .AddOrUpdateParameterToUrl("token", accessToken)
                .AddOrUpdateParameterToUrl("computer-name", computerName)
                .AddOrUpdateParameterToUrl("launcher-id", launcherId)
                .AddOrUpdateParameterToUrl("hardware-id", hardwareId);

            _ws = new WebSocket(url);
            _ws.Connect();
        }

        public void Disconnect()
        {
            _ws.Close();
        }

        private Task<bool> Send(WebSocketRequestMessage message)
        {
            TaskCompletionSource<bool> source = new TaskCompletionSource<bool>();

            _ws.SendAsync(JsonConvert.SerializeObject(message), (bool completed) => source.TrySetResult(completed));

            return source.Task;
        }

        private Task<WebSocketResponse<T>> GetResponse<T>()
        {
            TaskCompletionSource<WebSocketResponse<T>> source = new TaskCompletionSource<WebSocketResponse<T>>();

            _ws.OnMessage += (object sender, MessageEventArgs e) =>
            {
                source.TrySetResult(new WebSocketResponse<T>(e.Data));
            };

            return source.Task;
        }

        public async Task<GameAccount> GetGameAccount(string masterId)
        {
            WebSocketRequestMessage message = new WebSocketRequestMessage()
            {
                Method = "getGameAccount",
                Params = new GameAccountPayload()
                {
                    MasterId = masterId,
                },
                Id = Guid.NewGuid().ToString()
            };

            await Send(message);

            var response = await GetResponse<GameAccount[]>();
            return response.Data.Result[0];
        }

        public async Task<GameLoginCredentials> GetGameLoginCredentials(string accessToken, string masterId, string login)
        {
            WebSocketRequestMessage message = new WebSocketRequestMessage()
            {
                Method = "createGameTokenCode",
                Params = new GameLoginCredentialsPayload()
                {
                    AccessToken = accessToken,
                    IgnoreLicenseAcceptance = false,
                    Login = login,
                    MasterId = masterId
                },
                Id = Guid.NewGuid().ToString()
            };

            await Send(message);

            var response = await GetResponse<GameLoginCredentials>();
            return response.Data.Result;
        }

        public void Dispose()
        {
            _ws.Close();
        }
    }
}
