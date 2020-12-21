using System;
using System.ComponentModel.Composition;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Unlakki.Bns.Launcher.Core.Infrastructure.WebSocket;
using Unlakki.Bns.Launcher.Core.Infrastructure.WebSocket.Models;
using Unlakki.Bns.Launcher.Core.Models;
using Unlakki.Bns.Launcher.Core.Models.GameAccount;
using Unlakki.Bns.Launcher.Core.Services.Interfaces;

namespace Unlakki.Bns.Launcher.Core.Services
{
    [Export(typeof(IGameAuthProvider))]
    class GameAuthProvider : IGameAuthProvider
    {
        private readonly WebSocket _ws;

        [ImportingConstructor]
        public GameAuthProvider(WebSocket ws)
        {
            _ws = ws;
        }

        public async Task<GameTokenCode> GetGameTokenCode(string accessToken)
        {
            await _ws.ConnectAsync(accessToken);

            JwtSecurityToken jwt = new JwtSecurityToken(accessToken);

            await _ws.SendAsync(JsonConvert.SerializeObject(new WebSocketRequest {
                Id = Guid.NewGuid().ToString(),
                Method = "getGameAccount",
                Params = new GetGameAccountParams {
                    MasterId = jwt.Subject,
                }
            }));
            string gameAccountsString = await _ws.RecieveAsync();
            GameAccount gameAccount = JsonConvert
              .DeserializeObject<WebSocketResponse<GameAccount[]>>(gameAccountsString).Result[0];

            await _ws.SendAsync(JsonConvert.SerializeObject(new WebSocketRequest {
                Id = Guid.NewGuid().ToString(),
                Method = "createGameTokenCode",
                Params = new CreateGameTokenCodeParams {
                    AccessToken = accessToken,
                    IgnoreLicenseAcceptance = false,
                    Login = gameAccount.Login,
                    MasterId = jwt.Subject 
                }
            }));
            string responseString = await _ws.RecieveAsync();
            WebSocketResponse<GameTokenCode> response = JsonConvert
                .DeserializeObject<WebSocketResponse<GameTokenCode>>(responseString);

            await _ws.DisconnectAsync();

            if (response.Error == null)
            {
                return response.Result;
            }

            throw new Exception(response.Error.Code);
        }
    }
}
