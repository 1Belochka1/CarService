using CarService.Core.Requests;

namespace CarService.App.Interfaces.Persistence;

public interface IDayRecordsRepository
{
	Task<Guid> Create(DayRecord dayRecord);

	Task CreateRangeAsync(
		List<DayRecord> daysRecords);

	Task Update(DayRecord dayRecord);
	Task<DayRecord?> GetById(Guid id);
	Task<List<DayRecord>> GetAll();

	Task<List<DayRecord>> GetByCalendarIdByMonthByYearId(
		Guid calendarId,
		int? month,
		int? year);

	Task<List<DayRecord>> GetByCalendarId(Guid calendarId);
}