using System.ComponentModel.Composition;
using Unlakki.Bns.Launcher.Core.Services.Interfaces;
using Unlakki.Bns.Launcher.Shared.Services.Interfaces;

namespace Unlakki.Bns.Launcher.Core.Services
{
  [Export(typeof(GameRepositoryFactory))]
  [PartCreationPolicy(CreationPolicy.Shared)]
  public class GameRepositoryFactory
  {
    private readonly IGameInSystemRegistrator _gameInSystemRegistrator;

    [ImportingConstructor]
    public GameRepositoryFactory(IGameInSystemRegistrator gameInSystemRegistrator)
    {
      _gameInSystemRegistrator = gameInSystemRegistrator;
    }

    public IGameRepository Get()
    {
      return new GameRepository(_gameInSystemRegistrator);
    }
  }
}