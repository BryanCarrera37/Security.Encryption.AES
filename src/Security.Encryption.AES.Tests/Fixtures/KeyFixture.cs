using System.Security.Cryptography;

namespace Security.Encryption.AES.Tests.Fixtures
{
    public class KeyFixture
    {
        public byte[] Key { get; }

        public KeyFixture()
        {
            Key = RandomNumberGenerator.GetBytes(32);
        }
    }
}