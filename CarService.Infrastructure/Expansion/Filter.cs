using System.Linq.Expressions;
using CarService.Core.Records;
using CarService.Core.Services;
using Microsoft.EntityFrameworkCore;

namespace CarService.Infrastructure.Expansion;

public static class Filter
{
	public static List<Service> FilterServices(
		this List<Service> serviceList,
		string filterName,
		string filterValue)
	{
		return filterName switch
		{
			nameof(Service.ServiceTypes) + ".Name" => serviceList
				.Where(x =>
					x.ServiceTypes.Any(y => y.Name == filterValue))
				.ToList(),
			_ => serviceList
		};
	}

	public static List<Record> FilterRecords(
		this List<Record> recordList,
		string filterName,
		string filterValue)
	{
		return filterName switch
		{
			nameof(Record.Priority) => recordList
				.Where(x =>
					x.Priority.ToString() == filterValue)
				.ToList(),
			_ => recordList
		};
	}
}