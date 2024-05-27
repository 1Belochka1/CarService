namespace CarService.App.Common.ListWithPage;

public class ListWithPage<T>
{
	public ListWithPage(
		int totalItems,
		int? totalPages,
		int? currentPage,
		List<T> items)
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
	public Params(
		Guid? userId,
		string? roleId,
		bool sortDescending,
		string? searchValue = null,
		int page = 1,
		int pageSize = 10,
		string? sortProperty = null)
	{
		SortDescending = sortDescending;
		RoleId = roleId;
		UserId = userId;
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

	public string? RoleId { get; set; }

	public Guid? UserId { get; set; }
}

public class ParamsWhitFilter : Params
{
	public ParamsWhitFilter(
		Guid? userId,
		string? roleId,
		string? searchValue = null,
		List<Filter>? filters = null,
		int page = 1,
		int pageSize = 10,
		string? sortProperty = null,
		bool sortDescending = false)
		: base(userId, roleId, sortDescending,
			searchValue, page, pageSize,
			sortProperty)
	{
		Filters = filters;
	}

	public List<Filter>? Filters { get; set; }
}

public class Filter(string name, string value)
{
	public string Name { get; set; } = name;
	public string Value { get; set; } = value;
}