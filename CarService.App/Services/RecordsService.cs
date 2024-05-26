using CarService.App.Common.ListWithPage;
using CarService.App.Interfaces.Persistence;
using CarService.Core.Records;
using CarService.Core.Users;
using CSharpFunctionalExtensions;

namespace CarService.App.Services;

public class RecordsService
{
	private readonly IRecordsRepository _recordsRepository;

	public RecordsService(
		IRecordsRepository recordsRepository)
	{
		_recordsRepository = recordsRepository;
	}

	public async Task<Guid> CreateRecordAsync(
		Guid clientId,
		string phone,
		string carInfo,
		string description)
	{
		var id = Guid.NewGuid();

		var record = Record.Create(id, clientId, phone, carInfo,
			description, DateTime.UtcNow, null);

		return await _recordsRepository.CreateAsync(
			record.Value);
	}

	public async Task<Result<Record>> GetRecordByIdAsync(
		Guid id)
	{
		var record = await _recordsRepository.GetByIdAsync(id);

		if (record == null)
			return Result.Failure<Record>("Запись не найдена");

		return Result.Success(record);
	}

	public async Task<ListWithPage<Record>>
		GetAllRecordsAsync(ParamsWhitFilter parameters)
	{
		return await _recordsRepository.GetAllAsync(parameters);
	}

	public async Task<ListWithPage<Record>>
		GetActiveRecordsByMasterIdAsync(
			Guid masterId,
			Params parameters)
	{
		return await
			_recordsRepository.GetActiveByMasterIdAsync(masterId,
				parameters);
	}

	public async Task<ListWithPage<Record>>
		GetCompletedRecordsByMasterIdAsync(
			Guid masterId,
			Params parameters)
	{
		return await _recordsRepository
			.GetCompletedByMasterIdAsync(masterId, parameters);
	}

	public async Task UpdateRecordAsync(
		Guid id,
		string? phone = null,
		string? description = null,
		RecordPriority? priority = null,
		RecordStatus? status = null)
	{
		await _recordsRepository.UpdateAsync(id, phone, description,
			priority, status);
	}

	public async Task AddMastersAsync(
		Guid recordId,
		UserAuth master)
	{
		await _recordsRepository.AddMasters(recordId,
			new List<UserAuth> { master });
	}
}