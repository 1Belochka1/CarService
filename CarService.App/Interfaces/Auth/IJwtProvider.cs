using CarService.Core.Users;

namespace CarService.App.Interfaces.Auth;

public interface IJwtProvider
{
    string GenerateToken(UserAuth user);
}