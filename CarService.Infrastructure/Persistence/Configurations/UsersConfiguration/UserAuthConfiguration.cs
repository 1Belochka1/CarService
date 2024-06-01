using CarService.Core.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace
	CarService.Infrastructure.Persistence.Configurations.
	UsersConfiguration;

public class
	UserAuthConfiguration : IEntityTypeConfiguration<UserAuth>
{
	public void Configure(EntityTypeBuilder<UserAuth> builder)
	{
		builder.HasKey(x => x.Id);
		builder.Property(x => x.Id)
			.ValueGeneratedNever();

		builder.Property(x => x.Phone)
			.HasMaxLength(100)
			.IsRequired();

		builder.Property(x => x.PasswordHash)
			.HasMaxLength(200)
			.IsRequired();

		builder.Property(x => x.RoleId)
			.HasDefaultValue(1)
			.IsRequired();

		builder.HasIndex(x => x.Phone)
			.IsUnique();

		builder.Property(x => x.CreateDate);

		builder.HasOne(x => x.Role)
			.WithMany(x => x.Users)
			.HasForeignKey(x => x.RoleId)
			.OnDelete(DeleteBehavior.Restrict);
	}
}