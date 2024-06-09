using CarService.Core.Requests;

namespace CarService.Infrastructure.Expansion;

public static class FilterQuery
{
	public static List<Request> FilterRecords(
		this List<Request> recordList,
		string filterName,
		string filterValue)
	{
		return filterName switch
		{
			nameof(Request.Priority) => recordList
				.Where(x =>
					x.Priority.ToString() == filterValue)
				.ToList(),
			_ => recordList
		};
	}

	public static List<T> FilterWithName<T>(
		this List<T> list,
		string filterName,
		string filterValue)
	{
		var result = list
			.Where(x =>
				string.Equals(
					x.GetType()
						.GetProperty(filterName)
						.GetValue(x)
						.ToString(), filterValue,
					StringComparison.CurrentCultureIgnoreCase)
			);

		return result.ToList();
	}
}