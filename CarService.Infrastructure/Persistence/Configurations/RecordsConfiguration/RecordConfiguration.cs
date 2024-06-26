using CarService.Core.Requests;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace
	CarService.Infrastructure.Persistence.Configurations.
	RecordsConfiguration;

public class
	RecordConfiguration : IEntityTypeConfiguration<Request>
{
	public void Configure(EntityTypeBuilder<Request> builder)
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
				v => (RequestPriority)Enum.Parse(
					typeof(RequestPriority),
					v!)
			);

		builder.Property(x => x.Status)
			.HasMaxLength(15)
			.HasConversion(
				v => Enum.GetName(v),
				v => (RequestStatus)Enum.Parse(
					typeof(RequestStatus),
					v!)
			);

		builder.HasOne(x => x.Client)
			.WithMany(x => x.Requests)
			.HasForeignKey(x => x.ClientId)
			.OnDelete(DeleteBehavior.SetNull);

		builder.HasMany(x => x.Masters)
			.WithMany(x => x.Works)
			.UsingEntity("RequestMasters");
	}
}