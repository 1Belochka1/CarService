using CarService.App.Interfaces.Persistence;
using CarService.Core.Requests;
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

	public async Task<Guid> Create(DayRecord dayRecord)
	{
		await _context.DaysRecords.AddAsync(dayRecord);
		return dayRecord.Id;
	}

	public async Task CreateRangeAsync(
		List<DayRecord> daysRecords)
	{
		await _context.DaysRecords.AddRangeAsync(daysRecords);
		await _context.SaveChangesAsync();
	}

	public async Task Update(DayRecord dayRecord)
	{
		_context.DaysRecords.Update(dayRecord);
		await _context.SaveChangesAsync();
	}

	public async Task<DayRecord?> GetById(Guid id)
	{
		return await _context.DaysRecords.FirstOrDefaultAsync(
			x => x.Id == id);
	}

	public async Task<List<DayRecord>> GetAll()
	{
		return await _context.DaysRecords.ToListAsync();
	}

	public async Task<List<DayRecord>>
		GetByCalendarIdByMonthByYearId(
			Guid id, int? month, int? year)
	{
		var days = await _context.DaysRecords
			.Include(x =>
				x.TimeRecords.OrderBy(x => x.StartTime))
			.Where(x => x.CalendarId == id)
			.Where(x =>
				month == null || year == null ||
				x.Date.Year == year && x.Date.Month == month)
			.OrderBy(x => x.Date)
			.ToListAsync();

		return days;
	}

	public async Task<List<DayRecord>> GetByCalendarId(Guid
		calendarId)
	{
		var now = DateTime.UtcNow;
		var days = await _context.DaysRecords
			.Include(x =>
				x.TimeRecords.OrderBy(x => x.StartTime))
			.Where(x => x.CalendarId == calendarId)
			.Where(x => x.IsWeekend == false)
			.Where(x => x.Date > now)
			.OrderBy(x => x.Date)
			.ToListAsync();

		return days;
	}

	public async Task Delete(Guid id)
	{
		await _context.DaysRecords
			.Where(x => x.Id == id)
			.ExecuteDeleteAsync();
	}
}