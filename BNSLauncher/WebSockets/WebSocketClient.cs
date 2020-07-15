using System.Threading.Tasks;
using WebSocketSharp;
using BNSLauncher.Shared.Providers.Interfaces;
using BNSLauncher.Utils;
using Newtonsoft.Json.Linq;

namespace BNSLauncher.WebSockets
{
    class WebSocketClient
    {
        private static readonly string WS_URL = "wss://launcherbff.ru.4game.com/";

        private IComputerNameProvider computerNameProvider;

        private ILauncherIdProvider launcherIdProvider;

        private IHardwareIdProvider hardwareIdProvider;

        private WebSocket _ws;

        public WebSocketClient(IComputerNameProvider computerNameProvider, ILauncherIdProvider launcherIdProvider, IHardwareIdProvider hardwareIdProvider)
        {
            this.computerNameProvider = computerNameProvider;
            this.launcherIdProvider = launcherIdProvider;
            this.hardwareIdProvider = hardwareIdProvider;

            
        }

        public void Connect(string accessToken)
        {
            string computerName = Base64.Base64Encode(computerNameProvider.Get());
            string launcherId = launcherIdProvider.Get();
            string hardwareId = Base64.Base64Encode(hardwareIdProvider.Get());

            _ws = new WebSocket($"{WS_URL}?token={accessToken}&hardware-id={hardwareId}&launcher-id={launcherId}&computer-name={computerName}");
            _ws.Connect();
        }

        public Task<string> Send(string data)
        {
            TaskCompletionSource<string> source = new TaskCompletionSource<string>();

            _ws.SendAsync(data, (bool completed) =>
            {
                _ws.OnMessage += (object sender, MessageEventArgs e) =>
                {
                    source.TrySetResult(e.Data);
                };
            });

            return source.Task;
        }

        //public async Task<string> GetGameAccount()
        //{

        //}

        //public async Task<string> CreateGameTokenCode(string username)
        //{

        //}
    }
}
