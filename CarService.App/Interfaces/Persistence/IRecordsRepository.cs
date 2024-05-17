using CarService.Core.Records;
using CarService.Core.Users;

namespace CarService.App.Interfaces.Persistence;

public interface IRecordsRepository
{
	Task<Guid> CreateAsync(Record record);

	Task UpdateAsync(Guid id, string? description, RecordPriority? priority,
		RecordStatus? status);

	Task<ICollection<Record>> GetRecordsAsync();
	Task AddMasters(Guid recordId, ICollection<UserAuth> masters);
	Task<ICollection<Record>> GetCompletedByMasterIdAsync(Guid masterId, int page, int pageSize);
	Task<ICollection<Record>> GetActiveByMasterIdAsync(Guid masterId, int page, int pageSize);

	Task DeleteAsync(Guid id);
	Task<Record?> GetByIdAsync(Guid id);
	Task<IEnumerable<Record>> GetByClientIdAsync(Guid clientId);
}