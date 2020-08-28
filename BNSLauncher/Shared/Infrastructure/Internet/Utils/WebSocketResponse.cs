using Newtonsoft.Json;
using BNSLauncher.Shared.Models;

namespace BNSLauncher.Shared.Infrastructure.Internet.Utils
{
    class WebSocketResponse<TResult>
    {
        private string _data;

        public WebSocketResponse(string data)
        {
            _data = data;
        }

        public WebSocketResponseMessage<TResult> Data {
            get => JsonConvert.DeserializeObject<WebSocketResponseMessage<TResult>>(_data);
        }
    }
}
