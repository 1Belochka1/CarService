namespace CarService.Api.Contracts.Records;

public record CreateRecordRequest(
	string Email,
	string? CarInfo,
	string Description,
	Guid? DayRecordsId);