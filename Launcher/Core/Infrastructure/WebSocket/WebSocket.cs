using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Unlakki.Bns.Launcher.Core.Infrastructure.WebSocket.Interfaces;
using Unlakki.Bns.Launcher.Shared.Extensions;
using Unlakki.Bns.Launcher.Shared.Services.Interfaces;

namespace Unlakki.Bns.Launcher.Core.Infrastructure.WebSocket
{
    [Export(typeof(IWebSocket))]
    public class WebSocket : IWebSocket
    {
        private string _wsAddress = "wss://launcherbff.ru.4game.com";

        private IComputerNameProvider _computerNameProvider;

        private ILauncherIdProvider _launcherIdProvider;

        private IHardwareIdProvider _hardwareIdProvider;

        private ClientWebSocket _ws;

        [ImportingConstructor]
        public WebSocket(
            IComputerNameProvider computerNameProvider,
            ILauncherIdProvider launcherIdProvider,
            IHardwareIdProvider hardwareIdProvider)
        {
            _computerNameProvider = computerNameProvider;
            _launcherIdProvider = launcherIdProvider;
            _hardwareIdProvider = hardwareIdProvider;
        }

        public async Task ConnectAsync(string accessToken)
        {
            _ws = new ClientWebSocket();

            string computerName = Convert.ToBase64String(
                Encoding.Default.GetBytes(_computerNameProvider.Get()));
            string launcherId = _launcherIdProvider.Get();
            string hardwareId = Convert.ToBase64String(
                Encoding.Default.GetBytes(_hardwareIdProvider.Get()));

            string uriString = _wsAddress
                .AddOrUpdateParameterToUrl("token", accessToken)
                .AddOrUpdateParameterToUrl("computer-name", computerName)
                .AddOrUpdateParameterToUrl("launcher-id", launcherId)
                .AddOrUpdateParameterToUrl("hardware-id", hardwareId);

            await _ws.ConnectAsync(new Uri(uriString), CancellationToken.None);
        }

        public async Task SendAsync(string data)
        {
            ArraySegment<byte> buffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(data));
            await _ws.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
        }

        public async Task<string> RecieveAsync()
        {
            byte[] messageBytes = new byte[0];

            ArraySegment<byte> buffer;
            WebSocketReceiveResult result;

            do
            {
                buffer = new ArraySegment<byte>(new byte[1024]);
                result = await _ws.ReceiveAsync(buffer, CancellationToken.None);

                Array.Resize(ref messageBytes, messageBytes.Length + result.Count);
                Array.Copy(
                    buffer.Array.Take(result.Count).ToArray(),
                    0,
                    messageBytes,
                    messageBytes.Length - result.Count,
                    result.Count);
            } while (!result.EndOfMessage);

            return Encoding.UTF8.GetString(messageBytes);
        }

        public async Task DisconnectAsync()
        {
            await _ws.CloseAsync(
                WebSocketCloseStatus.NormalClosure, "Disconnect", CancellationToken.None);
        }
    }
}
