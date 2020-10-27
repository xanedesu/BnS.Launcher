using System.Threading.Tasks;

namespace Unlakki.Bns.Launcher.Core.Infrastructure.WebSocket.Interfaces
{
  public interface IWebSocket
  {
    Task ConnectAsync(string accessToken);

    Task SendAsync(string data);

    Task<string> RecieveAsync();

    Task DisconnectAsync();
  }
}
