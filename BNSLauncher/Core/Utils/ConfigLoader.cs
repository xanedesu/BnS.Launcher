using BNSLauncher.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace BNSLauncher.Core.Utils
{
    class ConfigLoader : IDisposable
    {
        private static readonly string CONFIG_FILE = "cfg";

        private Config config;

        private ConfigLoader(Config config)
        {
            this.config = config;
        }

        public static ConfigLoader LoadConfig()
        {
            try
            {
                Config config = ReadConfig();

                return new ConfigLoader(config);
            }
            catch (FileNotFoundException)
            {
                Config newConfig = new Config()
                {
                    PathToGame = null,
                    Accounts = new Account[0],
                    PrefferedAccount = null,
                };

                return new ConfigLoader(newConfig);
            }
        }

        public Dictionary<string, Auth> GetUsers() {
            Dictionary<string, Auth> users = new Dictionary<string, Auth>();

            foreach (Account account in config.Accounts)
            {
                users.Add(account.Username, account.Auth);
            }

            return users;
        }

        public string GetPathToGame() {
            return config.PathToGame;
        }

        public string GetPrefferedAccount() {
            return config.PrefferedAccount;
        }

        public static void SavePathToGame(string pathToGame)
        {
            using (ConfigLoader loader = LoadConfig())
            {
                Config config = loader.config;
                Config newConfig = new Config()
                {
                    PathToGame = pathToGame,
                    Accounts = config.Accounts,
                    PrefferedAccount = config.PrefferedAccount
                };

                WriteConfig(newConfig);
            }
        }

        public static void SaveAccount(string username, Auth auth)
        {
            using (ConfigLoader loader = LoadConfig())
            {
                Config config = loader.config;
                Account[] accounts = config.Accounts;

                int index = Array.FindIndex(config.Accounts, (account) => account.Username == username);
                if (index == -1)
                {
                    Array.Resize(ref accounts, accounts.Length + 1);

                    accounts[accounts.Length - 1] = new Account()
                    {
                        Username = username,
                        Auth = auth
                    };
                }
                else
                {
                    accounts[index] = new Account()
                    {
                        Username = username,
                        Auth = auth
                    };
                }

                Config newConfig = new Config()
                {
                    PathToGame = config.PathToGame,
                    Accounts = accounts,
                    PrefferedAccount = config.PrefferedAccount
                };

                WriteConfig(newConfig);
            }
        }

        public static void SavePrefferedAccount(string username)
        {
            using (ConfigLoader loader = LoadConfig())
            {
                Config config = loader.config;
                Config newConfig = new Config()
                {
                    PathToGame = config.PathToGame,
                    Accounts = config.Accounts,
                    PrefferedAccount = username
                };

                WriteConfig(newConfig);
            }
        }

        private static Config ReadConfig()
        {
            string text = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), CONFIG_FILE));
            return JsonConvert.DeserializeObject<Config>(text);
        }

        private static void WriteConfig(Config config)
        {
            string data = JsonConvert.SerializeObject(config);
            File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), CONFIG_FILE), data);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                config = null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
