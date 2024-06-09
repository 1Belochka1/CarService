using CarService.Core.Requests;
using CarService.Core.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace
	CarService.Infrastructure.Persistence.Configurations.
	ServicesConfiguration;

public class
	ServiceConfiguration : IEntityTypeConfiguration<Service>
{
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

		builder.HasOne(x =>
				x.Record)
			.WithOne(x => x.Service)
			.HasForeignKey<Record>(x => x.ServiceId);

		builder.HasOne(x => x.Image)
			.WithOne(x => x.Service)
			.HasForeignKey<Service>(x => x.ImageId)
			.OnDelete(DeleteBehavior.SetNull);
	}
}