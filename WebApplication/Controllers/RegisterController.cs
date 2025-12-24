using JsonWebToken.Core.Dto;
using JsonWebToken.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JsonWebToken.Web.Controllers;

[Route("api")]
[ApiController]
public class RegisterController(IUserRepository userRepository, ILogger<RegisterController> logger) : Controller
{
    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        logger.LogInformation("Register attempt from user: {Username}", request.Username);
        UserDto userDto = new(request.Username, request.Password, request.Role);
        var existingUser = userRepository.IsUserExistsByName(userDto.Username);
        if (existingUser) {
            return this.BadRequest("User already exists.");
        }

        userRepository.CreateUser(userDto);
        logger.LogDebug("User {Username} registered successfully.", request.Username);
        return this.Created();
    }
}

public record RegisterRequest(string Username, string Password, string Role);
