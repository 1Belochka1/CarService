namespace CarService.Api.Contracts.Users;

public record CreateUserInfoRequest(
	string Phone,
	string LastName,
	string FirstName,
	string? Patronymic,
	string? Address
);