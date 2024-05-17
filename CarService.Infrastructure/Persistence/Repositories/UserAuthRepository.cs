using CarService.App.Common.Users;
using CarService.App.Interfaces.Persistence;
using CarService.Core.Users;
using Microsoft.EntityFrameworkCore;

namespace CarService.Infrastructure.Persistence.Repositories;

public class UserAuthRepository : BaseRepository<UserAuth>, IUserAuthRepository
{
	private readonly CarServiceDbContext _context;

	public UserAuthRepository(CarServiceDbContext context) : base(context)
	{
		_context = context;
	}

	public async Task<Guid> CreateAsync(UserAuth user)
	{
		await _context.UserAuths.AddAsync(user);

		await _context.SaveChangesAsync();

		return user.Id;
	}

	public async Task<UserAuth?> GetByIdAsync(Guid id)
	{
		return await _context.UserAuths.FirstOrDefaultAsync(x => x.Id == id);
	}

	public async Task<UserAuth?> GetByEmailAsync(string email)
	{
		return await _context.UserAuths.FirstOrDefaultAsync(x => x.Email == email);
	}

	public async Task<(int TotalItems, int? TotalPages, int? CurrentPage, IEnumerable<WorkersDto> Users)>
		GetWorkersAsync(
			bool sortDescending,
			string? searchValue = null,
			int page = 1,
			int pageSize = 10,
			string? sortProperty = null
		)
	{
		var query = _context.UserAuths
			.Include(u => u.UserInfo)
			.Include(u => u.Role)
			.AsQueryable();

		query = query.Where(x => x.RoleId == 2);

		if (!string.IsNullOrEmpty(searchValue))
			query = query.Where(x =>
				x.Email.Contains(searchValue)
				|| x.UserInfo.LastName.Contains(searchValue)
				|| x.UserInfo.FirstName.Contains(searchValue)
				|| x.UserInfo.Phone.Contains(searchValue)
				|| (x.UserInfo.Address != null && x.UserInfo.Address.Contains(searchValue))
				|| (x.UserInfo.Patronymic != null &&
				    x.UserInfo.Patronymic.Contains(searchValue))
			);

		var totalItems = await query.CountAsync();

		if (sortProperty != null)
			query = SortWorkers(query, sortProperty, sortDescending);

		var resultQuery = SelectWorkers(query);

		var currentPage = page;
		var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
		if (currentPage <= totalPages)
			return (totalItems, totalPages, currentPage,
				Page(resultQuery, page, pageSize));

		currentPage = 1;

		return (totalItems, totalPages, currentPage, resultQuery);
	}

	public async Task<(int TotalItems, int? TotalPages, int? CurrentPage, IEnumerable<ClientsDto> Users)>
		GetClientsAsync(bool sortDescending, string? searchValue = null, int page = 1,
			int pageSize = 10, string? sortProperty = null)
	{
		var query = _context.UserAuths
			.Include(u => u.UserInfo)
			.Include(u => u.Role)
			.Include(u => u.Records)
			.AsQueryable();

		query = query.Where(x => x.RoleId == 3);

		if (!string.IsNullOrEmpty(searchValue))
			query = query.Where(x =>
				x.Email.Contains(searchValue)
				|| x.UserInfo.LastName.Contains(searchValue)
				|| x.UserInfo.FirstName.Contains(searchValue)
				|| x.UserInfo.Phone.Contains(searchValue)
				|| (x.UserInfo.Address != null && x.UserInfo.Address.Contains(searchValue))
				|| (x.UserInfo.Patronymic != null &&
				    x.UserInfo.Patronymic.Contains(searchValue))
			);

		var totalItems = await query.CountAsync();

		var resultQuery = SelectClients(query).AsEnumerable();

		if (sortProperty != null)
			resultQuery = SortClients(resultQuery, sortProperty, sortDescending);

		var currentPage = page;

		var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

		if (currentPage <= totalPages)
			return (totalItems, totalPages, currentPage,
				Page(resultQuery.AsQueryable(), page, pageSize));

		currentPage = 1;

		return (totalItems, totalPages, currentPage, resultQuery);
	}

