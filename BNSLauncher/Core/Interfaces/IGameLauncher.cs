using System.Threading.Tasks;

namespace BNSLauncher.Core.Interfaces
{
    interface IGameLauncher
    {
        Task<string> GetGameAuthString(string accessToken);
    }
}
