namespace CarService.Core.Stocks;

public class Stock
{
    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public string Address { get; private set; }
    
    public List<Product> Products { get; private set; } = [];
}