using System.Security.Cryptography;
using System.Text;
using Security.Encryption.AES.Interfaces;

namespace Security.Encryption.AES
{
    public class AesCbcCipher : ISecureEncryption
    {
        private readonly byte[] _key;
        public int IvSize => 16;

        internal AesCbcCipher(byte[] key)
        {
            _key = key;
        }

        public string Decrypt(byte[] payload)
        {
            var iv = payload.Take(IvSize).ToArray();
            var encryptedData = payload.Skip(IvSize).ToArray();

            using var aes = Aes.Create();
            aes.Key = _key;
            aes.IV = iv;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using var decoder = aes.CreateDecryptor();
            var plain = decoder.TransformFinalBlock(encryptedData, 0, encryptedData.Length);
            Console.WriteLine($"Decrypted data: {Encoding.UTF8.GetString(plain)}");
            return Encoding.UTF8.GetString(plain);
        }

        public byte[] Encrypt(string plainText)
        {
            using var aes = Aes.Create();
            aes.Key = _key;
            aes.GenerateIV();
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            var plainBytes = Encoding.UTF8.GetBytes(plainText);
            using var encoder = aes.CreateEncryptor();
            var encrypted = encoder.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
            return [.. aes.IV, .. encrypted];
        }
    }
}