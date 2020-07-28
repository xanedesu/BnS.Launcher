using BNSLauncher.Shared.Services.Interfaces;
using BNSLauncher.Shared.Providers.Interfaces;
using System;
using System.ComponentModel.Composition;
using System.Threading;

namespace BNSLauncher.Shared.Providers
{
    [Export(typeof(ILauncherIdProvider))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class LauncherIdProvider : ILauncherIdProvider
    {
        private readonly ILauncherInSystemRegistrator _launcherInSystemRegistrator;
        private readonly ILauncherIdGenerator _launcherIdGenerator;
        private Lazy<string> _launcherId;

        [ImportingConstructor]
        public LauncherIdProvider(
          ILauncherInSystemRegistrator launcherInSystemRegistrator,
          ILauncherIdGenerator launcherIdGenerator)
        {
            this._launcherIdGenerator = launcherIdGenerator;
            this._launcherInSystemRegistrator = launcherInSystemRegistrator;
            this.RefreshValue();
        }

        public string Get()
        {
            if (string.IsNullOrWhiteSpace(this._launcherId.Value))
                this.RefreshValue();
            return this._launcherId.Value;
        }

        private void RefreshValue()
        {
            this._launcherId = new Lazy<string>(new Func<string>(this.GetCore), LazyThreadSafetyMode.ExecutionAndPublication);
        }

        private string GetCore()
        {
            try
            {
                string launcherId = this._launcherInSystemRegistrator.GetLauncherSoftwareInfo("4game2.0").LauncherId;
                if (string.IsNullOrEmpty(launcherId))
                {
                    launcherId = this._launcherIdGenerator.GenegateNewId();
                    this._launcherInSystemRegistrator.SetLauncherIdIfNotExist("4game2.0", launcherId);
                }
                return launcherId;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
