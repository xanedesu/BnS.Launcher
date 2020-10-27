using System;
using System.ComponentModel.Composition;
using Unlakki.Bns.Launcher.Shared.Services.Interfaces;
using Unlakki.Bns.Launcher.Shared.Utils;

namespace Unlakki.Bns.Launcher.Shared.Services
{
  [Export(typeof(ILauncherInSystemRegistrator))]
  [PartCreationPolicy(CreationPolicy.Shared)]
  public class LauncherInWindowsRegistrator : ILauncherInSystemRegistrator
  {
    [ImportingConstructor]
    public LauncherInWindowsRegistrator()
    {
    }

    public void Register(LauncherRegistrationData registrationData, bool createShortcut)
    {
      RegisterInRegistry(registrationData);
      AddLauncherUrlScheme(registrationData);
      if (!createShortcut)
        return;
      CreateShortcuts(registrationData);
    }

    public void Unregister(LauncherRegistrationData unregistrationData)
    {
      try
      {
        RegistryHelper.DeleteUninstallInfo(unregistrationData.Key);
        //ShortcutHelper.DeleteDesktopShortcut(unregistrationData.Name);
        //ShortcutHelper.DeleteStartMenuShortcut("Innova Co. SARL", unregistrationData.Name);
        RegistryHelper.DeleteUrlScheme(unregistrationData.UrlSchemeName);
      }
      catch (Exception)
      {
        throw;
      }
    }

    public bool IsRegistered(string launcherKey)
    {
      return RegistryHelper.UninstallInfoExists(launcherKey);
    }

    public void SetLauncherIdIfNotExist(string launcherKey, string launcherId)
    {
      RegistryHelper.TrySetLauncherIdIfNotExist("Innova Co. SARL", launcherKey, launcherId);
    }

    public void Update(LauncherRegistrationDataUpdate updateData)
    {
      if (!RegistryHelper.TryUpdateUninstallInfo(updateData.Key, new RegistryUninstallInfo()
      {
        Version = updateData.Version,
        SizeBytes = new long?(updateData.SizeBytes),
        Name = updateData.Name,
        InstallationDate = updateData.UpdateDate,
        UninstallCommand = updateData.UninstallCommand,
        Publisher = "Innova Co. SARL"
      }))
      {
        throw new InvalidOperationException(
          "Can't update launcher uninstall info  " + updateData.Key + " in uninstall registry");
      }
      UpdateSoftwareInfo(new RegisterLauncherSoftwareInfo()
      {
        Publisher = "Innova Co. SARL",
        LauncherKey = updateData.Key,
        Version = updateData.Version,
        LastUpdateDate = new DateTime?(updateData.UpdateDate)
      });
    }

    public string GetInstallPath(string launcherKey)
    {
      return RegistryHelper.GetUninstallInfo(launcherKey).InstallationPath;
    }

    public RegisterLauncherSoftwareInfo GetLauncherSoftwareInfo(
      string launcherKey)
    {
      return RegistryHelper.GetRegisterLauncherSoftwareData("Innova Co. SARL", launcherKey);
    }

    public void UpdateSoftwareInfo(RegisterLauncherSoftwareInfo updateData)
    {
      if (!RegistryHelper.TryRegisterLauncherSoftwareData(updateData))
      {
        throw new InvalidOperationException(
          "Can't update launcher info " + updateData.LauncherKey + " in software registry");
      }
    }

    private void CreateShortcuts(LauncherRegistrationData registrationData)
    {
      try
      {
        //ShortcutHelper.CreateOrReplaceDesktopShortcut(registrationData.Name, registrationData.IconPath, registrationData.IconTarget, (string)null);
        //ShortcutHelper.CreateOrReplaceStartMenuShortcut("Innova Co. SARL", registrationData.Name, registrationData.IconPath, registrationData.IconTarget);
      }
      catch (Exception)
      {
        throw;
      }
    }

    private void RegisterInRegistry(LauncherRegistrationData registrationData)
    {
      bool flag = !IsRegistered(registrationData.Key);
      if (!RegistryHelper.TryRegisterUninstallInfo(registrationData.Key, new RegistryUninstallInfo()
      {
        Version = registrationData.Version,
        SizeBytes = new long?(registrationData.SizeBytes),
        InstallationPath = registrationData.InstallationPath,
        IconPath = registrationData.IconPath,
        Name = registrationData.Name,
        InstallationDate = registrationData.InstallationDate,
        UninstallCommand = registrationData.UninstallCommand,
        Publisher = "Innova Co. SARL"
      }))
      {
        throw new InvalidOperationException(
          "Can't register launcher " + registrationData.Key + " in unninstall registry");
      }
      if (!RegistryHelper.TryRegisterLauncherSoftwareData(new RegisterLauncherSoftwareInfo()
      {
        Publisher = "Innova Co. SARL",
        LauncherKey = registrationData.Key,
        Version = registrationData.Version,
        InstallationPath = registrationData.InstallationPath,
        InstallationDate = new DateTime?(registrationData.InstallationDate),
        OldGamesTaken = flag ? new bool?(false) : new bool?(),
        LastGamesInstallDirectory = registrationData.LastGamesInstallPath,
        LauncherRegion = registrationData.LauncherRegion
      }))
      {
        RegistryHelper.DeleteUninstallInfo(registrationData.Key);
        throw new InvalidOperationException(
          "Can't register launcher " + registrationData.Key + " in software registry");
      }
    }

    private void AddLauncherUrlScheme(LauncherRegistrationData registrationData)
    {
      if (!RegistryHelper.TryRegisterUrlScheme(registrationData.UrlSchemeName, registrationData.RunnerPath))
      {
        throw new InvalidOperationException("Can't register launcher urlScheme");
      }
    }
  }
}
