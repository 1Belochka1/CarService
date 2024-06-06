using CarService.App.Common.Records;
using CarService.App.Common.Users;
using CarService.App.Interfaces.Persistence;
using CarService.Core.Records;
using CarService.Core.Users;
using Microsoft.EntityFrameworkCore;

namespace CarService.Infrastructure.Persistence.
	Repositories;

public class RecordsRepository : IRecordsRepository
{
	private readonly CarServiceDbContext _context;

	public RecordsRepository(CarServiceDbContext context)
	{
		_context = context;
	}

	public async Task<Guid> CreateAsync(Record record)
	{
		await _context.Records.AddAsync(record);

		await _context.SaveChangesAsync();

		return record.Id;
	}

	public async Task UpdateAsync(
		Guid id,
		string? phone = null,
		string? description = null,
		RecordPriority? priority = null,
		RecordStatus? status = null
	)
	{
		var record = await _context.Records
			.FirstAsync(r => r.Id == id);

		if (description != null)
			record.SetDescription(description);

		if (priority != null)
			record.SetPriority(priority.Value);

		if (status != null) record.SetStatus(status.Value);

		await _context.SaveChangesAsync();
	}


	public async Task DeleteAsync(Guid id)
	{
		await _context.Records
			.Where(r => r.Id == id)
			.ExecuteDeleteAsync();
	}

	public async Task<List<Record>> GetAllAsync(string
		roleId, Guid? userId)
	{
		var query = await _context.Records
			.Include(x => x.Masters)
			.Include(x => x.Services)
			.AsNoTracking()
			.ToListAsync();

		if (roleId == "3")
			query = query
				.Where(x => x.ClientId == userId)
				.ToList();

		return query;
	}

	public async Task<RecordsDto?> GetByIdAsync(Guid id)
	{
		var records = SelectRecords(await _context.Records
			.Include(x => x.Masters)
			.ThenInclude(x => x.UserInfo)
			.Include(x => x.Client).ToListAsync());

		return records.FirstOrDefault(x =>
			x.Id == id);
	}

	public async Task<IEnumerable<Record>> GetByClientIdAsync(
		Guid clientId)
	{
		return await _context.Records
			.Where(x => x.ClientId == clientId).ToListAsync();
	}

	public async Task<List<Record>>
		GetCompletedByMasterIdAsync(Guid masterId)
	{
		var query = await _context.Records
			.Include(x => x.Masters)
			.Where(x => x.Masters.Any(m => m.Id == masterId)
			            && x.Status == RecordStatus.Done)
			.ToListAsync();


		return query;
	}

	public async Task<List<Record>>
		GetActiveByMasterIdAsync(Guid masterId)
	{
		var query = await _context.Records
			.Include(x => x.Masters)
			.Where(x => x.Masters.Any(m => m.Id == masterId)
			            && x.Status != RecordStatus.Done)
			.ToListAsync();

		return query;
	}

	public async Task AddMasters(Guid recordId,
		ICollection<UserAuth> masters)
	{
		var record = await _context.Records
			.Include(r => r.Masters)
			.FirstAsync(r => r.Id == recordId);
		record.AddMasters(masters);

		await _context.SaveChangesAsync();
	}

	public async Task Complete(Guid recordId)
	{
		var record = await _context.Records
			.FirstAsync(r => r.Id == recordId);

		record.SetStatus(RecordStatus.Done);

		await _context.SaveChangesAsync();
	}

	private static List<RecordsDto> SelectRecords(
		List<Record> query)
	{
		return query.Select(x => new RecordsDto(
			x.Id,
			x.ClientId,
			x.Client,
			x.CarInfo,
			x.Description,
			x.CreateTime,
			x.VisitTime,
			x.IsTransferred,
			x.CompleteTime,
			x.Priority,
			x.Status,
			x.Masters.Select(x => new WorkersDto(
				x.Id,
				x.UserInfo.LastName,
				x.UserInfo.FirstName,
				x.UserInfo.Patronymic,
				x.UserInfo.Address,
				x.UserInfo.Phone,
				x.RoleId
			)).ToList()
		)).ToList();
	}
}