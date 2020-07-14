using System;

namespace BNSLauncher.Shared.Exceptions
{
    public class BadRegistryPathPart : Exception
    {
        public BadRegistryPathPart(string part)
          : base("Bad registry path part $" + part)
        {
        }
    }
}
