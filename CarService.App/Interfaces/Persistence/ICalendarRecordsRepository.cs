using CarService.Core.Requests;

namespace CarService.App.Interfaces.Persistence;

public interface IRecordsRepository
{
	Task<Guid> Create(Record record);
	Task Update(Record record);
	Task Delete(Guid id);

	Task<Record?> GetByServiceId(
		Guid serviceId);

	Task<Record?> GetById(Guid id);

	Task<List<Record>> GetAll();
}