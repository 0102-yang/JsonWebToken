namespace JsonWebToken.Core.Interfaces.Token;

public interface IJsonWebTokenConfiguration
{
    byte[] Key { get; }

    string Issuer { get; }

    string Audience { get; }
}
