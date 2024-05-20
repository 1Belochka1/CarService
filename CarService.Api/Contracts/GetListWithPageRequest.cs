namespace CarService.Api.Contracts;

public record GetListWithPageRequest(
	string? SearchValue,
	int Page = 1,
	int PageSize = 10,
	string? SortProperty = null,
	bool SortDescending = false
);