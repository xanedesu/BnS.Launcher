using BNSLauncher.Shared.Models;
using System.Threading.Tasks;

namespace BNSLauncher.Shared.Infrastructure.Internet.Interfaces
{
    interface IWebHelper
    {
        Task<ForgameAccountTokens> Authorize(string username, string password);

        Task<ForgameAccountTokens> RefreshTokens(string refreshToken);

        Task<bool> SendVerificationCode(string sessionId, string code);
    }
}
