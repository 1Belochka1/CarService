using CarService.App.Interfaces.Persistence;
using CarService.Core.Records;

namespace CarService.App.Services;

public class RecordsService
{
	private readonly IRecordsRepository _recordsRepository;

	public RecordsService(IRecordsRepository recordsRepository)
	{
		_recordsRepository = recordsRepository;
	}

	public async Task<ICollection<Record>> GetAllRecordsAsync()
	{
		return await _recordsRepository.GetRecordsAsync();
	}

	public async Task<ICollection<Record>> GetActiveRecordsByMasterIdAsync(Guid masterId, int page = 1, int pageSize = 10)
	{
		return await _recordsRepository.GetActiveByMasterIdAsync(masterId, page, pageSize);
	}

	public async Task<ICollection<Record>> GetCompletedRecordsByMasterIdAsync(Guid masterId, int page = 1,
		int pageSize = 10)
	{
		return await _recordsRepository.GetCompletedByMasterIdAsync(masterId, page, pageSize);
	}

	public async Task<Guid> CreateRecordAsync(Guid clientId, string description)
	{
		var id = Guid.NewGuid();

		var record = Record.Create(id, clientId, description, DateTime.UtcNow);

		return await _recordsRepository.CreateAsync(record.Value);
	}

	public async Task UpdateRecordAsync(
		Guid id,
		string? description = null,
		RecordPriority? priority = null,
		RecordStatus? status = null)
	{
		await _recordsRepository.UpdateAsync(id, description, priority, status);
	}
}