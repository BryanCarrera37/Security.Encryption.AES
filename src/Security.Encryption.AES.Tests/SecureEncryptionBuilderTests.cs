using System.Security.Cryptography;
using Security.Encryption.AES.Enums;
using Security.Encryption.AES.Exceptions;
using Security.Encryption.AES.Tests.Fixtures;

namespace Security.Encryption.AES.Tests
{
    public class SecureEncryptionBuilderTests(KeyFixture fixture) : IClassFixture<KeyFixture>
    {
        private readonly byte[] _key = fixture.Key;
        private readonly SecureEncryptionBuilder _encryptionBuilder = new();

        [Fact]
        public void Build_WithNullKey_ShouldThrowKeyNullException()
        {
            Assert.Throws<KeyNullException>(() => _encryptionBuilder.Build());
        }

        [Fact]
        public void Build_OnlyWithValidKey_ShouldReturnGcmCipherInstance()
        {
            var cipher = _encryptionBuilder
                .WithKey(_key)
                .Build();
            Assert.NotNull(cipher);
            Assert.IsType<AesGcmCipher>(cipher);
        }

        [Fact]
        public void Build_WithValidKeyAndMode_ShouldReturnTheRightCipherInstance()
        {
            var cbcCipher = _encryptionBuilder.WithKey(_key)
                .WithMode(AesEncryptionMode.CBC)
                .Build();
            var gcmCipher = _encryptionBuilder.WithKey(_key)
                .WithMode(AesEncryptionMode.GCM)
                .Build();

            Assert.NotNull(cbcCipher);
            Assert.NotNull(gcmCipher);
            Assert.IsType<AesCbcCipher>(cbcCipher);
            Assert.IsType<AesGcmCipher>(gcmCipher);
        }
    }
}