namespace CarService.Api.Contracts.DayRecords;

public record CreateDayRecordsRequest(
	Guid CalendarId,
	TimeOnly StartTime,
	TimeOnly EndTime,
	short Offset,
	short Duration
);