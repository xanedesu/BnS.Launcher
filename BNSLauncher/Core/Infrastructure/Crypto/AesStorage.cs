using System;
using System.ComponentModel.Composition;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Unlakki.Bns.Launcher.Core.Infrastructure.Crypto.Interfaces;
using Unlakki.Bns.Launcher.Shared.Services.Interfaces;

namespace Unlakki.Bns.Launcher.Core.Infrastructure.Crypto
{
    [Export(typeof(ILauncherIdProvider))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class AesStorage : IAesStorage
    {
        private readonly IComputerNameProvider _computerNameProvider;
        
        private Lazy<byte[]> _key;

        private Lazy<byte[]> _iv;

        [ImportingConstructor]
        public AesStorage(IComputerNameProvider computerNameProvider)
        {
            _computerNameProvider = computerNameProvider;
        }

        public byte[] GetKey()
        {
            if (_key == null)
            {
                _key = new Lazy<byte[]>(new Func<byte[]>(GetComputerNameHash), LazyThreadSafetyMode.ExecutionAndPublication);
            }

            return _key.Value;
        }

        public byte[] GetIV()
        {
            if (_iv == null)
            {
                _iv = new Lazy<byte[]>(new Func<byte[]>(GetComputerNameHash), LazyThreadSafetyMode.ExecutionAndPublication);
            }

            return _iv.Value;
        }

        private byte[] GetComputerNameHash()
        {
            return HashAlgorithm.Create("MD5").ComputeHash(Encoding.UTF8.GetBytes(_computerNameProvider.Get()));
        }
    }
}
