namespace CarService.Api.Contracts.Records;

public record CreateRecordRequest(
	Guid ClientId,
	string Phone,
	string CarInfo,
	string Description);