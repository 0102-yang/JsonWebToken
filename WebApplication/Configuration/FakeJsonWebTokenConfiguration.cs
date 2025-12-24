using JsonWebToken.Core.Interfaces.Token;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JsonWebToken.Web.Configuration;

public class FakeJsonWebTokenConfiguration(IConfiguration configuration) : IJsonWebTokenConfiguration
{
    [MinLength(16)]
    public byte[] Key => Encoding.UTF8.GetBytes(configuration.GetValue<string>("JsonWebToken:Key")!);

    public string Issuer => configuration.GetValue<string>("JsonWebToken:Issuer")!;

    public string Audience => configuration.GetValue<string>("JsonWebToken:Audience")!;
}
