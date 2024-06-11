namespace CarService.Api.Contracts.Users;

public record UpdateUserRequest(
	Guid Id,
	string? Email,
	string? FirstName,
	string? LastName,
	string? Patronymic,
	string? Address,
	string? Phone,
	string? Password,
	int? RoleId
);