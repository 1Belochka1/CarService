using CarService.Core.Requests;

namespace CarService.Api.Contracts.Records;

public record UpdateRequestRequest(
	Guid Id,
	string? Phone = null,
	string? Description = null,
	RequestPriority? Priority = null,
	RequestStatus? Status = null);