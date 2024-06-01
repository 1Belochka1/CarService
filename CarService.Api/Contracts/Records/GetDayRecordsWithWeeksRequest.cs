namespace CarService.Api.Contracts.Records;

public record GetDayRecordsWithWeeksRequest(
	Guid CalendarId,
	int CurrentWeek,
	int WeeksVisible);