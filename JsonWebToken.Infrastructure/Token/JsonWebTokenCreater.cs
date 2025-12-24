using JsonWebToken.Core.Interfaces.Token;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace JsonWebToken.Infrastructure.Token;

public class JsonWebTokenCreater(IJsonWebTokenConfiguration configuration) : ITokenCreater
{
    public string CreateToken(string username, string role)
    {
        var claims = new List<Claim> {
            new(ClaimTypes.Name, username),
            new(ClaimTypes.Role, role)
        };

        var securityKey = new SymmetricSecurityKey(configuration.Key);
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var tokenDescriptor = new JwtSecurityToken(
            issuer: configuration.Issuer,
            audience: configuration.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials
        );

        var handler = new JwtSecurityTokenHandler();
        return handler.WriteToken(tokenDescriptor);
    }
}
