using System;

namespace Unlakki.Bns.Launcher.Core.Exceptions
{
  [Serializable]
  public class NeedToConfirmWithCode : Exception
  {
    public string SessionId { get; }

    public NeedToConfirmWithCode(string message, string sessionId) : base(message)
    {
      SessionId = sessionId;
    }
  }
}
