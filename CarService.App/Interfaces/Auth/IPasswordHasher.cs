namespace CarService.App.Interfaces.Auth;

public interface IPasswordHasher
{
    string Generate(string password);
    bool Validate(string password, string hash);
}