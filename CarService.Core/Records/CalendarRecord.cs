using CarService.Core.Services;
using CSharpFunctionalExtensions;

namespace CarService.Core.Records;

public class CalendarRecord
{
	private CalendarRecord(Guid id, string name,
		string? description, Guid? serviceId)
	{
		Id = id;
		Name = name;
		Description = description;
		ServiceId = serviceId;
	}

	public Guid Id { get; private set; }

	public string Name { get; private set; }

	public string? Description { get; private set; }

	public Guid? ServiceId { get; private set; }

	public Service? Service { get; private set; }

	public List<DayRecord>
		DaysRecords { get; private set; } = [];

	public static Result<CalendarRecord> Create(Guid id,
		string
			name, string? description, Guid? serviceId)
	{
		if (id == Guid.Empty)
			return Result.Failure<CalendarRecord>(
				"Id can't be empty");

		if (string.IsNullOrEmpty(name))
			return Result.Failure<CalendarRecord>(
				"Название не может быть пустым");

		var calendarRecords = new CalendarRecord(id, name,
			description,
			serviceId);

		return Result.Success(calendarRecords);
	}

	public void Update(string? requestName,
		string? requestDescription)
	{
		if (requestName != null)
			Name = requestName;

		if (requestDescription != null)
			Description = requestDescription;
	}
}