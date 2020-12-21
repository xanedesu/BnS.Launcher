using System;

namespace Unlakki.Bns.Launcher.Shared.Exceptions
{
    [Serializable]
    public class BadRegistryPathPart : Exception
    {
        public BadRegistryPathPart(string part)
          : base("Bad registry path part $" + part) { }
    }
}
