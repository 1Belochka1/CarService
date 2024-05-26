using CarService.Core.Services;

namespace CarService.Core.Records;

public class CalendarRecords
{
	private CalendarRecords(Guid id, string name, string? description, Service? service)
	{
		Id = id;
		Name = name;
		Description = description;
		Service = service;
	}

	public Guid Id { get; private set; }

	public string Name { get; private set; }

	public string? Description { get; private set; }

	public Service? Service { get; private set; }

	public List<DayRecords> Days { get; private set; } = [];

	public static CalendarRecords Create(Guid id, string name, string? description, Service? service)
	{
		return new CalendarRecords(id, name, description, service);
	}
}