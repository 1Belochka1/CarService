using CarService.App.Interfaces.Persistence;
using CarService.Core.Requests;
using Microsoft.EntityFrameworkCore;

namespace CarService.Infrastructure.Persistence.
	Repositories;

public class
	RecordsRepository : IRecordsRepository
{
	private readonly CarServiceDbContext _context;

	public RecordsRepository(
		CarServiceDbContext context)
	{
		_context = context;
	}

	public async Task<Guid> Create(Record record)
	{
		await _context.Records.AddAsync(record);
		await _context.SaveChangesAsync();

		return record.Id;
	}

	public async Task Update(Record record)
	{
		_context.Records.Update(record);
		await _context.SaveChangesAsync();
	}

	public async Task Delete(Guid id)
	{
		var records = await _context.Records
			.FirstOrDefaultAsync(x => x.Id == id);

		_context.Records.Remove(records);
		await _context.SaveChangesAsync();
	}

	public async Task<Record?> GetByServiceId(
		Guid serviceId)
	{
		return await _context.Records
			.Include(x => x.Service)
			.ThenInclude(x => x.Image)
			.FirstOrDefaultAsync(x => x.ServiceId == serviceId);
	}

	public async Task<Record?> GetById(Guid id)
	{
		return await _context.Records
			.Include(x => x.DaysRecords)
			.ThenInclude(x => x.TimeRecords)
			.Include(x => x.Service)
			.ThenInclude(x => x.Image)
			.FirstOrDefaultAsync(x => x.Id == id);
	}

	public async Task<List<Record>> GetAll()
	{
		return await _context.Records
			.Include(x => x.Service)
			.ToListAsync();
	}
}