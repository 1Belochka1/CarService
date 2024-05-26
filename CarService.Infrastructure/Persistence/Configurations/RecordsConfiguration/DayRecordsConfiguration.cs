using CarService.Core.Records;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarService.Infrastructure.Persistence.Configurations.RecordsConfiguration;

public class DayRecordsConfiguration : IEntityTypeConfiguration<DayRecords>
{
	public void Configure(EntityTypeBuilder<DayRecords> builder)
	{
		builder.HasKey(x => x.Id);
		
		builder.Property(x=>x.Id)
			.ValueGeneratedNever();
		
		builder.Property(x=>x.StartTime)
			.IsRequired();
		
		builder.Property(x=>x.EndTime)
			.IsRequired();
		
		builder.Property(x=>x.Offset)
			.IsRequired();
		
		builder.Property(x=>x.Duration)
			.IsRequired();

		builder.HasOne(x => x.Calendar)
			.WithMany(x => x.Days)
			.HasForeignKey(x => x.CalendarId)
			.OnDelete(DeleteBehavior.Cascade);
	}
}