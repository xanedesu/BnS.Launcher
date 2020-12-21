namespace Unlakki.Bns.Launcher.Core.Infrastructure.Crypto.Interfaces
{
    public interface ICryptoManager
    {
        string Encrypt(string input);

        string Decrypt(string input);
    }
}
