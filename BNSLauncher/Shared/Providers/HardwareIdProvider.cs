using BNSLauncher.Shared.Extensions;
using BNSLauncher.Shared.Providers.Interfaces;
using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Management;

namespace BNSLauncher.Shared.Providers
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
            this._launcherIdProvider = launcherIdProvider;
            this._hardwareId = new Lazy<string>(new Func<string>(this.GetHardwareId));
        }

        public string Get()
        {
            return this._hardwareId.Value;
        }

        private string GetHardwareId()
        {
            string motherBoardId = this.GetMotherBoardId();
            string str1 = motherBoardId != null ? motherBoardId.Replace(" ", "").SubstringOrSelf(0, 53) : (string)null;
            string hardDiskId = this.GetHardDiskId();
            string str2 = hardDiskId != null ? hardDiskId.Replace(" ", "").SubstringOrSelf(0, 53) : (string)null;
            string cpuId = this.GetCpuId();
            string str3 = cpuId != null ? cpuId.Replace(" ", "").SubstringOrSelf(0, 53) : (string)null;
            if (string.IsNullOrWhiteSpace(str1) && string.IsNullOrWhiteSpace(str1) && string.IsNullOrWhiteSpace(str1))
                return this._launcherIdProvider.Get();
            return (str1 + "-" + str2 + "-" + str3).Normalize();
        }

        private string GetMotherBoardId()
        {
            try
            {
                return new ManagementObjectSearcher("SELECT * FROM Win32_BaseBoard").Get().OfType<ManagementObject>().Select<ManagementObject, string>((Func<ManagementObject, string>)(v => v["SerialNumber"]?.ToString())).FirstOrDefault<string>((Func<string, bool>)(v => v != null));
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        private string GetHardDiskId()
        {
            try
            {
                ManagementObject managementObject = new ManagementObject("win32_logicaldisk.deviceid=\"" + Path.GetPathRoot(Environment.SystemDirectory).Substring(0, 2).ToLower() + "\"");
                managementObject.Get();
                return managementObject["VolumeSerialNumber"].ToString();
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        private string GetCpuId()
        {
            try
            {
                return new ManagementObjectSearcher("Select * From Win32_processor").Get().OfType<ManagementObject>().Select<ManagementObject, string>((Func<ManagementObject, string>)(v => v["ProcessorId"]?.ToString())).FirstOrDefault<string>((Func<string, bool>)(v => v != null));
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
    }
}