using BNSLauncher.Shared.Providers.Interfaces;
using System;
using System.ComponentModel.Composition;

namespace BNSLauncher.Shared.Providers
{
    [Export(typeof(IComputerNameProvider))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    class ComputerNameProvider : IComputerNameProvider
    {
        public string Get()
        {
            try {
                return Environment.MachineName;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
    }
}
