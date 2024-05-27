using CarService.App.Common.ListWithPage;
using CarService.App.Common.Users;
using CarService.App.Interfaces.Persistence;
using CarService.Core.Users;
using CarService.Infrastructure.Expansion;
using Clave.Expressionify;
using Microsoft.EntityFrameworkCore;

namespace CarService.Infrastructure.Persistence.
	Repositories;

public class UserAuthRepository : IUserAuthRepository
{
	private readonly CarServiceDbContext _context;

	public UserAuthRepository(CarServiceDbContext context)
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
		return await _context.UserAuths
			.Include(x => x.UserInfo)
			.FirstOrDefaultAsync(x =>
				x.Id == id);
	}

	public async Task<UserAuth?> GetByEmailAsync(string email)
	{
		return await _context.UserAuths.FirstOrDefaultAsync(x =>
			x.Email == email);
	}

	public async Task<ListWithPage<WorkersDto>>
		GetWorkersAsync(Params parameters)
	{
		var query = await _context.UserAuths
			.Include(u => u.UserInfo)
			.Include(u => u.Role)
			.AsQueryable().ToListAsync();

		query = query.Where(x => x.RoleId == 2).ToList();

		var resultList = SelectWorkers(query);

		if (!string.IsNullOrEmpty(parameters.SearchValue))
			resultList = resultList
				.Where(x => x.Search(parameters.SearchValue))
				.ToList();

		if (parameters.SortProperty != null)
			resultList = resultList.Sort(parameters.SortProperty,
				parameters.SortDescending);

		return resultList.Page(parameters.Page,
			parameters.PageSize);
	}

	public async Task<ListWithPage<ClientsDto>>
		GetClientsAsync(
			Params parameters
		)
	{
		var query = await _context.UserAuths
			.Include(u => u.UserInfo)
			.Include(u => u.Records)
			.AsQueryable().ToListAsync();

		query = query.Where(x => x.RoleId == 3).ToList();

		var resultList = SelectClients(query);

		if (!string.IsNullOrEmpty(parameters.SearchValue))
			resultList = resultList
				.Where(x => x.Search(parameters.SearchValue))
				.ToList();

		if (parameters.SortProperty == "LastRecordTime")
			resultList = resultList
				.Where(x => x.LastRecordTime != null).ToList();

		else if (parameters.SortProperty != null)
			resultList = resultList.Sort(parameters.SortProperty,
				parameters.SortDescending);


		return resultList.Page(parameters.Page,
			parameters.PageSize);
	}

	public async Task<ICollection<UserAuth>> GetWorkersByIds(
		ICollection<string> ids)
	{
		return await _context.UserAuths
			.Include(u => u.Works)
			.Where(x => ids.Contains(x.Id.ToString()))
			.ToListAsync();
	}

	public async Task<UserAuth?>
		GetClientByIdWithRecordsAsync(Guid userId)
	{
		return await _context.UserAuths
			.Include(u => u.UserInfo)
			.Include(u => u.Records)
			.FirstOrDefaultAsync(x => x.Id == userId);
	}

	public async Task<UserAuth?> GetWorkerByIdWithWorksAsync(
		Guid userId)
	{
		return await _context.UserAuths
			.Include(u => u.UserInfo)
			.Include(u => u.Works)
			.FirstOrDefaultAsync(x => x.Id == userId);
	}

	private static List<ClientsDto> SelectClients(
		List<UserAuth> query)
	{
		return query.Select(x => new ClientsDto(
			x.Id,
			x.Email,
			x.UserInfo.LastName, x.UserInfo.FirstName,
			x.UserInfo.Patronymic,
			x.UserInfo.Address,
			x.UserInfo.Phone,
			x.Records.Count > 0
				? x.Records.MaxBy(u => u.CreateTime)?.CreateTime
				: null,
			x.RoleId
		)).ToList();
	}

	private static List<WorkersDto> SelectWorkers(
		List<UserAuth> query)
	{
		return query.Select(x => new WorkersDto(
			x.Id,
			x.Email,
			x.UserInfo.LastName,
			x.UserInfo.FirstName,
			x.UserInfo.Patronymic,
			x.UserInfo.Address,
			x.UserInfo.Phone,
			x.RoleId
		)).ToList();
	}
}