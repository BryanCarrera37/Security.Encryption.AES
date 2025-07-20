using Security.Encryption.AES.Enums;
using Security.Encryption.AES.Interfaces;

namespace Security.Encryption.AES.Tests.Fixtures
{
    public class GcmFixture : KeyFixture
    {
        public ISecureEncryption Cipher { get; }

        public GcmFixture()
        {
            Cipher = new SecureEncryptionBuilder()
                .WithKey(Key)
                .WithMode(AesEncryptionMode.GCM)
                .Build();
        }
    }
}