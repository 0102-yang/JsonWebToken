using JsonWebToken.Core.Dto;
using JsonWebToken.Core.Interfaces;

namespace JsonWebToken.Infrastructure.Repository;

public class InMemoryUserRepository(IPasswordHasher passwordHasher) : IUserRepository
{
    record User(string Username, string PasswordHash, string Role);

    private Dictionary<string, User> users = [];

    public bool IsUserExistsByName(string username)
    {
        return users.ContainsKey(username);
    }

    public string GetUserPasswordHashByName(string username)
    {
        return users.GetValueOrDefault(username)?.PasswordHash ?? String.Empty;
    }

    public string GetUserRoleByName(string username)
    {
        return users.GetValueOrDefault(username)?.Role ?? String.Empty;
    }

    public bool CreateUser(UserDto user)
    {
        var passwordHash = passwordHasher.HashPassword(user.Password);
        var newUser = new User(user.Username, passwordHash, user.Role);
        return users.TryAdd(user.Username, newUser);
    }
}
