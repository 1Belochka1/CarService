using Microsoft.VisualBasic.CompilerServices;

namespace CarService.Api.Contracts;

public record CreateServiceRequest(
	string Name,
	string Description,
	bool IsShowLanding,
	IFormFile? File = null);