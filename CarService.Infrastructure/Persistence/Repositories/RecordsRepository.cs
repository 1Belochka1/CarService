using CarService.App.Common.ListWithPage;
using CarService.App.Common.Users;
using CarService.App.Interfaces.Persistence;
using CarService.Core.Records;
using CarService.Core.Users;
using CarService.Infrastructure.Expansion;
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

	public async Task<ListWithPage<Record>> GetAllAsync
	(ParamsWhitFilter
		parameters)
	{
		var query = await _context.Records
			.Include(x => x.Masters)
			.Include(x => x.Services)
			.AsNoTracking()
			.ToListAsync();

		if (parameters.RoleId == "3")
			query = query
				.Where(x => x.ClientId == parameters
					.UserId)
				.ToList();

		if (parameters.FilterName != null)
			query = query.FilterRecords(parameters.FilterName,
				parameters.FilterValue!);

		if (!string.IsNullOrEmpty(parameters.SearchValue))
			query = query
				.Where(x => x.Search(parameters.SearchValue))
				.ToList();

		if (parameters.SortProperty != null)
			query = query.Sort(parameters.SortProperty,
				parameters.SortDescending);

		return query.Page(parameters.Page, parameters.PageSize);
	}

	public async Task<Record?> GetByIdAsync(Guid id)
	{
		return await _context.Records
			.Include(x => x.Client)
			.ThenInclude(x => x.UserInfo)
			.FirstOrDefaultAsync(x =>
				x.Id == id);
	}

	public async Task<IEnumerable<Record>> GetByClientIdAsync(
		Guid clientId)
	{
		return await _context.Records
			.Where(x => x.ClientId == clientId).ToListAsync();
	}

	public async Task<ListWithPage<Record>>
		GetCompletedByMasterIdAsync(Guid masterId,
			Params parameters)
	{
		var query = await _context.Records
			.Where(x => x.Masters.Any(m => m.Id == masterId)
			            && x.Status == RecordStatus.Done)
			.ToListAsync();

		if (!string.IsNullOrEmpty(parameters.SearchValue))
			query = query
				.Where(x => x.Search(parameters.SearchValue))
				.ToList();

		if (parameters.SortProperty != null)
			query = query.Sort(parameters.SortProperty,
				parameters.SortDescending);

		return query.Page(parameters.Page, parameters.PageSize);
	}

	public async Task<ListWithPage<Record>>
		GetActiveByMasterIdAsync(Guid masterId,
			Params parameters)
	{
		var query = await _context.Records
			.Where(x => x.Masters.Any(m => m.Id == masterId)
			            && x.Status == RecordStatus.Work)
			.ToListAsync();

		if (!string.IsNullOrEmpty(parameters.SearchValue))
			query = query
				.Where(x => x.Search(parameters.SearchValue))
				.ToList();

		if (parameters.SortProperty != null)
			query = query.Sort(parameters.SortProperty,
				parameters.SortDescending);

		return query.Page(parameters.Page, parameters.PageSize);
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
}