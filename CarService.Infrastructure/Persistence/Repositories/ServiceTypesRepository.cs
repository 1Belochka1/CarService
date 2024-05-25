using CarService.App.Interfaces.Persistence;
using CarService.Core.Services;
using Microsoft.EntityFrameworkCore;

namespace CarService.Infrastructure.Persistence.
	Repositories;

public class ServiceTypesRepository : IServiceTypesRepository
{
	private readonly CarServiceDbContext _dbContext;

	public ServiceTypesRepository(
		CarServiceDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task<Guid> CreateAsync(
		ServiceType serviceType)
	{
		await _dbContext.ServiceTypes.AddAsync(serviceType);

		await _dbContext.SaveChangesAsync();

		return serviceType.Id;
	}

	public async Task<bool> Update(Guid id, string name)
	{
		var serviceType = await _dbContext.ServiceTypes
			.FirstOrDefaultAsync(x => x.Id == id);

		if (serviceType == null)
			return false;

		serviceType.SetName(name);

		await _dbContext.SaveChangesAsync();

		return true;
	}

	public async Task<bool> Delete(Guid id)
	{
		var serviceType = await _dbContext.ServiceTypes
			.FirstOrDefaultAsync(x => x.Id == id);

		if (serviceType == null)
			return false;

		_dbContext.ServiceTypes.Remove(serviceType);

		await _dbContext.SaveChangesAsync();

		return true;
	}

	public async Task<List<ServiceType>> GetAllAsync()
	{
		return await _dbContext.ServiceTypes.ToListAsync();
	}
}