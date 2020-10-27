using System;
using System.ComponentModel.Composition;
using Unlakki.Bns.Launcher.Shared.Services.Interfaces;

namespace Unlakki.Bns.Launcher.Shared.Services
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