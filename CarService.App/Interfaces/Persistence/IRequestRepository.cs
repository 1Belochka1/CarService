using CarService.App.Common.Requests;
using CarService.Core.Requests;
using CarService.Core.Users;

namespace CarService.App.Interfaces.Persistence;

public interface IRequestRepository
{
	Task<Guid> CreateAsync(Request request);

	Task UpdateAsync(
		Guid id,
		string? phone = null,
		string? description = null,
		RequestPriority? priority = null,
		RequestStatus? status = null
	);

	Task DeleteAsync(Guid id);

	Task<List<Request>> GetAllAsync(string
		roleId, Guid? userId);

	Task<RequestsDto?> GetByIdAsync(Guid id);

	Task<IEnumerable<Request>> GetByClientIdAsync(
		Guid clientId);

	Task<List<Request>>
		GetCompletedByMasterIdAsync(Guid masterId);

	Task<List<Request>>
		GetActiveByMasterIdAsync(Guid masterId);

	Task AddMasters(Guid recordId,
		ICollection<UserAuth> masters);

	Task DeleteMasters(
		Guid recordId,
		Guid masterId);

	Task Complete(Guid recordId);
}