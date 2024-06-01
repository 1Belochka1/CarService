namespace CarService.Api.Contracts.Records;

public record CreateCalendarRecordRequest(
	Guid ServiceId,
	string Name,
	string? Description);