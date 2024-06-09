namespace CarService.Api.Contracts.Records;

public record CreateRequestWithoutAuthUserRequest(
	string Email,
	string Phone,
	string FirstName,
	string? CarInfo,
	string Description,
	Guid? DayRecordsId);