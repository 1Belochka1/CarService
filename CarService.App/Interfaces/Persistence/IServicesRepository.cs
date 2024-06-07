using CarService.Core.Services;

namespace CarService.App.Interfaces.Persistence;

public interface IServicesRepository
{
	Task<Guid> CreateAsync(Service service);

	Task<List<Service>?> GetByMasterIdAsync(Guid
		masterId);

	Task<List<Service>> GetLendingAsync();

	Task<Service?> GetByNameAsync(string name);
	Task<Service?> GetByIdAsync(Guid id);

	Task<List<Service>> GetAllAsync();

	Task UpdateAsync(Service service);

	Task DeleteAsync(Guid id);

	Task<List<(Guid id, string fullname)>>
		GetServicesForAutocomplete();
}