using System.Threading.Tasks;
using WebSocketSharp;

namespace BNSLauncher.WebSockets
{
    class WebSocketClient
    {
        private WebSocket _ws;

        public WebSocketClient(string url)
        {
            _ws = new WebSocket(url);

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
