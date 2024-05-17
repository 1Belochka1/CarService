namespace CarService.App.Common.Users;

public record ClientsDto(
	Guid Id,
	string Email,
	string FullName,
	string? Address,
	string Phone,
	DateTime? LastRecord
);