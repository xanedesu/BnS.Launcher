using System;
using System.ComponentModel.Composition;
using Unlakki.Bns.Launcher.Shared.Services.Interfaces;

namespace Unlakki.Bns.Launcher.Shared.Services
{
    [Export(typeof(IComputerNameProvider))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ComputerNameProvider : IComputerNameProvider
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
