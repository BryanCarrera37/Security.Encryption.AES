namespace Security.Encryption.AES.Enums
{
    public enum AesEncryptionMode
    {
        /// <summary>
        /// Cipher Block Chaining mode.
        /// This mode requires an Initialization Vector (IV) that is generated automatically for encryption and decryption.
        /// </summary>
        CBC,

        /// <summary>
        /// Galois/Counter Mode (GCM).
        /// This mode is authenticated encryption with associated data (AEAD) and requires an Initialization Vector (IV) and a tag.
        /// The tag size is applied to 16 bytes.
        /// </summary>
        GCM
    };
}