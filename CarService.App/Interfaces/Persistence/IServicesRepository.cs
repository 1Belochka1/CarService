using CarService.App.Common.ListWithPage;
using CarService.Core.Services;

namespace CarService.App.Interfaces.Persistence;

public interface IServicesRepository
{
	Task<Guid> CreateAsync(Service service);

	Task<List<Service>?> GetByMasterIdAsync(Guid
		masterId);

	Task<List<Service>> GetLendingAsync();

	Task<Service?> GetByNameAsync(string name);

	Task<ListWithPage<Service>> GetAllAsync(
		ParamsWhitFilter parameters);
}