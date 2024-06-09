namespace CarService.Api.Contracts.Users;

public record GetWorkerResponse(
	string FirstName,
	string LastName,
	string? Patronymic,
	string? Address,
	string Phone);