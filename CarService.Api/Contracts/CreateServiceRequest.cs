namespace CarService.Api.Contracts;

public record CreateServiceRequest(
	string Name,
	string Description,
	bool IsShowLanding);