namespace CarService.App.Common.Users;

public record WorkersDto(
	Guid Id,
	string Email,
	string FullName,
	string? Address,
	string Phone,
	int RoleId
);