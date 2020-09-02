using System;

namespace Unlakki.Bns.Launcher.Core.Exceptions
{
    [Serializable]
    public class BadRequest : Exception
    {
        public BadRequest(string message) : base(message)
        {
        }
    }
}
