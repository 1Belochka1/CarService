using CarService.Core.Records;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarService.Infrastructure.Persistence.Configurations.RecordsConfiguration;

public class RecordConfiguration : IEntityTypeConfiguration<Record>
{
	public void Configure(EntityTypeBuilder<Record> builder)
	{
		builder.HasKey(x => x.Id);
		builder.Property(x => x.Id)
			.ValueGeneratedNever();

		builder.Property(x => x.Description)
			.HasMaxLength(1000);

		builder.Property(x => x.CreateTime)
			.IsRequired();
		
		builder.Property(x => x.CompleteTime)
			.IsRequired(false);

		builder.Property(x => x.Priority)
			.HasConversion(x => x.ToString(),
				x => Enum.Parse<RecordPriority>(x));

		builder.Property(x => x.Status)
			.HasConversion(x => x.ToString(),
				x => Enum.Parse<RecordStatus>(x));

		builder.HasOne(x => x.Client)
			.WithMany(x => x.Records)
			.HasForeignKey(x => x.ClientId)
			.OnDelete(DeleteBehavior.Restrict);

		builder.HasMany(x => x.Masters)
			.WithMany(x => x.Works)
			.UsingEntity("RecordsMasters");
	}
}