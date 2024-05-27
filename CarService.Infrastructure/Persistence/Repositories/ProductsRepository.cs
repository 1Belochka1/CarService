using CarService.App.Common.ListWithPage;
using CarService.App.Interfaces.Persistence;
using CarService.Core.Stocks;
using CarService.Infrastructure.Expansion;
using Microsoft.EntityFrameworkCore;

namespace CarService.Infrastructure.Persistence.
	Repositories;

public class ProductsRepository : IProductsRepository
{
	private readonly CarServiceDbContext _dbContext;

	public ProductsRepository(CarServiceDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task AddProduct(Product product)
	{
		await _dbContext.Products.AddAsync(product);
		await _dbContext.SaveChangesAsync();
	}

	public async Task UpdateProduct(Product product)
	{
		_dbContext.Update(product);
		await _dbContext.SaveChangesAsync();
	}

	public async Task DeleteProduct(Product product)
	{
		_dbContext.Remove(product);
		await _dbContext.SaveChangesAsync();
	}

	public async Task<ListWithPage<Product>> GetProducts(
		ParamsWhitFilter parameters
	)
	{
		var query = await _dbContext.Products
			.Include(x => x.Images)
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