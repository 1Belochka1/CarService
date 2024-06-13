namespace CarService.Api.Contracts.Records;

public record UpdateTimeRecords(
	Guid Id,
	bool IsBusy,
	string? Email,
	string? Phone,
	string? Name);