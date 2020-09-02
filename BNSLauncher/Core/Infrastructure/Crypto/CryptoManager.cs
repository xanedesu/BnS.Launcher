using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Unlakki.Bns.Launcher.Core.Infrastructure.Crypto.Interfaces;

namespace Unlakki.Bns.Launcher.Core.Infrastructure.Crypto
{
    [Export(typeof(ICryptoManager))]
    public class CryptoManager : ICryptoManager
    {
        private Aes _aes;

        [ImportingConstructor]
        public CryptoManager(IAesStorage aesStorage)
        {
            _aes = Aes.Create();
            _aes.Key = aesStorage.GetKey();
            _aes.IV = aesStorage.GetIV();
        }

        public string Encrypt(string input)
        {
            ICryptoTransform encryptor = _aes.CreateEncryptor(_aes.Key, _aes.IV);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (CryptoStream crypto = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter encrypt = new StreamWriter(crypto))
                    {
                        encrypt.Write(input);
                    }
                    return Convert.ToBase64String(memoryStream.ToArray());
                }
            }
        }

        public string Decrypt(string input)
        {
            ICryptoTransform decryptor = _aes.CreateDecryptor(_aes.Key, _aes.IV);
            using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(input)))
            {
                using (CryptoStream crypto = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader decrypt = new StreamReader(crypto))
                    {
                        return decrypt.ReadToEnd();
                    }
                }
            }
            //return input;
        }
    }
}