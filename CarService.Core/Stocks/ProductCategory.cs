using CSharpFunctionalExtensions;

namespace CarService.Core.Stocks;

public class ProductCategory
{
    private ProductCategory(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public virtual List<Product> Products { get; set; } = [];

    public static Result<ProductCategory> Create(Guid id, string name)
    {
        if (id == Guid.Empty)
            return Result.Failure<ProductCategory>("Id can't be empty");

        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<ProductCategory>("Name can't be empty");

        var category = new ProductCategory(id, name);

        return Result.Success(category);
    }
}