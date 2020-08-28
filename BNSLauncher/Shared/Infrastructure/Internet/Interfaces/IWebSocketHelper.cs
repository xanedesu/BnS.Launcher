using BNSLauncher.Shared.Models;
using System.Threading.Tasks;

namespace BNSLauncher.Shared.Infrastructure.Internet.Interfaces
{
    interface IWebSocketHelper
    {
        void Connect(string accessToken);

        void Disconnect();

        Task<GameAccount> GetGameAccount(string masterId);

        Task<GameLoginCredentials> GetGameLoginCredentials(string accessToken, string masterId, string login);
    }
}
