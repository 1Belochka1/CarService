using CarService.Core.Images;
using CarService.Core.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace
	CarService.Infrastructure.Persistence.Configurations.
	UsersConfiguration;

public class
	UserInfoConfiguration : IEntityTypeConfiguration<UserInfo>
{
	public void Configure(EntityTypeBuilder<UserInfo> builder)
	{
		builder.HasKey(x => x.Id);
		builder.Property(x => x.Id)
			.ValueGeneratedNever();

		builder.Property(x => x.LastName)
			.HasMaxLength(100);

		builder.Property(x => x.FirstName)
			.HasMaxLength(100)
			.IsRequired();

		builder.Property(x => x.Patronymic)
			.HasMaxLength(100);

		builder.Property(x => x.Address)
			.HasMaxLength(300);

		builder.Property(x => x.Phone)
			.HasMaxLength(12)
			.IsRequired();

		builder.HasIndex(x => x.Phone)
			.IsUnique();

		builder.HasOne(x => x.UserAuth)
			.WithOne(x => x.UserInfo)
			.HasForeignKey<UserAuth>(x => x.Id)
			.OnDelete(DeleteBehavior.Cascade);

		builder.HasOne(x => x.Image)
			.WithOne(x => x.UserInfo)
			.HasForeignKey<Image>(x => x.UserInfoId);
	}
}