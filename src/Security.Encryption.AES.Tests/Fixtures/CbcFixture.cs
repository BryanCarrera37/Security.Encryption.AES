using Security.Encryption.AES.Enums;
using Security.Encryption.AES.Interfaces;

namespace Security.Encryption.AES.Tests.Fixtures
{
    public class CbcFixture : KeyFixture
    {
        public ISecureEncryption Cipher { get; }

        public CbcFixture()
        {
            Cipher = new SecureEncryptionBuilder()
                .WithKey(Key)
                .WithMode(AesEncryptionMode.CBC)
                .Build();
        }
    }
}