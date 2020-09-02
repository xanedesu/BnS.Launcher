using System.ComponentModel.Composition;
using Unlakki.Bns.Launcher.Core.Services.Interfaces;
using Unlakki.Bns.Launcher.Shared.Models;
using Unlakki.Bns.Launcher.Shared.Services.Interfaces;

namespace Unlakki.Bns.Launcher.Core.Services
{
    [Export(typeof(IGameRepository))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class GameRepository : IGameRepository
    {
        private readonly IGameInSystemRegistrator _gameInSystemRegistrator;

        [ImportingConstructor]
        public GameRepository(IGameInSystemRegistrator gameInSystemRegistrator)
        {
            _gameInSystemRegistrator = gameInSystemRegistrator;
        }

        public InstalledGameInfo GetOrDefault(string gameKey)
        {
            return _gameInSystemRegistrator.GetInstalledGames("4game2.0").Find((InstalledGameInfo gameInfo) => gameInfo.GameKey == gameKey);
        }
    }
}