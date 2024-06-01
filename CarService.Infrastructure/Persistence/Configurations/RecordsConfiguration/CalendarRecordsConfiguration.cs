using CarService.Core.Records;
using CarService.Core.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace
	CarService.Infrastructure.Persistence.Configurations.
	RecordsConfiguration;

public class
	CalendarRecordsConfiguration : IEntityTypeConfiguration<
	CalendarRecord>
{
	public void Configure(
		EntityTypeBuilder<CalendarRecord> builder)
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
			.WithOne(x => x.Calendar)
			.HasForeignKey<Service>(x => x.CalendarId);
	}
}