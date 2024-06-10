using CarService.Core.Requests;

namespace CarService.App.Interfaces.Persistence;

public interface ITimeRecordsRepository
{
	Task<Guid> Create(TimeRecord timeRecord);

	Task CreateRangeAsync(
		List<TimeRecord> daysRecords);

	Task Update(TimeRecord timeRecord);
	Task<TimeRecord?> GetById(Guid id);
	Task<List<TimeRecord>> GetAll();

	Task<List<TimeRecord>> GetTimeRecordsByRecordIdAsync(
		Guid id);

	Task Delete(Guid id);
}