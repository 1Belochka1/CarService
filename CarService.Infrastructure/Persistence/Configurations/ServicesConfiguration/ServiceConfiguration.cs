using CarService.Core.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarService.Infrastructure.Persistence.Configurations.ServicesConfiguration;

public class ServiceConfiguration : IEntityTypeConfiguration<Service>
{
	
	// TODO: Возможно убрать связи с мастором и заявками

	public void Configure(EntityTypeBuilder<Service> builder)
	{
		builder.HasKey(x => x.Id);
		builder.Property(x => x.Id)
			.ValueGeneratedNever();

		builder.Property(x => x.Name)
			.IsRequired()
			.HasMaxLength(150);

		builder.Property(x => x.Description)
			.IsRequired()
			.HasMaxLength(1000);

		builder.HasIndex(x => x.Name)
			.IsUnique();

		builder.HasMany(x => x.ServiceTypes)
			.WithMany(x => x.Services)
			.UsingEntity("ServiceTypesServices");

		builder.HasMany(x => x.Masters)
			.WithMany(x => x.Services)
			.UsingEntity("MastersServices");

		builder.HasMany(x => x.Records)
			.WithMany(x => x.Services)
			.UsingEntity("RecordsServices");
	}
}