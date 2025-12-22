namespace JsonWebToken.Core.Interfaces;

public interface IPasswordHasher
{
    string HashPassword(string plaintextPassword);

    bool VerifyPassword(string hashedPassword, string plaintextPassword);
}
