using System.Threading.Tasks;
using Unlakki.Bns.Launcher.Core.Models.GameAccount;

namespace Unlakki.Bns.Launcher.Core.Services.Interfaces
{
    interface IGameAuthProvider
    {
        Task<GameTokenCode> GetGameTokenCode(string accessToken);
    }
}
