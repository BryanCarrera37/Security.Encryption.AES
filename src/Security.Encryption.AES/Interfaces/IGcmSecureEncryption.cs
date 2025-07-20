namespace Security.Encryption.AES.Interfaces
{
    public interface IGcmSecureEncryption : ISecureEncryption
    {
        /// <summary>
        /// Get the size of the tag used in GCM mode.
        /// </summary>
        int TagSize { get; }
    }
}