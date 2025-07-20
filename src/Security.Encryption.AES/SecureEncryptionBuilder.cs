using Security.Encryption.AES.Enums;
using Security.Encryption.AES.Exceptions;
using Security.Encryption.AES.Interfaces;

namespace Security.Encryption.AES
{
    public class SecureEncryptionBuilder
    {
        private byte[]? _key;
        private AesEncryptionMode _mode = AesEncryptionMode.GCM;

        public SecureEncryptionBuilder WithKey(byte[] key)
        {
            _key = key;
            return this;
        }

        public SecureEncryptionBuilder WithMode(AesEncryptionMode mode)
        {
            _mode = mode;
            return this;
        }

        public ISecureEncryption Build()
        {
            if (WasNotTheKeySet())
                throw new KeyNullException("Key must be set before building the encryption instance.");

            return _mode switch
            {
                AesEncryptionMode.CBC => new AesCbcCipher(_key!),
                AesEncryptionMode.GCM => new AesGcmCipher(_key!),
                _ => throw new NotSupportedException($"The encryption mode '{_mode}' is not supported.")
            };
        }

        private bool WasNotTheKeySet() => _key == null || _key.Length == 0;
    }
}