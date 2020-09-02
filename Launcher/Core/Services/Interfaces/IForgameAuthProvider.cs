using System.Threading.Tasks;
using Unlakki.Bns.Launcher.Core.Models.Account;

namespace Unlakki.Bns.Launcher.Core.Services.Interfaces
{
    public interface IForgameAuthProvider
    {
        Task<Tokens> Authorize(string username, string password);

        Task<Tokens> RefreshTokens(string refreshToken);

        Task SendActivationCode(string sessionId, string code);
    }
}
