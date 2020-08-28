using System;

namespace BNSLauncher.Shared.Infrastructure.Internet.Exceptions
{
    [Serializable]
    class NeedToConfirmWithCode : Exception
    {
        public NeedToConfirmWithCode(string message, string sessionId) : base(message)
        {
            SessionId = sessionId;
        }

        public string SessionId { get; }
    }
}
