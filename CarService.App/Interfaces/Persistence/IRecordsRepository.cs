using CarService.App.Common.ListWithPage;
using CarService.Core.Records;
using CarService.Core.Users;

namespace CarService.App.Interfaces.Persistence;

public interface IRecordsRepository
{
	Task<Guid> CreateAsync(Record record);

	Task UpdateAsync(Guid id, string? description,
		RecordPriority? priority,
		RecordStatus? status);

	Task<ListWithPage<Record>> GetAllAsync(
		ParamsWhitFilter parameters);

	Task AddMasters(Guid recordId,
		ICollection<UserAuth> masters);

	Task<ListWithPage<Record>> GetActiveByMasterIdAsync(
		Guid masterId, Params parameters);

	Task<ListWithPage<Record>> GetCompletedByMasterIdAsync(
		Guid masterId, Params parameters);

	Task DeleteAsync(Guid id);
	Task<Record?> GetByIdAsync(Guid id);

	Task<IEnumerable<Record>> GetByClientIdAsync(
		Guid clientId);
}