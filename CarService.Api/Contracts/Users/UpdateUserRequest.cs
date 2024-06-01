namespace CarService.Api.Contracts.Users;

public record UpdateUserRequest(
	Guid Id,
	string? FirstName,
	string? LastName,
	string? Patronymic,
	string? Address,
	string? Phone,
	int? RoleId
);