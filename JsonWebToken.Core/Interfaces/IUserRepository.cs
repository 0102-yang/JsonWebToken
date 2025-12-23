using JsonWebToken.Core.Dto;

namespace JsonWebToken.Core.Interfaces;

public interface IUserRepository
{
    bool IsUserExistsByName(string username);

    string GetUserPasswordHashByName(string username);

    string GetUserRoleByName(string username);

    bool CreateUser(UserDto user);
}
