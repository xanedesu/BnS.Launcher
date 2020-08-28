using BNSLauncher.Core.Enums;

namespace BNSLauncher.Core.Models
{
    class GameLaunchData
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public GameVersion Version { get; set; }

        public string Arguments { get; set; }
    }
}
