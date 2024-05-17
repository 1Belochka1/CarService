namespace CarService.Api.Contracts.Records;

public record CreateRecordRequest(
	Guid ClientId,
	string Description);