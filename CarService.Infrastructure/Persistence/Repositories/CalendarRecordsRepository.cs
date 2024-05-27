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

	public async Task<Guid> Create(CalendarRecords records)
	{
		await _context.CalendarRecords.AddAsync(records);
		await _context.SaveChangesAsync();

		return records.Id;
	}

	public async Task Update(CalendarRecords records)
	{
		_context.CalendarRecords.Update(records);
		await _context.SaveChangesAsync();
	}

	public async Task Delete(Guid id)
	{
		var records = await _context.CalendarRecords
			.FirstOrDefaultAsync(x => x.Id == id);

		_context.CalendarRecords.Remove(records);
		await _context.SaveChangesAsync();
	}

	public async Task<CalendarRecords?> GetByServiceId(
		Guid serviceId)
	{
		return await _context.CalendarRecords
			.FirstOrDefaultAsync(x => x.ServiceId == serviceId);
	}
}