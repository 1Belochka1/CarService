using CarService.App.Interfaces.Persistence;
using CarService.Core.Records;
using Microsoft.EntityFrameworkCore;

namespace CarService.Infrastructure.Persistence.
	Repositories;

public class
	CalendarRecordsRepository : ICalendarRecordsRepository
{
	private readonly CarServiceDbContext _context;

	public CalendarRecordsRepository(
		CarServiceDbContext context)
	{
		_context = context;
	}

	public async Task<Guid> Create(CalendarRecord record)
	{
		await _context.CalendarRecords.AddAsync(record);
		await _context.SaveChangesAsync();

		return record.Id;
	}

	public async Task Update(CalendarRecord record)
	{
		_context.CalendarRecords.Update(record);
		await _context.SaveChangesAsync();
	}

	public async Task Delete(Guid id)
	{
		var records = await _context.CalendarRecords
			.FirstOrDefaultAsync(x => x.Id == id);

		_context.CalendarRecords.Remove(records);
		await _context.SaveChangesAsync();
	}

	public async Task<CalendarRecord?> GetByServiceId(
		Guid serviceId)
	{
		return await _context.CalendarRecords
			.FirstOrDefaultAsync(x => x.ServiceId == serviceId);
	}

	public async Task<CalendarRecord?> GetById(Guid id)
	{
		return await _context.CalendarRecords
			.Include(x => x.DaysRecords)
			.ThenInclude(x => x.TimeRecords)
			.FirstOrDefaultAsync(x => x.Id == id);
	}

	public async Task<List<CalendarRecord>> GetAll()
	{
		return await _context.CalendarRecords
			.ToListAsync();
	}
}