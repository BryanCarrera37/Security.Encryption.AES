namespace Security.Encryption.AES.Interfaces
{
    public interface ISecureEncryption
    {
        /// <summary>
        /// Gets the size of the Initialization Vector (IV) used in encryption.
        /// </summary>
        int IvSize { get; }

        /// <summary>
        /// Decrypts the given payload.
        /// </summary>
        /// <param name="payload">The encrypted data to decrypt (IV + Encrypted).</param>
        /// <returns>The decrypted string.</returns>
        string Decrypt(byte[] payload);

        /// <summary>
        /// Encrypts the given plain text.
        /// </summary>
        /// <param name="plainText">The plain text to encrypt.</param>
        /// <returns>A value with the encrypted data and the IV generated included (IV + Encrypted) as a collection of bytes (byte[])</returns>
        byte[] Encrypt(string plainText);
    }

}