using System;

namespace BNSLauncher.Net.Exceptions
{
    class RequestException: Exception
    {
        public RequestException(string message): base(message)
        {

        }
    }
}
