namespace Security.Encryption.AES.Exceptions
{
    /// <summary>
    /// This exception is used to indicate that the key must be provided before performing encryption or decryption operations.
    /// </summary>
    /// <param name="message">The message to be applied to the Exception</param>
    public class KeyNullException(string? message) : Exception(message ?? "The encryption key cannot be null")
    {
    }
}