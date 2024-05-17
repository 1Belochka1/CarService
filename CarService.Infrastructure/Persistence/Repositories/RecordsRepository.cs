using CarService.App.Interfaces.Persistence;
using CarService.Core.Records;
using CarService.Core.Users;
using Microsoft.EntityFrameworkCore;

namespace CarService.Infrastructure.Persistence.Repositories;

public class RecordsRepository : BaseRepository<Record>, IRecordsRepository
{
	private readonly CarServiceDbContext _context;

	public RecordsRepository(CarServiceDbContext context) : base(context)
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
		string? description = null,
		RecordPriority? priority = null,
		RecordStatus? status = null
	)
	{
		var record = await _context.Records
			.FirstAsync(r => r.Id == id);

		if (description != null) record.SetDescription(description);

		if (priority != null) record.SetPriority(priority.Value);

		if (status != null) record.SetStatus(status.Value);

		await _context.SaveChangesAsync();
	}


	public async Task DeleteAsync(Guid id)
	{
		await _context.Records
			.Where(r => r.Id == id)
			.ExecuteDeleteAsync();
	}

	public async Task<ICollection<Record>> GetRecordsAsync()
	{
		return await _context.Records.Include(r => r.Masters).ToListAsync();
	}

	public async Task<Record?> GetByIdAsync(Guid id)
	{
		return await _context.Records.FirstOrDefaultAsync(x => x.Id == id);
	}

	public async Task<IEnumerable<Record>> GetByClientIdAsync(Guid clientId)
	{
		return await _context.Records.Where(x => x.ClientId == clientId).ToListAsync();
	}

	public async Task<ICollection<Record>> GetCompletedByMasterIdAsync(Guid masterId, int page, int pageSize)
	{
		var records = _context.Records
			.Where(x => x.Masters.Any(m => m.Id == masterId) && x.Status == RecordStatus.Done);
		var result = Page(records, page, pageSize);
		return await result.ToListAsync();
	}

	public async Task<ICollection<Record>> GetActiveByMasterIdAsync(Guid masterId, int page, int pageSize)
	{
		var records = _context.Records
			.Where(x => x.Masters.Any(m => m.Id == masterId) && x.Status == RecordStatus.Work);
		var result = Page(records, page, pageSize);
		return await result.ToListAsync();
	}

	public async Task AddMasters(Guid recordId, ICollection<UserAuth> masters)
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
}