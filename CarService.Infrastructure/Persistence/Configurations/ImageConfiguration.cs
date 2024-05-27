using CarService.Core.Images;
using CarService.Core.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarService.Infrastructure.Persistence.
	Configurations;

public class
	ImageConfiguration : IEntityTypeConfiguration<Image>
{
	public void Configure(EntityTypeBuilder<Image> builder)
	{
		builder.HasKey(x => x.Id);

		builder.Property(x => x.Id)
			.ValueGeneratedNever();

		builder.Property(x => x.FileName)
			.HasMaxLength(1000);

		builder.HasOne(x => x.Product)
			.WithMany(x => x.Images)
			.HasForeignKey(x => x.ProductId);

		builder.HasOne(x => x.UserInfo)
			.WithOne(x => x.Image)
			.HasForeignKey<UserInfo>(x => x.ImageId);
	}
}