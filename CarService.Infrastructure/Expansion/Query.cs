using System.Reflection;
using CarService.App.Common.ListWithPage;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CarService.Infrastructure.Expansion;

public static partial class Query
{
	public static List<TModel> Sort<TModel>(
		this List<TModel> query, string sortProperty,
		bool sortDescending) where TModel : class, new()
	{
		PropertyInfo? prop =
			typeof(TModel).GetProperties().FirstOrDefault(x =>
				x.Name.Equals(sortProperty,
					StringComparison.CurrentCultureIgnoreCase));

		if (prop != null)
			return sortDescending
				? query
					.OrderByDescending(x => prop.GetValue(x, null))
					.ToList()
				: query.OrderBy(x => prop.GetValue(x, null))
					.ToList();

		FieldInfo? field =
			typeof(TModel).GetField(sortProperty,
				BindingFlags.IgnoreCase);

		if (field != null)
			return sortDescending
				? query.OrderByDescending(x => field.GetValue(x))
					.ToList()
				: query.OrderBy(x => field.GetValue(x)).ToList();

		return query;
	}

	public static ListWithPage<TModel> Page<TModel>(
		this List<TModel> query,
		int page, int pageSize)
	{
		var totalItems = query.Count;

		var currentPage = page;

		var totalPages =
			(int)Math.Ceiling((double)totalItems / pageSize);

		var result = query.Skip((page - 1) * pageSize)
			.Take(pageSize).ToList();

		if (currentPage <= totalPages)
			return new ListWithPage<TModel>(totalItems,
				totalPages, currentPage, result);

		currentPage = 1;

		return new ListWithPage<TModel>(totalItems, totalPages,
			currentPage, result);
	}

	public static bool Search<TModel>(this TModel model,
		string search)
		=> typeof(TModel).GetProperties().ToList()
			.Any(property =>
				property.GetValue(model)?.ToString()?.Contains(
					search,
					StringComparison.InvariantCultureIgnoreCase) ??
				false);
}