using CarService.Core.Services;

namespace CarService.Infrastructure.Persistence.Repositories;

public interface IServicesRepository
{
	Task<Guid> CreateAsync(Service service);
	Task<Service?> GetByIdAsync(Guid id);
}