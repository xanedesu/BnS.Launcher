namespace Unlakki.Bns.Launcher.Core.Infrastructure.Crypto.Interfaces
{
    public interface IAesStorage
    {
        byte[] GetKey();

        byte[] GetIV();
    }
}
