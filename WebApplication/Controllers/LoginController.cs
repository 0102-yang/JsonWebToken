using JsonWebToken.Core.Interfaces;
using JsonWebToken.Core.Interfaces.Token;
using Microsoft.AspNetCore.Mvc;

namespace JsonWebToken.Web.Controllers;

[Route("api")]
[ApiController]
public class LoginController(IUserRepository userRepository, ITokenCreater tokenCreater, IPasswordHasher passwordHasher, ILogger<LoginController> logger) : ControllerBase
{
    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        logger.LogInformation("Login attempt from user: {Username}", request.Username);
        var existingUser = userRepository.IsUserExistsByName(request.Username);
        if (!existingUser) {
            return this.Unauthorized("User not exists.");
        }
        var isPasswordValid = this.VerifyPassword(request.Username, request.Password);
        if (!isPasswordValid) {
            return this.Unauthorized("Wrong password.");
        }

        var role = userRepository.GetUserRoleByName(request.Username);
        var token = tokenCreater.CreateToken(request.Username, role);
        return this.Ok(new LoginResponse(request.Username, token));
    }

    private bool VerifyPassword(string Username, string enteredPassword)
    {
        var hashedPassword = userRepository.GetUserPasswordHashByName(Username);
        return passwordHasher.VerifyPassword(hashedPassword, enteredPassword);
    }
}

public record LoginRequest(string Username, string Password);

public record LoginResponse(string Username, string Token);
