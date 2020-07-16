using BNSLauncher.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace BNSLauncher.Utils
{
    class ConfigLoader
    {
        private static readonly string CONFIG_FILE = "cfg";

        private Config _cfg;

        private ConfigLoader(Config config)
        {
            this._cfg = config;
        }

        public static ConfigLoader LoadConfig()
        {
            try
            {
                string text = Read();
                return new ConfigLoader(JsonConvert.DeserializeObject<Config>(text));
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

        public Dictionary<string, Auth> GetUsers()
        {
            Dictionary<string, Auth> users = new Dictionary<string, Auth>();

            Account[] accounts = this._cfg.Accounts;
            for (int i = 0; i < accounts.Length; i++)
            {
                Account account = accounts[i];
                users.Add(account.Username, account.Auth);
            }

            return users;
        }

        public string GetPathToGame()
        {
            return this._cfg.PathToGame;
        }

        public string GetPrefferedAccount()
        {
            return this._cfg.PrefferedAccount;
        }

        public static void SavePathToGame(string pathToGame)
        {
            ConfigLoader config = LoadConfig();

            string data = JsonConvert.SerializeObject(
                new Config()
                {
                    PathToGame = pathToGame,
                    Accounts = config._cfg.Accounts,
                    PrefferedAccount = config._cfg.PrefferedAccount
                });

            Write(data);
        }

        public static void SaveAccount(string username, Auth auth)
        {
            ConfigLoader config = LoadConfig();

            if (Array.FindIndex(config._cfg.Accounts, (u) => u.Username == username) == -1)
            {
                Account account = new Account()
                {
                    Username = username,
                    Auth = auth
                };

                Account[] accounts = new Account[config._cfg.Accounts.Length + 1];
                config._cfg.Accounts.CopyTo(accounts, 0);
                accounts[accounts.Length - 1] = account;

                string data = JsonConvert.SerializeObject(new Config()
                    {
                        PathToGame = config._cfg.PathToGame,
                        Accounts = accounts,
                        PrefferedAccount = config._cfg.PrefferedAccount
                    });

                Write(data);
            }
        }

        public static void SavePrefferedAccount(string username)
        {
            ConfigLoader config = LoadConfig();

            string data = JsonConvert.SerializeObject(
                new Config()
                {
                    PathToGame = config._cfg.PathToGame,
                    Accounts = config._cfg.Accounts,
                    PrefferedAccount = username
                });

            Write(data);
        }

        private static string Read()
        {
            return File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), CONFIG_FILE));
        }

        private static void Write(string data)
        {
            File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), CONFIG_FILE), data);
        }
    }
}
