using System.Security.Cryptography;
using System.Text;
using Security.Encryption.AES.Interfaces;

namespace Security.Encryption.AES
{
    public class AesGcmCipher : IGcmSecureEncryption
    {
        private readonly byte[] _key;

        public int TagSize => 16;

        public int IvSize => 12;

        internal AesGcmCipher(byte[] key)
        {
            _key = key;
        }

        public string Decrypt(byte[] payload)
        {
            var iv = payload.Take(IvSize).ToArray();
            var tag = payload.Skip(IvSize).Take(TagSize).ToArray();
            var cipher = payload.Skip(IvSize + TagSize).ToArray();

            byte[] plainBytes = new byte[cipher.Length];
            using var aes = new AesGcm(_key, TagSize);
            aes.Decrypt(iv, cipher, tag, plainBytes);

            return Encoding.UTF8.GetString(plainBytes);
        }

        public byte[] Encrypt(string plainText)
        {
            byte[] iv = new byte[IvSize];
            RandomNumberGenerator.Fill(iv);

            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] cipherBytes = new byte[plainBytes.Length];
            byte[] tag = new byte[TagSize];

            using var aes = new AesGcm(_key, TagSize);
            aes.Encrypt(iv, plainBytes, cipherBytes, tag);
            return [.. iv, .. tag, .. cipherBytes];
        }
    }
}