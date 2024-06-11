using CarService.App.Interfaces.Persistence;
using CarService.Core.Requests;
using Microsoft.EntityFrameworkCore;

namespace CarService.Infrastructure.Persistence.
	Repositories;

public class TimeRecordsRepository : ITimeRecordsRepository
{
	private readonly CarServiceDbContext _context;

	public TimeRecordsRepository(CarServiceDbContext context)
	{
		_context = context;
	}

	public async Task<Guid> Create(TimeRecord timeRecord)
	{
		await _context.TimesRecords.AddAsync(timeRecord);
		return timeRecord.Id;
	}

	public async Task CreateRangeAsync(
		List<TimeRecord> daysRecords)
	{
		await _context.TimesRecords.AddRangeAsync(daysRecords);
		await _context.SaveChangesAsync();
	}

	public async Task Update(TimeRecord timeRecord)
	{
		_context.TimesRecords.Update(timeRecord);
		await _context.SaveChangesAsync();
	}

	public async Task<TimeRecord?> GetById(Guid id)
	{
		return await _context.TimesRecords.Include(x => x.Client).FirstOrDefaultAsync(
			x => x.Id == id);
	}

	public async Task<List<TimeRecord>> GetAll()
	{
		return await _context.TimesRecords.ToListAsync();
	}

	public async Task<List<TimeRecord>>
		GetTimeRecordsByRecordIdAsync(Guid id)
	{
		return await _context.TimesRecords
			.OrderBy(x => x.StartTime)
			.Where(t => t.DayRecordId == id).ToListAsync();
	}

	public async Task Delete(Guid id)
	{
		await _context.TimesRecords.Where(x => x.Id == id)
			.ExecuteDeleteAsync();
	}
}