namespace CarService.Api.Contracts.Records;

public record FillDayRecordsRequest(
	Guid CalendarId,
	DateTime StartDate,
	DateTime EndDate,
	TimeOnly StartTime,
	TimeOnly EndTime,
	TimeOnly? BreakStartTime,
	TimeOnly? BreakEndTime,
	int Duration,
	int Offset,
	List<DateTime> WeekendsDay
);