namespace BNSLauncher.Shared.Models.GameConfig
{
    abstract class GameConfig
    {
        public abstract string GameKey { get; }

        public abstract string LaunchParams { get; }
    }
}
