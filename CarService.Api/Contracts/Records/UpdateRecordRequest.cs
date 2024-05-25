using CarService.Core.Records;

namespace CarService.Api.Contracts.Records;

public record UpdateRecordRequest(
	Guid Id,
	string? Description = null,
	RecordPriority? Priority = null,
	RecordStatus? Status = null);