using BNSLauncher.Shared.Services.Interfaces;
using System;
using System.ComponentModel.Composition;

namespace BNSLauncher.Shared.Services
{
    [Export(typeof(IComputerNameProvider))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    class ComputerNameProvider : IComputerNameProvider
    {
        public string Get()
        {
            try
            {
                return Environment.MachineName;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
