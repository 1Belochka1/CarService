using CarService.Core.Images;
using CarService.Core.Requests;
using CarService.Core.Services;
using CarService.Core.Users;
using CarService.Infrastructure.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CarService.Infrastructure.Persistence;

public class CarServiceDbContext : DbContext
{
	public CarServiceDbContext(
		DbContextOptions<CarServiceDbContext> options) : base(
		options)
	{
	}

	public DbSet<UserInfo> UserInfos { get; set; } = null!;

	public DbSet<UserAuth> UserAuths { get; set; } = null!;

	public DbSet<Role> Roles { get; set; } = null!;

	public DbSet<Service> Services { get; set; } = null!;
	
	public DbSet<Request> Request { get; set; } = null!;

	public DbSet<Record> Records { get; set; } = null!;

	public DbSet<DayRecord> DaysRecords { get; set; } =
		null!;

	public DbSet<TimeRecord> TimesRecords { get; set; } =
		null!;

	public DbSet<Image> Images { get; set; } = null!;

	protected override void OnModelCreating(
		ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(
			typeof(CarServiceDbContext).Assembly);
		
		var userinfo = UserInfo.Create(
			new Guid("2269F768-D05B-4A6B-AA8C-E4C67D3E53F2"),
			"admin@localhost",
			"Админ",
			"Админ",
			"Админ",
			"Админ",
			"00000000000"
		);

		
		modelBuilder.Entity<UserInfo>().HasData(
			[userinfo.Value]
		);
		
		var hasher = new PasswordHasher();
		var passwordHash = hasher.Generate("admin");

		UserAuth.Create(Guid.NewGuid(), new Guid("2269F768-D05B-4A6B-AA8C-E4C67D3E53F2"), "admin@localhost",
			passwordHash, DateTime.UtcNow, 1);
		
		modelBuilder.Entity<UserAuth>().HasData([
			UserAuth.Create(Guid.NewGuid(), new Guid("2269F768-D05B-4A6B-AA8C-E4C67D3E53F2"),
				"admin@localhost", passwordHash, DateTime.UtcNow, 1).Value
		]);


	}
}