using CarService.Core.Images;
using CarService.Core.Requests;
using CarService.Core.Services;
using CarService.Core.Users;
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
	}
}