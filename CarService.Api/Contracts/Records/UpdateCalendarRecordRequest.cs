namespace CarService.Api.Contracts.Records;

public record UpdateCalendarRecordRequest(
	Guid Id,
	string? Name,
	string? Description);