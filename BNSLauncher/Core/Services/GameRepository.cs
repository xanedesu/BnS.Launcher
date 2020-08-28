using BNSLauncher.Core.Services.Interfaces;
using BNSLauncher.Shared.Models;
using BNSLauncher.Shared.Services.Interfaces;
using System.ComponentModel.Composition;

namespace BNSLauncher.Core.Services
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