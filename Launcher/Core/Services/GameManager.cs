using System;
using System.Diagnostics;
using System.IO;
using Unlakki.Bns.Launcher.Core.Enums;
using Unlakki.Bns.Launcher.Core.Exceptions.GameStart;
using Unlakki.Bns.Launcher.Core.Models;
using Unlakki.Bns.Launcher.Core.Services.Interfaces;
using Unlakki.Bns.Launcher.Shared.Models;
using Unlakki.Bns.Launcher.Shared.Models.GameConfig;
using Unlakki.Bns.Launcher.Shared.Services.Interfaces;

namespace Unlakki.Bns.Launcher.Core.Services
{
    public class GameManager
    {
        private IGameRepository _gameRepository;

        private IGamesConfigProvider _gamesConfigProvider;

        public GameManager(
            IGameInSystemRegistrator gameInSystemRegistrator,
            IGamesConfigProvider gamesConfigProvider)
        {
            _gameRepository = new GameRepositoryFactory(gameInSystemRegistrator).Get();
            _gamesConfigProvider = gamesConfigProvider;
        }

        public void Launch(string gameKey, GameLaunchData data)
        {
            try
            {
                GameConfig gameConfig = _gamesConfigProvider.Get(gameKey);

                string launchString = gameConfig.LaunchParams.Replace("%LOGIN%", data.Login).Replace("%PASS%", data.Password);
                string launchParams = $"{launchString} {data.Arguments}";

                InstalledGameInfo gameInfo = _gameRepository.GetOrDefault(gameConfig.EnvKey);

                string gameLaunchPath = GetFullGamePath(gameInfo.Path, data.Version);
                if (!File.Exists(gameLaunchPath))
                {
                    throw new GameStartException(gameKey, "game_exe_not_found", $"Not found game exe by path {gameLaunchPath}");
                }
                string directoryName = Path.GetDirectoryName(gameLaunchPath);

                ProcessStartInfo startInfo = new ProcessStartInfo()
                {
                    FileName = gameLaunchPath,
                    WorkingDirectory = directoryName,
                    Arguments = launchParams
                };

                Process process = Process.Start(startInfo);
                if (process == null || process.HasExited)
                {
                    throw new GameStartException(gameKey, "game_exe_not_found", $"Not started game exe {gameLaunchPath}");
                }
            }
            catch (Exception)
            {
            }
        }

        public string GetFullGamePath(string path, GameVersion version)
        {
           switch (version)
            {
                case GameVersion.x32:
                    return Path.Combine(path, "bin", "Client.exe");
                case GameVersion.x64:
                    return Path.Combine(path, "bin64", "Client.exe");
                default:
                    throw new ArgumentOutOfRangeException("Invalid game version");
            }
        }
    }
}
