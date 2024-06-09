namespace CarService.Api.Contracts.Records;

public record CreateRequestRequest(
	Guid ServiceId,
	string Name,
	string? Description);