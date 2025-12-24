namespace JsonWebToken.Core.Interfaces.Token;

public interface ITokenCreater
{
    string CreateToken(string Username, string Role);
}
