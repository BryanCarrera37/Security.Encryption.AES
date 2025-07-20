using System.Security.Cryptography;
using Security.Encryption.AES.Enums;
using Security.Encryption.AES.Interfaces;
using Security.Encryption.AES.Tests.Fixtures;

namespace Security.Encryption.AES.Tests
{
    public class AesGcmCipherTests(GcmFixture fixture) : IClassFixture<GcmFixture>
    {
        private readonly ISecureEncryption _cipher = fixture.Cipher;

        [Fact]
        public void Encrypt_Then_Decrypt_ShouldReturnOriginalText()
        {
            var original = "Hello, secure world!";

            var encrypted = _cipher.Encrypt(original);
            var decrypted = _cipher.Decrypt(encrypted);

            Assert.Equal(original, decrypted);
        }

        [Fact]
        public void Decrypt_WithTamperedCiphertext_ShouldThrowAuthenticationTagMismatchException()
        {
            var plaintext = "Sensitive data";
            var encrypted = _cipher.Encrypt(plaintext);

            // Tamper with ciphertext (flip a bit)
            encrypted[^1] ^= 0xFF;

            Assert.Throws<AuthenticationTagMismatchException>(() => _cipher.Decrypt(encrypted));
        }

        [Fact]
        public void Decrypt_WithTamperedTag_ShouldThrowAuthenticationTagMismatchException()
        {
            var plaintext = "Data with integrity";
            var encrypted = _cipher.Encrypt(plaintext);

            // Tamper with authentication tag
            encrypted[_cipher.IvSize] ^= 0xAA;

            Assert.Throws<AuthenticationTagMismatchException>(() => _cipher.Decrypt(encrypted));
        }

        [Fact]
        public void Decrypt_WithTamperedIV_ShouldThrowAuthenticationTagMismatchException()
        {
            var plaintext = "Nonce matters";
            var encrypted = _cipher.Encrypt(plaintext);

            // Tamper with IV
            encrypted[0] ^= 0x01;

            Assert.Throws<AuthenticationTagMismatchException>(() => _cipher.Decrypt(encrypted));
        }

        [Fact]
        public void Encrypt_ShouldProduceDifferentOutputs_ForSameInputDueToRandomIV()
        {
            var plaintext = "Same message";

            var encrypted1 = _cipher.Encrypt(plaintext);
            var encrypted2 = _cipher.Encrypt(plaintext);

            Assert.NotEqual(Convert.ToBase64String(encrypted1), Convert.ToBase64String(encrypted2));
        }

        [Fact]
        public void Decrypt_WithInvalidPayloadLength_ShouldThrow()
        {
            // Payload shorter than IV + TAG
            var invalidPayload = new byte[_cipher.IvSize + ((IGcmSecureEncryption)_cipher).TagSize - 1];

            Assert.ThrowsAny<Exception>(() => _cipher.Decrypt(invalidPayload));
        }

        [Fact]
        public void Encrypt_WithEmptyString_ShouldReturnNonNullPayload()
        {
            var encrypted = _cipher.Encrypt(string.Empty);
            Assert.NotNull(encrypted);
            Assert.True(encrypted.Length >= _cipher.IvSize + ((IGcmSecureEncryption)_cipher).TagSize);
        }

        [Fact]
        public void Decrypt_EmptyPlaintext_ShouldReturnEmptyString()
        {
            var encrypted = _cipher.Encrypt(string.Empty);
            var decrypted = _cipher.Decrypt(encrypted);
            Assert.Equal(string.Empty, decrypted);
        }
    }
}
