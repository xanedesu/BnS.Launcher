using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Text;
using Unlakki.Bns.Launcher.Core.Infrastructure.Crypto.Interfaces;

namespace Unlakki.Bns.Launcher.Core.Services
{
    [Export(typeof(ILauncherConfigDataProvider))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class LauncherConfigDataProvider : ILauncherConfigDataProvider
    {
        private readonly int _blockSize = 1024;

        private readonly string _configPath = Path.Combine(
            Directory.GetCurrentDirectory(), ".config");

        private readonly ICryptoManager _cryptoManager;

        [ImportingConstructor]
        public LauncherConfigDataProvider(ICryptoManager cryptoManager)
        {
            _cryptoManager = cryptoManager;
        }

        public string Read()
        {
            if (!File.Exists(_configPath))
            {
                throw new FileNotFoundException("File not found", _configPath);
            }

            using (FileStream fileStream = File.OpenRead(_configPath))
            {
                byte[] buffer = new byte[_blockSize];
                UTF8Encoding utf8Encoding = new UTF8Encoding(true);

                string textContent = string.Empty;

                int bytesRead;
                do
                {
                    bytesRead = fileStream.Read(buffer, 0, buffer.Length);
                    textContent += utf8Encoding.GetString(buffer, 0, bytesRead);
                    Array.Clear(buffer, 0, bytesRead);
                } while (bytesRead > 0);

                return _cryptoManager.Decrypt(textContent);
            }
        }

        public void Write(string configString)
        {
            if (File.Exists(_configPath))
            {
                File.Delete(_configPath);
            }

            string textContent = _cryptoManager.Encrypt(configString);
            UTF8Encoding utf8Encoding = new UTF8Encoding(true);
            byte[] buffer = utf8Encoding.GetBytes(textContent);
            using (FileStream fileStream = File.OpenWrite(_configPath))
            {
                fileStream.Write(buffer, 0, buffer.Length);
            }

            File.SetAttributes(_configPath, FileAttributes.Hidden);
        }
    }
}
