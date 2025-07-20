using System.Security.Cryptography;
using Security.Encryption.AES.Interfaces;
using Security.Encryption.AES.Tests.Fixtures;

namespace Security.Encryption.AES.Tests
{
    public class AesCbcCipherTests(CbcFixture fixture) : IClassFixture<CbcFixture>
    {
        private readonly ISecureEncryption _cipher = fixture.Cipher;

        [Fact]
        public void Encrypt_Then_Decrypt_ShouldReturnOriginalText()
        {
            var originalText = "Hello, World!";
            var encryptedText = _cipher.Encrypt(originalText);
            var decryptedText = _cipher.Decrypt(encryptedText);

            Assert.Equal(originalText, decryptedText);
        }

        [Fact]
        public void Encrypt_ShouldReturnNonNullAndNonEmptyByteArray()
        {
            var encrypted = _cipher.Encrypt("Test");

            Assert.NotNull(encrypted);
            Assert.NotEmpty(encrypted);
            Assert.True(encrypted.Length > _cipher.IvSize);
        }

        [Fact]
        public void Decrypt_WithTamperedCiphertext_ShouldThrowCryptographicException()
        {
            var encrypted = _cipher.Encrypt("Attack at dawn");
            encrypted[^1] ^= 0xFF;

            Assert.Throws<CryptographicException>(() => _cipher.Decrypt(encrypted));
        }

        [Fact]
        public void Decrypt_WithTamperedIV_ShouldProduceIncorrectPlaintext()
        {
            var input = "Defend at dusk";
            var encrypted = _cipher.Encrypt(input);

            encrypted[0] ^= 0x01;
            var decrypted = _cipher.Decrypt(encrypted);

            Assert.NotEqual(input, decrypted);
        }

        [Fact]
        public void EncryptDecrypt_EmptyString_ShouldWork()
        {
            var encrypted = _cipher.Encrypt(string.Empty);
            var decrypted = _cipher.Decrypt(encrypted);

            Assert.Equal(string.Empty, decrypted);
        }

        [Fact]
        public void Decrypt_WithShortPayload_ShouldThrow()
        {
            var tooShort = new byte[_cipher.IvSize - 1]; // Not even a full IV
            Assert.ThrowsAny<Exception>(() => _cipher.Decrypt(tooShort));
        }
    }
}