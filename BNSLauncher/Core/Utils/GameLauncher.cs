using BNSLauncher.Shared.Infrastructure.Internet.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using BNSLauncher.Core.Interfaces;

namespace BNSLauncher.Core.Utils
{
    class GameLauncher : IGameLauncher
    {
        private IWebSocketHelper socketHelper;

        public GameLauncher(IWebSocketHelper socketHelper)
        {
            this.socketHelper = socketHelper;
        }

        public async Task<string> GetGameAuthString(string accessToken)
        {
            string masterId = new JwtSecurityToken(accessToken).Subject;

            socketHelper.Connect(accessToken);

            string username = (await socketHelper.GetGameAccount(masterId)).Login;
            string password = (await socketHelper.CreateGameTokenCode(accessToken, masterId, username)).Password;

            socketHelper.Disconnect();

            return $"/username:{username} /password:{password}";
        }
    }
}
