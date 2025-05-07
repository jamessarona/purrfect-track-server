using PurrfectTrack.Shared.Security;
using System.Security.Cryptography;
using System.Text;

namespace PurrfectTrack.Infrastructure.Security;

public class Sha256PasswordHasher : IPasswordHasher
{
    public string Hash(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }

    public bool Verify(string password, string hashedPassword)
    {
        var hashOfInput = Hash(password);
        return hashOfInput == hashedPassword;
    }
}