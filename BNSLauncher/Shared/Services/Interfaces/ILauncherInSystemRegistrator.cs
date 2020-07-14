using BNSLauncher.Shared.Utils;

namespace BNSLauncher.Shared.Services.Interfaces
{
    public interface ILauncherInSystemRegistrator
    {
        void Register(LauncherRegistrationData registrationData, bool createShortcut);

        void Unregister(LauncherRegistrationData unregistrationData);

        bool IsRegistered(string registrationKey);

        void SetLauncherIdIfNotExist(string launcherKey, string launcherId);

        void Update(LauncherRegistrationDataUpdate updateData);

        string GetInstallPath(string registrationKey);

        RegisterLauncherSoftwareInfo GetLauncherSoftwareInfo(string launcherKey);

        void UpdateSoftwareInfo(RegisterLauncherSoftwareInfo updateData);
    }
}
