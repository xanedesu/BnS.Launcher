using System.Threading.Tasks;
using Unlakki.Bns.Launcher.Core.Models.Account;

namespace Unlakki.Bns.Launcher.Core.Services.Interfaces
{
  public interface IForgameAuthProvider
  {
    Task<Token> Authorize(string username, string password);

    Task<Token> Refresh(string refreshToken);

    Task SendActivationCode(string sessionId, string code);
  }
}
