using CarService.Core.Users;

namespace CarService.App.Interfaces.Auth;

public interface IJwtProvider
{
	string GenerateTokenForAuth(UserAuth user);
}