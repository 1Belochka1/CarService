using System.Reflection;

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


	public static bool Search<TModel>(this TModel model,
		string search)
		=> typeof(TModel).GetProperties().ToList()
			.Any(property =>
				property.GetValue(model)?.ToString()?.Contains(
					search,
					StringComparison.InvariantCultureIgnoreCase) ??
				false);
}