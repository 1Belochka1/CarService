using CarService.App.Interfaces.Persistence;
using CarService.Core.Services;
using Microsoft.EntityFrameworkCore;

namespace CarService.Infrastructure.Persistence.
	Repositories;

public class ServicesRepository : IServicesRepository
{
	private readonly CarServiceDbContext _context;

	public ServicesRepository(CarServiceDbContext context)
	{
		_context = context;
	}

	public async Task<Guid> CreateAsync(Service service)
	{
		await _context.Services.AddAsync(service);

		await _context.SaveChangesAsync();

		return service.Id;
	}

	public async Task<List<Service>> GetLendingAsync()
	{
		return await _context.Services
			.Include(x => x.ServiceTypes)
			.Include(x => x.Image)
			.Where(x => x.IsShowLending)
			.ToListAsync();
	}

	public async Task<List<Service>?> GetByMasterIdAsync(Guid
		masterId)
	{
		return await _context.Services
			.Include(x => x.Masters)
			.Where(x => x.Masters.Any(m => m.Id == masterId))
			.ToListAsync();
	}

	public async Task<Service?> GetByNameAsync(string name)
	{
		return await _context.Services.FirstOrDefaultAsync(x =>
			x.Name == name);
	}

	public async Task<Service?> GetByIdAsync(Guid id)
	{
		return await _context.Services.FirstOrDefaultAsync(x =>
			x.Id == id);
	}

	public async Task<List<Service>> GetAllAsync()
	{
		var query = await _context.Services
			.Include(x => x.ServiceTypes)
			.Include(x => x.Image)
			.ToListAsync();

		return query;
	}

	public async Task UpdateAsync(Service service)
	{
		_context.Services.Update(service);

		await _context.SaveChangesAsync();
	}
}


// TODO: Обнолвнеие сервиса
// TODO: Добавление мастера
// TODO: Добавление категории