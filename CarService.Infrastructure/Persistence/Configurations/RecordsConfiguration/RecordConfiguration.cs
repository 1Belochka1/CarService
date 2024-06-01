using CarService.Core.Records;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

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
			.HasMaxLength(15)
			.HasConversion(
				v => Enum.GetName(v),
				v => (RecordPriority)Enum.Parse(
					typeof(RecordPriority),
					v!)
			);

		builder.Property(x => x.Status)
			.HasMaxLength(15)
			.HasConversion(
				v => Enum.GetName(v),
				v => (RecordStatus)Enum.Parse(typeof(RecordStatus),
					v!)
			);

		builder.HasOne(x => x.Client)
			.WithMany(x => x.Records)
			.HasForeignKey(x => x.ClientId)
			.OnDelete(DeleteBehavior.Restrict);

		builder.HasMany(x => x.Masters)
			.WithMany(x => x.Works)
			.UsingEntity("RecordsMasters");
	}
}