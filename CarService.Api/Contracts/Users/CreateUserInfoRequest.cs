namespace CarService.Api.Contracts.Users;

public record CreateUserInfoRequest(
	string Email,
	string Phone,
	string LastName,
	string FirstName,
	string? Patronymic,
	string? Address
);