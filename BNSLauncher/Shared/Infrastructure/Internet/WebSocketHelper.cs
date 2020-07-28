using BNSLauncher.Core.Utils;
using BNSLauncher.Shared.Extensions;
using BNSLauncher.Shared.Infrastructure.Internet.Interfaces;
using BNSLauncher.Shared.Models;
using BNSLauncher.Shared.Providers.Interfaces;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using WebSocketSharp;

namespace BNSLauncher.Shared.Infrastructure.Internet
{
    class WebSocketHelper: IWebSocketHelper, IDisposable
    {
        private static readonly string WS_URL = "wss://launcherbff.ru.4game.com/";

        private IComputerNameProvider computerNameProvider;

        private ILauncherIdProvider launcherIdProvider;

        private IHardwareIdProvider hardwareIdProvider;

        private WebSocket ws;

        public WebSocketHelper(IComputerNameProvider computerNameProvider, ILauncherIdProvider launcherIdProvider, IHardwareIdProvider hardwareIdProvider)
        {
            this.computerNameProvider = computerNameProvider;
            this.launcherIdProvider = launcherIdProvider;
            this.hardwareIdProvider = hardwareIdProvider;
        }

        public void Connect(string accessToken)
        {
            string computerName = Base64.Encode(computerNameProvider.Get());
            string launcherId = launcherIdProvider.Get();
            string hardwareId = Base64.Encode(hardwareIdProvider.Get());

            string url = WS_URL
                .AddOrUpdateParameterToUrl("token", accessToken)
                .AddOrUpdateParameterToUrl("computer-name", computerName)
                .AddOrUpdateParameterToUrl("launcher-id", launcherId)
                .AddOrUpdateParameterToUrl("hardware-id", hardwareId);

            ws = new WebSocket(url);
            ws.Connect();
        }

        public void Disconnect()
        {
            ws.Close();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                ws.Close();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private Task<bool> Send(string data)
        {
            TaskCompletionSource<bool> source = new TaskCompletionSource<bool>();

            ws.SendAsync(data, (bool completed) => source.TrySetResult(completed));

            return source.Task;
        }

        private Task<Func<T>> GetReciever<T>()
        {
            TaskCompletionSource<Func<T>> source = new TaskCompletionSource<Func<T>>();

            ws.OnMessage += (object sender, MessageEventArgs e) =>
            {
                source.TrySetResult(() => JsonConvert.DeserializeObject<T>(e.Data));
            };

            return source.Task;
        }

        public async Task<GameAccount> GetGameAccount(string masterId)
        {
            WebSocketMessage message = new WebSocketMessage()
            {
                Method = "getGameAccount",
                Params = new GetGameAccountMessage()
                {
                    MasterId = masterId,
                },
                Id = Guid.NewGuid().ToString()
            };

            await Send(JsonConvert.SerializeObject(message));

            Func<WebSocketResponse<GameAccount[]>> reciever = await GetReciever<WebSocketResponse<GameAccount[]>>();
            WebSocketResponse<GameAccount[]> account = reciever();

            return account.Result[0];
        }

        public async Task<GameToken> CreateGameTokenCode(string accessToken, string masterId, string login)
        {
            WebSocketMessage message = new WebSocketMessage()
            {
                Method = "createGameTokenCode",
                Params = new CreateGameTokenCodeMessage()
                {
                    AccessToken = accessToken,
                    IgnoreLicenseAcceptance = false,
                    Login = login,
                    MasterId = masterId
                },
                Id = Guid.NewGuid().ToString()
            };

            await Send(JsonConvert.SerializeObject(message));

            Func<WebSocketResponse<GameToken>> reciever = await GetReciever<WebSocketResponse<GameToken>>();
            WebSocketResponse<GameToken> token = reciever();

            return token.Result;
        }
    }
}
