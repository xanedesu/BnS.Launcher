using System;

namespace Unlakki.Bns.Launcher.Core.Exceptions.GameStart
{
    public class GameStartException : Exception
    {
        public string GameKey { get; }

        public string Code { get; }

        public GameStartException(string gameKey, string code)
          : base("Game problem. Code: " + code + " Game: " + gameKey)
        {
            Code = code;
            GameKey = gameKey;
        }

        public GameStartException(string gameKey, string code, string message)
          : base(message + " Code: " + code + " Game: " + gameKey)
        {
            Code = code;
            GameKey = gameKey;
        }

        public GameStartException(
          string gameKey,
          string code,
          string message,
          Exception innerException)
          : base(message + " Code: " + code + " Game:" + gameKey, innerException)
        {
            Code = code;
            GameKey = gameKey;
        }
    }
}
