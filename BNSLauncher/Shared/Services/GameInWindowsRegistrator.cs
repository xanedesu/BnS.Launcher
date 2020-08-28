using BNSLauncher.Shared.Models;
using BNSLauncher.Shared.Services.Interfaces;
using BNSLauncher.Shared.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;

namespace BNSLauncher.Shared.Services
{
    [Export(typeof(IGameInSystemRegistrator))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class GameInWindowsRegistrator : IGameInSystemRegistrator
    {
        public static readonly string GameIconName = "4game_icon.ico";

        public void Register(GameRegistrationData registrationData)
        {
            string iconPath = this.GetIconPath(registrationData.InstallationPath);
            registrationData.IconPath = iconPath;
            bool gameWasInstalled = this.IsRegistered(registrationData.LauncherKey, registrationData.Key);
            this.RegisterInRegistry(registrationData);
            this.CreateDesktopShortcut(registrationData, gameWasInstalled);
        }

        public void Unregister(GameUnregistrationData unregistrationData)
        {
            try
            {
                RegistryHelper.DeleteUninstallInfo(this.GetUninstallGameRegistryKey(unregistrationData.LauncherKey, unregistrationData.Key));
                RegistryHelper.DeleteGameSoftwareData(new DeleteGameSoftwareInfo()
                {
                    Publisher = "Innova Co. SARL",
                    LauncherKey = unregistrationData.LauncherKey,
                    GameName = unregistrationData.Name
                });
                //FileSystemHelper.DeleteFileIfExists(this.GetIconPath(unregistrationData.InstallationPath));
                //ShortcutHelper.DeleteDesktopShortcut(unregistrationData.Name);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool IsRegistered(string launcherKey, string gameRegistrationKey)
        {
            return RegistryHelper.UninstallInfoExists(this.GetUninstallGameRegistryKey(launcherKey, gameRegistrationKey));
        }

        public List<InstalledGameInfo> GetInstalledGames(string launcherKey)
        {
            string gameKeysPrefix = launcherKey + "_";
            return RegistryHelper.GetUninstallInfoKeys(gameKeysPrefix).Select(k =>
            {
                RegistryUninstallInfo uninstallInfo = RegistryHelper.GetUninstallInfo(k);
                RegisterGameSoftwareInfo gameSoftwareInfo = uninstallInfo != null ? RegistryHelper.GetGameSoftwareData("Innova Co. SARL", launcherKey, uninstallInfo.Name) : (RegisterGameSoftwareInfo)null;
                return new
                {
                    Key = k,
                    Info = uninstallInfo,
                    SoftwareInfo = gameSoftwareInfo
                };
            }).Where(e => e.Info != null && e.SoftwareInfo != null).Select(info => new InstalledGameInfo()
            {
                GameKey = info.Key.Replace(gameKeysPrefix, ""),
                GameName = info.Info.Name,
                Path = info.Info.InstallationPath,
                Version = info.Info.Version,
                InstallationDate = new DateTime?(info.Info.InstallationDate),
                IconPath = info.Info.IconPath
            }).ToList<InstalledGameInfo>();
        }

        private void CreateDesktopShortcut(GameRegistrationData registrationData, bool gameWasInstalled)
        {
            try
            {
                File.WriteAllBytes(registrationData.IconPath, registrationData.IconData);
                if (gameWasInstalled)
                    return;
                //ShortcutHelper.CreateOrReplaceDesktopShortcut(registrationData.Name, registrationData.IconPath, registrationData.RunnerPath, registrationData.RunnerArgs);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void RegisterInRegistry(GameRegistrationData registrationData)
        {
            string uninstallGameRegistryKey = this.GetUninstallGameRegistryKey(registrationData.LauncherKey, registrationData.Key);
            long? nullable = new long?();
            long result;
            if (long.TryParse(registrationData.Size, out result))
                nullable = new long?(result);
            if (!RegistryHelper.TryRegisterUninstallInfo(uninstallGameRegistryKey, new RegistryUninstallInfo()
            {
                Version = registrationData.Version,
                SizeBytes = nullable,
                InstallationPath = registrationData.InstallationPath,
                IconPath = registrationData.IconPath,
                Name = registrationData.Name,
                InstallationDate = registrationData.InstallationDate,
                UninstallCommand = registrationData.UninstallCommand,
                Publisher = "Innova Co. SARL"
            }))
            {
                throw new Exception("Can't register game " + uninstallGameRegistryKey + " in unninstall registry");
            }
            if (!RegistryHelper.TryRegisterGameSoftwareData(new RegisterGameSoftwareInfo()
            {
                Publisher = "Innova Co. SARL",
                LauncherKey = registrationData.LauncherKey,
                Version = registrationData.Version,
                InstallationPath = registrationData.InstallationPath,
                InstallationDate = new DateTime?(registrationData.InstallationDate),
                GameName = registrationData.Name
            }))
            {
                RegistryHelper.DeleteUninstallInfo(uninstallGameRegistryKey);
                throw new Exception("Can't register game " + uninstallGameRegistryKey + " in software registry");
            }
        }

        private string GetIconPath(string gamePath)
        {
            return Path.Combine(gamePath, GameInWindowsRegistrator.GameIconName);
        }

        private string GetUninstallGameRegistryKey(string launcherKey, string gameKey)
        {
            return launcherKey + "_" + gameKey;
        }
    }
}
