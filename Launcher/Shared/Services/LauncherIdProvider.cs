using System;
using System.ComponentModel.Composition;
using System.Threading;
using Unlakki.Bns.Launcher.Shared.Services.Interfaces;

namespace Unlakki.Bns.Launcher.Shared.Services
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
            _launcherIdGenerator = launcherIdGenerator;
            _launcherInSystemRegistrator = launcherInSystemRegistrator;

            RefreshValue();
        }

        public string Get()
        {
            if (string.IsNullOrWhiteSpace(_launcherId.Value))
            {
                RefreshValue();
            }

            return _launcherId.Value;
        }

        private void RefreshValue()
        {
            _launcherId = new Lazy<string>(
                new Func<string>(GetCore), LazyThreadSafetyMode.ExecutionAndPublication);
        }

        private string GetCore()
        {
            try
            {
                string launcherId = _launcherInSystemRegistrator
                    .GetLauncherSoftwareInfo("4game2.0").LauncherId;

                if (string.IsNullOrEmpty(launcherId))
                {
                    launcherId = _launcherIdGenerator.GenegateNewId();
                    _launcherInSystemRegistrator.SetLauncherIdIfNotExist("4game2.0", launcherId);
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
