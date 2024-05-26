using CarService.Core.Chats;
using CarService.Core.Images;
using CarService.Core.Records;
using CarService.Core.Services;
using CarService.Core.Stocks;
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

	public DbSet<Chat> Chats { get; set; } = null!;

	public DbSet<Message> Messages { get; set; } = null!;

	public DbSet<CalendarRecords> CalendarRecords { get; set; } = null!;
	
	public DbSet<DayRecords> DaysRecords { get; set; } = null!;
	
	public DbSet<Image> Images { get; set; } = null!;
	
	public DbSet<Product> Products { get; set; } = null!;
	
	public DbSet<ProductCategory> ProductCategories { get; set; } = null!;
	
	
	
	protected override void OnModelCreating(
		ModelBuilder modelBuilder)
	{
		modelBuilder.HasPostgresEnum<RecordPriority>();
		modelBuilder.HasPostgresEnum<RecordStatus>();

		modelBuilder.ApplyConfigurationsFromAssembly(
			typeof(CarServiceDbContext).Assembly);
	}
}