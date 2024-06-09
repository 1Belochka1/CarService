namespace CarService.Api.Contracts.Records;

public record UpdateRecordRequest(
	Guid Id,
	string? Name,
	string? Description);