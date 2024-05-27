using CarService.App.Common;
using CarService.App.Common.ListWithPage;
using CarService.App.Interfaces.Persistence;
using CarService.Core.Services;
using CarService.Infrastructure.Expansion;
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

	public async Task<ListWithPage<Service>> GetAllAsync(
		ParamsWhitFilter parameters)
	{
		var query = await _context.Services
			.Include(x => x.ServiceTypes)
			.ToListAsync();

		if (parameters.Filters != null)
			parameters.Filters.ForEach(x =>
			{
				query = query.FilterWithName(x.Name, x.Value);
			});

		if (!string.IsNullOrEmpty(parameters.SearchValue))
			query = query
				.Where(x => x.Search(parameters.SearchValue))
				.ToList();

		if (parameters.SortProperty != null)
			query = query.Sort(parameters.SortProperty,
				parameters.SortDescending);

		return query.Page(parameters.Page, parameters.PageSize);
	}
}


// TODO: Обнолвнеие сервиса
// TODO: Добавление мастера
// TODO: Добавление категории