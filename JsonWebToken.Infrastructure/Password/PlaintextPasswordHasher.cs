using JsonWebToken.Core.Interfaces;

namespace JsonWebToken.Infrastructure.Password;

public class PlaintextPasswordHasher : IPasswordHasher
{
    public string HashPassword(string plaintextPassword)
    {
        return plaintextPassword;
    }

    public bool VerifyPassword(string hashedPassword, string plaintextPassword)
    {
        return String.Equals(hashedPassword, plaintextPassword);
    }
}
