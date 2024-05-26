using CarService.App.Common.ListWithPage;
using CarService.Core.Stocks;

namespace CarService.Infrastructure.Persistence.Repositories;

public class ProductsRepository
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
		throw new NotImplementedException();
	}
	
	
	
}