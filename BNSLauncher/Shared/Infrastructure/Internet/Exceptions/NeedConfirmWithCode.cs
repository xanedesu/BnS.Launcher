using System;

namespace BNSLauncher.Shared.Infrastructure.Internet.Exceptions
{
    [Serializable]
    class NeedConfirmWithCode : Exception
    {
        public NeedConfirmWithCode(string message, string sessionId): base(message)
        {
            this.SessionId = sessionId;
        }

        public string SessionId { get; }
    }
}
