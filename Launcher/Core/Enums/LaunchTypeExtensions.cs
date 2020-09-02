using Unlakki.Bns.Launcher.Core.Enums;
using System;

namespace Unlakki.Bns.Launcher.Shared.Extensions
{
    public static class LaunchTypeExtensions
    {
        public static LaunchType ToLaunchType(this string launchTypeStr)
        {
            int result;
            return !int.TryParse(launchTypeStr, out result) || !Enum.IsDefined(typeof(LaunchType), result) ? LaunchType.Unknown : (LaunchType)result;
        }
    }
}