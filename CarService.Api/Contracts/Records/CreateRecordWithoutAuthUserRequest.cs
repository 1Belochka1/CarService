namespace CarService.Api.Contracts.Records;

public record CreateRecordWithoutAuthUserRequest(
	string Phone,
	string FirstName,
	string? CarInfo,
	string Description,
	Guid? DayRecordsId);