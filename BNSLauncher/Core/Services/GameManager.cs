using BNSLauncher.Core.Enums;
using BNSLauncher.Core.Exceptions.GameStart;
using BNSLauncher.Core.Models;
using BNSLauncher.Core.Services.Interfaces;
using BNSLauncher.Shared.Models;
using BNSLauncher.Shared.Services.Interfaces;
using System;
using System.Diagnostics;
using System.IO;

namespace BNSLauncher.Core.Services
{
    class GameManager
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
                InstalledGameInfo gameInfo = _gameRepository.GetOrDefault(gameKey);

                string launchString = _gamesConfigProvider.Get(gameKey).LaunchParams.Replace("%LOGIN%", data.Login).Replace("%PASS%", data.Password);
                string launchParams = $"{launchString} {data.Arguments}";

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
