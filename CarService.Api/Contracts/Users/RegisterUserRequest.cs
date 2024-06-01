namespace CarService.Api.Contracts.Users;

public record RegisterUserRequest(
    string LastName,
    string FirstName,
    string? Patronymic,
    string? Address,
    string Phone,
    string Password
);