	public async Task<ICollection<UserAuth>> GetWorkersByIds(ICollection<string> ids)
	{
		return await _context.UserAuths
			.Include(u => u.Works)
			.Where(x => ids.Contains(x.Id.ToString())).ToListAsync();
	}

	public async Task<UserAuth?> GetClientByIdWithRecordsAsync(Guid userId)
	{
		return await _context.UserAuths
			.Include(u => u.UserInfo)
			.Include(u => u.Records)
			.FirstOrDefaultAsync(x => x.Id == userId);
	}

	public async Task<UserAuth?> GetWorkerByIdWithWorksAsync(Guid userId)
	{
		return await _context.UserAuths
			.Include(u => u.UserInfo)
			.Include(u => u.Works)
			.FirstOrDefaultAsync(x => x.Id == userId);
	}

	private static IEnumerable<ClientsDto> SelectClients(IEnumerable<UserAuth> query)
	{
		return query.Select(x => new ClientsDto(
			x.Id,
			x.Email,
			x.UserInfo.LastName + " " + x.UserInfo.FirstName + " " + x.UserInfo.Patronymic,
			x.UserInfo.Address,
			x.UserInfo.Phone,
			x.Records.Count > 0 ? x.Records.MaxBy(u => u.Time).Time : null
		));
	}

	private static IQueryable<WorkersDto> SelectWorkers(IQueryable<UserAuth> query)
	{
		return query.Select(x => new WorkersDto(
			x.Id,
			x.Email,
			x.UserInfo.LastName + " " + x.UserInfo.FirstName + " " + x.UserInfo.Patronymic,
			x.UserInfo.Address,
			x.UserInfo.Phone,
			x.RoleId
		));
	}

	private static IQueryable<UserAuth> SortWorkers(IQueryable<UserAuth> query, string sortProperty,
		bool sortDescending)
	{
		return sortProperty.ToLower() switch
		{
			"fullname" => sortDescending
				? query
					.OrderByDescending(x => x.UserInfo.LastName)
					.ThenByDescending(x => x.UserInfo.FirstName)
					.ThenByDescending(x => x.UserInfo.Patronymic)
				: query
					.OrderBy(x => x.UserInfo.LastName)
					.ThenBy(x => x.UserInfo.FirstName)
					.ThenBy(x => x.UserInfo.Patronymic),
			"phone" => sortDescending
				? query.OrderByDescending(x => x.UserInfo.Phone)
				: query.OrderBy(x => x.UserInfo.Phone),
			"address" => sortDescending
				? query.OrderByDescending(x => x.UserInfo.Address)
				: query.OrderBy(x => x.UserInfo.Address),
			"email" => sortDescending
				? query.OrderByDescending(x => x.Email)
				: query.OrderBy(x => x.Email),
			_ => query
		};
	}

	private static IEnumerable<ClientsDto> SortClients(IEnumerable<ClientsDto> query, string sortProperty,
		bool sortDescending)
	{
		return sortProperty.ToLower() switch
		{
			"fullname" => sortDescending
				? query
					.OrderByDescending(x => x.FullName)
				: query
					.OrderBy(x => x.FullName),
			"phone" => sortDescending
				? query.OrderByDescending(x => x.Phone)
				: query.OrderBy(x => x.Phone),
			"address" => sortDescending
				? query.OrderByDescending(x => x.Address)
				: query.OrderBy(x => x.Address),
			"email" => sortDescending
				? query.OrderByDescending(x => x.Email)
				: query.OrderBy(x => x.Email),
			"lastrecord" => sortDescending
				? query.OrderByDescending(x => x.LastRecord)
				: query.OrderBy(x => x.LastRecord),
			_ => query
		};
	}
}