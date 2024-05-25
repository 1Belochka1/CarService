using CarService.Core.Services;

namespace CarService.App.Interfaces.Persistence;

public interface IServiceTypesRepository
{
	Task<Guid> CreateAsync(
		ServiceType serviceType);

	Task<bool> Update(Guid id, string name);
	Task<bool> Delete(Guid id);
	Task<List<ServiceType>> GetAllAsync();
}