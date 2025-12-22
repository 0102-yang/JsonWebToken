using JsonWebToken.Infrastructure.Password;

namespace JsonWebToken.Infrastructure.UnitTest;

public class SHA256SaltedPasswordHasherTest
{
    private const string PlaintextPassword = "Hello World";

    private SHA256SaltedPasswordHasher Hasher { get; } = new();

    [Fact]
    public void HashPassword_ThrowsArgumentException_WhenPasswordIsEmpty()
    {
        Assert.Throws<ArgumentException>(() => this.Hasher.HashPassword(""));
    }

    [Fact]
    public void VerifyPassword_ThrowsArgumentException_WhenHashedPasswordIsEmpty()
    {
        Assert.Throws<ArgumentException>(() => this.Hasher.VerifyPassword("", PlaintextPassword));
    }

    [Fact]
    public void HashPassword_HandlesVeryLongPassword()
    {
        var longPassword = new string('A', 10000);
        var hashedPassword = this.Hasher.HashPassword(longPassword);
        Assert.True(this.Hasher.VerifyPassword(hashedPassword, longPassword));
    }

    [Fact]
    public void HashPassword_ReturnsTrue_WhenPasswordIsCorrect()
    {
        var hashedSaltedPassword = this.Hasher.HashPassword(PlaintextPassword);
        Assert.True(this.Hasher.VerifyPassword(hashedSaltedPassword, PlaintextPassword));
    }

    [Fact]
    public void VerifyPassword_ReturnsFalse_WhenPasswordIsIncorrect()
    {
        var hashedSaltedPassword = this.Hasher.HashPassword(PlaintextPassword);
        Assert.False(this.Hasher.VerifyPassword(hashedSaltedPassword, "WrongPassword"));
    }

    [Fact]
    public void HashPassword_GeneratesDifferentHash_EachTime()
    {
        var results = Enumerable.Range(0, 10)
                                .Select(_ => this.Hasher.HashPassword(PlaintextPassword))
                                .ToArray();
        var uniqueResults = results.Distinct().Count();
        Assert.Equal(results.Length, uniqueResults);
    }
}
