using System;

namespace BNSLauncher.Net.Exceptions
{
    class NeedConfirmWithCode : Exception
    {
        public NeedConfirmWithCode(string message, string sessionId): base(message)
        {
            this.SessionId = sessionId;
        }

        public string SessionId { get; }
    }
}
