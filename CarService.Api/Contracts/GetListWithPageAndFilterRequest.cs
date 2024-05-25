namespace CarService.Api.Contracts;

public record GetListWithPageAndFilterRequest(
	string? SearchValue,
	int Page = 1,
	int PageSize = 10,
	string? SortProperty = null,
	bool SortDescending = false,
	string? FilterProperty = null,
	string? FilterValue = null
);