using BNSLauncher.Shared.Models;
using System.Threading.Tasks;

namespace BNSLauncher.Shared.Infrastructure.Internet.Interfaces
{
    interface IWebSocketHelper
    {
        void Connect(string accessToken);
        Task<GameAccount> GetGameAccount(string masterId);

        Task<GameToken> CreateGameTokenCode(string accessToken, string masterId, string login);
    }
}
