using CarService.Core.Stocks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarService.Infrastructure.Persistence.Configurations.ProductsConfiguration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedNever();
        
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x=> x.Price)
            .HasColumnType("numeric(10, 2)")
            .IsRequired();
        
        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(x => x.InStock)
            .HasColumnType("serial");
        
        builder.HasIndex(x => x.Name)
            .IsUnique();
        
        builder.HasMany(  x => x.Images)
            .WithOne(x => x.Product);
        
        builder.HasMany(x => x.Category)
            .WithMany(x => x.Products)
            .UsingEntity("ProductsCategories");
    }
}