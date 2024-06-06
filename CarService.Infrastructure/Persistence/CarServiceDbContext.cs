using CarService.Core.Images;
using CarService.Core.Records;
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

	public DbSet<ServiceType> ServiceTypes { get; set; } =
		null!;

	public DbSet<Record> Records { get; set; } = null!;

	public DbSet<CalendarRecord> CalendarRecords
	{
		get;
		set;
	} = null!;

	public DbSet<DayRecord> DaysRecords { get; set; } =
		null!;

	public DbSet<TimeRecord> TimesRecords { get; set; } =
		null!;

	public DbSet<Image> Images { get; set; } = null!;


	protected override void OnModelCreating(
		ModelBuilder modelBuilder)
	{
		modelBuilder.HasPostgresEnum<RecordPriority>();
		modelBuilder.HasPostgresEnum<RecordStatus>();

		modelBuilder.ApplyConfigurationsFromAssembly(
			typeof(CarServiceDbContext).Assembly);
	}
}