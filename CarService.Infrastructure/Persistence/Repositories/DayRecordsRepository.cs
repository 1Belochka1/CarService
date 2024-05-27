using CarService.App.Interfaces.Persistence;
using CarService.Core.Records;
using Microsoft.EntityFrameworkCore;

namespace CarService.Infrastructure.Persistence.
	Repositories;

public class DayRecordsRepository : IDayRecordsRepository
{
	private readonly CarServiceDbContext _context;

	public DayRecordsRepository(CarServiceDbContext context)
	{
		_context = context;
	}

	public async Task<Guid> Create(DayRecords dayRecords)
	{
		await _context.DaysRecords.AddAsync(dayRecords);
		return dayRecords.Id;
	}

	public async Task Update(DayRecords dayRecords)
	{
		_context.DaysRecords.Update(dayRecords);
		await _context.SaveChangesAsync();
	}

	public async Task<DayRecords?> GetById(Guid id)
	{
		return await _context.DaysRecords.FirstOrDefaultAsync(
			x => x.Id == id);
	}

	public async Task<List<DayRecords>> GetAll()
	{
		return await _context.DaysRecords.ToListAsync();
	}
}