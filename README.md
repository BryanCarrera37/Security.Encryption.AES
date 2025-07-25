# Security.Encryption.AES

**Security.Encryption.AES** is a .NET library providing easy-to-use, secure AES encryption and decryption utilities. It supports both AES-GCM (Galois/Counter Mode) and AES-CBC (Cipher Block Chaining) modes, making it suitable for a wide range of cryptographic scenarios.

## Table of Content

- [**Functionalities**](#functionalities)
- [**How to install**](#how-to-install)
- [**Usage / Implementation**](#usage--implementation)
  - [**1. Create an Encryptor**](#1-create-an-encryptor)
  - [**2. Encrypt Data**](#2-encrypt-data)
  - [**3. Decrypt Data**](#3-decrypt-data)
  - [**Example with CBC Mode**](#example-with-cbc-mode)
- [**Contribution**](#contribution)
  - [**Test**](#test)

## Functionalities

- **AES-GCM Encryption/Decryption**: Authenticated encryption for modern security needs.
- **AES-CBC Encryption/Decryption**: Traditional block cipher mode with PKCS7 padding.
- **Key and IV management**: Secure handling of cryptographic keys and initialization vectors.
- **Dynamic Key usage**: You provide the key to be used, so the key is not stored for other purposes or generated.
- **Easy to use**: Easy integration with your .NET projects.
- **Builder Pattern**: Use the `SecureEncryptionBuilder` to configure and instantiate ciphers.

## How to Install

Add the library to your project using the .NET CLI:

```sh
dotnet add package Security.Encryption.AES
```

## Usage / Implementation

### 1. Create an Encryptor

```csharp
using Security.Encryption.AES;
using Security.Encryption.AES.Enums;

byte[] key = Convert.FromBase64String("your-base64-key-here");

var cipher = new SecureEncryptionBuilder()
    .WithKey(key)
    .WithMode(AesEncryptionMode.GCM)
    .Build();
```

### 2. Encrypt Data

```csharp
string plainText = "Sensitive Data";
byte[] encrypted = cipher.Encrypt(plainText);
```

### 3. Decrypt Data

```csharp
string decrypted = cipher.Decrypt(encrypted);
```

### Example with CBC Mode

```csharp
var cbcCipher = new SecureEncryptionBuilder()
    .WithKey(key)
    .WithMode(AesEncryption.CBC)
    .Build();

byte[] encrypted = cbcCipher.Encrypt("Another secret");
string decrypted = cbcCipher.Decrypt(encrypted);
```

For more details see the interfaces `ISecureEncryption` and `IGcmSecureEncryption`.

## Contribution

Contributions are welcome! Please fork the repository and submit a pull request.

### Test

This project uses [**xUnit**](https://xunit.net/) for **unit testing**.

To run the tests:

```sh
cd src/Security.Encryption.AES.Tests
dotnet test
```

Test files are located in the `src/Security.Encryption.AES.Tests` directory and cover all the functionalities.
