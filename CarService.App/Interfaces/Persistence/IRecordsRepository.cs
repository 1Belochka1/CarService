using CarService.App.Common.Records;
using CarService.Core.Records;
using CarService.Core.Users;

namespace CarService.App.Interfaces.Persistence;

public interface IRecordsRepository
{
	Task<Guid> CreateAsync(Record record);

	Task UpdateAsync(Guid id,
		string? phone,
		string? description,
		RecordPriority? priority,
		RecordStatus? status);

	Task<List<Record>> GetAllAsync(string
		roleId, Guid? userId);

	Task AddMasters(Guid recordId,
		ICollection<UserAuth> masters);

	Task<List<Record>> GetActiveByMasterIdAsync(
		Guid masterId);

	Task<List<Record>> GetCompletedByMasterIdAsync(
		Guid masterId);

	Task DeleteAsync(Guid id);
	Task<RecordsDto?> GetByIdAsync(Guid id);

	Task<IEnumerable<Record>> GetByClientIdAsync(
		Guid clientId);
}