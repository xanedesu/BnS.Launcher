using Newtonsoft.Json.Linq;
using System;

namespace BNSLauncher.Shared.Infrastructure.Internet.Exceptions
{
    [Serializable]
    class RequestError : Exception
    {
        public RequestError(string message, JObject json) : base(message)
        {
            Json = json;
        }

        public JObject Json { get; }
    }
}
