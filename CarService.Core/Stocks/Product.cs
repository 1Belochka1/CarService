using CSharpFunctionalExtensions;

namespace CarService.Core.Stocks;

public class Product
{
    private Product(Guid id, string name, int inStock, string? description)
    {
        Id = id;
        Name = name;
        InStock = inStock;
        Description = description;
    }

    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public string? Description { get; private set; }

    public int InStock { get; private set; } = 0;
    
    public virtual List<ProductCategory> Category { get; private set; } = [];

    public static Result<Product> Create(Guid id, string name, int inStock, string? description)
    {
        if (id == Guid.Empty)
            return Result.Failure<Product>("Id can't be empty");

        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<Product>("Name can't be empty");

        if (inStock < 0)
            return Result.Failure<Product>("InStock can't be negative");

        var product = new Product(id, name, inStock, description);

        return Result.Success(product);
    }
}