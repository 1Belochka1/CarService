using CarService.Core.Requests;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace
	CarService.Infrastructure.Persistence.Configurations.
	RecordsConfiguration;

public class
	DayRecordsConfiguration : IEntityTypeConfiguration<
	DayRecord>
{
	public void Configure(
		EntityTypeBuilder<DayRecord> builder)
	{
		builder.HasKey(x => x.Id);

		builder.Property(x => x.Id)
			.ValueGeneratedNever();

		builder.Property(x => x.StartTime)
			.IsRequired();

		builder.Property(x => x.EndTime)
			.IsRequired();

		builder.Property(x => x.Offset)
			.IsRequired();

		builder.Property(x => x.IsWeekend);

		builder.HasOne(x => x.Calendar)
			.WithMany(x => x.DaysRecords)
			.HasForeignKey(x => x.CalendarId)
			.OnDelete(DeleteBehavior.Cascade);
	}
}