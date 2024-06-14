using CarService.App.Common.Users;
using CarService.App.Interfaces.Persistence;
using CarService.Core.Users;
using Microsoft.EntityFrameworkCore;

namespace CarService.Infrastructure.Persistence.
	Repositories;

public class UserInfoRepository : IUserInfoRepository
{
	private readonly CarServiceDbContext _context;

	public UserInfoRepository(CarServiceDbContext context)
	{
		_context = context;
	}

	public async Task<Guid> CreateAsync(UserInfo user)
	{
		await _context.UserInfos.AddAsync(user);

		await _context.SaveChangesAsync();

		return user.Id;
	}

	public async Task<List<(Guid id, string fullname)>>
		GetWorkersForAutocomplete()
	{
		return await this._context.UserInfos.Include(x => x
				.UserAuth)
			.Where(x => x.UserAuth.RoleId == 2)
			.AsNoTracking()
			.Select(x => new Tuple<Guid, string>(x.Id,
				x.LastName + " " + x.FirstName + " " +
				x.Patronymic).ToValueTuple()).ToListAsync();
	}

	public async Task<List<WorkersDto>> GetByPredicate
		(Func<UserAuth, bool> func)
	{
		return SelectWorkers(_context.UserAuths
			.Include(x => x.Works).Where(func)
			.ToList());
	}

	public async Task Delete(Guid id)
	{
		await _context.UserInfos.Where(x => x.Id == id)
			.ExecuteDeleteAsync();
	}

	public async Task<List<WorkersDto>>
		GetWorkersAsync()
	{
		var query = await _context.UserAuths
			.Include(u => u.Works)
			.Include(u => u.UserInfo)
			.Include(u => u.Role)
			.AsNoTracking()
			.ToListAsync();

		query = query.Where(x => x.RoleId == 2).ToList();

		return SelectWorkers(query);
	}

	public async Task<List<ClientsDto>>
		GetClientsAsync()
	{
		var query = await _context.UserInfos
			.Include(u => u.UserAuth)
			.Include(x => x.Requests)
			.AsNoTracking()
			.ToListAsync();

		query = query.Where(x => x.UserAuth == null ||
		                         x.UserAuth.RoleId
		                         == 3)
			.ToList();

		return SelectClients(query);
	}

	public async Task<UserInfo?> GetByPhone(string phone)
	{
		return await _context.UserInfos
			.Include(x => x.UserAuth)
			
			.FirstOrDefaultAsync(x =>
				x.Phone == phone);
	}

	public async Task<UserInfo?> GetByEmail(string email)
	{
		return await _context.UserInfos
			.Include(x => x.UserAuth)
			.FirstOrDefaultAsync(x =>
				x.Email == email);
	}


	public async Task<UserInfo?> GetByIdAsync(Guid id)
	{
		return await _context.UserInfos
			.Include(x => x.UserAuth)
			.AsNoTracking()
			.FirstOrDefaultAsync(x =>
				x.Id == id);
	}

	public async Task UpdateAsync(UserInfo user)
	{
		_context.UserInfos.Update(user);

		await _context.SaveChangesAsync();
	}

	private static List<ClientsDto> SelectClients(
		List<UserInfo> query)
	{
		return query.Select(x => new ClientsDto(
			x.Id,
			x.Email,
			x.LastName,
			x.FirstName,
			x.Patronymic,
			x.Address,
			x.Phone,
			x.Requests.Count > 0
				? x.Requests.MaxBy(u => u.CreateTime)
					?.CreateTime
				: null,
			x.UserAuth?.RoleId != null
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