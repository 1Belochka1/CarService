namespace CarService.Api.Contracts.Users;

public record GetUsersRequest(
	string? SearchValue,
	int Page = 1,
	int PageSize = 10,
	string? SortProperty = null,
	bool SortDescending = false
);