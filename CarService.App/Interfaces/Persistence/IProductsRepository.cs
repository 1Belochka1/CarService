using CarService.App.Common.ListWithPage;
using CarService.Core.Stocks;

namespace CarService.App.Interfaces.Persistence;

public interface IProductsRepository
{
	Task AddProduct(Product product);
	Task UpdateProduct(Product product);
	Task DeleteProduct(Product product);

	Task<ListWithPage<Product>> GetProducts(
		ParamsWhitFilter parameters
	);
}