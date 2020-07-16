using BNSLauncher.Shared.Models;
using System.Threading.Tasks;

namespace BNSLauncher.Shared.Infrastructure.Internet.Interfaces
{
    interface IWebHelper
    {
        Task<AuthData> Authorize(string username, string password);

        Task<AuthData> RefreshTokens(string refreshToken);

        Task<bool> SendVerificationCode(string sessionId, string code);
    }
}
