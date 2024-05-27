using CarService.Core.Records;

namespace CarService.App.Interfaces.Persistence;

public interface IDayRecordsRepository
{
	Task<Guid> Create(DayRecords dayRecords);
	Task Update(DayRecords dayRecords);
	Task<DayRecords?> GetById(Guid id);
	Task<List<DayRecords>> GetAll();
}