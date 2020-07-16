using BNSLauncher.Shared.Extensions;
using BNSLauncher.Shared.Models;
using BNSLauncher.Shared.Providers.Interfaces;
using BNSLauncher.Shared.Utils;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using WebSocketSharp;

namespace BNSLauncher.Shared.Infrastructure.Internet
{
    class WebSocketHelper
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

            this.ws = new WebSocket(url);
            this.ws.Connect();
        }

        private Task<bool> Send(string data)
        {
            TaskCompletionSource<bool> source = new TaskCompletionSource<bool>();

            this.ws.SendAsync(data, (bool completed) => source.TrySetResult(completed));

            return source.Task;
        }

        private Task<Func<T>> GetReciever<T>()
        {
            TaskCompletionSource<Func<T>> source = new TaskCompletionSource<Func<T>>();

            this.ws.OnMessage += (object sender, MessageEventArgs e) =>
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

            await this.Send(JsonConvert.SerializeObject(message));

            Func<WebSocketResponse<GameAccount[]>> reciever = await this.GetReciever<WebSocketResponse<GameAccount[]>>();
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

            await this.Send(JsonConvert.SerializeObject(message));

            Func<WebSocketResponse<GameToken>> reciever = await this.GetReciever<WebSocketResponse<GameToken>>();
            WebSocketResponse<GameToken> token = reciever();

            return token.Result;
        }
    }
}
