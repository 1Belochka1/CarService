namespace CarService.App.Common.ListWithPage;

public class ListWithPage<T>
{
	public ListWithPage(int totalItems, int? totalPages, int? currentPage, List<T> items)
	{
		TotalItems = totalItems;
		TotalPages = totalPages;
		CurrentPage = currentPage;
		Items = items;
	}

	public int TotalItems { get; set; }

	public int? TotalPages { get; set; }

	public int? CurrentPage { get; set; }

	public List<T> Items { get; set; }
}

public class Params
{
	public Params(bool sortDescending, string? searchValue = null, int page = 1, int pageSize = 10,
		string? sortProperty = null)
	{
		SortDescending = sortDescending;
		SearchValue = searchValue;
		Page = page;
		PageSize = pageSize;
		SortProperty = sortProperty;
	}

	public bool SortDescending { get; set; }
	public string? SearchValue { get; set; } = null;
	public int Page { get; set; }
	public int PageSize { get; set; }
	public string? SortProperty { get; set; } = null;
}