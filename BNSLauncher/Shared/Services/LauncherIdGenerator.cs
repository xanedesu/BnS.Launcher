using BNSLauncher.Shared.Services.Interfaces;
using System;
using System.ComponentModel.Composition;

namespace BNSLauncher.Shared.Services
{
    [Export(typeof(ILauncherIdGenerator))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class LauncherIdGenerator : ILauncherIdGenerator
    {
        public string GenegateNewId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}