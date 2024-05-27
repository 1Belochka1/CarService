using CarService.Core.Records;

namespace CarService.App.Interfaces.Persistence;

public interface ICalendarRecordsRepository
{
	Task<Guid> Create(CalendarRecords records);
	Task Update(CalendarRecords records);
	Task Delete(Guid id);

	Task<CalendarRecords?> GetByServiceId(
		Guid serviceId);
}