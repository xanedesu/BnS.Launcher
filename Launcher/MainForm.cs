using System.Windows.Forms;
using Unlakki.Bns.Launcher.Components;
using Unlakki.Bns.Launcher.Components.Router;
using Unlakki.Bns.Launcher.Core.Infrastructure.Crypto;
using Unlakki.Bns.Launcher.Core.Services;
using Unlakki.Bns.Launcher.Shared.Services;

namespace Unlakki.Bns.Launcher
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            var launcherInWindowsRegistrator = new LauncherInWindowsRegistrator();
            var launcherIdGenerator = new LauncherIdGenerator();

            var computerNameProvider = new ComputerNameProvider();
            var launcherIdProvider = new LauncherIdProvider(launcherInWindowsRegistrator, launcherIdGenerator);
            var hardwareIdProvider = new HardwareIdProvider(launcherIdProvider);

            var forgameAuthProvider = new ForgameAuthProvider(computerNameProvider, launcherIdProvider, hardwareIdProvider);

            var aesStorage = new AesStorage(computerNameProvider);
            var cryptoManager = new CryptoManager(aesStorage);

            var launcherConfigDataProvider = new LauncherConfigDataProvider(cryptoManager);
            var launcherConfigParser = new LauncherConfigJsonParser();
            var launcherConfigProvider = new LauncherConfigProvider(launcherConfigDataProvider, launcherConfigParser);
            launcherConfigProvider.Init();

            Router _router = new Router(this);

            _router.AddRoute(
                "/",
                () => new LauncherPage(
                    computerNameProvider,
                    launcherIdProvider,
                    hardwareIdProvider,
                    launcherConfigProvider,
                    forgameAuthProvider),
                new RouteData { Title = "bns-ru" });

            _router.AddRoute(
                "/auth",
                () => new LoginPage(launcherConfigProvider, forgameAuthProvider),
                new RouteData { Title = "bns-ru: Login" });
            
            _router.AddRoute(
                "/auth/activate/{sessionId}",
                () => new ActivationCodePage(launcherConfigProvider, forgameAuthProvider),
                new RouteData { Title = "bns-ru: Activate" });

            _router.AddRoute(
                "/settings",
                () => new SettingsPage(launcherConfigProvider),
                new RouteData { Title = "bns-ru: Settings" });
        }
    }
}
