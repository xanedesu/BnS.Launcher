using BNSLauncher.Shared.Infrastructure.Internet.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace BNSLauncher
{
    class GameLauncher
    {
        private IWebSocketHelper socketHelper;

        public GameLauncher(IWebSocketHelper socketHelper)
        {
            this.socketHelper = socketHelper;
        }

        public async Task<string> GetGameAuthString(string accessToken)
        {
            string masterId = new JwtSecurityToken(accessToken).Subject;

            this.socketHelper.Connect(accessToken);

            var login = (await this.socketHelper.GetGameAccount(masterId)).Login;
            var password = (await this.socketHelper.CreateGameTokenCode(accessToken, masterId, login)).Password;

            this.socketHelper.Disconnect();

            return $"/username:{login} /password:{password}";
        }
    }
}
