using JsonWebToken.Core.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace JsonWebToken.Infrastructure.Password;

public class SHA256SaltedPasswordHasher : IPasswordHasher
{
    private const int SaltLength = 16;

    private const char Delimiter = ':';

    public string HashPassword(string plaintextPassword)
    {
        if (String.IsNullOrEmpty(plaintextPassword)) {
            throw new ArgumentException("Password cannot be null or empty", nameof(plaintextPassword));
        }

        var saltBytes = GenerateSalt();
        var plaintextPasswordBytes = Encoding.UTF8.GetBytes(plaintextPassword);
        var hashBytes = HashPassword(plaintextPasswordBytes, saltBytes);
        string saltString = Convert.ToBase64String(saltBytes);
        string hashString = Convert.ToBase64String(hashBytes);
        return String.Format("{0}{1}{2}", saltString, Delimiter, hashString);
    }

    public bool VerifyPassword(string hashedPasswordWithSalt, string plaintextPassword)
    {
        if (String.IsNullOrEmpty(hashedPasswordWithSalt)) {
            throw new ArgumentException("Hashed password cannot be null or empty", nameof(hashedPasswordWithSalt));
        }
        if (String.IsNullOrEmpty(plaintextPassword)) {
            throw new ArgumentException("Password cannot be null or empty", nameof(plaintextPassword));
        }

        var parts = hashedPasswordWithSalt.Split(Delimiter);
        var saltBytes = Convert.FromBase64String(parts[0]);
        var hashedPassword = parts[1];
        var passwordBytes = Encoding.UTF8.GetBytes(plaintextPassword);
        var hashBytes = HashPassword(passwordBytes, saltBytes);
        return hashedPassword.Equals(Convert.ToBase64String(hashBytes));
    }

    private static byte[] GenerateSalt()
    {
        using var rng = RandomNumberGenerator.Create();
        byte[] saltBytes = new byte[SaltLength];
        rng.GetBytes(saltBytes);
        return saltBytes;
    }

    private static byte[] HashPassword(byte[] plaintextPasswordBytes, byte[] saltBytes)
    {
        byte[] combinedBytes = [.. saltBytes, .. plaintextPasswordBytes];
        return SHA256.HashData(combinedBytes);
    }
}
