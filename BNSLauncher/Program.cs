using BNSLauncher.Shared.Providers;
using BNSLauncher.Shared.Services;
using System;
using System.Windows.Forms;

namespace BNSLauncher
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            LauncherInWindowsRegistrator launcherInWindowsRegistrator = new LauncherInWindowsRegistrator();
            LauncherIdGenerator launcherIdGenerator = new LauncherIdGenerator();

            ComputerNameProvider computerNameProvider = new ComputerNameProvider();
            LauncherIdProvider launcherIdProvider = new LauncherIdProvider(launcherInWindowsRegistrator, launcherIdGenerator);
            HardwareIdProvider hardwareIdProvider = new HardwareIdProvider(launcherIdProvider);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LauncherForm(computerNameProvider, launcherIdProvider, hardwareIdProvider));
        }
    }
}
