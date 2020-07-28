using Newtonsoft.Json.Linq;
using System;

namespace BNSLauncher.Shared.Infrastructure.Internet.Exceptions
{
    class FetchException : Exception
    {
        public readonly JObject Json;

        public FetchException(string message, JObject json) : base(message)
        {
            Json = json;
        }
    }
}
