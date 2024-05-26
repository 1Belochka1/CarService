using CarService.Core.Records;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarService.Infrastructure.Persistence.Configurations.RecordsConfiguration;

public class CalendarRecordsConfiguration : IEntityTypeConfiguration<CalendarRecords>
{
	public void Configure(EntityTypeBuilder<CalendarRecords> builder)
	{
		builder.HasKey(x => x.Id);

		builder.Property(x => x.Id)
			.ValueGeneratedNever();

		builder.Property(x => x.Name)
			.HasMaxLength(100);

		builder.Property(x => x.Description)
			.HasMaxLength(500);
	}
}