using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Management;
using Unlakki.Bns.Launcher.Shared.Extensions;
using Unlakki.Bns.Launcher.Shared.Services.Interfaces;

namespace Unlakki.Bns.Launcher.Shared.Services
{
    [Export(typeof(IHardwareIdProvider))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public sealed class HardwareIdProvider : IHardwareIdProvider
    {
        private readonly ILauncherIdProvider _launcherIdProvider;

        private readonly Lazy<string> _hardwareId;

        [ImportingConstructor]
        public HardwareIdProvider(ILauncherIdProvider launcherIdProvider)
        {
            _launcherIdProvider = launcherIdProvider;
            _hardwareId = new Lazy<string>(new Func<string>(GetHardwareId));
        }

        public string Get()
        {
            return _hardwareId.Value;
        }

        private string GetHardwareId()
        {
            string motherBoardId = GetMotherBoardId();
            string str1 = motherBoardId != null
                ? motherBoardId.Replace(" ", "").SubstringOrSelf(0, 53)
                : null;
            string hardDiskId = GetHardDiskId();
            string str2 = hardDiskId != null ? hardDiskId.Replace(" ", "").SubstringOrSelf(0, 53) : null;
            string cpuId = GetCpuId();
            string str3 = cpuId != null ? cpuId.Replace(" ", "").SubstringOrSelf(0, 53) : null;

            if (string.IsNullOrWhiteSpace(str1)
                && string.IsNullOrWhiteSpace(str1) && string.IsNullOrWhiteSpace(str1))
            {
                return _launcherIdProvider.Get();
            }

            return $"{str1}-{str2}-{str3}".Normalize();
        }

        private string GetMotherBoardId()
        {
            try
            {
                return new ManagementObjectSearcher("SELECT * FROM Win32_BaseBoard")
                    .Get()
                    .OfType<ManagementObject>()
                    .Select(v => v["SerialNumber"]?.ToString())
                    .FirstOrDefault(v => v != null);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        private string GetHardDiskId()
        {
            try
            {
                ManagementObject managementObject = new ManagementObject(
                    $"win32_logicaldisk.deviceid=\"{Path.GetPathRoot(Environment.SystemDirectory).Substring(0, 2).ToLower()}\"");
                managementObject.Get();

                return managementObject["VolumeSerialNumber"].ToString();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        private string GetCpuId()
        {
            try
            {
                return new ManagementObjectSearcher("Select * From Win32_processor")
                    .Get()
                    .OfType<ManagementObject>()
                    .Select(v => v["ProcessorId"]?.ToString())
                    .FirstOrDefault(v => v != null);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
