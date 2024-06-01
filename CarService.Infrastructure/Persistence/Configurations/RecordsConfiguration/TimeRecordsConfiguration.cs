using CarService.Core.Records;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace
	CarService.Infrastructure.Persistence.Configurations.
	RecordsConfiguration;

public class TimeRecordsConfiguration :
	IEntityTypeConfiguration<TimeRecord>
{
	public void Configure(
		EntityTypeBuilder<TimeRecord> builder)
	{
		builder.HasKey(x => x.Id);

		builder.Property(x => x.Id)
			.ValueGeneratedNever();

		builder.HasOne(x => x.DayRecord)
			.WithMany(x => x.TimeRecords)
			.HasForeignKey(x => x.DayRecordId)
			.OnDelete(DeleteBehavior.Cascade);

		builder.HasOne(x => x.Client)
			.WithMany(x => x.TimeRecords)
			.HasForeignKey(x => x.ClientId)
			.OnDelete(DeleteBehavior.SetNull);

		builder.HasOne(x => x.Record)
			.WithOne(x => x.TimeRecord)
			.HasForeignKey<TimeRecord>(x => x.RecordId)
			.OnDelete(DeleteBehavior.SetNull);
	}
}