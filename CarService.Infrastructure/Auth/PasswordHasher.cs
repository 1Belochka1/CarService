using CarService.App.Interfaces.Auth;

namespace CarService.Infrastructure.Auth;


public class PasswordHasher : IPasswordHasher
{
    public string Generate(string password) => BCrypt.Net.BCrypt.HashPassword(password);
    
    public bool Validate(string password, string hash) => BCrypt.Net.BCrypt.Verify(password, hash);
}