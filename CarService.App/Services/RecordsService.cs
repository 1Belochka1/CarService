using CarService.App.Common.ListWithPage;
using CarService.App.Interfaces.Persistence;
using CarService.Core.Records;
using CarService.Core.Users;

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

	public async Task<ListWithPage<Record>> GetActiveRecordsByMasterIdAsync(Guid masterId, Params parameters)
	{
		return await _recordsRepository.GetActiveByMasterIdAsync(masterId, parameters);
	}

	public async Task<ListWithPage<Record>> GetCompletedRecordsByMasterIdAsync(Guid masterId, Params parameters)
	{
		return await _recordsRepository.GetCompletedByMasterIdAsync(masterId, parameters);
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

	public async Task AddMastersAsync(Guid recordId, UserAuth master)
	{
		await _recordsRepository.AddMasters(recordId, new List<UserAuth> { master });
	}
}