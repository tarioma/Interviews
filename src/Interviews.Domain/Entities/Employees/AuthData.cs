using System.Security.Cryptography;
using Ardalis.GuardClauses;

namespace Interviews.Domain.Entities.Employees;

public class AuthData
{
    internal const int MaxPasswordLength = 100;

    public AuthData(string password)
    {
        Guard.Against.NullOrWhiteSpace(password);
        Guard.Against.StringTooLong(password, MaxPasswordLength);

        var salt = GenerateSalt();
        var passwordHash = GeneratePasswordHash(password, salt);

        PasswordHash = passwordHash;
        Salt = salt;
    }

    public string PasswordHash { get; }
    public byte[] Salt { get; }

    private static byte[] GenerateSalt()
    {
        const int saltLength = 16;
        var salt = new byte[saltLength];

        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(salt);

        return salt;
    }

    private static string GeneratePasswordHash(string password, byte[] salt)
    {
        using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10_000, HashAlgorithmName.SHA256);
        const int sha256HashLength = 64;
        var hash = pbkdf2.GetBytes(sha256HashLength);

        return Convert.ToBase64String(hash);
    }
}