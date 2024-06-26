namespace CarService.Api.Contracts.Users;

public record GetUsersResponse<T>(
	int TotalCount,
	int? TotalPages,
	int? CurrentPage,
	List<T> Users
);