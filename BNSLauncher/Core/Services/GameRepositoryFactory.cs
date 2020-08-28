using BNSLauncher.Core.Services.Interfaces;
using BNSLauncher.Shared.Services.Interfaces;
using System.ComponentModel.Composition;

namespace BNSLauncher.Core.Services
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