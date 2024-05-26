using CarService.Core.Records;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace
	CarService.Infrastructure.Persistence.Configurations.
	RecordsConfiguration;

public class
	RecordConfiguration : IEntityTypeConfiguration<Record>
{
	public void Configure(EntityTypeBuilder<Record> builder)
	{
		builder.HasKey(x => x.Id);
		builder.Property(x => x.Id)
			.ValueGeneratedNever();

		builder.Property(x => x.CarInfo)
			.HasMaxLength(200);

		builder.Property(x => x.Description)
			.HasMaxLength(1000);

		builder.Property(x => x.CreateTime)
			.IsRequired();

		builder.Property(x => x.CompleteTime)
			.IsRequired(false);

		builder.Property(x => x.VisitTime)
			.IsRequired(false);

		builder.Property(x => x.IsTransferred)
			.HasDefaultValue(false);

		builder.Property(x => x.Priority)
			.HasColumnType("record_priority");

		builder.Property(x => x.Status)
			.HasColumnType("record_status");

		builder.HasOne(x => x.Client)
			.WithMany(x => x.Records)
			.HasForeignKey(x => x.ClientId)
			.OnDelete(DeleteBehavior.Restrict);

		builder.HasMany(x => x.Masters)
			.WithMany(x => x.Works)
			.UsingEntity("RecordsMasters");
		
		builder.HasOne(x => x.DayRecords)
			.WithMany(x => x.Records)
			.HasForeignKey(x => x.DayRecordsId)
			.OnDelete(DeleteBehavior.SetNull);
	}
}