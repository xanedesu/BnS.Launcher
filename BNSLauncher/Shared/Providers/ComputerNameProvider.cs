using BNSLauncher.Shared.Providers.Interfaces;
using System;

namespace BNSLauncher.Shared.Providers
{
    class ComputerNameProvider : IComputerNameProvider
    {
        public string Get()
        {
            return Environment.MachineName;
        }
    }
}
