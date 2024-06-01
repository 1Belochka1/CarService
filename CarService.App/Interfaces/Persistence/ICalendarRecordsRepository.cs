using CarService.Core.Records;

namespace CarService.App.Interfaces.Persistence;

public interface ICalendarRecordsRepository
{
	Task<Guid> Create(CalendarRecord record);
	Task Update(CalendarRecord record);
	Task Delete(Guid id);

	Task<CalendarRecord?> GetByServiceId(
		Guid serviceId);

	Task<CalendarRecord?> GetById(Guid id);

	Task<List<CalendarRecord>> GetAll();
}