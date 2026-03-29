using System.Security.Cryptography;
using Application.Security;

namespace Infrastructure.Security;

public class Sha256PasswordHasher: IPasswordHasher
{
    private const int SaltSize = 16;      // 128-bit
    private const int KeySize  = 32;      // 256-bit
    private const int Iterations = 100_000;  // PBKDF2 iteration count
    private static readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA256;

    public string HashPassword(string password)
    {
        // Generate salt
        var salt = RandomNumberGenerator.GetBytes(SaltSize);

        // Derive key
        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, Algorithm);
        var key = pbkdf2.GetBytes(KeySize);

        // Store: [salt][key] in Base64
        var saltAndKey = new byte[SaltSize + KeySize];
        Buffer.BlockCopy(salt, 0, saltAndKey, 0, SaltSize);
        Buffer.BlockCopy(key, 0, saltAndKey, SaltSize, KeySize);

        return Convert.ToBase64String(saltAndKey);
    }

    public bool VerifyPassword(string password, string passwordHash)
    {
        var saltAndKey = Convert.FromBase64String(passwordHash);

        // Extract salt and key
        var salt = new byte[SaltSize];
        var storedKey = new byte[KeySize];

        Buffer.BlockCopy(saltAndKey, 0, salt, 0, SaltSize);
        Buffer.BlockCopy(saltAndKey, SaltSize, storedKey, 0, KeySize);

        // Derive a key from input password using the stored salt
        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, Algorithm);
        var computedKey = pbkdf2.GetBytes(KeySize);

        // Constant-time comparison
        return CryptographicOperations.FixedTimeEquals(storedKey, computedKey);
    }
}