namespace CarService.Api.Contracts.Records;

public record FillDayRecordsRequest(
	String CalendarId,
	DateTime StartDate,
	DateTime EndDate,
	String StartTime,
	String EndTime,
	String? BreakStartTime,
	String? BreakEndTime,
	int Duration,
	int Offset
);