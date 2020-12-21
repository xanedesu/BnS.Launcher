namespace Unlakki.Bns.Launcher.Core.Services
{
    public interface ILauncherConfigDataProvider
    {
        string Read();

        void Write(string configText);
    }
}
