namespace BNSLauncher.Shared.Models.GameConfig
{
    class BnsConfig : GameConfig
    {
        public override string GameKey { get => "bns-ru_live"; }

        public override string LaunchParams { get => "/username:%LOGIN% /password:%PASS%"; }
    }
}
