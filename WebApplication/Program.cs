using JsonWebToken.Core.Interfaces;
using JsonWebToken.Core.Interfaces.Token;
using JsonWebToken.Infrastructure.Password;
using JsonWebToken.Infrastructure.Repository;
using JsonWebToken.Infrastructure.Token;
using JsonWebToken.Web.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateSlimBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddSingleton<IUserRepository, InMemoryUserRepository>();
builder.Services.AddSingleton<IPasswordHasher, SHA256SaltedPasswordHasher>();
builder.Services.AddSingleton<ITokenCreater, JsonWebTokenCreater>();
builder.Services.AddSingleton<IJsonWebTokenConfiguration, FakeJsonWebTokenConfiguration>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration.GetValue<string>("JsonWebToken:Issuer")!,
            ValidateAudience = true,
            ValidAudience = builder.Configuration.GetValue<string>("JsonWebToken:Audience")!,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("JsonWebToken:Key")!))
        };
    });

var app = builder.Build();

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment()) {
    app.MapOpenApi();
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
