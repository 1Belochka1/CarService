using CarService.Core.Requests;
using CarService.Core.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace
	CarService.Infrastructure.Persistence.Configurations.
	RecordsConfiguration;

public class
	RecordsConfiguration : IEntityTypeConfiguration<
	Record>
{
	public void Configure(
		EntityTypeBuilder<Record> builder)
	{
		builder.HasKey(x => x.Id);

		builder.Property(x => x.Id)
			.ValueGeneratedNever();

		builder.Property(x => x.Name)
			.HasMaxLength(100);

		builder.Property(x => x.Description)
			.HasMaxLength(500);

		builder.HasOne(x =>
				x.Service)
			.WithOne(x => x.Record)
			.HasForeignKey<Service>(x => x.CalendarId);
	}
}