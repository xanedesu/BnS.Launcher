using System.Threading.Tasks;

namespace Unlakki.Bns.Launcher.Shared.Services.Interfaces
{
  public interface IGamesConfigDataProvider
  {
    Task<string> GetData();
  }
}
